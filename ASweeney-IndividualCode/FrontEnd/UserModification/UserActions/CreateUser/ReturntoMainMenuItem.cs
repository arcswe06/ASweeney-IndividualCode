using ASweeney_IndividualCode.Backend.MenuSystem;
using ASweeney_IndividualCode.FrontEnd.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.FrontEnd.UserModification.UserActions.CreateUser
{
    internal class ReturntoMainMenuItem : MenuItem
    {

        public ReturntoMainMenuItem()
        {
        }

        public override string MenuText()
        {
            return "Exit";
        }

        public override void Select()
        {
            ConsoleMenu next = new MainMenuConsoleMenu();
            next.Select();
        }
    }
}
