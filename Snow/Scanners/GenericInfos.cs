using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snow
{
    internal class GenericInfos
    {
        public async Task startGenericInfos()
        {
            await Task.Run(() =>
            {
                stringsHelper.Print("Checking generic infos...");
                Writer.writeLine("-------------------------------------\n\t\tInfo(s)");
                Writer.writeLine($"ID #{stringsHelper.resultString}");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
                {
                    string r = "";
                    ManagementObjectCollection information = searcher.Get();
                    if (information != null)
                    {
                        foreach (ManagementObject obj in information)
                        {
                            r = obj["Caption"].ToString();
                        }
                    }
                    r = r.Replace("NT 5.1.2600", "XP");
                    r = r.Replace("NT 5.2.3790", "Server 2003");
                    r = r.Replace("Microsoft", "");
                    Writer.writeLine("OS:" + r);
                }
                if (Process.GetProcessesByName("Javaw").Length != 0)
                {
                    Parallel.ForEach(listsHelper.versList, (v) =>
                    {
                        if (Process.GetProcessesByName("Javaw")[0].MainWindowTitle.Contains(v))
                        {
                            Writer.writeLine($"Version: {v}");
                        }
                    });
                }
                else
                {
                    Writer.writeLine("Version: Not found");
                }
                string culture = CultureInfo.CurrentCulture.EnglishName;
                Writer.writeLine("Location: " + culture.Substring(culture.IndexOf('(') + 1, culture.LastIndexOf(')') - culture.IndexOf('(') - 1));
                using (var searcher1 = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
                {
                    using (var items = searcher1.Get())
                    {
                        foreach (var item in items)
                        {
                            string manufacturer = item["Manufacturer"].ToString().ToLower();
                            if ((manufacturer == "microsoft corporation" && item["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL"))
                                || manufacturer.Contains("vmware")
                                || item["Model"].ToString() == "VirtualBox")
                            {
                                Writer.writeLine("Using Virtual Machine: Yes");
                            }
                            else
                            {
                                Writer.writeLine("Using Virtual Machine: None");
                            }
                        }
                    }
                    int vpn = 0;
                    if (NetworkInterface.GetIsNetworkAvailable())
                    {
                        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                        foreach (NetworkInterface Interface in interfaces)
                        {

                            if (Interface.OperationalStatus == OperationalStatus.Up)
                            {

                                if ((Interface.NetworkInterfaceType == NetworkInterfaceType.Ppp) && (Interface.NetworkInterfaceType != NetworkInterfaceType.Loopback))
                                {
                                    IPv4InterfaceStatistics statistics = Interface.GetIPv4Statistics();
                                    vpn++;
                                }
                            }
                        }
                    }
                    if (vpn > 0)
                    {
                        Writer.writeLine("Using VPN: Yes");
                    }
                    else
                    {
                        Writer.writeLine("Using VPN: None");
                    }
                    Writer.writeLine($"Date: {DateTime.Today.ToString("dd-MM-yyyy")}");
                    stringsHelper.Print("Checking processes start time...");
                    Writer.writeLine("-------------------------------------\n\t\tStart Time");
                    Writer.writeLine("Explorer: " + Process.GetProcessesByName("explorer")[0].StartTime.ToString("dd/MM/yyyy HH:mm:ss"));
                    if (Process.GetProcessesByName("Javaw").Length != 0)
                    {
                        Writer.writeLine("Javaw: " + Process.GetProcessesByName("Javaw")[0].StartTime.ToString("dd/MM/yyyy HH:mm:ss"));
                    }
                    else
                    {
                        Writer.writeLine("Javaw: Not Found");
                    }
                    Writer.writeLine("System: " + randomsHelper.start.ToString("dd/MM/yyyy HH:mm:ss"));
                    try
                    {
                        stringsHelper.CMD("CMD.exe", $@"C: && cd C:\$RECYCLE.BIN && cls && dir * /AD /B /ON /S > C:\Users\{stringsHelper.username}\AppData\Local\Temp\directory.txt");
                        string recycle = File.ReadAllText($@"C:\Users\{stringsHelper.username}\AppData\Local\Temp\directory.txt");
                        Writer.writeLine("Recycle Bin: " + File.GetLastWriteTime(recycle).ToString().Replace("AM", "").Replace("PM", ""));
                    }
                    catch { }
                    Writer.writeLine("-------------------------------------\n\t\tAlt(s)");
                    stringsHelper.Print("Checking alts...");
                    stringsHelper.checkAlt($@"C:\Users\{stringsHelper.username}\AppData\Roaming\.minecraft\launcher_accounts.json", " [MC]");
                    stringsHelper.checkAlt($@"C:\Users\{stringsHelper.username}\.lunarclient\settings\game\accounts.json", " [LC]");
                    stringsHelper.checkAlt($@"C:\Users\{stringsHelper.username}\AppData\Roaming\.minecraft\cheatbreaker_accounts.json", " [CB]");
                    if (stringsHelper.altsCounter == 0)
                    {
                        Writer.writeLine("- No Alts found");
                    }
                }
            });
        }
    }
}