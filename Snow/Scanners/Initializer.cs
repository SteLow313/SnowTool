using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Snow
{
    internal class Initializer
    {

        public async Task startInitializer()
        {
            await Task.Run(() =>
            {
              
                stringsHelper.sendTGMessage($@"User {stringsHelper.username} with the #{stringsHelper.finalHwid} performed a scan with the id #{stringsHelper.resultString}");

                Directory.CreateDirectory($@"C:\Users\{stringsHelper.username}\Appdata\Roaming\{stringsHelper.SnowDir}");
                
                using (var client = new WebClient())
                    client.DownloadFile("https://cdn.discordapp.com/attachments/959860305272377416/969004323474395186/strings2.exe", $@"C:\Users\{stringsHelper.username}\Appdata\Roaming\{stringsHelper.SnowDir}\strings2.exe");
                if (Process.GetProcessesByName("Javaw").Length != 0)
                {
                    stringsHelper.CMD("CMD.exe", $@"C: && cd C:\Users\{stringsHelper.username}\AppData\Roaming\{stringsHelper.SnowDir}\ && strings2.exe -pid {Process.GetProcessesByName("Javaw")[0].Id} > Javaw.txt");
                }
                Parallel.ForEach(listsHelper.Services, (s) =>
                {
                    stringsHelper.Dump(s);
                });
            });
        }
    }
}