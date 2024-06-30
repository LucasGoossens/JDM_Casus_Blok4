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
        public List<Parent> PatientParents { get; set; }


        public Doctor(int id, string firstname, string lastname) : base(id, firstname, lastname)
        {
            Patients = new List<Patient>();
            PatientParents = new List<Parent>();
        }

        public static Doctor GetDoctorById(int id)
        {
            DAL.Dal Dal = DAL.Dal.Instance;
            Doctor doctor = Dal.GetDoctorById(id);
            return doctor;
        }
        public static List<Doctor> GetAllDoctors()
        {
            DAL.Dal Dal = DAL.Dal.Instance;
            List<Doctor> allDoctors = Dal.GetAllDoctors();
            return allDoctors;
        }

        public void AddPatient(Patient patient)
        {
            Patients.Add(patient);
        }
    }
}
