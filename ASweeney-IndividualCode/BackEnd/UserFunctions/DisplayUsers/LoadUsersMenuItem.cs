using ASweeney_IndividualCode.Backend.GlobalData;
using ASweeney_IndividualCode.Backend.MenuSystem;
using ASweeney_IndividualCode.Backend.UserFunctions;
using ASweeney_IndividualCode.BackEnd.GlobalResources;
using ASweeney_IndividualCode.FrontEnd.MainMenu;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ASweeney_IndividualCode.BackEnd.UserFunctions.DisplayUsers
{
    internal class LoadUsersMenuItem : MenuItem
    {
        private UserLoader.UserData _user;
        private bool _showUserType;
        private bool _showMood;
        private bool _showUnresolvedReports;
        private bool _showReportsAge;
        private bool _showNextMeeting;
        private int SelectFunction;


        private ConsoleMenu _parentMenu;

        public LoadUsersMenuItem(
            ConsoleMenu parentMenu,
            UserLoader.UserData user,
            bool showUserType = true,
            bool showMood = true,
            bool showUnresolvedReports = true,
            bool showReportsAge = true,
            bool showNextMeeting = true,
            int selectFunction = 0)
            {
                _parentMenu = parentMenu;
                _user = user;
                _showUserType = showUserType;
                _showMood = showMood;
                _showUnresolvedReports = showUnresolvedReports;
                _showReportsAge = showReportsAge;
                _showNextMeeting = showNextMeeting;
                SelectFunction = selectFunction;
            }

        public override string MenuText()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(_user.ID.ToString().PadRight(6));
            sb.Append(_user.Name.PadRight(20));

            if (_showUserType)
                sb.Append(_user.UserType.PadRight(18));

            if (_showMood)
            {
                if (_user.Mood.HasValue)
                {
                    if (_user.Mood < 2)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (_user.Mood < 5)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (_user.Mood == 5)
                        Console.ForegroundColor = ConsoleColor.Green;
                }
                sb.Append((_user.Mood.HasValue ? _user.Mood.Value.ToString() : "-").PadRight(6));
                Console.ResetColor();
            }

            if (_showUnresolvedReports)
                sb.Append(_user.UnresolvedReportsCount.ToString().PadRight(10));

            if (_showReportsAge)
                sb.Append((_user.OldestReportDate.HasValue ? GetTimeAgo(_user.OldestReportDate.Value) : "-").PadRight(12));

            if (_showNextMeeting)
                sb.Append((_user.NextMeetingDate.HasValue ? _user.NextMeetingDate.Value.ToString("yyyy-MM-dd HH:mm") : "-").PadRight(20));

            return sb.ToString();
        }

        private string GetTimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.UtcNow - dt.ToUniversalTime();
            if (span.TotalDays >= 1)
                return $"{(int)span.TotalDays}d ago";
            else if (span.TotalHours >= 1)
                return $"{(int)span.TotalHours}h ago";
            else if (span.TotalMinutes >= 1)
                return $"{(int)span.TotalMinutes}m ago";
            else
                return "Just now";
        }

        public override void Select()
        {
            ConsoleMenu Main = new MainMenuConsoleMenu();

            switch (SelectFunction)
            {
                case 0:
                    DeleteUser.Delete(_user.ID);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Deleted user: {_user.Name}");
                    Console.ResetColor();
                    PressEnter.Press();
                    Main.Select();

                    break;
            }

        }

        // -----------------------------
        // Internal class for user data
        // -----------------------------

    }
}
