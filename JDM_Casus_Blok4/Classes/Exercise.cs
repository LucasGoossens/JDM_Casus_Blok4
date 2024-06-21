using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;

namespace JDM_Casus_Blok4.Classes
{
    public class Exercise
    {
        public int Id { get; set; }
        public int ExerciseNumber { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public Feedback Feedback { get; set; }


        public Exercise(int id, int exerciseNumber, string name, int score, int maxScore)
        {
            Id = id;
            ExerciseNumber = exerciseNumber;
            Name = name;
            Score = score;
            MaxScore = maxScore;
        }
        public Exercise(int exerciseNumber, string name, int score, int maxScore)
        {
            ExerciseNumber = exerciseNumber;
            Name = name;
            Score = score;
            MaxScore = maxScore;
        }
        public void ViewExercise()
        {
            Console.WriteLine($"Exercise number: {ExerciseNumber}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Score: {Score}");
            Console.WriteLine($"Max score: {MaxScore}");
            Console.WriteLine($"Feedback: {Feedback.Message}");
        }   
    }
}