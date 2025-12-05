using ASweeney_IndividualCode.Backend.GlobalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.Backend.UserFunctions
{
    internal class DeleteUser
    {
        public static void Delete(int id)
        {
            var connection = StoredVariables.Connection;
            connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Users WHERE ID = @id;";
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();
            }
        }

    }
}
