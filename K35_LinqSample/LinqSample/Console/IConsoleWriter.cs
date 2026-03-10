namespace LinqSample
{
    /// <summary>
    /// 画面への出力口
    /// </summary>
    public interface IConsoleWriter
    {
        /// <summary>
        /// 1行出力
        /// </summary>
        void WriteLine(string line);
    }
}
