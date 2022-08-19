using System;
using System.Windows.Forms;

namespace Snow
{
    internal static class Program
    {
        [STAThread]

        public static void Main()
        {
            randomsHelper.HideConsole();

            voidsHelper.checkVersion();
            voidsHelper.getHwidStatus();
            voidsHelper.generateId();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Snow());
        }
    }
}
