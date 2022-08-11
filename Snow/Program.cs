using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Snow
{
    internal static class Program
    {
        [STAThread]
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
        public static void HideConsole()
        {
            randomsHelper.hidden = true;
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
        }
        public static void ShowConsole()
        {
            randomsHelper.hidden = false;
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_SHOW);
        }
        public static void Main()
        {
            voidsHelper.checkVersion();
            voidsHelper.getHwidStatus();
            voidsHelper.generateId();

            HideConsole();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Snow());
        }
    }
}
