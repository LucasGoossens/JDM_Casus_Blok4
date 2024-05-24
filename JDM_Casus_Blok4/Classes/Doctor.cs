using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    public class Doctor : User
    {
        public List<Child> Patients { get; set; }
        public List<Parent> PatientParents { get; set; }

        public void ViewProgression() { }
        public void ValidateAssessment() { }
        public void ViewAssessments() { }
        public void DefineFrequency() { }
        public void CreateChildAccount() { }
        public void CreateParentAccount(Child child) { }
    }
}
