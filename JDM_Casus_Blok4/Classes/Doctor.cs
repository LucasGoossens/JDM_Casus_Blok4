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
        public List<Patient> Patients = new List<Patient>();

        public Doctor(int id, string firstname, string lastname) : base(id, firstname, lastname)
        {
            Patients = new List<Patient>();
        }

        public static Doctor GetDoctorById(int id)
        {
            DAL.Dal Dal = DAL.Dal.Instance;
            Doctor doctor = Dal.GetDoctorById(id);
            return doctor;
        }


        public void AddPatient(Patient patient)
        {
            Patients.Add(patient);
        }
    }
}
