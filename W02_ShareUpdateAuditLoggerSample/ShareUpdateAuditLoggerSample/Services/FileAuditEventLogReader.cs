using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ShareUpdateAuditLoggerSample.Services
{
    /// <summary>
    /// Security ログ参照処理
    /// </summary>
    public class FileAuditEventLogReader
    {
        /// <summary>
        /// オブジェクト アクセス イベント ID
        /// </summary>
        private const int SecurityObjectAccessEventId = 4663;

        /// <summary>
        /// オブジェクト 削除イベント ID
        /// </summary>
        private const int SecurityObjectDeletedEventId = 4660;

        /// <summary>
        /// DELETE アクセス マスク
        /// </summary>
        private const long DeleteAccessMask = 0x00010000;

        /// <summary>
        /// 書き込み系 アクセス マスク
        /// </summary>
        private const long WriteLikeAccessMask = 0x00000002 | 0x00000004 | 0x00000010 | 0x00000100;

        /// <summary>
        /// Security ログ参照可否確認処理
        /// </summary>
        /// <param name="message">結果メッセージ</param>
        /// <returns>参照可否</returns>
        public bool TryCheckSecurityLogAccess(out string message)
        {
            message = string.Empty;

            try
            {
                EventLogQuery query = new EventLogQuery("Security", PathType.LogName, "*[System[EventID=4663]]")
                {
                    ReverseDirection = true
                };

                using (EventLogReader reader = new EventLogReader(query))
                using (EventRecord record = reader.ReadEvent())
                {
                    message = record == null
                        ? "Security ログ参照成功。4663 は未取得"
                        : "Security ログ参照成功。4663 を取得";
                    return true;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                message = "Security ログ参照権限不足: " + ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                message = "Security ログ参照失敗: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 対象パスに一致する最新監査情報検索処理
        /// </summary>
        /// <param name="fullPath">変更後フルパス</param>
        /// <param name="lookBackSeconds">検索秒数</param>
        /// <param name="detectedAt">検知時刻</param>
        /// <param name="changeType">変更種別</param>
        /// <param name="oldFullPath">変更前フルパス</param>
        /// <returns>監査情報</returns>
        public EventAuditInfo FindLatestAuditInfo(string fullPath, int lookBackSeconds, DateTime detectedAt, WatcherChangeTypes changeType, string oldFullPath = null)
        {
            EventAuditInfo result = new EventAuditInfo();

            if (string.IsNullOrWhiteSpace(fullPath))
            {
                result.備考 = "対象パス未設定";
                return result;
            }

            string message;
            List<EventAuditInfo> candidates = GetRecentAuditCandidates(lookBackSeconds, 80, out message);

            if (candidates.Count == 0)
            {
                result.備考 = message;
                return result;
            }

            // 4663 を主軸にした候補抽出
            EventAuditInfo matched4663 = candidates
                .Where(x => x.イベントID == SecurityObjectAccessEventId)
                .Where(x => IsTargetPath(x.オブジェクト名, fullPath, oldFullPath))
                .Select(x => new
                {
                    Info = x,
                    Score = CalculateScore(x, fullPath, oldFullPath, detectedAt, changeType)
                })
                .OrderByDescending(x => x.Score)
                .ThenBy(x => GetTimeDistance(detectedAt, x.Info))
                .Select(x => x.Info)
                .FirstOrDefault();

            if (matched4663 == null)
            {
                result.備考 = "4663/4660 取得済みだが対象パス不一致。先頭候補: " + candidates[0].オブジェクト名;
                return result;
            }

            // 削除時だけ 4660 を追加照合
            if (changeType == WatcherChangeTypes.Deleted)
            {
                EventAuditInfo deletedInfo = TryAttachDeletedEvent(matched4663, candidates, detectedAt);

                if (!string.IsNullOrWhiteSpace(deletedInfo.ユーザー名))
                {
                    return deletedInfo;
                }
            }

            matched4663.備考 = BuildMatchedNote(changeType, matched4663);
            return matched4663;
        }

        /// <summary>
        /// 監査候補一覧取得処理
        /// </summary>
        /// <param name="lookBackSeconds">検索秒数</param>
        /// <param name="maxCount">最大件数</param>
        /// <param name="message">結果メッセージ</param>
        /// <returns>候補一覧</returns>
        private List<EventAuditInfo> GetRecentAuditCandidates(int lookBackSeconds, int maxCount, out string message)
        {
            List<EventAuditInfo> list = new List<EventAuditInfo>();
            message = string.Empty;

            if (lookBackSeconds <= 0)
            {
                lookBackSeconds = 30;
            }

            if (maxCount <= 0)
            {
                maxCount = 30;
            }

            try
            {
                string queryText = string.Format(
                    "*[System[(((EventID={0}) or (EventID={1})) and TimeCreated[timediff(@SystemTime) <= {2}])]]",
                    SecurityObjectAccessEventId,
                    SecurityObjectDeletedEventId,
                    lookBackSeconds * 1000);

                EventLogQuery query = new EventLogQuery("Security", PathType.LogName, queryText)
                {
                    ReverseDirection = true
                };

                using (EventLogReader reader = new EventLogReader(query))
                {
                    for (EventRecord record = reader.ReadEvent(); record != null && list.Count < maxCount; record = reader.ReadEvent())
                    {
                        using (record)
                        {
                            list.Add(CreateEventAuditInfo(record));
                        }
                    }
                }

                message = "4663/4660 候補取得件数: " + list.Count;
                return list;
            }
            catch (UnauthorizedAccessException ex)
            {
                message = "Security ログ参照権限不足: " + ex.Message;
                return list;
            }
            catch (Exception ex)
            {
                message = "4663/4660 候補取得失敗: " + ex.Message;
                return list;
            }
        }

        /// <summary>
        /// 削除確度補強処理
        /// </summary>
        /// <param name="matched4663">4663 一致候補</param>
        /// <param name="candidates">候補一覧</param>
        /// <param name="detectedAt">検知時刻</param>
        /// <returns>監査情報</returns>
        private EventAuditInfo TryAttachDeletedEvent(EventAuditInfo matched4663, List<EventAuditInfo> candidates, DateTime detectedAt)
        {
            if (matched4663 == null)
            {
                return new EventAuditInfo();
            }

            if (!HasDeleteAccess(matched4663))
            {
                matched4663.備考 = "4663 一致";
                return matched4663;
            }

            string handleId = NormalizeHandleId(matched4663.ハンドルID);

            if (string.IsNullOrWhiteSpace(handleId))
            {
                matched4663.備考 = "4663 削除候補一致";
                return matched4663;
            }

            EventAuditInfo matched4660 = candidates
                .Where(x => x.イベントID == SecurityObjectDeletedEventId)
                .Where(x => string.Equals(NormalizeHandleId(x.ハンドルID), handleId, StringComparison.OrdinalIgnoreCase))
                .Where(x => IsSameActor(x, matched4663))
                .OrderBy(x => GetTimeDistance(detectedAt, x))
                .FirstOrDefault();

            if (matched4660 == null)
            {
                matched4663.備考 = "4663 削除候補一致";
                return matched4663;
            }

            return new EventAuditInfo
            {
                イベントID = matched4660.イベントID,
                イベント時刻 = matched4660.イベント時刻 ?? matched4663.イベント時刻,
                ユーザー名 = FirstNotEmpty(matched4660.ユーザー名, matched4663.ユーザー名),
                ドメイン名 = FirstNotEmpty(matched4660.ドメイン名, matched4663.ドメイン名),
                プロセス名 = FirstNotEmpty(matched4660.プロセス名, matched4663.プロセス名),
                オブジェクト名 = matched4663.オブジェクト名,
                アクセス内容 = matched4663.アクセス内容,
                アクセスマスク = matched4663.アクセスマスク,
                ハンドルID = handleId,
                備考 = "4663/4660 削除一致"
            };
        }

        /// <summary>
        /// 一致メモ生成処理
        /// </summary>
        /// <param name="changeType">変更種別</param>
        /// <param name="auditInfo">監査情報</param>
        /// <returns>メモ</returns>
        private string BuildMatchedNote(WatcherChangeTypes changeType, EventAuditInfo auditInfo)
        {
            if (auditInfo == null)
            {
                return string.Empty;
            }

            if (changeType == WatcherChangeTypes.Created && HasWriteLikeAccess(auditInfo))
            {
                return "4663 作成候補一致";
            }

            if (changeType == WatcherChangeTypes.Changed && HasWriteLikeAccess(auditInfo))
            {
                return "4663 更新候補一致";
            }

            if (changeType == WatcherChangeTypes.Renamed)
            {
                return "4663 名称変更候補一致";
            }

            if (changeType == WatcherChangeTypes.Deleted && HasDeleteAccess(auditInfo))
            {
                return "4663 削除候補一致";
            }

            return "4663 一致";
        }

        /// <summary>
        /// 候補スコア算出処理
        /// </summary>
        /// <param name="candidate">候補</param>
        /// <param name="fullPath">変更後フルパス</param>
        /// <param name="oldFullPath">変更前フルパス</param>
        /// <param name="detectedAt">検知時刻</param>
        /// <param name="changeType">変更種別</param>
        /// <returns>スコア</returns>
        private int CalculateScore(EventAuditInfo candidate, string fullPath, string oldFullPath, DateTime detectedAt, WatcherChangeTypes changeType)
        {
            int score = 0;

            // パス一致の重み付け
            if (PathEquals(candidate.オブジェクト名, fullPath))
            {
                score += 1000;
            }

            if (changeType == WatcherChangeTypes.Renamed && PathEquals(candidate.オブジェクト名, oldFullPath))
            {
                score += 950;
            }

            // 検知時刻近傍の重み付け
            double distance = GetTimeDistance(detectedAt, candidate);

            if (distance <= 300)
            {
                score += 500;
            }
            else if (distance <= 1000)
            {
                score += 350;
            }
            else if (distance <= 3000)
            {
                score += 200;
            }

            // 変更種別ごとの重み付け
            if (changeType == WatcherChangeTypes.Created && HasWriteLikeAccess(candidate))
            {
                score += 300;
            }

            if (changeType == WatcherChangeTypes.Changed && HasWriteLikeAccess(candidate))
            {
                score += 350;
            }

            if (changeType == WatcherChangeTypes.Deleted && HasDeleteAccess(candidate))
            {
                score += 500;
            }

            if (changeType == WatcherChangeTypes.Renamed && HasDeleteAccess(candidate))
            {
                score += 150;
            }

            return score;
        }

        /// <summary>
        /// 同一主体判定処理
        /// </summary>
        /// <param name="left">比較元</param>
        /// <param name="right">比較先</param>
        /// <returns>一致判定結果</returns>
        private bool IsSameActor(EventAuditInfo left, EventAuditInfo right)
        {
            if (left == null || right == null)
            {
                return false;
            }

            bool userMatched =
                string.IsNullOrWhiteSpace(left.ユーザー名) ||
                string.IsNullOrWhiteSpace(right.ユーザー名) ||
                string.Equals(left.ユーザー名, right.ユーザー名, StringComparison.OrdinalIgnoreCase);

            bool processMatched =
                string.IsNullOrWhiteSpace(left.プロセス名) ||
                string.IsNullOrWhiteSpace(right.プロセス名) ||
                string.Equals(left.プロセス名, right.プロセス名, StringComparison.OrdinalIgnoreCase);

            return userMatched && processMatched;
        }

        /// <summary>
        /// 時刻差取得処理
        /// </summary>
        /// <param name="detectedAt">検知時刻</param>
        /// <param name="candidate">候補</param>
        /// <returns>時刻差ミリ秒</returns>
        private double GetTimeDistance(DateTime detectedAt, EventAuditInfo candidate)
        {
            if (candidate == null || !candidate.イベント時刻.HasValue)
            {
                return double.MaxValue;
            }

            return Math.Abs((detectedAt - candidate.イベント時刻.Value).TotalMilliseconds);
        }

        /// <summary>
        /// 削除アクセス判定処理
        /// </summary>
        /// <param name="auditInfo">監査情報</param>
        /// <returns>判定結果</returns>
        private bool HasDeleteAccess(EventAuditInfo auditInfo)
        {
            long accessMask = ParseHexValue(auditInfo == null ? null : auditInfo.アクセスマスク);

            if ((accessMask & DeleteAccessMask) != 0)
            {
                return true;
            }

            return ContainsIgnoreCase(auditInfo == null ? null : auditInfo.アクセス内容, "DELETE")
                || ContainsIgnoreCase(auditInfo == null ? null : auditInfo.アクセス内容, "削除");
        }

        /// <summary>
        /// 書き込み系アクセス判定処理
        /// </summary>
        /// <param name="auditInfo">監査情報</param>
        /// <returns>判定結果</returns>
        private bool HasWriteLikeAccess(EventAuditInfo auditInfo)
        {
            long accessMask = ParseHexValue(auditInfo == null ? null : auditInfo.アクセスマスク);

            if ((accessMask & WriteLikeAccessMask) != 0)
            {
                return true;
            }

            return ContainsIgnoreCase(auditInfo == null ? null : auditInfo.アクセス内容, "Write")
                || ContainsIgnoreCase(auditInfo == null ? null : auditInfo.アクセス内容, "Append")
                || ContainsIgnoreCase(auditInfo == null ? null : auditInfo.アクセス内容, "書き込み");
        }

        /// <summary>
        /// 16 進値解析処理
        /// </summary>
        /// <param name="value">対象値</param>
        /// <returns>解析結果</returns>
        private long ParseHexValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            string text = value.Trim();

            if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                text = text.Substring(2);
            }

            long result;

            if (long.TryParse(text, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            return 0;
        }

        /// <summary>
        /// ハンドル ID 正規化処理
        /// </summary>
        /// <param name="handleId">対象ハンドル ID</param>
        /// <returns>正規化後文字列</returns>
        private string NormalizeHandleId(string handleId)
        {
            if (string.IsNullOrWhiteSpace(handleId))
            {
                return string.Empty;
            }

            return handleId.Trim().ToLowerInvariant();
        }

        /// <summary>
        /// イベント情報生成処理
        /// </summary>
        /// <param name="record">イベントレコード</param>
        /// <returns>監査情報</returns>
        private EventAuditInfo CreateEventAuditInfo(EventRecord record)
        {
            string xml = record.ToXml();

            return new EventAuditInfo
            {
                イベントID = record.Id,
                イベント時刻 = record.TimeCreated.HasValue ? record.TimeCreated.Value.ToLocalTime() : (DateTime?)null,
                ユーザー名 = GetEventDataValue(xml, "SubjectUserName"),
                ドメイン名 = GetEventDataValue(xml, "SubjectDomainName"),
                プロセス名 = GetEventDataValue(xml, "ProcessName"),
                オブジェクト名 = GetEventDataValue(xml, "ObjectName"),
                アクセス内容 = GetEventDataValue(xml, "AccessList"),
                アクセスマスク = GetEventDataValue(xml, "AccessMask"),
                ハンドルID = GetEventDataValue(xml, "HandleId")
            };
        }

        /// <summary>
        /// EventData 値取得処理
        /// </summary>
        /// <param name="xml">イベント XML</param>
        /// <param name="name">項目名</param>
        /// <returns>項目値</returns>
        private string GetEventDataValue(string xml, string name)
        {
            if (string.IsNullOrWhiteSpace(xml) || string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            try
            {
                XDocument document = XDocument.Parse(xml);

                XElement dataElement = document
                    .Descendants()
                    .FirstOrDefault(x =>
                        string.Equals(x.Name.LocalName, "Data", StringComparison.OrdinalIgnoreCase) &&
                        string.Equals((string)x.Attribute("Name"), name, StringComparison.OrdinalIgnoreCase));

                return dataElement == null ? string.Empty : dataElement.Value;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 対象パス一致判定処理
        /// </summary>
        /// <param name="objectName">イベント側オブジェクト名</param>
        /// <param name="fullPath">変更後フルパス</param>
        /// <param name="oldFullPath">変更前フルパス</param>
        /// <returns>一致判定結果</returns>
        private bool IsTargetPath(string objectName, string fullPath, string oldFullPath)
        {
            if (PathEquals(objectName, fullPath))
            {
                return true;
            }

            if (PathEquals(objectName, oldFullPath))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// パス一致判定処理
        /// </summary>
        /// <param name="left">比較元</param>
        /// <param name="right">比較先</param>
        /// <returns>一致判定結果</returns>
        private bool PathEquals(string left, string right)
        {
            if (string.IsNullOrWhiteSpace(left) || string.IsNullOrWhiteSpace(right))
            {
                return false;
            }

            return string.Equals(
                NormalizePath(left),
                NormalizePath(right),
                StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// パス正規化処理
        /// </summary>
        /// <param name="path">対象パス</param>
        /// <returns>正規化後パス</returns>
        private string NormalizePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return string.Empty;
            }

            return path.Trim().TrimEnd('\\');
        }

        /// <summary>
        /// 部分一致判定処理
        /// </summary>
        /// <param name="source">対象文字列</param>
        /// <param name="keyword">検索文字列</param>
        /// <returns>判定結果</returns>
        private bool ContainsIgnoreCase(string source, string keyword)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(keyword))
            {
                return false;
            }

            return source.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// 非空文字列優先取得処理
        /// </summary>
        /// <param name="first">第 1 候補</param>
        /// <param name="second">第 2 候補</param>
        /// <returns>採用値</returns>
        private string FirstNotEmpty(string first, string second)
        {
            if (!string.IsNullOrWhiteSpace(first))
            {
                return first;
            }

            return second;
        }
    }
}
