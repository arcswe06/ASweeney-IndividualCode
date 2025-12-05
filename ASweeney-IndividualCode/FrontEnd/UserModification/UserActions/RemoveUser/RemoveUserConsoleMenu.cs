using ASweeney_IndividualCode.Backend.MenuSystem;
using ASweeney_IndividualCode.Backend.UserFunctions;
using ASweeney_IndividualCode.BackEnd.UserFunctions.DisplayUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.FrontEnd.UserModification.UserActions.RemoveUser
{
    internal class RemoveUserConsoleMenu : ConsoleMenu
    {
        private string _filterUserType;
        private int? _filterPersonalSupervisorID;
        private int? _filterMood;
        private bool _filterUnresolvedReports;
        private bool _filterHasMeetings;

        private List<UserLoader.UserData> _users = new List<UserLoader.UserData>();

        public RemoveUserConsoleMenu(
            string filterUserType = null,
            int? personalSupervisorID = null,
            int? mood = null,
            bool unresolvedReports = false,
            bool hasMeetings = false)
        {
            _filterUserType = filterUserType;
            _filterPersonalSupervisorID = personalSupervisorID;
            _filterMood = mood;
            _filterUnresolvedReports = unresolvedReports;
            _filterHasMeetings = hasMeetings;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            int selectFunction = 0;
            // Load all users using UserLoader
            var loadedUsers = UserLoader.LoadUsers(
                filterUserType: _filterUserType,
                filterPersonalSupervisorID: _filterPersonalSupervisorID,
                filterMood: _filterMood,
                filterUnresolvedReports: _filterUnresolvedReports,
                filterHasMeetings: _filterHasMeetings
            );

            // Add each user as an individual menu item
            foreach (var user in loadedUsers)
            {
                _menuItems.Add(new LoadUsersMenuItem(
                    this,
                    user,
                    showUserType: string.IsNullOrEmpty(_filterUserType),
                    showMood: _filterMood.HasValue,
                    showUnresolvedReports: _filterUnresolvedReports,
                    showReportsAge: _filterUnresolvedReports,
                    showNextMeeting: _filterHasMeetings,
                    selectFunction
                ));
            }


            _menuItems.Add(new ExitMenuItem(this));
        }

        public override string MenuText()
        {
            return "User Removal Menu";
        }
    }

}
