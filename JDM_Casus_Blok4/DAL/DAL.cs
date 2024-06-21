using JDM_Casus_Blok4.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_blok4
{
    public class DAL
    {
        private readonly string connectionString = "Server=tcp:casus-blok-4.database.windows.net,1433;Initial Catalog=JDMDatabase;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;";

        public List<Assessment> Assessments { get; set; } = new List<Assessment>();

        public Assessment CreateAssessment(Assessment assessment)
        {
            Console.WriteLine("Create assessment in DAL");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Assessments (Date, TotalScore, Validated, PatientAge) OUTPUT INSERTED.ID VALUES (@Date, @TotalScore, @Validated, @PatientAge)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Date", assessment.Date);
                command.Parameters.AddWithValue("@TotalScore", assessment.TotalScore);
                command.Parameters.AddWithValue("@Validated", assessment.Validated);
                

                connection.Open();
                int insertedId = (int)command.ExecuteScalar();
                assessment.Id = insertedId;
            }
            return assessment;
        }

        public Assessment GetAssessmentById(int id)
        {
            Assessment assessment = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Date, TotalScore, Validated FROM Assessments WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    assessment = new Assessment
                    {
                        Id = (int)reader["Id"],
                        Date = (DateTime)reader["Date"],
                        TotalScore = (int)reader["TotalScore"],
                        Validated = (bool)reader["Validated"],
                        
                    };
                }
            }
            return assessment;
        }

        public List<Assessment> GetAllAssessments()
        {
            List<Assessment> assessments = new List<Assessment>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Assessments";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Assessment assessment = new Assessment
                    {
                        Id = (int)reader["Id"],
                        Date = (DateTime)reader["Date"],
                        TotalScore = (int)reader["TotalScore"],
                        Validated = (bool)reader["Validated"],
                        
                    };
                    assessments.Add(assessment);
                }
            }
            return assessments;
        }

        public void UpdateAssessment(Assessment assessment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Assessments SET Date = @Date, TotalScore = @TotalScore, Validated = @Validated, PatientAge = @PatientAge WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Date", assessment.Date);
                command.Parameters.AddWithValue("@TotalScore", assessment.TotalScore);
                command.Parameters.AddWithValue("@Validated", assessment.Validated);
                command.Parameters.AddWithValue("@Id", assessment.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAssessment(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Assessments WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}