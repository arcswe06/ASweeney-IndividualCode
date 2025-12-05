using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.BackEnd.UserFunctions.DisplayUsers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ASweeney_IndividualCode.Backend.GlobalData;
    using Microsoft.Data.Sqlite;

    internal class UserLoader
    {
        public class UserData
        {
            public int ID;
            public string Name;
            public string UserType;
            public int? Mood;
            public int UnresolvedReportsCount;
            public DateTime? OldestReportDate;
            public DateTime? NextMeetingDate;
        }

        /// <summary>
        /// Loads users from the database according to filters
        /// </summary>
        public static List<UserData> LoadUsers(
            string filterUserType = null,
            int? filterPersonalSupervisorID = null,
            int? filterMood = null,
            bool filterUnresolvedReports = false,
            bool filterHasMeetings = false)
        {
            var users = new List<UserData>();
            var connection = StoredVariables.Connection;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT u.ID, u.Name, u.UserType, u.Mood,");
            sb.AppendLine("(SELECT COUNT(*) FROM Reports r WHERE r.StudentID = u.ID AND r.Resolved = 0) AS UnresolvedReportsCount,");
            sb.AppendLine("(SELECT MIN(r.CreatedAt) FROM Reports r WHERE r.StudentID = u.ID AND r.Resolved = 0) AS OldestReportDate,");
            sb.AppendLine("(SELECT MIN(m.DateTime) FROM Meetings m WHERE m.StudentID = u.ID AND m.DateTime >= CURRENT_TIMESTAMP) AS NextMeetingDate");
            sb.AppendLine("FROM Users u");
            sb.AppendLine("LEFT JOIN Relationships rel ON u.ID = rel.StudentID");
            sb.AppendLine("WHERE 1=1");

            if (!string.IsNullOrEmpty(filterUserType))
                sb.AppendLine("AND u.UserType = @userType");
            if (filterPersonalSupervisorID.HasValue)
                sb.AppendLine("AND rel.PersonalSupervisorID = @psID");
            if (filterMood.HasValue)
                sb.AppendLine("AND u.Mood = @mood");
            if (filterHasMeetings)
                sb.AppendLine("AND EXISTS (SELECT 1 FROM Meetings m WHERE m.StudentID = u.ID AND m.DateTime >= CURRENT_TIMESTAMP)");
            if (filterUnresolvedReports)
                sb.AppendLine("AND EXISTS (SELECT 1 FROM Reports r WHERE r.StudentID = u.ID AND r.Resolved = 0)");

            sb.AppendLine("ORDER BY u.Mood DESC, OldestReportDate ASC NULLS LAST, NextMeetingDate ASC NULLS LAST;");

            using var cmd = new SqliteCommand(sb.ToString(), connection);

            if (!string.IsNullOrEmpty(filterUserType))
                cmd.Parameters.AddWithValue("@userType", filterUserType);
            if (filterPersonalSupervisorID.HasValue)
                cmd.Parameters.AddWithValue("@psID", filterPersonalSupervisorID.Value);
            if (filterMood.HasValue)
                cmd.Parameters.AddWithValue("@mood", filterMood.Value);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var user = new UserData
                {
                    ID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    UserType = reader.GetString(2),
                    Mood = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                    UnresolvedReportsCount = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                    OldestReportDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                    NextMeetingDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)
                };
                users.Add(user);
            }

            return users;
        }
    }

}
