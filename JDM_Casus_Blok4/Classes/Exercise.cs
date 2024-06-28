
namespace JDM_Casus_Blok4.Classes
{
    public class Exercise
    {
        public int Id { get; set; }
        public int ExerciseNumber { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public Feedback? Feedback { get; set; }
        public List<string> ResultOptions { get; set; }

        public Exercise(int id, int exerciseNumber, string name, int score, int maxScore, List<string> resultOptions)
        {
            Id = id;
            ExerciseNumber = exerciseNumber;
            Name = name;
            Score = score;
            MaxScore = maxScore;
            ResultOptions = resultOptions;
        }

        public Exercise(int exerciseNumber, string name, int score, int maxScore, List<string> resultOptions)
        {
            ExerciseNumber = exerciseNumber;
            Name = name;
            Score = score;
            MaxScore = maxScore;
            ResultOptions = resultOptions;
        }

        public Exercise(int exerciseNumber, string name, List<string> resultOptions)
        {
            ExerciseNumber = exerciseNumber;
            Name = name;
            ResultOptions = resultOptions;
        }


        public void ViewExercise()
        {
            Console.WriteLine($"Exercise number: {ExerciseNumber}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Score: {Score}");
            Console.WriteLine($"Max score: {MaxScore}");
            if (Feedback != null)
            {
                Console.WriteLine($"Feedback: {Feedback?.Message}");
            }
            Console.WriteLine();
        }
        public void ViewExerciseResearcher()
        {
            Console.WriteLine($"Exercise number: {ExerciseNumber}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Score: {Score}");
            Console.WriteLine($"Max score: {MaxScore}");
            Console.WriteLine();
            
        }   
    }
}