using JDM_Casus_Blok4.Interfaces;
using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    public class Doctor : User, IProvider
    {
        public List<Patient> Patients = new List<Patient>();
        public List<Parent> PatientParents { get; set; }
        public void ViewProgression() { }
        public void ValidateAssessment() { }
        public void ViewAssessments() { }
        public void DefineFrequency() { }
        public void CreatePatientAccount() { }
        public void CreateParentAccount(Patient patient) { }

        public Doctor(int id, string firstname, string lastname) : base(id, firstname, lastname)
        {
            Patients = new List<Patient>();
            PatientParents = new List<Parent>();
        }
    }
}
