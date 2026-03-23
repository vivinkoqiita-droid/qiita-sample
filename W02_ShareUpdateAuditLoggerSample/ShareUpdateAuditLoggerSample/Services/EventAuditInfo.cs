using System;

namespace ShareUpdateAuditLoggerSample.Services
{
    /// <summary>
    /// 監査情報
    /// </summary>
    public class EventAuditInfo
    {
        /// <summary>
        /// イベント ID
        /// </summary>
        public int? イベントID { get; set; }

        /// <summary>
        /// イベント時刻
        /// </summary>
        public DateTime? イベント時刻 { get; set; }

        /// <summary>
        /// ユーザー名
        /// </summary>
        public string ユーザー名 { get; set; }

        /// <summary>
        /// ドメイン名
        /// </summary>
        public string ドメイン名 { get; set; }

        /// <summary>
        /// プロセス名
        /// </summary>
        public string プロセス名 { get; set; }

        /// <summary>
        /// オブジェクト名
        /// </summary>
        public string オブジェクト名 { get; set; }

        /// <summary>
        /// アクセス内容
        /// </summary>
        public string アクセス内容 { get; set; }

        /// <summary>
        /// アクセスマスク
        /// </summary>
        public string アクセスマスク { get; set; }

        /// <summary>
        /// ハンドル ID
        /// </summary>
        public string ハンドルID { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string 備考 { get; set; }
    }
}
