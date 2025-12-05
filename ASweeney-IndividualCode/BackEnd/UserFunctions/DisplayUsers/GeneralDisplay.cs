using ASweeney_IndividualCode.Backend.GlobalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.BackEnd.UserFunctions.DisplayUsers
{
    internal class GeneralDisplay
    {
        public static void Display(string userType = "", bool showMood = false)
        {
            var connection = StoredVariables.Connection;

            // Define display order
            string[] order = { "SeniorTutor", "PersonalSupervisor", "Student" };

            foreach (var type in order)
            {
                // Skip types if a filter is provided
                if (!string.IsNullOrEmpty(userType) && !string.Equals(type, userType, StringComparison.OrdinalIgnoreCase))
                    continue;

                // Print heading in yellow
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(type.ToUpper().Replace("SENIORTUTOR", "SENIOR TUTORS")
                                               .Replace("PERSONALSUPERVISOR", "PERSONAL SUPERVISORS")
                                               .Replace("STUDENT", "STUDENTS"));
                Console.ResetColor();

                using var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                SELECT ID, Name, Mood
                FROM Users
                WHERE UserType = @type
                ORDER BY ID;
            ";
                cmd.Parameters.AddWithValue("@type", type);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string moodDisplay = "";

                    if (showMood && !reader.IsDBNull(2))
                        moodDisplay = $" ({reader.GetInt32(2)})";

                    Console.WriteLine($"{id,-10} {name}{moodDisplay}");
                }

                Console.WriteLine(); // extra spacing between groups
            }
        }
    }
}
