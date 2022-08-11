using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Threading.Tasks;
namespace Snow
{
    internal class GenericChecks
    {
        public static int cheats = 0;
        public async Task startGenericChecks()
        {
            await Task.Run(() =>
            {
                stringsHelper.Print("Checking generic cheat...");
                Writer.writeLine("-------------------------------------\n\t\tCheck(s)");
                try
                {
                    if (Process.GetProcessesByName("Javaw").Length != 0)
                    {
                        voidsHelper.Scan("https://pastebin.com/raw/0Br70B73", '*', $@"C:\Users\{stringsHelper.username}\AppData\Roaming\{stringsHelper.SnowDir}\Javaw.txt");
                    }
                    voidsHelper.Scan("https://pastebin.com/raw/imQ4pSnJ", '*', $@"C:\Users\{stringsHelper.username}\AppData\Roaming\{stringsHelper.SnowDir}\DPS.txt");
                    Parallel.ForEach(listsHelper.dnsStrings, (visited_site) =>
                    {
                        if (File.ReadAllText($@"C:\Users\{stringsHelper.username}\AppData\Roaming\{stringsHelper.SnowDir}\DNSCache.txt").Contains(visited_site))
                        {
                            Writer.writeLine($"[Out Of Istance] User visited https://{visited_site}/");
                            cheats++;
                        }
                    });
                    voidsHelper.Scan("https://pastebin.com/raw/TUR2zm0a", '*', $@"C:\Users\{stringsHelper.username}\AppData\Roaming\{stringsHelper.SnowDir}\PcaSvc.txt");
                }
                catch {}
                Parallel.ForEach(Directory.GetFiles(@"C:\Windows\Prefetch"), allPfFiles =>
                {

                    if (new FileInfo(allPfFiles).Attributes.HasFlag(FileAttributes.ReadOnly))
                    {
                        Writer.writeLine("[Generic Bypass Method] This file is in read-only " + allPfFiles);
                        cheats++;
                    }
                    Parallel.ForEach(listsHelper.pfFiles, (suspFiles) =>
                    {
                        if (randomsHelper.start <= File.GetCreationTime(allPfFiles) && allPfFiles.Contains(suspFiles))
                        {
                            Writer.writeLine("[Generic Bypass Method] Found " + allPfFiles + " at " + File.GetCreationTime(allPfFiles).ToString("HH:mm") + $" possible {suspFiles} method?");
                            cheats++;
                        }
                    });
                });
                ManagementObjectSearcher s = new ManagementObjectSearcher(new SelectQuery("Win32_Service", "State='Stopped'"));
                foreach (ManagementObject service in s.Get())
                {
                    Parallel.ForEach(listsHelper.ListServices, (Service) =>
                    {
                        if ((service.ToString()).Contains(Service))
                        {
                            Writer.writeLine($"[Generic Bypass Method] {Service.Replace(@"""", null)} Missed");
                            cheats++;
                        }
                    });
                }
                if (Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\\PrefetchParameters").GetValue("EnablePrefetcher").ToString() == "0")
                {
                    Writer.writeLine("[Generic Bypass Method] Prefetch Disabled (Regedit) ");
                    cheats++;
                }
                Parallel.ForEach(DriveInfo.GetDrives(), d =>
                {
                    {
                        if (d.IsReady == true && !d.DriveFormat.Contains("NTFS"))
                        {
                            Writer.writeLine("[Generic Bypass Method] Journal Disabled on " + d.Name);
                            cheats++;
                        }
                    }
                });
                if (Process.GetProcessesByName("XMouseButtonControl").Length != 0)
                {
                    Writer.writeLine("[Out Of Istance] Found XMouseButton");
                    cheats++;
                };
                Parallel.ForEach(Directory.GetFiles($@"C:\Users\{stringsHelper.username}\AppData\Local\Temp"), (JnativeHookFile) =>
                {
                    if (JnativeHookFile.Contains("JNativeHook") && File.GetCreationTime(JnativeHookFile) > randomsHelper.start)
                    {
                        Writer.writeLine($"[Out Of Istance] Found JNativeHook (" + File.GetCreationTime(JnativeHookFile).ToString("HH:mm") + ")");
                        cheats++;
                    }
                });
                stringsHelper.CMD("CMD.exe", $@"C: && fsutil usn readjournal c: csv | findstr /i /C:""%date%"" | findstr /i /C:""jnativehook"" | findstr /i /C:"".dll"" | findstr /i /C:""0x80000200"" > C:\Users\{stringsHelper.username}\AppData\Local\Temp\jnativehook.txt");
                string[] jhLines = File.ReadAllLines($@"C:\Users\{stringsHelper.username}\AppData\Local\Temp\jnativehook.txt");
                Parallel.ForEach(jhLines, totalLines =>
                {
                    if (totalLines.Contains("0x80000200"))
                    {
                        Writer.writeLine("[Out Of Istance] Found JNativeHook [Deleted]");
                        cheats++;
                    }
                });
                if (Directory.GetFiles($@"C:\Users\{stringsHelper.username}\AppData\Roaming\Microsoft\Windows\Recent").Length == 0)
                {
                    Writer.writeLine("[Generic Bypass Method] shell:recent is empty");
                    cheats++;
                }
                if (randomsHelper.start <= File.GetLastWriteTime($@"C:\Users\{stringsHelper.username}\AppData\Roaming\Microsoft\Windows\PowerShell\PSReadLine\ConsoleHost_history.txt"))
                {
                    Writer.writeLine($@"[Generic Bypass Method] Powershell history edited today ({File.GetLastWriteTime($@"C:\Users\{stringsHelper.username}\AppData\Roaming\Microsoft\Windows\PowerShell\PSReadLine\ConsoleHost_history.txt").ToString("HH:mm")})");
                    cheats++;
                }
                Parallel.ForEach(listsHelper.Application_entries, Application_log =>
                {
#pragma warning disable CS0618
                    if (Application_log.EventID == 3079 && randomsHelper.start <= Application_log.TimeGenerated)
                    {
                        Writer.writeLine($"[Generic Bypass Method] USN Journal was deleted ({Application_log.TimeGenerated.ToString("HH:mm")})");
                        cheats++;
                    }
                }
                );
                Parallel.ForEach(listsHelper.Security_entries, Security_Log =>
                {
                    if (Security_Log.InstanceId == 1102 && randomsHelper.start <= Security_Log.TimeGenerated)
                    {
                        Writer.writeLine($"[Generic Bypass Method] Security log deleted ({Security_Log.TimeGenerated.ToString("HH:mm")})");
                        cheats++;
                    }
                });
                Parallel.ForEach(listsHelper.System_entries, System_Log =>
                {
                    if (System_Log.InstanceId == 104 && randomsHelper.start <= System_Log.TimeGenerated)
                    {
                        Writer.writeLine($"[Generic Bypass Method] Application or System log deleted ({System_Log.TimeGenerated.ToString("HH:mm")})");
                        cheats++;
                    }
                });
                if (cheats == 0)
                {
                    Writer.writeLine("- No cheat(s) found");
                }
            });
        }
    }
}