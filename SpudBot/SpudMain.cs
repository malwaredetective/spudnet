using System;

namespace SpudBot
{
    class SpudMain
    {
        static void Main(string[] args)
        {
            string server = args[0];
            int port = Int32.Parse(args[1]);
            SpudNet.Connect(server, port);
        }
    }
}
