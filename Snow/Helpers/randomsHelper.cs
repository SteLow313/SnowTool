using System;
using System.Diagnostics;

namespace Snow
{
    class randomsHelper
    {
        public static bool isbanned = false;
        public static bool hidden = false;

        public static EventLog GetSecurity_log = new EventLog("Security");
        public static EventLog GetSystem_log = new EventLog("System");
        public static EventLog GetApplication_log = new EventLog("Application");

        public static DateTime start = Process.GetProcessesByName("winlogon")[0].StartTime;
    }
}
