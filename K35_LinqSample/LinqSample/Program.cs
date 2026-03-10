using System;
using System.Windows.Forms;

namespace LinqSample
{
    /// <summary>
    /// アプリの入口
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// 起動処理
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // WinForms 既定の初期化
            // ※ ApplicationConfiguration は .NET 側の用意
            ApplicationConfiguration.Initialize();

            // メイン画面の起動
            Application.Run(new MainForm());
        }
    }
}
