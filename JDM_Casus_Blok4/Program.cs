﻿using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world!");

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
                    // ParentMenu();
                    break;
                case 3:
                    // DoctorMenu();
                    break;
                case 4:
                    // PhysicalTherapistMenu();
                    break;
                case 5:
                    ResearcherMenu();
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
                    ViewAssessments();
                    break;
            }
        }

        public static void ViewAssessments()
        {
            Console.Clear();
            Console.WriteLine("Viewing all assessments...");

            Researcher researcher = new Researcher();
            // Add some dummy patients for testing
            researcher.AllPatients.Add(new Patient("John Doe") { Assessments = new List<string> { "Assessment 1", "Assessment 2" } });
            researcher.AllPatients.Add(new Patient("Jane Smith") { Assessments = new List<string> { "Assessment A", "Assessment B" } });

            researcher.ViewAssessments();

            // Go back to the ResearcherMenu after viewing assessments
            ResearcherMenu();
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
}