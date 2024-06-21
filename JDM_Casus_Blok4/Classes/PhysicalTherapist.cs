using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    public class PhysicalTherapist : User, Interfaces.IProvider
    {
        public List<Patient> Patients { get; set; }

        public void ValidateAssessment()
        {
            Patients = new List<Patient>();
        }

        public void EnterAssessment()
        {

        }

        public void CreateAssessment()
        {

        }
    }
}
