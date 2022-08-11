using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snow
{

    class voidsHelper
    {
        public static void Write(string text)
        {
            Random rnd = new Random();
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(rnd.Next(60, 120));
            }
        }
        public static void Scan(string link, char separator, string result)
        {
            string file_lines = "";
            file_lines = File.ReadAllText(result, Encoding.Default);
            WebClient client = new WebClient();
            string cheat, client_str;
            Regex get_initialstring = new Regex(".*?/");
            Regex remove_junkdps_strings2 = new Regex("\\.exe!.*?/");
            Regex due_puntiescl = new Regex("!!");
            Regex regular_string = new Regex("!!.*?!");
            Regex remove_junk1 = new Regex("!");
            string streamReader_line;
            using (Stream stream = client.OpenRead(link))
            {
                using (BufferedStream bs = new BufferedStream(stream))
                {
                    using (StreamReader streamReader = new StreamReader(bs))
                    {
                        while ((streamReader_line = streamReader.ReadLine()) != null)
                        {
                            //dns
                            client_str = streamReader_line.Split(separator)[0];
                            cheat = streamReader_line.Split(separator)[1];
                            //DPS
                            if (link == "https://pastebin.com/raw/imQ4pSnJ" && file_lines.ToLower().Contains(client_str))
                            {
                                string[] file_lines2 = File.ReadAllLines(result);
                                string cheat_filename = "";
                                Parallel.ForEach(file_lines2, index =>
                                {
                                    if (index.Contains(client_str))
                                    {
                                        Match mch = get_initialstring.Match(index);
                                        Match regular = regular_string.Match(index);
                                        cheat_filename = due_puntiescl.Replace(regular.Value, "");
                                        cheat_filename = remove_junk1.Replace(cheat_filename, "");
                                        if (cheat_filename.Length != 0)
                                        {
                                            cheat_filename = cheat_filename.Replace(" ", "");
                                            Writer.writeLine($"[Istance] Traces found for {cheat} as {cheat_filename}");
                                        }
                                        else
                                        {
                                            Writer.writeLine("[Instance] Traces found for " + cheat);
                                        }
                                    }
                                });
                            }
                            //PcaSvc 
                            if (link == "https://pastebin.com/raw/TUR2zm0a" && file_lines.ToLower().Contains(client_str))
                            {
                                Writer.writeLine("[Istance] Traces found for " + cheat);
                            }
                            //Javaw https://pastebin.com/raw/2a8xU3zV
                            if (Process.GetProcessesByName("Javaw").Length != 0 && link == "https://pastebin.com/raw/0Br70B73" && file_lines.ToLower().Contains(client_str))
                            {
                                if (client_str.Contains("Generic Cheat"))
                                {
                                    Writer.writeLine("[Istance] Traces found for " + cheat);
                                }
                                else
                                {
                                    Writer.writeLine("[Istance] Traces found for " + cheat + " [Beta]");
                                }
                            }
                        }
                    }
                }
            }
        }


        public static void getHwidStatus()
        {
            ManagementObjectCollection managementObjectCollection = new ManagementObjectSearcher("Select ProcessorId From Win32_processor").Get();

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollection.GetEnumerator();
            using (enumerator)
            {
                if (enumerator.MoveNext())
                {
                    stringsHelper.finalHwid = ((ManagementObject)enumerator.Current)["ProcessorId"].ToString();
                }
            }
            WebClient client = new WebClient();
            string hwid = "";
            string streamReader_line;
            using (Stream stream = client.OpenRead("https://pastebin.com/raw/d3Urk1Zg"))
            {
                using (BufferedStream bs = new BufferedStream(stream))
                {
                    using (StreamReader streamReader = new StreamReader(bs))
                    {
                        while ((streamReader_line = streamReader.ReadLine()) != null)
                        {
                            hwid = streamReader_line.Split('*')[0];
                            stringsHelper.reason = streamReader_line.Split('*')[1];
                            if (hwid.Contains(stringsHelper.finalHwid))
                            {
                                randomsHelper.isbanned = true;
                            }
                        }
                    }
                }
            }
        }
        public static void generateId()
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] Charsarr = new char[15];
            Random random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }
            stringsHelper.resultString = new string(Charsarr);

        }
        public static void checkVersion()
        {
            WebClient client = new WebClient();
            string url = "https://pastebin.com/raw/kRgT88rz";
            string version = "1.1.0";
            if (version != client.DownloadString(url))
            {
                MessageBox.Show("You are using an old version of the tool, please download the newest one!", "Snow ScreenShare Tool - Version Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
