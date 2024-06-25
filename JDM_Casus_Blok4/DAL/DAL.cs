using JDM_Casus_Blok4.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.DAL
{
    public sealed class Dal
    {
        private static Dal _instance = new Dal();
        public string connStr = "Data Source=LUCAS;Initial Catalog=JDMDatabase;Integrated Security=True;Encrypt=False;";

        private Dal()
        {
        }

        public static Dal Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Dal();
                }
                return _instance;
            }
        }

        // Crud Create:
        public void CreateAssessment(Assessment assessment)
        {
            string query = "INSERT INTO Assessment (CompletionDate, TotalScore, Validated, PatientAge, PatientId) VALUES (@CompletionDate, @TotalScore, @Validated, @PatientAge, @PatientId);";

            DateTime completionDateTime = assessment.Date.ToDateTime(TimeOnly.MinValue);

            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CompletionDate", completionDateTime);
                        command.Parameters.AddWithValue("@TotalScore", assessment.TotalScore);
                        command.Parameters.AddWithValue("@Validated", assessment.Validated);
                        command.Parameters.AddWithValue("@PatientAge", assessment.PatientAge);
                        command.Parameters.AddWithValue("@PatientId", assessment.PatientId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving Assessment: " + ex.Message);
            }
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

        public void GetResearcher()
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
