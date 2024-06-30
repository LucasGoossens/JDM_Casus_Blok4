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
            Assessments = new List<Assessment>();
        }


        public void AddAssessment(Assessment assessment) 
        {
            Assessments.Add(assessment);  // Add to local list
        }



        // New method to get a Researcher by ID
        public static Researcher GetResearcherById(int id)
        {
            Dal dal = Dal.Instance;
            return dal.GetResearcherById(id);
        }
    }
}