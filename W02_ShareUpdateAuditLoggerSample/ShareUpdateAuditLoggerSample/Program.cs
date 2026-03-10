using System;
using System.Windows.Forms;

namespace ShareUpdateAuditLoggerSample
{
    /// <summary>
    /// アプリケーション起動処理
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// メイン エントリ ポイント
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
