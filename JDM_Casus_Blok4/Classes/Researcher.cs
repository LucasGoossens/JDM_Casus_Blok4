using JDM_Casus_Blok4.DAL;
using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;

namespace JDM_Casus_Blok4.Classes
{
    public class Researcher : User
    {
        public List<Assessment> Assessments { get; private set; }

        private Dal dal;

        public Researcher() : base()
        {
            InitializeDal();
            InitializeAssessments();
        }

        public Researcher(int id, string userName, string email, string password)
            : base(id, userName, email, password)
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
    }
}