using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.UserClasses
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public User(int id, string firstname, string lastname)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
        }
    }
}
