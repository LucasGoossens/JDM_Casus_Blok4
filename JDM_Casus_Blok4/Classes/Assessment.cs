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
        public Feedback? Feedback { get; set; }

        // constructor zonder id, id wordt pas aangemaakt zodra het in db wordt opgeslagen. 
        public Assessment(DateOnly date, bool validated, int patientId)
        {            
            Exercises = new List<Exercise>();
            TotalScore = 0;
            Date = date;
            Validated = validated;
            PatientId = patientId;
            
        }

        public Assessment(int id, DateOnly date, bool validated, int totalScore)
        {
            Id = id;
            Exercises = new List<Exercise>();
            Date = date;
            Validated = validated;
            TotalScore = totalScore;
        }

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
            TotalScore += exercise.Score;
        }

        public void ValidateAssessment(User user)
        {
            Validated = true;
            ValidatorId = user.Id;
            DAL.Dal Dal = DAL.Dal.Instance;
            Dal.UpdateAssessment(this);
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

        public static List<Assessment> GetAllValidatedAssessments()
        {
            DAL.Dal Dal = DAL.Dal.Instance;
            List<Assessment> assessments = Dal.GetAllValidatedAssessments();
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
