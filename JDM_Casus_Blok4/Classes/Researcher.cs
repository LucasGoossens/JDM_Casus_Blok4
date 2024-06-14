﻿using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    public class Researcher : User
    {
        public List<Patient> AllPatient { get; set; }

        public Researcher() : base()
        {
            AllPatient = new List<Patient>();
        }

        public void ViewAssessments()
        {
            Console.Clear();
            Console.WriteLine("Assessments:");
            foreach (Patient patient in AllPatient)
            {
                Console.WriteLine($"Patient: {patient.UserName}");
                foreach (var assessment in patient.Assessments)
                {
                    Console.WriteLine($"- {assessment}");
                }
            }
            Console.WriteLine("");
        }
    }
}