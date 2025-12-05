using ASweeney_IndividualCode.Backend.MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.FrontEnd.UserModification.UserActions.ChangePassword
{
    internal class ChangePasswordAdminMenuItem : MenuItem
    {
        ConsoleMenu _menu;

        public ChangePasswordAdminMenuItem(ConsoleMenu parentItem)
        {
            _menu = parentItem;
        }

        public override string MenuText()
        {
            return "Change Password (ADMIN)";
        }

        public override void Select()
        {

        }
    }
}
