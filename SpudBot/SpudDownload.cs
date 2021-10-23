using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SpudBot
{
    class SpudDownload
    {
        public static string Download()
        {
            string currentUser = Environment.GetEnvironmentVariable("USERNAME");
            string file = "C:\\Users\\" + currentUser + "\\Downloads\\the_thoughtful_potato.txt";
            string url = "https://pastebin.com/raw/d8TAUr6W";

            using (WebClient SpudDownloader = new WebClient())
            {
                try
                {
                    SpudDownloader.DownloadFile(url, file);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            string openFile = "/c " + file;
            return openFile;
        }       
    }
}
