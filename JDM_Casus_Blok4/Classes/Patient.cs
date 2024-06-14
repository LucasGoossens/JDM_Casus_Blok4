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
        public string Name { get; set; }
        public List<string> Assessments { get; set; }

        public Patient(string name)
        {
            Name = name;
            Assessments = new List<string>();
        }
    }
}
