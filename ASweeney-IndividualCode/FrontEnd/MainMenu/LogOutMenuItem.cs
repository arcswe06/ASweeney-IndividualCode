using ASweeney_IndividualCode.Backend.MenuSystem;
using ASweeney_IndividualCode.FrontEnd.SignIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.FrontEnd.MainMenu
{
    internal class LogOutMenuItem : MenuItem
    {
        ConsoleMenu _menu;

        public LogOutMenuItem(ConsoleMenu parentItem)
        {
            _menu = parentItem;
        }

        public override string MenuText()
        {
            return "Log Out";
        }

        public override void Select()
        {
            SignInMenu.SignIn();
        }
    }

}
