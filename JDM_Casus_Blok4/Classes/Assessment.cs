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
        public DateTime Date { get; set; }
        public bool Validated { get; set; }
        public int? TotalScore { get; set; }

        // from database
        public Assessment(int id, List<Exercise> exercises, DateTime date, bool validated, int totalScore)
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
            Date = DateTime.Now;
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
        public void MakeValidated()
        {
            Validated = true;
        }

    }
}
