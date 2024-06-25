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
        public List<Patient> Patients { get; set; }


        public Parent(int id, string firstname, string lastname) : base(id, firstname, lastname)
        {
            Patients = new List<Patient>();
        }

        public Parent(int id, string firstname, string lastname, List<Patient> patients) : base(id, firstname, lastname)
        {
            Patients = patients;
        }

        public static Parent GetParent()
        {
            DAL.Dal Dal = DAL.Dal.Instance;
            Parent parent = Dal.GetParent();
            return parent;
        }
        public void AddPatient(Patient patient)
        {
            Patients.Add(patient);
        }
    }
}
