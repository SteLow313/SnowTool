using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace Snow
{
    class randomsHelper
    {
     

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
        public static void HideConsole()
        {
            hidden = true;
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
        }
        public static void ShowConsole()
        {
            hidden = false;
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_SHOW);
        }
        public static bool isbanned = false;
        public static bool hidden = false;

        public static EventLog GetSecurity_log = new EventLog("Security");
        public static EventLog GetSystem_log = new EventLog("System");
        public static EventLog GetApplication_log = new EventLog("Application");

        public static DateTime start = Process.GetProcessesByName("winlogon")[0].StartTime;
        public static WebClient wc = new WebClient();
    }
}
