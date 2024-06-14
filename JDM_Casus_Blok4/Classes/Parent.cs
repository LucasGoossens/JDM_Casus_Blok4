using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    public class Parent : User

    {
        public List<Patient> Patient { get; set; }

        // Constructor zonder parameters
        public Parent()
        {
            Patient = new List<Patient>();
        }

        // Constructor met parameters
        public Parent(int id, string userName, string email, string password, List<Patient> patient)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Password = password;
            Patient = patient ?? new List<Patient>();
        }
    }
}
