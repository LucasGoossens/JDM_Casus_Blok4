using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JDM_Casus_Blok4.Classes;
using JDM_Casus_Blok4.UserClasses;
using System.Data;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

internal class Program
{
    static public List<Patient> testPatients = new List<Patient>();
    static public Doctor testDoctor = new Doctor();
    static public List<Exercise> CMAS = new List<Exercise>
    {
        new Exercise(1, "Head elevation", new List<string> { "0 = unable", "1 = 1-9 seconds", "2 = 10-29 seconds", "3 = 30-59 seconds", "4 = 60-119 seconds", "5 = >2 minutes" }),
        new Exercise(2, "Leg raise/touch object", new List<string> { "0 = unable to lift leg off table", "1 = able to clear table but cannot touch object", "2 = able to lift leg high enough to touch object" }),
        new Exercise(3, "Straight leg lift", new List<string> { "0 = unable", "1 = 1-9 seconds", "2 = 10-29 seconds", "3 = 30-59 seconds", "4 = 60-119 seconds", "5 = >2 minutes" }),
        new Exercise(4, "Supine to prone", new List<string> { "0 = unable", "1 = turns onto side fairly easily; but cannot fully free arms and is not able to fully assume a prone position", "2 = easily turns onto side; has some difficulty freeing arms, but fully frees them and fully assumes a prone position", "3 = easily turns over, free arms with no difficulty" }),
        new Exercise(5, "Sit ups \n\nFor each type of sit-up give “0” (unable) or “1” (able).\r\nThen enter the total sub score (maximum possible item score 6). \n a) Hands on thighs, with counterbalance -\n b) Hands across chest, with counterbalance -\n c) Hands behind head, with counterbalance -\n d) Hands on thighs, without counterbalance -\n e) Hands across chest, without counterbalance -\n f) Hands behind head, without counterbalance -", new List<string> {"Enter Sub Score = (maximum possible item score 6)", "", "", "", "", "", "" }),
        new Exercise(6, "Supine to sit", new List<string> { "0 = unable", "1 = much difficulty. Very slow, struggles greatly, barely makes it. Almost unable", "2 = some difficulty. Able, but is somewhat slow, struggles some.", "3 = no difficulty" }),
        new Exercise(7, "Arm raise/straighten", new List<string> { "0 = cannot raise wrists", "1 = can raise wrists at least up to the level of the acromioclavicular joint but not above top of head", "2 = can raise wrists above top of head but cannot raise arms straight above head so that elbows are in full extension", "3 = can raise arms straight above head so that elbows are in full extension" }),
        new Exercise(8, "Arm raise/duration", new List<string> { "0 = unable", "1 = 1-9 seconds", "2 = 10-29 seconds", "3 = 30-59 seconds", "4 = >60 seconds" }),
        new Exercise(9, "Floor sit", new List<string> { "0 = unable. Afraid to even try. Even if allowed to use a chair for support. Child fears that he/she will collapse, fall into a sit or self-harm", "1 = much difficulty. Able, but needs to hold onto chair for support during descent (unable or unwilling to try if not able to use a chair for support)", "2 = some difficulty. Can go from stand to sit without using a chair for support but has at least some difficulty during descent. Descends somewhat slowly and/or apprehensively; may not have full control or balance as manoeuvres into a sit", "3 = no difficulty. Requires no compensatory manoeuvring" }),
        new Exercise(10, "All-fours manoeuvre", new List<string> { "0 = unable to go from a prone to an all-fours position", "1 = barely able to assume and maintain an all-fours position", "2 = can maintain all-fours position with straight back and head raised (so as to look straight ahead). But cannot crawl forward", "3 = can maintain all fours, look straight ahead and crawl forward", "4 = maintains balance while lifting and extending leg" }),
        new Exercise(11, "Floor rise", new List<string> { "0 = unable, even if allowed to use a chair for support", "1 = much difficulty. Able, but needs to use a chair for support. Unable if not allowed to use a chair", "2 = moderate difficulty. Able to get up without a chair for support but needs to place on or both hands on thighs/knees or floor. Unable without using hands.", "3 = mild difficulty. Does not need to place hands on knees, thighs or floor but has at least some difficulty during ascent.", "4 = no difficulty" }),
        new Exercise(12, "Chair rises", new List<string> { "0 = unable to rise from chair, even if allowed to place hands on sides of chair", "1 = much difficulty. Able but needs to place hands on side of seat. Unable if not allowed to place hands on knees/thighs", "2 = moderate difficulty. Able but needs to place hands on knees/thighs. Does not need to place hands on side of seat", "3 = mild difficulty. Able; does not need to use hands at all, but has at least some difficulty", "4 = no difficulty" }),
        new Exercise(13, "Stool step", new List<string> { "0 = unable", "1 = much difficulty. Able but needs to place one hand on exam table or examiner’s hand", "2 = some difficulty. Able; does not need to use exam table for support but needs to use hands on knee/thigh", "3 = able. Does not need to use exam table or hands on knee/thigh" }),
        new Exercise(14, "Pick up", new List<string> { "0 = able to bend over and pick up pencil on floor", "1 = much difficulty. Able but relies heavily on support gained by placing hands on knees/thighs", "2 = some difficulty. Needs to at least minimally and briefly place hands on knees/thighs for support and is somewhat slow", "3 = No difficulty. No compensatory manoeuvre necessary." })

    };
    static void Main(string[] args)
    {
        Console.WriteLine("Hello world!");

        // loop om test/mock/dummy objects toe te voegen 
        for (int i = 0; i < 3; i++)
        {

            DateOnly testDate = DateOnly.FromDateTime(DateTime.Now);

            Assessment testAssessment = new Assessment(i, CMAS, testDate, false, 100);
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
        Assessment newAssessment = new Assessment(false);

        foreach (Exercise exerciseTemplate in CMAS)
        {
            Console.Clear();
            Console.WriteLine($"Exercise {exerciseTemplate.ExerciseNumber}: {exerciseTemplate.Name}");
            Console.WriteLine("");
            
            foreach (string result in exerciseTemplate.ResultOptions)
            {
                if(result == "") { break; }
                // edge case voor Situps, 
                // de hoogste te behalen score van alle andere exercises komt overeen met exerciseTemplate.ResultOptions.Count() - 1,
                // maar Situps heeft subscores,in totaal maximaal 6, dus er zitten lege 6 strings in de list van situps                

                Console.WriteLine(result);
            }

            Console.WriteLine("");
            int exerciseAssessmentScore = 0;
            do
            {
                if(exerciseAssessmentScore > exerciseTemplate.ResultOptions.Count() -1 || exerciseAssessmentScore < 0)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
                Console.WriteLine("Enter your score.");
                exerciseAssessmentScore = Convert.ToInt32(Console.ReadLine());
            } while (exerciseAssessmentScore > exerciseTemplate.ResultOptions.Count() - 1 || exerciseAssessmentScore < 0);

            // hier zit nog geen invoer validatie
            Exercise newExercise = new Exercise(exerciseTemplate.ExerciseNumber, exerciseTemplate.Name, exerciseAssessmentScore);
            newAssessment.AddExercise(newExercise);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();
        }

        newAssessment.Date = DateOnly.FromDateTime(DateTime.Now);        
        newAssessment.CalculatingScore();


        // alle placeholder data
        // dummy patient
        testPatients[1].Assessments.Add(newAssessment);        
        // dit werkt niet:
        // newAssessment.PatientAge = testPatients[1].DateOfBirth >> int <!> DateOnly
        // Patient.Age maken?
        // of kunt misschien alleen het jaar van DateOnly gebruiken en naar int casten, kijk dinsdag
        // newAssement.PatientAge = patient.Age; 

        newAssessment.PatientId = testPatients[1].Id;
        newAssessment.PatientAge = 999;
        
        newAssessment.SaveAssessment(); 

        PatientMenu();

    }

    public static void PatientViewProgression()
    {
        List<string> options = new List<string> { };

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
            patientOptions.Add($"{patient.FirstName}");
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
                ViewProgression(patientToView, parent);
                break;
            case 2:
                ViewAssessment(patientToView, parent);
                break;
            case 3:
                //EnterAssessment();
                break;
        }
    }

