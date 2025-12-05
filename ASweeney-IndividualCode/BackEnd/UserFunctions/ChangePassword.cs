using ASweeney_IndividualCode.Backend.GlobalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.Backend.UserFunctions
{
    internal class ChangePassword
    {
        public static void Change(int userId, string newPassword, User userObject = null)
        {
            var connection = StoredVariables.Connection;
            connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE Users SET Password = @password WHERE ID = @id;";
                cmd.Parameters.AddWithValue("@password", newPassword);
                cmd.Parameters.AddWithValue("@id", userId);

                int rowsAffected = cmd.ExecuteNonQuery();

                Console.WriteLine($"Password for user ID {userId} updated successfully.");

                // Update the user object if provided
                if (userObject != null)
                {
                    userObject.Password = newPassword;
                }

            }
        }
    }
}
