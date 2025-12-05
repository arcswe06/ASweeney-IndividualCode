using IndividualCode.GlobalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.UserFunctions
{
    internal class LoadUser
    {
        public static User Load(int id)
        {
            var connection = StoredVariables.Connection;

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"
                SELECT 
                    ID, Name, Password, UserType, Mood, HasPersonalSupervisor
                FROM Users 
                WHERE ID = @id;
            ";

                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        return null; // no user found

                    int userId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string password = reader.GetString(2);
                    string userType = reader.GetString(3);

                    // Mood and HasPS may be null for non-students
                    object moodObj = reader.GetValue(4);
                    object hasPSObj = reader.GetValue(5);

                    User user;

                    switch (userType)
                    {
                        case "Student":
                            var student = new Student();
                            if (moodObj != DBNull.Value)
                                student.Mood = Convert.ToInt32(moodObj);

                            user = student;
                            break;

                        case "PersonalSupervisor":
                            user = new PersonalSupervisor();
                            break;

                        case "SeniorTutor":
                            user = new SeniorTutor();
                            break;

                        default:
                            throw new Exception("Unknown user type in database.");
                    }

                    // Shared properties
                    user.ID = userId;
                    user.Name = name;
                    user.Password = password;

                    return user;
                }
            }
        }
    }
}
