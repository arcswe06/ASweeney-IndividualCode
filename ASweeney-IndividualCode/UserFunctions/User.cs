using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.UserFunctions
{
    //Use hierarchy for creating users. All types of users share the below values.
    public abstract class User
    {
        public int ID { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class PersonalSupervisor : User
    {
    }

    //Senior tutors and PS have no other values
    public class SeniorTutor : User
    {
    }

    //Mood and the rest is stored in different databases
    public class Student : User
    {
        public int Mood { get; set; } = 5;
    }
}
