using System;
using System.Windows.Forms;

namespace FileSystemObserver
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーション起点
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
