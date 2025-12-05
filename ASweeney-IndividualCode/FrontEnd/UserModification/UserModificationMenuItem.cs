using ASweeney_IndividualCode.Backend.MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.FrontEnd.UserModification
{
    internal class UserModificationMenuItem : MenuItem
    {
        ConsoleMenu _menu;

        public UserModificationMenuItem(ConsoleMenu parentItem)
        {
            _menu = parentItem;
        }

        public override string MenuText()
        {
            return "User Modification";
        }

        public override void Select()
        {
            ConsoleMenu next = new UserModificationConsoleMenu(_menu);
            next.Select();
        }
    }
}
