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
        public int AssessmentFrequentie { get; set; }
        public List<Assessment> Assessments = new List<Assessment>();
        public Patient(int id, string firstName, string lastName, string password) : base(id, firstName, lastName, password)
        {
            Assessments = new List<Assessment>();
            AssessmentFrequentie = 0;
        }

        public void GetAssessments()
        {
            DAL.Dal Dal = DAL.Dal.Instance;
            Assessments = Dal.GetAssessmentsById(Id);
        }

        public void EditAssessmentFrequentie(int assessmentFrequentie)
        {
            AssessmentFrequentie = assessmentFrequentie;
            DAL.Dal Dal = DAL.Dal.Instance;
            Dal.UpdatePatient(this);
        }
    }
}
