using System;

namespace FileSystemObserver
{
    /// <summary>
    /// 一覧表示用イベント情報
    /// </summary>
    internal sealed class FileEventEntry
    {
        /// <summary>
        /// 連番
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 変更種別
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// 相対パス
        /// </summary>
        public string RelativePath { get; set; }

        /// <summary>
        /// ファイル名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// 更新者表示
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// 判定元表示
        /// </summary>
        public string DetectionSource { get; set; }

        /// <summary>
        /// 実行プロセス表示
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 補足表示
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// 完全パス
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// 集約回数
        /// </summary>
        public int MergeCount { get; set; }
    }
}
