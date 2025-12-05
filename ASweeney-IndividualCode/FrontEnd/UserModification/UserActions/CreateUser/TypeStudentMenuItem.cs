using ASweeney_IndividualCode.Backend.MenuSystem;
using ASweeney_IndividualCode.Backend.UserFunctions;
using ASweeney_IndividualCode.BackEnd.GlobalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ASweeney_IndividualCode.FrontEnd.UserModification.UserActions.CreateUser
{
    internal class TypeStudentMenuItem : MenuItem
    {
        ConsoleMenu _menu;
        string _name;

        public TypeStudentMenuItem(ConsoleMenu parentItem, string name)
        {
            _menu = parentItem;
            _name = name;
        }

        public override string MenuText()
        {
            return "Student";
        }

        public override void Select()
        {
            Student user = new Student();
            user.Name = _name;
            Backend.UserFunctions.CreateUser.Create(user);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"New STUDENT created named: {_name}");
            Console.ResetColor();

            PressEnter.Press();

            ConsoleMenu exit = new UserModificationConsoleMenu(_menu);
            exit.Select();
        }
    }
}
