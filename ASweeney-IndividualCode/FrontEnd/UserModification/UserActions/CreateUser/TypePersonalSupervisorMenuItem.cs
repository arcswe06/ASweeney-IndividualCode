using ASweeney_IndividualCode.BackEnd.GlobalResources;
using ASweeney_IndividualCode.Backend.MenuSystem;
using ASweeney_IndividualCode.Backend.UserFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.FrontEnd.UserModification.UserActions.CreateUser
{
    internal class TypePersonalSupervisorMenuItem : MenuItem
    {
        ConsoleMenu _menu;
        string _name;

        public TypePersonalSupervisorMenuItem(ConsoleMenu parentItem, string name)
        {
            _menu = parentItem;
            _name = name;
        }

        public override string MenuText()
        {
            return "Personal Supervisor";
        }

        public override void Select()
        {
            PersonalSupervisor user = new PersonalSupervisor();
            user.Name = _name;
            Backend.UserFunctions.CreateUser.Create(user);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"New PERSONAL SUPERVISOR created named: {_name}");
            Console.ResetColor();

            PressEnter.Press();

            ConsoleMenu exit = new UserModificationConsoleMenu(_menu);
            exit.Select();
        }
    }
}
