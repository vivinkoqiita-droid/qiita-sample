namespace DesignerSafeControlSample
{
    /// <summary>
    /// アプリケーション起動処理。
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// エントリポイント。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
