using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    internal class Parent : User

    {
        public List<Child> Children { get; set; }

        // Constructor zonder parameters
        public Parent()
        {
            Children = new List<Child>();
        }

        // Constructor met parameters
        public Parent(string id, string userName, string email, string password, List<Child> children)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Password = password;
            Children = children ?? new List<Child>();
        }
    }
}
