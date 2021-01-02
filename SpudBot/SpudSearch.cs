using System;
using System.Diagnostics;

namespace SpudBot
{
    class SpudSearch
    {
        public static void Google()
        {
            Process.Start("https://www.google.com/search?q=potatoes+near+me&oq=potatoes+near+me");
        }
    }
}
