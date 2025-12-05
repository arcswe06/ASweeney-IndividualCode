using ASweeney_IndividualCode.Backend.GlobalData;
using ASweeney_IndividualCode.Backend.MenuSystem;
using ASweeney_IndividualCode.Backend.UserFunctions;
using ASweeney_IndividualCode.FrontEnd.UserModification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.FrontEnd.MainMenu
{
    internal class MainMenuConsoleMenu : ConsoleMenu
    {
        public MainMenuConsoleMenu()
        {
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new UserModificationMenuItem(this));         
            _menuItems.Add(new LogOutMenuItem(this));
        }


        public override string MenuText()
        {
            return $"Welcome {StoredVariables.CurrentUser.Name}\n\nMain Menu";
        }
    }
}
