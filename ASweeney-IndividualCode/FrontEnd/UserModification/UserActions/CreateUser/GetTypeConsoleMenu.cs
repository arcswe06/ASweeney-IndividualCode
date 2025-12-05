using ASweeney_IndividualCode.Backend.MenuSystem;
using ASweeney_IndividualCode.BackEnd.GlobalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ASweeney_IndividualCode.FrontEnd.UserModification.UserActions.CreateUser
{
    internal class GetTypeConsoleMenu : ConsoleMenu
    {
        private ConsoleMenu _menu;
        private string _name;

        public GetTypeConsoleMenu(ConsoleMenu parentItem, string name)
        {
            _menu = parentItem;
            _name = name;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new TypeStudentMenuItem(this, _name));
            _menuItems.Add(new TypePersonalSupervisorMenuItem(this, _name));
            _menuItems.Add(new TypeSeniorTutorMenuItem(this, _name));
            _menuItems.Add(new ReturntoMainMenuItem());
        }


        public override string MenuText()
        {
            return $"Enter TYPE of user";
        }
    }
}
