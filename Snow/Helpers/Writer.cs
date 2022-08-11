using System.IO;

namespace Snow
{
    internal class Writer

    {
        public static string writeLine(string content)
        {
            using (StreamWriter sw = File.AppendText(($@"C:\Users\{stringsHelper.username}\AppData\Local\Temp\{stringsHelper.SnowDir}-Result.log")))
            {
                sw.WriteLine(content);
            }
            return content;
        }
    }
}
