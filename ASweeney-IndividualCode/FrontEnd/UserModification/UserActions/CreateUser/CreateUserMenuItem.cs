using ASweeney_IndividualCode.Backend.MenuSystem;
using ASweeney_IndividualCode.BackEnd.GlobalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.FrontEnd.UserModification.UserActions.CreateUser
{
    internal class CreateUserMenuItem : MenuItem
    {
        ConsoleMenu _menu;

        public CreateUserMenuItem(ConsoleMenu parentItem)
        {
            _menu = parentItem;
        }

        public override string MenuText()
        {
            return "Create User";
        }

        public override void Select()
        {
            string name;
            while (true)
            {
                Console.Write("Enter full name: ");
                name = Console.ReadLine().Trim();

                // Length check
                if (name.Length > 20)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Name cannot be longer than 20 characters.");
                    Console.ResetColor();
                    continue;
                }

                // Allowed characters only
                foreach (char c in name)
                {
                    if (!char.IsLetter(c) && c != ' ' && c != '-')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Name contains invalid characters. Only letters and hyphens are allowed.");
                        Console.ResetColor();
                        goto ContinueLoop;
                    }
                }

                // Must contain exactly two parts (allowing double-barrel last name)
                string[] parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Name must contain exactly two words: first name and last name.");
                    Console.ResetColor();
                    continue;
                }

                string first = parts[0];
                string last = parts[1];

                // Check capitalisation
                bool IsCapitalised(string s)
                {
                    if (s.Contains('-'))
                    {
                        // Check each part of double-barrel name
                        string[] split = s.Split('-');
                        foreach (var part in split)
                        {
                            if (part.Length == 0 ||
                                !char.IsUpper(part[0]) ||
                                (part.Length > 1 && !part.Substring(1).All(char.IsLower)))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return char.IsUpper(s[0]) &&
                               (s.Length == 1 || s.Substring(1).All(char.IsLower));
                    }
                }

                if (!IsCapitalised(first) || !IsCapitalised(last))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Each name must be capitalised. Example: John Smith or John Smith-Jones");
                    Console.ResetColor();
                    continue;
                }

                // If we reach here, name is valid
                break;

            ContinueLoop:;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Valid name entered: {name}");
            Console.ResetColor();

            PressEnter.Press();

            ConsoleMenu next = new GetTypeConsoleMenu(_menu, name);
            next.Select();
        }
    }
}
