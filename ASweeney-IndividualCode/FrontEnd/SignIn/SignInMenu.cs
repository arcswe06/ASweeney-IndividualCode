using ASweeney_IndividualCode.Backend.GlobalData;
using ASweeney_IndividualCode.Backend.UserFunctions;
using ASweeney_IndividualCode.BackEnd.UserFunctions.DisplayUsers;
using ASweeney_IndividualCode.FrontEnd.MainMenu;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.FrontEnd.SignIn
{
    public class SignInMenu
    {
        public static void SignIn()
        {
            Console.Clear();

            bool ValidSign = false;
            bool ValidID = false;
            bool ValidPassword = false;
            var connection = StoredVariables.Connection;

            GeneralDisplay.Display();

            while (true)
            {
                Console.Write("Enter your User ID: ");
                string idInput = Console.ReadLine();

                if (!int.TryParse(idInput, out int userID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid ID format. Must be a number.");
                    Console.ResetColor();
                    continue;
                }

                Console.Write("Enter your Password: ");
                string passwordInput = Console.ReadLine();

                using var cmd = new SqliteCommand("SELECT ID, Name, UserType, Mood FROM Users WHERE ID = @id AND Password = @password", connection);
                cmd.Parameters.AddWithValue("@id", userID);
                cmd.Parameters.AddWithValue("@password", passwordInput);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Valid user found
                    User signedInUser;

                    string userType = reader.GetString(2);

                    switch (userType)
                    {
                        case "Student":
                            signedInUser = new Student
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Password = passwordInput,
                                Mood = reader.IsDBNull(3) ? 5 : reader.GetInt32(3)
                            };
                            break;

                        case "PersonalSupervisor":
                            signedInUser = new PersonalSupervisor
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Password = passwordInput
                            };
                            break;

                        case "SeniorTutor":
                            signedInUser = new SeniorTutor
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Password = passwordInput
                            };
                            break;

                        default:
                            throw new Exception($"Unknown user type: {userType}");
                    }

                    StoredVariables.CurrentUser = signedInUser;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Sign-in successful. Welcome {signedInUser.Name}!");
                    Console.ResetColor();

                    StoredVariables.CurrentUser = signedInUser;

                    MainMenuConsoleMenu next = new MainMenuConsoleMenu();
                    next.Select();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid ID or password. Please try again.");
                    Console.ResetColor();
                }
                
            }
            

        }
    }
}
