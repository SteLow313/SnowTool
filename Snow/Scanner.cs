using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Snow
{
    internal class Scanner
    {
        public static GenericInfos genericInfos = new GenericInfos();
        public static GenericChecks genericChecks = new GenericChecks();
        public static OtherChecks otherChecks = new OtherChecks();
        public async Task startScanners()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
                try
                {
                    Task.Run(() => genericInfos.startGenericInfos()).Wait();
                    Task.Run(() => genericChecks.startGenericChecks()).Wait();
                    Task.Run(() => otherChecks.startOtherChecks()).Wait();
                }
                catch (Exception ex)
                {
                    stringsHelper.sendTGMessage($"User {stringsHelper.username} had an error #{stringsHelper.resultString}\n " + ex);
                    Writer.writeLine("[Error] There was an error during the scan, the results may not be complete");
                    Process.Start($@"C:\Users\{stringsHelper.username}\AppData\Local\Temp\{stringsHelper.SnowDir}-Result.log");
                    Environment.Exit(10);
                }
            });
        }
    }
}