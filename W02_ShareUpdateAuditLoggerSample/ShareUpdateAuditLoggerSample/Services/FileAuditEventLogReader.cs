using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        /// 監査対象イベント ID
        /// </summary>
        private const int SecurityObjectAccessEventId = 4663;

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
        /// 直近 4663 候補一覧取得処理
        /// </summary>
        /// <param name="lookBackSeconds">検索秒数</param>
        /// <param name="maxCount">最大件数</param>
        /// <param name="message">結果メッセージ</param>
        /// <returns>候補一覧</returns>
        public List<EventAuditInfo> GetRecent4663Candidates(int lookBackSeconds, int maxCount, out string message)
        {
            List<EventAuditInfo> list = new List<EventAuditInfo>();
            message = string.Empty;

            if (lookBackSeconds <= 0)
            {
                lookBackSeconds = 30;
            }

            if (maxCount <= 0)
            {
                maxCount = 10;
            }

            try
            {
                string queryText = string.Format(
                    "*[System[(EventID={0}) and TimeCreated[timediff(@SystemTime) <= {1}]]]",
                    SecurityObjectAccessEventId,
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

                message = "4663 候補取得件数: " + list.Count;
                return list;
            }
            catch (UnauthorizedAccessException ex)
            {
                message = "Security ログ参照権限不足: " + ex.Message;
                return list;
            }
            catch (Exception ex)
            {
                message = "4663 候補取得失敗: " + ex.Message;
                return list;
            }
        }

        /// <summary>
        /// 対象パスに一致する最新監査情報検索処理
        /// </summary>
        /// <param name="fullPath">変更後フルパス</param>
        /// <param name="lookBackSeconds">検索秒数</param>
        /// <param name="oldFullPath">変更前フルパス</param>
        /// <returns>監査情報</returns>
        public EventAuditInfo FindLatestAuditInfo(string fullPath, int lookBackSeconds, string oldFullPath = null)
        {
            EventAuditInfo result = new EventAuditInfo();

            if (string.IsNullOrWhiteSpace(fullPath))
            {
                result.備考 = "対象パス未設定";
                return result;
            }

            string message;
            List<EventAuditInfo> candidates = GetRecent4663Candidates(lookBackSeconds, 30, out message);

            if (candidates.Count == 0)
            {
                result.備考 = message;
                return result;
            }

            EventAuditInfo matched = candidates
                .Where(x => IsTargetPath(x.オブジェクト名, fullPath, oldFullPath))
                .OrderBy(x => x.イベント時刻.HasValue ? Math.Abs((DateTime.Now - x.イベント時刻.Value).TotalMilliseconds) : double.MaxValue)
                .FirstOrDefault();

            if (matched == null)
            {
                result.備考 = "4663 取得済みだが対象パス不一致。先頭候補: " + candidates[0].オブジェクト名;
                return result;
            }

            matched.備考 = "4663 一致";
            return matched;
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
                アクセス内容 = GetEventDataValue(xml, "AccessList")
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
    }
}