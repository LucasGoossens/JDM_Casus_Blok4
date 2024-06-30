using JDM_Casus_Blok4.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JDM_Casus_Blok4.DAL
{
    public class Dal
    {
        private static readonly Dal _instance = new Dal();
        public string connStr = "Server=tcp:casus-blok-4.database.windows.net,1433;Initial Catalog=JDMDatabase;Persist Security Info=False;User ID=tacoadmin;Password=rN6yPGff856Dq#Fj;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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

                                int assessmentId = (int)reader["Id"];
                                DateOnly assessmentDate = DateOnly.FromDateTime((DateTime)reader["CompletionDate"]);
                                int assessmentTotalScore = (int)reader["TotalScore"];
                                bool assessmentValidated = (bool)reader["Validated"];
                                int assessmentPatientAge = (int)reader["PatientAge"];
                                int assessmentPatientId = (int)reader["PatientId"];

                                List<Exercise> assessmentExercises = GetExercisesByAssessmentId(assessmentId);

                                Assessment newAssessment = new Assessment(assessmentId, assessmentExercises, assessmentDate, assessmentValidated, assessmentTotalScore, assessmentPatientAge, assessmentPatientId);

                                assessments.Add(newAssessment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return assessments;

        }

        public void CreateAssessment(Assessment assessment)
        {
            // Todo - patientid verwerken in de database wanneer een assessment wordt opgeslagen
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
                Console.ReadLine();
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

        public List<Exercise> GetExercisesByAssessmentId(int id)
        {
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
                                int exerciseId = (int)reader["Id"];
                                int exerciseExerciseNumber = (int)reader["ExerciseNumber"];
                                string exerciseName = (string)reader["Name"];
                                int exerciseScore = (int)reader["Score"];
                                int exerciseMaxScore = (int)reader["MaxScore"];

                                Exercise exercise = new Exercise(exerciseId, exerciseExerciseNumber, exerciseName, exerciseScore, exerciseMaxScore, new List<string>());
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



        public void CreateFeedback(Feedback feedback, string feedBackType, int assessmentId)
        {
            string query = "INSERT INTO [Feedback] (Message, ProviderId) VALUES (@Message, @ProviderId);";

            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Message", feedback.Message);
                        command.Parameters.AddWithValue("@ProviderId", feedback.ProviderId);

                        command.ExecuteNonQuery();
                    }

                    string query2 = $"UPDATE [{feedBackType}] SET FeedbackId = @FeedbackId WHERE Id = @AssessmentId;";

                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        command2.Parameters.AddWithValue("@FeedbackId", feedback.Id);
                        command2.Parameters.AddWithValue("@AssessmentId", assessmentId);
                        command2.ExecuteNonQuery();
                    }

                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error creating feedback: " + e.Message);
            }
        }

        // Crud Read:

        public List<Assessment> GetAllValidatedAssessments()
        {
            List<Assessment> assessments = new List<Assessment>();

            string assessmentQuery = "SELECT Id, CompletionDate, TotalScore, Validated, PatientAge, PatientId FROM Assessment WHERE Validated = 1;";
            string exerciseQuery = "SELECT Id, AssessmentId, ExerciseNumber, Name, Score, MaxScore FROM Exercise WHERE AssessmentId = @AssessmentId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(assessmentQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                DateOnly date = DateOnly.FromDateTime(reader.GetDateTime(1));
                                int totalScore = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                bool validated = reader.GetBoolean(3);
                                int patientAge = reader.GetInt32(4);
                                int patientId = reader.GetInt32(5);

                                Assessment assessment = new Assessment(id, new List<Exercise>(), date, validated, totalScore, patientAge, patientId);

                                using (SqlCommand exerciseCommand = new SqlCommand(exerciseQuery, connection))
                                {
                                    exerciseCommand.Parameters.AddWithValue("@AssessmentId", id);

                                    using (SqlDataReader exerciseReader = exerciseCommand.ExecuteReader())
                                    {
                                        while (exerciseReader.Read())
                                        {
                                            int exerciseId = exerciseReader.GetInt32(0);
                                            int exerciseNumber = exerciseReader.GetInt32(2);
                                            string name = exerciseReader.GetString(3);
                                            int score = exerciseReader.GetInt32(4);
                                            int maxScore = exerciseReader.GetInt32(5);

                                            Exercise exercise = new Exercise(exerciseId, exerciseNumber, name, score, maxScore, new List<string>());
                                            assessment.AddExercise(exercise);
                                        }
                                    }
                                }

                                assessments.Add(assessment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving Assessments: " + ex.StackTrace + ex.Message);
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
            Patient patient = null; 

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                string getPatientQuery = @"
            SELECT 
                [User].Id, [User].Firstname, [User].Lastname, [User].Dateofbirth, a.Id AS AssessmentId, a.CompletionDate, a.TotalScore, a.Validated, a.ValidatorId 
            FROM [User] 
            LEFT JOIN Assessment a ON [User].Id = a.PatientId 
            WHERE [User].Id = @patientId;";

                using (SqlCommand command = new SqlCommand(getPatientQuery, connection))
                {
                    command.Parameters.AddWithValue("@patientId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (patient == null)
                            {
                                int patientId = reader.GetInt32(0);
                                string patientFirstName = reader.GetString(1);
                                string patientLastName = reader.GetString(2);
                                string patientDateOfBirthString = reader["Dateofbirth"].ToString();
                                DateOnly patientDateOfBirth = DateOnly.Parse(patientDateOfBirthString);
                                int? patientAssessmentFrequency = null;
                                patient = new Patient(patientId, patientFirstName, patientLastName, patientDateOfBirth, patientAssessmentFrequency);
                                patient.Assessments = GetAssessmentsById(patientId);
                            }
                          
                        }
                    }
                }
            }

            return patient;
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
                                patient.Assessments = GetAssessmentsById(patientId);
                                parent.AddPatient(patient);
                            }
                        }
                    }


                    return parent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting Parent from the database: {ex}");
                return null;
            }
        }


        public PhysicalTherapist GetPhysiotherapist()

        {

            try
            {
                PhysicalTherapist? physicalTherapist = null;
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    string query = "SELECT * FROM [User] Where [Type] = 'therapist';";
                    using SqlCommand command = new SqlCommand(query, connection);
                    {
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string firstName = reader.GetString(1);
                                string lastName = reader.GetString(2);
                                physicalTherapist = new PhysicalTherapist(id, firstName, lastName);
                            }
                        }
                    }
                    string query2 = "SELECT * " +
                "FROM [User] " +
                "INNER JOIN [User2User] ON [User].Id = [User2User].UserOne " +
                "WHERE User2User.UserTwo = @therapistId";

                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        command2.Parameters.AddWithValue("@therapistId", physicalTherapist.Id);
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
                                physicalTherapist.AddPatient(patient);
                            }
                        }
                    }


                    return physicalTherapist;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting PhysicalTherapist from the database: {ex}");
                return null;
            }
        }
        

        public Doctor GetDoctorById(int id)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("GetDoctorById");
                Doctor? doctor = null;
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    // probleem hiermee is dat dit niet controleert of de Id die je meegeeft aan deze method wel een doctor type is
                    // maar is geen probleem zolang je alleen een id meegeeft waarvan je weet dat het een doctor is
                    string query = "SELECT * FROM [User] Where Id = @Id;";

                    using SqlCommand command = new SqlCommand(query, connection);
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using SqlDataReader reader = command.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                string firstName = reader.GetString(1);
                                string lastName = reader.GetString(2);
                                doctor = new Doctor(id, firstName, lastName);
                                System.Diagnostics.Debug.WriteLine(doctor.Firstname);
                            }
                        }
                    }
                    string query2 = "SELECT * " +
                "FROM [User] " +
                "INNER JOIN [User2User] ON [User].Id = [User2User].UserOne " +
                "WHERE User2User.UserTwo = @doctorId";

                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        command2.Parameters.AddWithValue("@doctorId", doctor.Id);
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
                                patient.Assessments = GetAssessmentsById(patientId);
                                doctor.AddPatient(patient);
                            }
                        }
                    }


                    return doctor;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting Doctor from the database: {ex.StackTrace}");
                return null;
            }
        }

        public Researcher GetResearcherById(int id)
        {
            // not comletly tested yet
            List<Assessment> assessments = GetAllValidatedAssessments();

            Researcher researcher = null;
            string query = "SELECT Id, Firstname, Lastname FROM [User] WHERE Id = @Id AND Type = 'Researcher'";
            string query2 = "SELECT AssessmentId FROM Assessment_Researcher WHERE ResearcherId = @Id";

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
                                researcher = new Researcher(id, FirstName, LastName);
                            }
                        }
                    }
                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        command2.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                int assessmentId = reader2.GetInt32(0);
                                Assessment assessment = assessments.Find(a => a.Id == assessmentId);
                                researcher.AddAssessment(assessment);
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching Researcher: " + ex.Message + ex.StackTrace);
                Console.ReadLine();
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
            string query = "UPDATE [User] SET Firstname = @Firstname, Lastname = @Lastname, Dateofbirth = @Dateofbirth, AssessmentFrequency = @AssessmentFrequency WHERE Id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Firstname", patient.Firstname);
                        command.Parameters.AddWithValue("@Lastname", patient.Lastname);
                        string databaseDateOfBirth = patient.DateOfBirth.ToString();
                        command.Parameters.AddWithValue("@Dateofbirth", databaseDateOfBirth);
                        if (patient.AssessmentFrequency == null)
                        {
                            command.Parameters.AddWithValue("@AssessmentFrequency", DBNull.Value);
                        }
                        else
                        { command.Parameters.AddWithValue("@AssessmentFrequency", patient.AssessmentFrequency); }
                        command.Parameters.AddWithValue("@Id", patient.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating Patient: " + ex.Message);
            }
        }

        public void UpdateFeedback()
        {
            // Update a feedback
        }

        // Crud Delete:


    }
}
