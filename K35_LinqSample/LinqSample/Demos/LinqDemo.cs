using System;

namespace LinqSample
{
    /// <summary>
    /// 1つの実行ボタン用デモ
    /// </summary>
    public sealed class LinqDemo
    {
        /// <summary>
        /// タブ名
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// ボタン名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 表示順
        /// </summary>
        public int DisplayOrder { get; }

        /// <summary>
        /// 実行本体
        /// </summary>
        public Action<IConsoleWriter> Run { get; }

        /// <summary>
        /// 表示用コード
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// 値の受け取り
        /// </summary>
        public LinqDemo(string category, string name, string code, Action<IConsoleWriter> run, int displayOrder = 0)
        {
            Category = category;
            Name = name;
            Code = code;
            Run = run;
            DisplayOrder = displayOrder;
        }
    }
}
