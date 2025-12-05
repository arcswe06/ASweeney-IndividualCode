using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.BackEnd.GlobalResources
{
    internal class PressEnter
    {
        public static void Press() 
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("Press ENTER to continue");

            Console.ResetColor();

            Console.ReadLine();
        }
    }
}
