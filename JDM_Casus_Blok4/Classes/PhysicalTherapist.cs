using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    public class PhysicalTherapist : User
    {
        public List<Patient> Patients { get; set; }


        public PhysicalTherapist(int id, string firstname, string lastname) : base(id, firstname, lastname)
        {
            Patients = new List<Patient>();
        }
        public PhysicalTherapist(int id, string firstname, string lastname, List<Patient> patients) : base(id, firstname, lastname)
        {
            Patients = patients;
        }


        public void AddPatient(Patient patient)
        {
            Patients.Add(patient);
        }
        public static PhysicalTherapist GetPhysicalTherapist()
        {
            DAL.Dal Dal = DAL.Dal.Instance;
            PhysicalTherapist physicalTherapist = Dal.GetPhysiotherapist();
            return physicalTherapist;

        }
    }
}
