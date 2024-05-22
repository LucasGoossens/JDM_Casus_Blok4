using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.UserClasses
{ 
    public abstract class User
    {
        public string Id{ get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // lege constructor zodat 'base:()' werkt voor nu
        protected User()
        {
            
        }
        public User(string userName, string email, string password)
        {         
            UserName = userName;
            Email = email;
            Password = password;
        }

        public User(string id, string userName, string email, string password)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Password = password;
        }
    }
}
