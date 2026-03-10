using System;

namespace ShareUpdateAuditLoggerSample.Models
{
    /// <summary>
    /// グリッド表示用 1 行データ
    /// </summary>
    public class FileAuditLogEntry
    {
        /// <summary>
        /// 検知時刻
        /// </summary>
        public DateTime 検知時刻 { get; set; }

        /// <summary>
        /// 検知時刻表示文字列
        /// </summary>
        public string 検知時刻表示
        {
            get { return 検知時刻.ToString("yyyy/MM/dd HH:mm:ss.fff"); }
        }

        /// <summary>
        /// 変更種別
        /// </summary>
        public string 変更種別 { get; set; }

        /// <summary>
        /// フルパス
        /// </summary>
        public string フルパス { get; set; }

        /// <summary>
        /// ファイル名
        /// </summary>
        public string ファイル名 { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime? 更新時間 { get; set; }

        /// <summary>
        /// 更新時間表示文字列
        /// </summary>
        public string 更新時間表示
        {
            get { return 更新時間.HasValue ? 更新時間.Value.ToString("yyyy/MM/dd HH:mm:ss") : string.Empty; }
        }

        /// <summary>
        /// 最終更新者
        /// </summary>
        public string 最終更新者 { get; set; }

        /// <summary>
        /// ドメイン
        /// </summary>
        public string ドメイン { get; set; }

        /// <summary>
        /// プロセス名
        /// </summary>
        public string プロセス名 { get; set; }

        /// <summary>
        /// イベント ID
        /// </summary>
        public int? イベントID { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string 備考 { get; set; }
    }
}
