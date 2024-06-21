
using JDM_Casus_Blok4.Classes;
using JDM_Casus_Blok4.UserClasses;
using System.Data;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    static public List<Patient> testPatients = new List<Patient>();
    static public Doctor testDoctor = new Doctor();
    static void Main(string[] args)
    {
        Console.WriteLine("Hello world!");

        // loop om test/mock/dummy objects toe te voegen 
        for (int i = 0; i < 3; i++)
        {
            List<Exercise> testExerciseList = new List<Exercise>();
            for (int j = 0; j < 3; j++)
            {
                Exercise testExercise = new Exercise(i + j, "test", 80, 100);
                testExerciseList.Add(testExercise);
            }
            DateOnly testDate = DateOnly.FromDateTime(DateTime.Now);

            Assessment testAssessment = new Assessment(i, testExerciseList, testDate, false, 100);
            Patient newPatient = new Patient(i, $"testname{i}", "testmail", "testpassword");
            testPatients.Add(newPatient);
            newPatient.Assessments.Add(testAssessment);
            testDoctor.Patients.Add(newPatient);
        }

        bool flag = true;
        while (flag)
        {
            Console.WriteLine("");
            MainMenu();

            flag = false;
        }
    }




    public static void MainMenu()
    {
        List<string> options = new List<string> {
            "Patient",
            "Parent",
            "Doctor",
            "Physical therapist",
            "Researcher",
        };

        int choice = DisplayMenuOptions(options, "Main menu - select the type of person you want to login with");

        switch (choice)
        {
            case 1:
                PatientMenu();
                break;
            case 2:
                Parent parent = Parent.GetParent();
                ParentMenu(parent);
                break;
            case 3:
                DoctorMenu(testDoctor);
                break;
            case 4:
            //PhysicalTherapistMenu();
            case 5:
                //ResearcherMenu();
                break;
        }
    }


    public static void PatientMenu()
    {
        List<string> options = new List<string> {
            "Enter assessment",
            "View progression",
        };

        int choice = DisplayMenuOptions(options, "Patient menu - press '0' to choose another login");

        switch (choice)
        {
            case 0:
                MainMenu();
                break;
            case 1:
                EnterAssessment();
                break;
            case 2:
                PatientViewProgression();
                break;
        }
    }

    public static void EnterAssessment()
    {
        Console.Clear();
        Console.WriteLine("Exercise 1: Head elevation");
        Console.WriteLine("");
        Console.WriteLine("0 = unable");
        Console.WriteLine("1 = 1-9 seconds");
        Console.WriteLine("2 = 10-29 seconds");
        Console.WriteLine("3 = 30-59 seconds");
        Console.WriteLine("4 = 60-119 seconds");
        Console.WriteLine("5 = >2 minutes");
        Console.WriteLine("");
        int exercise1Score = Convert.ToInt32(Console.ReadLine());
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        ClearCurrentConsoleLine();
        Console.WriteLine($"Score entered {exercise1Score}");
        Console.WriteLine("");
        Console.WriteLine("----------------");
        Console.WriteLine("");

        Console.WriteLine("Exercise 2: Leg raise");
        Console.WriteLine("");
        Console.WriteLine("0 = unable to lift leg off table");
        Console.WriteLine("1 = able to clear table but cannot touch object");
        Console.WriteLine("2 = able to lift leg high enough to touch object");
        Console.WriteLine("");
        int exercise2Score = Convert.ToInt32(Console.ReadLine());
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        ClearCurrentConsoleLine();
        Console.WriteLine($"Score entered {exercise2Score}");
        Console.WriteLine("");
        Console.WriteLine("----------------");
        Console.WriteLine("");


        Console.WriteLine("Exercise 3: Straight leg lift");
        Console.WriteLine("");
        Console.WriteLine("0 = unable");
        Console.WriteLine("1 = 1-9 seconds");
        Console.WriteLine("2 = 10-29 seconds");
        Console.WriteLine("3 = 30-59 seconds");
        Console.WriteLine("4 = 60-119 seconds");
        Console.WriteLine("5 = >2 minutes");
        Console.WriteLine("");
        int exercise3Score = Convert.ToInt32(Console.ReadLine());
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        ClearCurrentConsoleLine();
        Console.WriteLine($"Score entered {exercise3Score}");
        Console.WriteLine("");
        Console.WriteLine("----------------");
        Console.WriteLine("");

        PatientMenu();

    }

    public static void PatientViewProgression()
    {
        List<string> options = new List<string>
        {
        };

        Console.Clear();
        Console.WriteLine();
        int[] data = { 5, 6, 8, 10, 11, 11, 8, 9, 12, 15 };

        // Find the maximum value in the data
        int maxValue = 0;
        foreach (int value in data)
        {
            if (value > maxValue)
                maxValue = value;
        }

        // Draw the graph
        Console.WriteLine("   ^");
        Console.WriteLine("   |");
        Console.WriteLine("   |");
        for (int i = maxValue; i > 0; i--)
        {
            Console.Write($"   |");
            foreach (int value in data)
            {
                if (value >= i)
                    Console.Write(" * ");
                else
                    Console.Write("   ");
            }
            Console.WriteLine();
        }

        // Print the x-axis labels
        Console.Write("   +");
        for (int i = 0; i < data.Length * 3; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine(">");
        Console.WriteLine("");

        int choice = DisplayMenuOptions(options, "press '0' to go back to the main menu", false);

        switch (choice)
        {
            case 0:
                PatientMenu();
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    public static void ParentMenu(Parent parent)
    {
        List<string> patientOptions = new List<string>();
        foreach (Patient patient in parent.Patients)
        {
            patientOptions.Add($"{patient.UserName}");
        }
        int Patientchoice = DisplayMenuOptions(patientOptions, "Select patient to view.") - 1;
        Patient patientToView = parent.Patients[Patientchoice];

            List<string> options = new List<string>
        {
            "View progression",
            "View assessment",
            "Enter Assesmet",
        };

        int choice = DisplayMenuOptions(options, "Parent menu - press '0' to choose another login");
        

        switch (choice)
        {
            case 0:
                MainMenu();
                break;
            case 1:
                //ViewProgression();
                break;
            case 2:
                ViewAssessment(patientToView, parent);
                break;
            case 3:
                //EnterAssessment();
                break;
        }
    }

    public static void DoctorMenu(Doctor docter)
    {
        List<string> patientOptions = new List<string>();

        foreach (Patient patient in testPatients)
        {
            patientOptions.Add($"{patient.UserName}");

        }
        int patientId = DisplayMenuOptions(patientOptions, "Select patient ID to view.") -1;

        Patient newPatient = testPatients.Find(x => x.Id == patientId);

        List<string> options = new List<string>{            
            "View Progression(werkt niet)",
            "View Assessment"
        };

        int choice = DisplayMenuOptions(options, "Select patient ID to view. - Press 0 to return to main menu");

        switch (choice)
        {
            case 0:
                MainMenu();
                break;
            case 1:
                //PatientViewProgression(newPatient);
                break;
            case 2:
                ViewAssessment(newPatient, docter);
                break;
            case 3:
                //ChooseFrequency(newPatient);
                break;

        }



    }

    public static void ViewAssessment(Patient patient, User user)
    {
        Console.WriteLine($"View assessment placeholder: {patient.UserName}");
        List<string> assessmentOptions = new List<string>();
        Console.WriteLine("");
        Console.WriteLine("Assessments:");
        foreach (Assessment assessmentOption in patient.Assessments)
        {
            assessmentOptions.Add($"{assessmentOption.Date}");
        }
        Assessment assessmentToView = patient.Assessments[DisplayMenuOptions(assessmentOptions, "Select assessment to view.") - 1];
        Console.Clear();
        assessmentToView.VieuwAssessment();

        Console.WriteLine("");
        Console.WriteLine("1. Go back");
        Console.WriteLine("2. Main menu");
        if (assessmentToView.Validated == false)
        {
            Console.WriteLine("3. Validate assessment");
        }
        bool validInput = false;
        while (!validInput)
        {
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        validInput = true;
                        break;
                    case 2:
                        MainMenu();
                        validInput = true;
                        break;
                    case 3:
                        assessmentToView.MakeValidated(user);
                        break;
                    default:
                        validInput = true;
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }

    public static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    public static int DisplayMenuOptions(List<string> options, string title = "", bool clearConsole = true)
    {

        if (clearConsole)
        {
            Console.Clear();
        }
        if (title != "")
        {
            Console.WriteLine(title);
            Console.WriteLine();
        }
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
        Console.WriteLine("");
        try
        {
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }
        catch
        {
            Console.WriteLine("Invalid choice. Please try again.");
            return DisplayMenuOptions(options, title, clearConsole);
        }
    }
}