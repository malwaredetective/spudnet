using System;
using System.Windows.Forms;
using System.Drawing;

namespace SpudBot
{
    class SpudFact
    {

        static NotifyIcon notifyIcon = new NotifyIcon();

        public static string randomSpudFact = "";
        
        public static string GetSpudFact()
        {
            Random randomNumber = new Random();
            var randomSpud = randomNumber.Next(0, 8);
            string fact = "";
            switch (randomSpud)
            {
                case 0:
                    fact = "An 8 ounce baked or boiled potato has only about 100 calories.";
                    Console.WriteLine(randomSpud);
                    break;
                case 1:
                    fact = "The potato is the fourth most important crop in the world after wheat, rice and corn.";
                    Console.WriteLine(randomSpud);
                    break;
                case 2:
                    fact = "Up until the late 18th century, the French believed that potatoes caused leprosy.";
                    Console.WriteLine(randomSpud);
                    break;
                case 3:
                    fact = "The average American eats about 124 pounds of potatoes per year while Germans eat about twice as much.";
                    Console.WriteLine(randomSpud);
                    break;
                case 4:
                    fact = "Today potatoes are grown in all 50 states of the USA and in about 125 countries throughout the world.";
                    Console.WriteLine(randomSpud);
                    break;
                case 5:
                    fact = "Thomas Jefferson gets the credit for introducing \'french fries\' to America when he served them at a White House dinner.";
                    Console.WriteLine(randomSpud);
                    break;
                case 6:
                    fact = "According to the Guinness Book of World Records, the largest potato grown was 7 pounds 1 ounce by J. East (1953) and J. Busby (1982) of Great Britain.";
                    Console.WriteLine(randomSpud);
                    break;
                case 7:
                    fact = "In October 1995, the potato became the first vegetable to be grown in space. NASA and the University of Wisconsin, Madison, created the technology with the goal of feeding astronauts on long space voyages, and eventually, feeding future space colonies.";
                    Console.WriteLine(randomSpud);
                    break;
                case 8:
                    fact = "Potato blossoms used to be a big hit in royal fashion. Potatoes first became fashionable when Marie Antoinette paraded through the French countryside wearing potato blossoms in her hair.";
                    Console.WriteLine(randomSpud);
                    break;
            }
            return fact;
        }

        public static Icon Application { get; }

        public static string DisplaySpudFact()
        {
            randomSpudFact = GetSpudFact();
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.BalloonTipTitle = "SpudFact";
            notifyIcon.BalloonTipText = randomSpudFact;
            notifyIcon.BalloonTipIcon = ToolTipIcon.None;
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(30000);
            return randomSpudFact;
        }
    }
}
