using ASweeney_IndividualCode.Backend.MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.FrontEnd.UserModification.UserActions.RemoveUser
{
    internal class RemoveUserMenuItem : MenuItem
    {
        ConsoleMenu _menu;

        public RemoveUserMenuItem(ConsoleMenu parentItem)
        {
            _menu = parentItem;
        }

        public override string MenuText()
        {
            return "Remove User";
        }

        public override void Select()
        {
            ConsoleMenu next = new RemoveUserConsoleMenu(null, null, null, false, false);
            next.Select();
        }
    }
}
