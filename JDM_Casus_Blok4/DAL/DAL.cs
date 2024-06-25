using JDM_Casus_Blok4.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JDM_Casus_Blok4.DAL
{
    public class Dal
    {
        private static readonly Dal _instance = new Dal();
        public string connStr = "Server=tcp:casus-blok-4.database.windows.net,1433;Initial Catalog=JDMDatabase;Persist Security Info=False;User ID=tacoadmin;Password=rN6yPGff856Dq#Fj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

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

        public List<Assessment> GetAssessments()
        {
            List<Assessment> assessments = new List<Assessment>();
            string query = "SELECT * FROM Assessment";

            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                List<Exercise> exercises = new List<Exercise>();
                                DateOnly date = DateOnly.FromDateTime(reader.GetDateTime(1));
                                bool Validated = reader.GetBoolean(3);
                                int TotalScore = reader.GetInt32(2);
                                int PatientAge = reader.GetInt32(4);
                                int PatientId = reader.GetInt32(5);

                                Assessment newAssessment = new Assessment(id, exercises, date, Validated, TotalScore, PatientAge, PatientId);
                   
                                assessments.Add(newAssessment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching Assessments: " + ex.Message);
            }

            return assessments;
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

        public Patient GetPatient(int id)
        {

            using SqlConnection connection = new(connStr);
            connection.Open();
            string getPatientQuery = "SELECT [User].Id, [User].Firstname, [User].Lastname, [User].Type, [User].Dateofbirth, a.id, a.CompletionDate, a.TotalScore, a.Validated, a.ValidatorId " +
                "FROM [User] " +
                "JOIN Assessment a ON [User].Id = a.PatientId " +
                "WHERE a.PatientId = @patientId;";

            using SqlCommand command = new(getPatientQuery, connection);
            command.Parameters.AddWithValue("@patientId", id);
            command.ExecuteNonQuery();

            using SqlDataReader reader = command.ExecuteReader();

            string firstname = "";
            string lastname = "";
            DateOnly dateOfBirth = new DateOnly();

            while (reader.Read())
            {
                if (firstname == "")
                {
                    firstname = reader[1].ToString();
                }
                if (lastname == "")
                {
                    lastname = reader[2].ToString();
                }
                if (dateOfBirth == new DateOnly())
                {
                    string dateString = reader[4].ToString();
                    DateOnly date = DateOnly.Parse(dateString);
                }
                Console.WriteLine(reader[0]);
                Console.WriteLine(reader[1]);
                Console.WriteLine(reader[2]);
                Console.WriteLine(reader[3]);
                Console.WriteLine(reader[4]);
                Console.WriteLine("Assessment id");
                Console.WriteLine(reader[5]);
                int assessmentId = (int)reader[5];
                Console.WriteLine("Assessment date");
                Console.WriteLine(reader[6]);
                //string assessmentDateString = reader[4].ToString();
                //DateOnly assessmentDate = DateOnly.Parse(assessmentDateString);

                //Assessment assessment;
                //if (assessmentDate == null)
                //{
                //    Console.WriteLine("Assessmentdate is null");
                //    assessment = new Assessment(assessmentId);
                //}
                //else
                //{
                //    Console.WriteLine("Assessmentdate has date");
                //    //assessment = new Assessment(assessmentId, assessmentDate);
                //}
            }

            // To do - assessment frequency
            return new Patient(id, firstname, lastname, dateOfBirth, 2);
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

        public Researcher GetResearcherById(int id)
        {
            Researcher researcher = null;
            string query = "SELECT Id, UserName, Email, Password FROM Users WHERE Id = @Id AND UserType = 'Researcher'";

            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                int Id = reader.GetInt32(0);
                                string FirstName = reader.GetString(1);
                                string LastName = reader.GetString(2);

                                Researcher testresearcher = new Researcher(id, FirstName, LastName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching Researcher: " + ex.Message);
            }

            return researcher;
        }


        // Crud Update:

        public void UpdateAssessment(Assessment assessment)
        {
            string query = "UPDATE Assessment SET CompletionDate = @CompletionDate, TotalScore = @TotalScore, Validated = @Validated, PatientAge = @PatientAge, PatientId = @PatientId WHERE Id = @Id";

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
                        command.Parameters.AddWithValue("@Id", assessment.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating Assessment: " + ex.Message);
            }
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
