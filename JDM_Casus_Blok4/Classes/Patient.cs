using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    public class Patient : User
    {
        DateOnly DateOfBirth { get; set; }
        public List<Assessment> Assessments = new List<Assessment>();
        public Patient(int id, string userName, string email, string password) : base(id, userName, email, password)
        {
            
        }
    }
}
