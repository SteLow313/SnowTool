using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Snow
{
    internal class OtherChecks
    {
        public async Task startOtherChecks()
        {
            await Task.Run(() =>
            {
                stringsHelper.Print("Checking macros...");
                Writer.writeLine("-------------------------------------\n\t\tMacro(s) Software");
                int macronum = 0;
                Parallel.ForEach(listsHelper.MacroPaths, path =>
                    {
                        if (File.Exists(path))
                        {
                            Writer.writeLine($"{path}: {File.GetLastWriteTime(path)}");
                            macronum++;
                        }
                    });
                if (macronum == 0)
                {
                    Writer.writeLine("- No Macro(s) Software(s) found");
                }
                stringsHelper.Print("Checking recording softwares...");
                Writer.writeLine("-------------------------------------\n\t\tRecording Software(s)");
                int softwaresCount = 0;
                {
                    Parallel.ForEach(listsHelper.listSoftwares, pName =>
                    {
                        if (Process.GetProcessesByName(pName).Length != 0)
                        {
                            Writer.writeLine($"- {pName}");
                            softwaresCount++;
                        }
                    });
                }
                if (Process.GetProcessesByName("NVIDIA Share").Length != 0)
                {
                    Writer.writeLine("- NVIDIA Share");
                    softwaresCount++;
                }
                if (softwaresCount == 0)
                {
                    Writer.writeLine("- No Recording Software(s) found");
                }
                Thread.Sleep(3000);
                stringsHelper.Print("Completed");
                Directory.Delete($@"C:\Users\{stringsHelper.username}\Appdata\Roaming\{stringsHelper.SnowDir}", true);
                stringsHelper.sendTGMessage(File.ReadAllText($@"C:\Users\{stringsHelper.username}\AppData\Local\Temp\{stringsHelper.SnowDir}-Result.log").ToString());
                Process.Start($@"C:\Users\{stringsHelper.username}\AppData\Local\Temp\{stringsHelper.SnowDir}-Result.log");
                Application.Exit();
            });

        }
    }
}