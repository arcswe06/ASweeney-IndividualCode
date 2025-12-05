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
            // 2. Generate ID: YEAR + increment
            // Example: 2025001
            // ------------------------
            int year = DateTime.Now.Year;

            int currentCount = 0;
            using (var countCmd = new SqliteCommand(
                "SELECT COUNT(*) FROM Users WHERE ID LIKE @prefix",
                connection))
            {
                countCmd.Parameters.AddWithValue("@prefix", year + "%");
                currentCount = Convert.ToInt32(countCmd.ExecuteScalar());
            }

            // Increment by 1
            int newNumber = currentCount + 1;

            // Build final ID: YEAR + 3-digit padded number
            int newID = int.Parse(year + newNumber.ToString("000"));

            // ------------------------
            // 3. Determine UserType
            // ------------------------
            string userType = user switch
            {
                Student => "Student",
                PersonalSupervisor => "PersonalSupervisor",
                SeniorTutor => "SeniorTutor",
            };

            // ------------------------
            // 4. Additional fields
            // ------------------------
            int? moodValue = null;
            bool hasPS = false;

            if (user is Student student)
            {
                moodValue = student.Mood;
                hasPS = false; // student does not have a PS until linked later
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
            // 6. Update user object
            // ------------------------
            user.ID = newID;
            user.Password = password;
        }
    }
}
