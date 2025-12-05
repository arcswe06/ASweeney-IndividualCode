using ASweeney_IndividualCode.Backend.GlobalData;
using ASweeney_IndividualCode.Backend.UserFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.BackEnd.StartProcedure
{
    internal class DefaultUser
    {
        public static void Create()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Creating default user");
            Console.ResetColor();

            var connection = StoredVariables.Connection;
            connection.Open();

            // Check if user with ID 1 already exists
            using (var checkCmd = connection.CreateCommand())
            {
                checkCmd.CommandText = "SELECT COUNT(*) FROM Users WHERE ID = 1;";
                long exists = (long)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Default user already exists.");
                    Console.ResetColor();
                    return;
                }
            }

            // Create SeniorTutor
            var user = new SeniorTutor
            {
                ID = 1,
                Name = "Default",
                Password = "default123"
            };

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"
                INSERT INTO Users
                (ID, Name, Password, UserType, Mood, HasPersonalSupervisor)
                VALUES
                (@id, @name, @password, @type, @mood, @hasPS);
            ";

                cmd.Parameters.AddWithValue("@id", user.ID);
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@type", "SeniorTutor");
                cmd.Parameters.AddWithValue("@mood", DBNull.Value);       // Not used for SeniorTutor
                cmd.Parameters.AddWithValue("@hasPS", false);            // Not used for SeniorTutor

                cmd.ExecuteNonQuery();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Default SeniorTutor created with ID 1.");
            Console.ResetColor();
        }

    }
}
