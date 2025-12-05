using ASweeney_IndividualCode.Backend.MenuSystem;
using ASweeney_IndividualCode.BackEnd.GlobalResources;
using ASweeney_IndividualCode.FrontEnd.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.FrontEnd.UserModification.UserActions.ChangePassword
{
    internal class ChangePasswordMenuItem : MenuItem
    {
        ConsoleMenu _menu;

        public ChangePasswordMenuItem(ConsoleMenu parentItem)
        {
            _menu = parentItem;
        }

        public override string MenuText()
        {
            return "Change Password";
        }

        public override void Select()
        {
            string password;

            while (true)
            {
                Console.Write("Enter password: ");
                password = Console.ReadLine();

                // Length check
                if (password.Length < 8)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Password must be at least 8 characters long.");
                    Console.ResetColor();
                    continue;
                }

                // Capital letter check
                if (!password.Any(char.IsUpper))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Password must contain at least one capital letter.");
                    Console.ResetColor();
                    continue;
                }

                // Symbol check
                if (!password.Any(c => !char.IsLetterOrDigit(c)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Password must contain at least one symbol (non-letter, non-digit).");
                    Console.ResetColor();
                    continue;
                }

                // If all checks pass, exit the loop
                break;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Valid password entered.");
            Console.ResetColor();
            PressEnter.Press();
            ConsoleMenu main = new MainMenuConsoleMenu();
            main.Select();
        }
    }
}
