namespace WinFormsUiFreezeSample
{
    /// <summary>
    /// アプリケーション開始クラス
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// アプリケーション開始点
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
