using JDM_Casus_Blok4.DAL;
using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;

namespace JDM_Casus_Blok4.Classes
{
    public class Researcher : User
    {
        public List<Assessment> Assessments { get; private set; }

        public Researcher(int id, string firstname, string lastname) : base(id, firstname, lastname)
        {
            InitializeDal();
            InitializeAssessments();
        }

        public Researcher(int id, string firstName, string lastName, string password)
            : base(id, firstName, lastName, password)
        {
            InitializeDal();
            InitializeAssessments();
        }

        private void InitializeDal()
        {
            dal = Dal.Instance;
        }

        private void InitializeAssessments()
        {
            Assessments = dal.GetAssessments();
        }

        public void CreateAssessment(Assessment assessment)
        {
            dal.CreateAssessment(assessment);
            Assessments.Add(assessment);  // Add to local list
        }

        public void UpdateAssessment(Assessment assessment)
        {
            dal.UpdateAssessment(assessment);
            // Optionally update the local list as well, if necessary
        }

        // New method to get a Researcher by ID
        public Researcher GetResearcherById(int id)
        {
            return dal.GetResearcherById(id);
        }
    }
}