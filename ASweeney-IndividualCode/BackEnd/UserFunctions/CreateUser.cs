using ASweeney_IndividualCode.Backend.GlobalData;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.Backend.UserFunctions
{
    internal class CreateUser
    {
        public static void Create(User user)
        {
            var connection = StoredVariables.Connection;
            connection.Open();

            // ------------------------
            // 1. Generate Password
            // ------------------------
            string lastName = user.Name.Trim().Split(' ').Last().ToLower();
            string password = lastName + "123";

            // ------------------------
            // 2. Generate ID safe for deletions
            // ------------------------
            int year = DateTime.Now.Year;
            int lastNumber = 0;

            using (var idCmd = new SqliteCommand(
                @"SELECT MAX(ID) 
              FROM Users 
              WHERE ID BETWEEN @min AND @max", connection))
            {
                idCmd.Parameters.AddWithValue("@min", year * 1000);
                idCmd.Parameters.AddWithValue("@max", (year * 1000) + 999);

                object result = idCmd.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    int lastID = Convert.ToInt32(result);
                    lastNumber = lastID % 1000; // Extract last 3 digits
                }
            }

            int newID = (year * 1000) + (lastNumber + 1);

            // ------------------------
            // 3. Determine UserType
            // ------------------------
            string userType = user switch
            {
                Student => "Student",
                PersonalSupervisor => "PersonalSupervisor",
                SeniorTutor => "SeniorTutor",
                _ => throw new Exception("Unknown user type")
            };

            // ------------------------
            // 4. Additional fields
            // ------------------------
            int? moodValue = null;
            bool hasPS = false;

            if (user is Student student)
            {
                moodValue = student.Mood;
                hasPS = false;
            }

            // ------------------------
            // 5. Insert into Users table
            // ------------------------
            using (var insertCmd = new SqliteCommand(@"
        INSERT INTO Users 
        (ID, Name, Password, UserType, Mood, HasPersonalSupervisor)
        VALUES 
        (@id, @name, @password, @type, @mood, @hasPS);
    ", connection))
            {
                insertCmd.Parameters.AddWithValue("@id", newID);
                insertCmd.Parameters.AddWithValue("@name", user.Name);
                insertCmd.Parameters.AddWithValue("@password", password);
                insertCmd.Parameters.AddWithValue("@type", userType);

                if (moodValue.HasValue)
                    insertCmd.Parameters.AddWithValue("@mood", moodValue.Value);
                else
                    insertCmd.Parameters.AddWithValue("@mood", DBNull.Value);

                insertCmd.Parameters.AddWithValue("@hasPS", hasPS);

                insertCmd.ExecuteNonQuery();
            }

            // ------------------------
            // 6. Update object
            // ------------------------
            user.ID = newID;
            user.Password = password;
        }
    }
}
