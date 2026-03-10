using System;

namespace LinqSample
{
    /// <summary>
    /// 画面に出す Writer
    /// </summary>
    public sealed class UiConsoleWriter : IConsoleWriter
    {
        private readonly Action<string> _appendLine;

        /// <summary>
        /// 出力先の受け取り
        /// </summary>
        public UiConsoleWriter(Action<string> appendLine)
        {
            _appendLine = appendLine;
        }

        /// <summary>
        /// 1行出力
        /// </summary>
        public void WriteLine(string line)
        {
            _appendLine(line);
        }
    }
}
