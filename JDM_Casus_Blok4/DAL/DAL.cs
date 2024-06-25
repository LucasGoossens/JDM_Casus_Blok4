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

        public Parent GetParent()
        {

            try
            {
                Parent? parent = null;
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    string query = "SELECT * FROM [User] Where [Type] = 'parent';";
                    using SqlCommand command = new SqlCommand(query, connection);
                    {
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string firstName = reader.GetString(1);
                                string lastName = reader.GetString(2);
                                parent = new Parent(id, firstName, lastName);
                            }
                        }
                    }
                    string query2 = "SELECT * " +
                "FROM [User] " +
                "INNER JOIN [User2User] ON [User].Id = [User2User].UserOne " +
                "WHERE User2User.UserTwo = @ParentId";

                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        command2.Parameters.AddWithValue("@ParentId", parent.Id);
                        using (SqlDataReader reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                int patientId = reader2.GetInt32(0);
                                string patientFirstName = reader2.GetString(1);
                                string patientLastName = reader2.GetString(2);
                                string patientDateOfBirthString = reader2.GetString(4);
                                DateOnly patientDateOfBirth = DateOnly.Parse(patientDateOfBirthString);
                                int? patientAssessmentFrequency = null;
                                if (!reader2.IsDBNull(5))
                                {
                                    patientAssessmentFrequency = reader2.GetInt32(5);
                                }
                                Patient patient = new Patient(patientId, patientFirstName, patientLastName, patientDateOfBirth, patientAssessmentFrequency);
                                parent.Patients.Add(patient);
                            }
                        }
                    }
                    

                    return parent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting Parent from database: {ex}");
                return null;
            }
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
