using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    public class Assessment
    {
        public int Id { get; set; }
        public List<Exercise> Exercises { get; set; }
        public DateOnly Date { get; set; }
        public bool Validated { get; set; }
        public int? TotalScore { get; set; }
        public int ValidatorId { get; set; }
        public int PatientAge { get; set; }
        public int PatientId { get; set; }
        public Feedback Feedback { get; set; }

        public Assessment(int id, List<Exercise> exercises, DateOnly date, bool validated, int totalScore, int patientAge, int patientId)
        {
            Id = id;
            Exercises = exercises;
            Date = date;
            Validated = validated;
            TotalScore = totalScore;
            PatientAge = patientAge;
            PatientId = patientId;
        }

        public void AddExercise(Exercise exercise)
        {
            Exercises.Add(exercise);
        }
        public void CalculatingScore()
        {
            TotalScore = 0;
            foreach (Exercise exercise in Exercises)
            {
                TotalScore += exercise.Score;
            }

        }
        public void ValidateAssessment(User user)
        {
            Validated = true;
            ValidatorId = user.Id;
            // add reference to database
        }

        public void ViewAssessment()
        {
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Total score: {TotalScore}");
            Console.WriteLine($"Validated: {Validated}");
            if (Feedback != null)
            {
                Console.WriteLine($"Feedback: {Feedback.Message}");
            }
            Console.WriteLine("Exercises:");
            foreach (Exercise exercise in Exercises)
            {
                exercise.ViewExercise();
            }
        }

        public void SaveAssessment()
        {
            DAL.Dal Dal = DAL.Dal.Instance;
            Dal.CreateAssessment(this);
        }

        public static List<Assessment> GetAllAssessments()
        {
            List<Assessment> assessments = new List<Assessment>();
            // get assessments from database
            return assessments;
        }

        public void ViewAssessmentResearcher()
        {

            Console.WriteLine($"Total score: {TotalScore}");
            Console.WriteLine($"Validated: {Validated}");
            Console.WriteLine("Exercises:");
            foreach (Exercise exercise in Exercises)
            {
                exercise.ViewExerciseResearcher();
            }
        }

    }
}
