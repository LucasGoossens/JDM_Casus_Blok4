﻿using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    public class Patient : User
    {
        public DateOnly DateOfBirth { get; set; }
        public List<Assessment> Assessments = new List<Assessment>();
        public int? AssessmentFrequency { get; set; }


        public Patient(int id, string firstname, string lastname, DateOnly dateOfBirth, int? assessmentFrequency) : base(id, firstname, lastname)
        {
            DateOfBirth = dateOfBirth;
            AssessmentFrequency = assessmentFrequency;
            Assessments = new List<Assessment>();
        }

        //public Patient(int id, string firstname, string lastname, List<Assessment> assessments) : base(id, firstname, lastname)
        //{
        //    Assessments = assessments;
        //}

        public static Patient GetPatient(int id)
        {
            // Get patient from database
            DAL.Dal Dal = DAL.Dal.Instance;
            Patient patient = Dal.GetPatient(id);
            return patient;
        }
    }
}