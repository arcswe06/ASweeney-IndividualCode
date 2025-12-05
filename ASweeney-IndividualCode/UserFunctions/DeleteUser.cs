using IndividualCode.GlobalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.UserFunctions
{
    internal class DeleteUser
    {
        public static void Delete(int id)
        {
            var connection = StoredVariables.Connection;

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Users WHERE ID = @id;";
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();
            }
        }

    }
}
