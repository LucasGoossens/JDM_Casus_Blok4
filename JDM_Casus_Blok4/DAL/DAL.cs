using JDM_Casus_Blok4.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.DAL
{
    public class Dal
    {
        private static readonly Dal _instance = new Dal();
        private Dal()
        {}
        public static Dal Instance
        {
            get
            {
                return _instance;
            }
        }

        List<Assessment> assessments = new List<Assessment>();
        List<Exercise> exercises = new List<Exercise>();
        List<Feedback> feedbacks = new List<Feedback>();

        // Crud Create:

        public void CreateAssessment()
        {
            
        }

        public void CreateExercise()
        {
            // Create a new exercise
        }

        public void CreateFeedback()
        {
            // Create a new feedback
        }

        // Crud Read:

        public void GetAssessments()
        {
            // Read exercises
        }

        public void GetExercises()
        {
            // Read exercises
        }

        public void GetFeedback()
        {
            // Read feedback
        }

        public void GetPatients()
        {
            // Read patients
        }

        public void GetParent()
        {
            // Read parent
        }

        public void GetPhysiotherapist()
        {
            // Read physiotherapist
        }

        public void GetDoctor()
        {
            // Read doctor
        }

        public void GetRearcher()
        {
            // Read rearcher
        }

        // Crud Update:

        public void UpdateAssessment()
        {
            // Update an assessment
        }

        public void UpdateExercise()
        {
            // Update an exercise
        }

        public void UpdatePatient(Patient patient)
        {
            // Update a patient
        }

        public void UpdateFeedback()
        {
            // Update a feedback
        }

        // Crud Delete:


    }
}
