using JDM_Casus_Blok4.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.DAL
{
    public class Dal
    {
        private static readonly Dal _instance = new Dal();
        public string connStr = "Data Source=LUCAS;Initial Catalog=JDMDatabase;Integrated Security=True;Encrypt=False;";

        private Dal()
        {
        }

        public static Dal Instance
        {
            get
            {
                return _instance;
            }
        }

        // Crud Create:

        public List<Assessment> GetAssessmentsById(int patientId)
        {
            string query = "SELECT * FROM Assessment WHERE PatientId = @PatientId;";

            List<Assessment> assessments = new List<Assessment>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PatientId", patientId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                System.Diagnostics.Debug.WriteLine(DateOnly.FromDateTime(reader.GetDateTime(1)));


                                Assessment assessment = new Assessment();
                                assessment.Id = (int)reader["Id"];
                                assessment.Date = DateOnly.FromDateTime((DateTime)reader["CompletionDate"]);
                                assessment.TotalScore = (int)reader["TotalScore"];
                                assessment.Validated = (bool)reader["Validated"];
                                assessment.PatientAge = (int)reader["PatientAge"];
                                assessment.PatientId = (int)reader["PatientId"];

                                assessment.Exercises = GetExercisesByAssessmentId(assessment.Id);

                                assessments.Add(assessment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting Assessments: " + ex.Message);
            }
            return assessments;

        }

        public void CreateAssessment(Assessment assessment)
        {
            string query = "INSERT INTO Assessment (CompletionDate, TotalScore, Validated, PatientAge, PatientId) " +
                           "OUTPUT INSERTED.Id " +
                           "VALUES (@CompletionDate, @TotalScore, @Validated, @PatientAge, @PatientId);";

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

                        int newId = (int)command.ExecuteScalar();
                        System.Diagnostics.Debug.WriteLine("INSERT ID");
                        System.Diagnostics.Debug.WriteLine(newId);

                        // weet niet of dit nodig is maar kan handig zijn
                        assessment.Id = newId;

                        CreateExercise(assessment.Exercises, newId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving Assessment: " + ex.Message);
            }
        }


        public void CreateExercise(List<Exercise> exercises, int assessmentId)
        {
            string query = "INSERT INTO Exercise (ExerciseNumber, Name, Score, MaxScore, AssessmentId) VALUES (@ExerciseNumber, @Name, @Score, @MaxScore, @AssessmentId);";

            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        foreach (Exercise exercise in exercises)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@ExerciseNumber", exercise.ExerciseNumber);
                            command.Parameters.AddWithValue("@Name", exercise.Name);
                            command.Parameters.AddWithValue("@Score", exercise.Score);
                            command.Parameters.AddWithValue("@MaxScore", exercise.MaxScore);
                            command.Parameters.AddWithValue("@AssessmentId", assessmentId);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving Exercise: " + ex.Message);
            }
        }

        public List<Exercise> GetExercisesByAssessmentId(int id) {
            string query = "SELECT * FROM Exercise WHERE AssessmentId = @AssessmentId;";

            List<Exercise> exercises = new List<Exercise>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AssessmentId", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Exercise exercise = new Exercise();
                                exercise.Id = (int)reader["Id"];
                                exercise.ExerciseNumber = (int)reader["ExerciseNumber"];
                                exercise.Name = (string)reader["Name"];
                                exercise.Score = (int)reader["Score"];
                                exercise.MaxScore = (int)reader["MaxScore"];

                                exercises.Add(exercise);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting Exercises: " + ex.Message);
            }
            return exercises;
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
