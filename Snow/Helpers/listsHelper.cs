using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Snow
{
    class listsHelper
    {
        public static List<EventLogEntry> Security_entries = randomsHelper.GetSecurity_log.Entries.Cast<EventLogEntry>().ToList();
        public static List<EventLogEntry> System_entries = randomsHelper.GetSystem_log.Entries.Cast<EventLogEntry>().ToList();
        public static List<EventLogEntry> Application_entries = randomsHelper.GetApplication_log.Entries.Cast<EventLogEntry>().ToList();
        public static List<string> listSoftwares = new List<string>()
        {
            "obs64", "obs32", "Action", "RadeonSettings","ShareX", "CamRecorder", "Fraps", "GameBar", "RadeonSettings", "CamRecorder", "Fraps", "bdcam"
        };
        public static List<string> versList = new List<string>()
        {
            "1.7", "1.8", "1.9", "1.12", "1.16", "1.17", "1.18", "1.19"
        };
        public static List<string> dnsStrings = new List<string>()
          {
        "vape.gg", "drip.gg", "neverlack.in", "entropy.club", "whiteout.lol", "antic.run", "ploow.store","dopp.in", "dreamclient.xyz", "cloudcheats.net", "unicorn-hacks.net", "martianproject.net","yukio.cc", "dropsy.cc", "sideware.club", "slapp.in", "glockclicker.xyz", "intent.store", "ghost.brizy.site", "ozix.sellix.io", "aresclicker.sellix.io", "ares.lgbt", "fox-services.sellix.io", "iridium.wtf", "rapi.st", "katana.cam", "hovac.lol", "qxkk.tk", "duskclicker.xyz", "sarm.gg", "cyde.xyz", "lustful.pub", "cyde.xyz", "owo.glitch.cam", "apeclient.ga", "jelte.lol", "lithiumtools.sellix.io", "inquity.selly.store"
          };
        public static List<string> Services = new List<string>()
        {
            "DPS", "DNSCache", "PcaSvc"
        };
        public static List<string> pfFiles = new List<string>()
        {
            "WMIC", "CACLS",
        };
        public static List<string> ListServices = new List<string>()
        {
            @"""DPS""", "EventLog", "AppInfo", "PcaSvc", "SysMain", "DiagTrack"
        };
        public static List<string> MacroPaths = new List<string>()
        {
           $@"C:\Users\{stringsHelper.username}\AppData\Local\Razer\Synapse3\Log\Razer Synapse 3.log", $@"C:\Users\{stringsHelper.username}\AppData\Local\LGHUB\settings.db", $@"C:\Users\{stringsHelper.username}\AppData\Local\BY-COMBO2\pro.dct"
        };
    }
}
