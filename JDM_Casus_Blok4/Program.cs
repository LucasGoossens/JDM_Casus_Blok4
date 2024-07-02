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
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using System.Runtime.InteropServices;


internal class Program
{
    //static public List<Patient> testPatients = new List<Patient>();
    //static public Doctor testDoctor = new Doctor(2, "Shanon", "Shelton");
    //static public PhysicalTherapist therapist = new PhysicalTherapist(3, "Wilbur", "Stevens");
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
        Console.WriteLine("Hello world");


        // loop om test/mock/dummy objects toe te voegen 
        for (int i = 0; i < 3; i++)
        {

            bool flag = true;
            while (flag)
            {
                Console.WriteLine("");
                MainMenu();

                flag = false;
            }
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
                Patient patient = Patient.GetPatient(1);
                PatientMenu(patient);
                break;
            case 2:
                Parent parent = Parent.GetParent();
                ParentMenu(parent);
                break;
            case 3:
                Doctor doctor = Doctor.GetDoctorById(2);
                DoctorMenu(doctor);
                break;
            case 4:
                PhysicalTherapist therapist = PhysicalTherapist.GetPhysicalTherapist();
                PhysicalTherapistMenu(therapist);
                break;
            case 5:
                Researcher researcher = Researcher.GetResearcherById(5);
                ResearcherMenu(researcher);
                break;
        }
    }

    public static void PatientMenu(Patient patient)
    {
        List<string> options = new List<string> {
                "Enter assessment",
                "View assessments",
                "View progression",
            };

        int choice = DisplayMenuOptions(options, "Patient menu - press '0' to choose another login");

        switch (choice)
        {
            case 0:
                MainMenu();
                break;
            case 1:
                Assessment newAssessment = EnterAssessment(patient.Id);
                newAssessment.SaveAssessment();
                PatientMenu(patient);
                break;
            case 2:
                ViewAssessment(patient, patient);
                PatientMenu(patient);
                break;
            case 3:
                ViewProgression(patient, patient);
                break;
        }
    }

    public static Assessment EnterAssessment(int patientId)
    {
        // het enige wat een nieuwe assessment nodig heeft bij het voor de eerste keer aanmaken is de datum, validated = false en de patientId.
        // de rest wordt in deze method door de gebruiker ingevuld en toegevoegd met {get;set;}
        Assessment newAssessment = new Assessment(DateOnly.FromDateTime(DateTime.Now), false, patientId);
        //List<Exercise> exercises = new List<Exercise>();

        foreach (Exercise exerciseTemplate in CMAS)
        {
            Console.Clear();
            Console.WriteLine($"Exercise {exerciseTemplate.ExerciseNumber}: {exerciseTemplate.Name}");
            Console.WriteLine("");

            foreach (string result in exerciseTemplate.ResultOptions)
            {
                if (result == "") { break; }
                // edge case voor Situps, 
                // de hoogste te behalen score van alle andere exercises komt overeen met exerciseTemplate.ResultOptions.Count() - 1,
                // maar Situps heeft subscores,in totaal maximaal 6, dus er zitten lege 6 strings in de list van situps                

                Console.WriteLine(result);
            }

            Console.WriteLine("");
            int exerciseAssessmentScore = 0;
            bool isValidInput = false;

            do
            {
                Console.WriteLine("Enter your score.");
                string input = Console.ReadLine();
                isValidInput = int.TryParse(input, out exerciseAssessmentScore);

                if (!isValidInput || exerciseAssessmentScore > exerciseTemplate.ResultOptions.Count() - 1 || exerciseAssessmentScore < 0)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
                
            } while (!isValidInput || exerciseAssessmentScore > exerciseTemplate.ResultOptions.Count() - 1 || exerciseAssessmentScore < 0);


            Exercise newExercise = new Exercise(exerciseTemplate.ExerciseNumber, exerciseTemplate.Name, exerciseAssessmentScore, exerciseTemplate.ResultOptions.Count() - 1, exerciseTemplate.ResultOptions);

            newAssessment.AddExercise(newExercise);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();
        }

        return newAssessment;
    }


    public static void ParentMenu(Parent parent)
    {
        List<string> patientOptions = new List<string>();
        foreach (Patient patient in parent.Patients)
        {
            patientOptions.Add($"{patient.Firstname}");
        }
        int Patientchoice = DisplayMenuOptions(patientOptions, "Select patient to view.");
        Patient patientToView = parent.Patients[Patientchoice -1];

        List<string> options = new List<string>
        {
            "View progression",
            "View assessment",
            "Enter Assessment",
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
                Assessment newAssessment = EnterAssessment(patientToView.Id);
                newAssessment.SaveAssessment();
                ParentMenu(parent);
                break;

        }
    }

    public static void DoctorMenu(Doctor doctor)
    {
        List<string> patientOptions = new List<string>();

        foreach (Patient patient in doctor.Patients)
        {
            patientOptions.Add($"{patient.Firstname}");

        }
        int patientId = DisplayMenuOptions(patientOptions, "Select patient ID to view.");

        Patient newPatient = doctor.Patients[patientId-1];

        List<string> options = new List<string>{
            "View Progression",
            "View Assessment",
            "Define assesment frequency"
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
        Console.WriteLine($"View assessment: {patient.Firstname}");
        List<string> assessmentOptions = new List<string>();
        Console.WriteLine("");
        Console.WriteLine("Assessments:");

        patient.GetAssessments();

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
        if (assessmentToView.Validated == false && user != patient && user is not Parent)
        {
            Console.WriteLine("3. Validate assessment");
        }
        if (user != patient && user is not Parent)
        {
            Console.WriteLine("4. Give feedback");
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
                        if (user is Patient)
                        {
                            PatientMenu(patient);
                        }
                        if (user is Parent parent)
                        {
                            ParentMenu(parent);
                        }
                        else if (user is Doctor doctor)
                        {
                            DoctorMenu(doctor);
                        }
                        else if (user is PhysicalTherapist therapist)
                        {
                            PhysicalTherapistMenu(therapist);
                        }
                        break;
                    case 2:
                        MainMenu();
                        validInput = true;
                        break;
                    case 3:
                        if (assessmentToView.Validated == true) break;
                        assessmentToView.ValidateAssessment(user);
                        validInput = true;
                        break;
                    case 4:
                        if (user == patient) break;
                            GiveFeedback(assessmentToView, patient, user.Id);
                        validInput = true;
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

    public static void GiveFeedback(Assessment assessment, Patient patient, int providerId)
    {
        Console.WriteLine("Select exercise to give feedback");
        List<string> options = new List<string>();

        foreach (Exercise exercise in assessment.Exercises)
        {
            options.Add($"{exercise.ExerciseNumber}: {exercise.Name}");
        }
        options.Add("General feedback");

        int choice = DisplayMenuOptions(options, $"Select exercise to give specific feedback, or press {assessment.Exercises.Count() + 1} to give general feedback.");

        if (choice == assessment.Exercises.Count() + 1)
        {

            Console.WriteLine("Enter feedback:");
            string feedback = Console.ReadLine();
            Feedback newFeedback = new Feedback(feedback, providerId);
            assessment.Feedback = newFeedback;
            newFeedback.SaveFeedback(providerId, "Assessment", assessment.Id);
            Console.WriteLine("Feedback saved.");
        }
        else
        {
            Console.WriteLine("Enter feedback:");
            string feedback = Console.ReadLine();
            Feedback newFeedback = new Feedback(feedback, providerId);
            assessment.Exercises[choice - 1].Feedback = newFeedback;
            newFeedback.SaveFeedback(providerId, "Exercise", assessment.Exercises[choice - 1].Id);
            Console.WriteLine("Feedback saved.");
        }

    }

    public static void ResearcherMenu(Researcher researcher)
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
                ViewAssessmentsResearcher(researcher);
                break;
        }
    }

    public static void ViewAssessmentsResearcher(Researcher researcherUser)
    {
        Console.WriteLine("Pick a userId from the patient where you want to view the assessments from.");
        List<int> PotentialPatientIds = new List<int>();
        int counter = 1;

        foreach (Assessment assessment in researcherUser.Assessments)
        {
            if (!PotentialPatientIds.Contains(assessment.PatientId))
            {
                PotentialPatientIds.Add(assessment.PatientId);
                Console.WriteLine($"{counter}. Id = {assessment.PatientId}");
                counter++;
            }
        }

        bool validInput = false;
        int PatientIdToOrderBy = 0;

        while (!validInput)
        {
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                // Ensure the choice is within the valid range
                if (choice > 0 && choice <= PotentialPatientIds.Count)
                {
                    PatientIdToOrderBy = PotentialPatientIds[choice - 1];
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        foreach (Assessment assessment in researcherUser.Assessments)
        {
            if (assessment.PatientId == PatientIdToOrderBy)
            {
                assessment.ViewAssessmentResearcher();
            }
        }
        Console.WriteLine();
        Console.WriteLine("1. Researcher Menu");
        Console.ReadLine();
        ResearcherMenu(researcherUser);
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
        string progressionString = patientToVieuw.GetProgression();
        Console.WriteLine("Progression of assessment scores:");

        // checkt of er assessments zijn
        string progressionStringToShow = progressionString.Length < 3 ? "No assessments available" : progressionString.Substring(0, progressionString.Length - 3);
        Console.WriteLine(progressionStringToShow);
        Console.WriteLine("press enter to continue");
        Console.ReadLine();
        Console.Clear();
        if (user is Patient patient)
        {
            PatientMenu(patient);
        }
        if (user is Parent parent)
        {
            ParentMenu(parent);
        }
        else if (user is Doctor doctor)
        {
            DoctorMenu(doctor);
        }
        else if (user is PhysicalTherapist therapist)
        {
            PhysicalTherapistMenu(therapist);
        }
    }

    public static void ChooseFrequency(Patient patient)
    {
        Console.WriteLine($"The current frequency between assessments {patient.AssessmentFrequency} days:");
        Console.WriteLine("Enter new frequency (in days):");
        bool validInput = false;
        while (!validInput)
        {
            try
            {
                int newFrequency = Convert.ToInt32(Console.ReadLine());
                validInput = true;
                patient.EditAssessmentFrequency(newFrequency);
            }
            catch
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
        DoctorMenu(Doctor.GetDoctorById(2));

    }

    public static void PhysicalTherapistMenu(PhysicalTherapist therapist)
    {
        List<string> patientListOptions = new List<string>();
        foreach (Patient patient in therapist.Patients)
        {
            patientListOptions.Add($"{patient.Firstname}");
        }
        int selectedPatientchoice = DisplayMenuOptions(patientListOptions, "Select patient to view.") - 1;
        Patient patientToView = therapist.Patients[selectedPatientchoice];

        List<string> options = new List<string>
        {
            "View assessment",
            "Enter assesment",
        };

        int choice = DisplayMenuOptions(options, "therapist menu - press '0' to choose another login");


        switch (choice)
        {
            case 0:
                MainMenu();
                break;
            case 1:
                //View assessment
                ViewAssessment(patientToView, therapist);
                break;
            case 2:
                // Enter assessment
                Assessment newAssessment = EnterAssessment(patientToView.Id);
                newAssessment.SaveAssessment();                
                break;
        }
    }
}
