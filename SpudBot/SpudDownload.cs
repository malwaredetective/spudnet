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
            string file = "C:\\Users\\" + currentUser + "\\Downloads\\Ulitimate_Twice-Backed_Potatoes.mp3";
            string url = "https://manifest.prod.boltdns.net/manifest/v1/hls/v4/clear/1033249144001/d2463fb7-9954-446f-a037-67ee95b51aa3/10s/master.m3u8?fastly_token=NWZmMDg1OTRfOTlkZDUyOGU5ODAzZTY0YzhjN2I4Yzk3ZmQ1OTc3OWMyOWI5OWMzYWFkZGU4M2Q4ZWQ0MWJmOGZkZWI3YmIyOA%3D%3D";

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
            string playVideo = "/c " + file;
            return playVideo;
        }       
    }
}
