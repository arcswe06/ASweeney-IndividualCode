using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.Backend.MenuSystem
{
    static class ConsoleHelper
    {
        public static int GetIntegerInRange(int pMin, int pMax, string pMessage)
        {
            if (pMin > pMax)
            {
                throw new Exception($"Minimum value {pMin} cannot be greater than maximum value {pMax}");
            }

            int result;

            do
            {
                Console.WriteLine(pMessage);
                Console.WriteLine($"Please enter a number between {pMin} and {pMax} inclusive.");

                string userInput = Console.ReadLine();

                try
                {
                    result = int.Parse(userInput);
                }
                catch
                {
                    Console.WriteLine($"{userInput} is not a number");
                    continue;
                }

                if (result >= pMin && result <= pMax)
                {
                    return result;
                }
                Console.WriteLine($"{result} is not between {pMin} and {pMax} inclusive.");
            } while (true);
        }


        public static int GetSelectionFromMenu(IEnumerable<string> items, string prompt)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(prompt);

            int itemNumber = 0;

            foreach (string item in items)
            {
                itemNumber++;
                sb.AppendLine($"{itemNumber}. {item}");
            }

            return GetIntegerInRange(1, itemNumber, sb.ToString()) - 1;
        }
    }
}