    public static void DoctorMenu(Doctor doctor)
    {
        List<string> patientOptions = new List<string>();

        foreach (Patient patient in testPatients)
        {
            patientOptions.Add($"{patient.FirstName}");

        }
        int patientId = DisplayMenuOptions(patientOptions, "Select patient ID to view.") - 1;

        Patient newPatient = testPatients.Find(x => x.Id == patientId);

        List<string> options = new List<string>{
            "View Progression",
            "View Assessment"
        };

        int choice = DisplayMenuOptions(options, "Doctor menu. - Press 0 to return to main menu");

        switch (choice)
        {
            case 0:
                MainMenu();
                break;
            case 1:
                ViewProgression(newPatient, doctor);
                break;
            case 2:
                ViewAssessment(newPatient, doctor);
                break;
            case 3:
                ChooseFrequency(newPatient);
                break;
                //case 4:
                //    ValidateAssessment(newPatient, doctor);
                //    break;

        }

    }

    public static void ViewAssessment(Patient patient, User user)
    {
        Console.WriteLine($"View assessment placeholder: {patient.FirstName}");
        List<string> assessmentOptions = new List<string>();
        Console.WriteLine("");
        Console.WriteLine("Assessments:");
        foreach (Assessment assessmentOption in patient.Assessments)
        {
            assessmentOptions.Add($"{assessmentOption.Date}");
        }
        Assessment assessmentToView = patient.Assessments[DisplayMenuOptions(assessmentOptions, "Select assessment to view.") - 1];
        Console.Clear();
        assessmentToView.ViewAssessment();

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
                        if (assessmentToView.Validated == true) break; // "3. Validate assessment" wordt niet geprint, maar kunt zonder deze line nog steeds op 3 drukken
                        assessmentToView.ValidateAssessment(user);
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

    public static void ResearcherMenu()
    {
        List<string> options = new List<string> {
                "View assessments",
            };

        int choice = DisplayMenuOptions(options, "Researcher menu - press '0' to choose another login");

        switch (choice)
        {
            case 0:
                MainMenu();
                break;
            case 1:
                ViewAssessmentsResearcher();
                break;
        }
    }

    public static void ViewAssessmentsResearcher()
    {
        // nog toe te passen 
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
    public static void ViewProgression(Patient patientToVieuw, User user)
    {
        List<int> progression = new List<int>();
        string progressionString = "";
        foreach (Assessment assessment in patientToVieuw.Assessments)
        {
            if (assessment.Validated)
            {
                progressionString += $"{assessment.TotalScore} + ";
            }
        }
        Console.WriteLine("Progression of assessment scores:");

        // checkt of er assessments zijn
        string progressionStringToShow = progressionString.Length < 3 ? "No assessments available" : progressionString.Substring(0, progressionString.Length - 3);
        Console.WriteLine(progressionStringToShow);
        Console.WriteLine("press enter to continue");
        Console.ReadLine();
        Console.Clear();
        if (user is Parent parent)
        {
            ParentMenu(parent);
        }
        else if (user is Doctor doctor)
        {
            DoctorMenu(doctor);
        }


    }
    public static void ChooseFrequency(Patient patient)
    {
        Console.WriteLine($"The current frequency between assessments {patient.AssessmentFrequentie} days:");
        Console.WriteLine("Enter new frequency (in days):");
        bool validInput = false;
        while (!validInput)
        {
            try
            {
                int newFrequency = Convert.ToInt32(Console.ReadLine());
                validInput = true;
            }
            catch
            {
                Console.WriteLine("Invalid input. Please try again.");
                ChooseFrequency(patient);
            }
        }

    }
}
