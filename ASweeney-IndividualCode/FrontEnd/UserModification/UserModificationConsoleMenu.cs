using ASweeney_IndividualCode.Backend.GlobalData;
using ASweeney_IndividualCode.Backend.MenuSystem;
using ASweeney_IndividualCode.Backend.UserFunctions;
using ASweeney_IndividualCode.FrontEnd.MainMenu;
using ASweeney_IndividualCode.FrontEnd.UserModification;
using ASweeney_IndividualCode.FrontEnd.UserModification.UserActions;
using ASweeney_IndividualCode.FrontEnd.UserModification.UserActions.CreateUser;
using ASweeney_IndividualCode.FrontEnd.UserModification.UserActions.RemoveUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.FrontEnd.UserModification
{
    internal class UserModificationConsoleMenu : ConsoleMenu
    {
        private ConsoleMenu _menu;

        public UserModificationConsoleMenu(ConsoleMenu parentItem)
        {
            _menu = parentItem;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            if (StoredVariables.CurrentUser.GetType() == typeof(SeniorTutor))
            {
                _menuItems.Add(new CreateUserMenuItem(this));
                _menuItems.Add(new RemoveUserMenuItem(this));
            }

            _menuItems.Add(new ReturntoMainMenuItem());
        }


        public override string MenuText()
        {
            return $"User Modification";
        }
    }
}
