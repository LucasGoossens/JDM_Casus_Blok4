using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    public class Researcher : User
    {
        public List<Patient> AllPatients { get; set; }

        public Researcher() : base()
        {
            AllPatients = new List<Patient>();
        }

        public void ViewAssessments()
        {
            Console.Clear();
            Console.WriteLine("Assessments:");
            foreach (var patient in AllPatients)
            {
                Console.WriteLine($"Patient: {patient.Name}");
                foreach (var assessment in patient.Assessments)
                {
                    Console.WriteLine($"- {assessment}");
                }
            }
            Console.WriteLine("");
        }
    }
}