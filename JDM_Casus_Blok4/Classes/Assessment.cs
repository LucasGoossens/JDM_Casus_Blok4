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

        // from database
        public Assessment(int id, List<Exercise> exercises, DateOnly date, bool validated, int totalScore)
        {
            Id = id;
            Exercises = exercises;
            Date = date;
            Validated = validated;
            TotalScore = totalScore;
        }
        // voor invoer van gebruiker 
        public Assessment(bool validated)
        {
            Exercises = new List<Exercise>();
            // invoer kind zal false zijn, van fysio true
            Validated = validated;
            Date = DateOnly.FromDateTime(DateTime.Now);
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
        public void MakeValidated(User user)
        {
            Validated = true;
            ValidatorId = user.Id;
            // add reference to database
        }

        public void VieuwAssessment()
        {
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Total score: {TotalScore}");
            Console.WriteLine($"Validated: {Validated}");
            Console.WriteLine($"Feedback: {Feedback.Message}");
            Console.WriteLine("Exercises:");
            foreach (Exercise exercise in Exercises)
            {
                exercise.ViewExercise();
            }
        }

        public static List<Assessment> GetAllAssessments()
        {
            List<Assessment> assessments = new List<Assessment>();
            // get assessments from database
            return assessments;
        }
    }
}
