using JDM_Casus_Blok4.UserClasses;
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


        public void GetAssessments()
        {
            DAL.Dal Dal = DAL.Dal.Instance;
            Assessments = Dal.GetAssessmentsById(Id);
        }

        public void EditAssessmentFrequency(int assessmentFrequentie) 
        {
            AssessmentFrequency = assessmentFrequentie;
            DAL.Dal Dal = DAL.Dal.Instance;
            Dal.UpdatePatient(this);
        }



        public static Patient GetPatient(int id)
        {
            // Get patient from database
            DAL.Dal Dal = DAL.Dal.Instance;
            Patient patient = Dal.GetPatient(id);
            return patient;
        }

        public string GetProgression()
        {
            string progressionString = "";
            foreach (Assessment assessment in Assessments)
            {
                if (assessment.Validated)
                {
                    progressionString += $"{assessment.TotalScore} + ";
                }
            }
            return progressionString;
        }

    }
}