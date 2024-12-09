﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Quizify
{
    internal class Program
    {
        //for global variablez
        static List<string> quizTitles = new List<string>();
        static List<List<string>> quizQuestions = new List<List<string>>();
        static List<List<List<string>>> quizOptions = new List<List<List<string>>>();
        static List<List<int>> correctAnswers = new List<List<int>>();


        static void Main(string[] args)
        {

            //Where user choose what are their going to do.
            int choice; 
            do
            {
                Console.WriteLine("Welcome to Quizify!");
                Console.WriteLine("1. Create a New Quiz");
                Console.WriteLine("2. Add Questions to a Quiz");
                Console.WriteLine("3. Display Quizzes");
                Console.WriteLine("4. Test a Quiz");
                Console.WriteLine("5. Update a Quiz");
                Console.WriteLine("6. Remove a Quiz");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice (1-5): ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CreateQuiz();
                        break;
                    case 2:
                        AddQuestions();
                        break;
                    case 3:
                        DisplayQuizzes();
                        break;
                    case 4:
                        TestQuiz();
                        break;
                    case 5:
                        UpdateQuiz();
                        break;
                    case 6:
                        RemoveQuiz();
                        break;
                }
            }while(choice != 7);   
        }

        //The user choose the 'Create a New Quiz' option.
        static void CreateQuiz()
        {
            Console.WriteLine("\nCreating a Quiz");
            Console.Write("Enter a title for the new quiz: ");
            string quizTitle = Console.ReadLine();

            if (quizTitle == "")
            {
                Console.WriteLine("Cannot be empty! Please input some title.");
                return;
            }
            else
            {
                quizTitles.Add(quizTitle); //Adding the quizTitle to the array quizTitles.
                quizQuestions.Add(new List<String>()); //Setting the quizTitle for new the new index of quizQuestion.
                quizOptions.Add(new List<List<String>>()); //Setting the quizTitle for new the new index of quizOption.
                correctAnswers.Add(new List<int>()); //Setting the quizTitle for new the new index of correctAnswers.

                Console.WriteLine($"Quiz '{quizTitle}' created successfully!\n");
            }
        }

        //to add questions, options, and answers
        static void AddQuestions()
        {
            if(quizTitles.Count == 0)
            {
                Console.WriteLine("\nNo quizzes available. Please create one first!\n");
                return;
            }
            else
            {
                Console.WriteLine("\nAvailable Quizzez:");
                for (int i = 0; i < quizTitles.Count; i++) //To see all the available quiz/zes
                {
                    Console.WriteLine($"{i + 1}. {quizTitles[i]}"); //1. quizTitle
                }

                Console.Write("Select a quiz to add questions (number): ");
                int quizIndex = Convert.ToInt32(Console.ReadLine()) - 1; //Becasue index starts at 0

                if(quizIndex < 0 || quizIndex >= quizTitles.Count)
                {
                    Console.WriteLine("Invalid input!");
                    return;
                }

                Console.Write("How many questions would you like to add: ");
                int questionsCount = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < questionsCount; i++)
                {
                    Console.Write($"Enter the question {i + 1}: ");
                    string question = Console.ReadLine();

                    Console.Write("Enter the number of options: ");
                    int numOptions = Convert.ToInt32(Console.ReadLine());

                    List<string> options = new List<string>(); //Array to display options
                    for (int j = 0; j < numOptions; j++)
                    {
                        Console.Write($"Enter option {j + 1}: ");
                        options.Add(Console.ReadLine());
                    }

                    Console.Write("Enter the correct answer (number): ");
                    int correctAnswer = Convert.ToInt32(Console.ReadLine()) - 1;

                    quizQuestions[quizIndex].Add(question); //Setting the question to the quizIndex of quizQuesitons
                    quizOptions[quizIndex].Add(options); //Setting the options to the quizIndex of quizOptions
                    correctAnswers[quizIndex].Add(correctAnswer); //Setting the correctAnswer to the quizIndex of correctAnswers

                    Console.WriteLine("Question added Successfully!\n");
                }
            }
        }

        //To display all the available quizzes
        static void DisplayQuizzes()
        {
            if (quizTitles.Count == 0)
            {
                Console.WriteLine("\nNo quizzes available.\n");
            }
            else
            {
                Console.WriteLine("\nAll Quizzes: ");
                for (int i = 0; i < quizTitles.Count; i++)
                {
                    Console.WriteLine($"\nQuiz {i + 1}: {quizTitles[i]}"); //Quiz 1: quizTitles
                    for (int j = 0; j < quizQuestions[i].Count; j++)
                    {
                        Console.WriteLine($" Question {j + 1}: {quizQuestions[i][j]}"); //Question 1: quizQuestions
                        for (int k = 0; k < quizOptions[i][j].Count; k++)
                        {
                            Console.WriteLine($"    Option {k + 1}. {quizOptions[i][j][k]}"); //Option 1: quizOptions
                        }
                    }

                    Console.WriteLine();
                }
            }
        }

        static void TestQuiz()
        {
            if(quizTitles.Count == 0)
            {
                Console.WriteLine("\nNo Quizzez available to test.\n");
                return;
            }
            else
            {
                Console.WriteLine("\nAvailable Quizzes: ");
                for (int i = 0; i < quizTitles.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {quizTitles[i]}"); // 1. quizTitles
                }

                Console.Write("Select a quiz to test: ");
                int quizIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                if(quizIndex < 0 || quizIndex >= quizTitles.Count)
                {
                    Console.WriteLine("Invalid input!");
                }

                Console.WriteLine($"\nTesting Quiz: {quizTitles[quizIndex]}"); //Testing Quiz: quizTitles of quizIndex
                int correctCount = 0;

                for (int i = 0; i < quizQuestions[quizIndex].Count; i++)
                {
                    Console.WriteLine($"\nQuestion {i + 1}: {quizQuestions[quizIndex][i]}");
                    for (int j = 0; j < quizOptions[quizIndex][i].Count; j++)
                    {
                        Console.WriteLine($"  Choice {j + 1}. {quizOptions[quizIndex][i][j]}");
                    }

                    Console.Write("Enter your answer: ");
                    int userAnswer = int.Parse(Console.ReadLine()) - 1;

                    if (userAnswer == correctAnswers[quizIndex][i])
                    {
                        Console.WriteLine("Correct!");
                        correctCount++;
                    }
                    else
                    {
                        Console.WriteLine($"Wrong! The correct answer is {correctAnswers[quizIndex][i] + 1}");
                    }
                }

                Console.WriteLine($"Quiz finished! You got {correctCount} out of {quizQuestions[quizIndex].Count}.\n");
            }
        }

        //The user is updating the quiz 
        static void UpdateQuiz()
        {
            if (quizTitles.Count == 0)
            {
                Console.WriteLine("\nNo quizzes available to update.\n");
                return;
            }

            Console.WriteLine("\nQuizzes Available: ");
            for (int i = 0; i < quizTitles.Count; i++)
            {
                Console.WriteLine($"Quiz {i + 1}. {quizTitles[i]}");
            }

            Console.Write("Enter the number of the quiz to update: ");
            int quizIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            if (quizIndex < 0 || quizIndex >= quizTitles.Count)
            {
                Console.WriteLine("Invalid Choice!");
                return;
            }

            Console.WriteLine("What would you like to update?");
            Console.WriteLine("1. Quiz Title");
            Console.WriteLine("2. Question and Answers");
            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1) //the user chose option1
            {
                Console.Write("\nEnter new title: ");
                string newTitle = Console.ReadLine();
                if (newTitle == "")
                {
                    Console.WriteLine("Title cannot be empty!");
                    return;
                }
                quizTitles[quizIndex] = newTitle;
                Console.WriteLine("Quiz title updated successfully!\n");
            }
            else if (choice == 2) // the user chose option 2
            {
                Console.WriteLine($"\nUpdating questions for Quiz: {quizTitles[quizIndex]}");

                for (int i = 0; i < quizQuestions[quizIndex].Count; i++)
                {
                    Console.WriteLine($"\nQuestion {i + 1}: {quizQuestions[quizIndex][i]}");
                    Console.WriteLine(" Options:");

                    for (int j = 0; j < quizOptions[quizIndex][i].Count; j++)
                    {
                        Console.WriteLine($"   {j + 1}: {quizOptions[quizIndex][i][j]}");
                    }

                    // Update the question
                    Console.Write("Enter a new question (or leave blank to keep): ");
                    string newQuestion = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newQuestion))
                    {
                        quizQuestions[quizIndex][i] = newQuestion;
                    }

                    // Update the options
                    for (int j = 0; j < quizOptions[quizIndex][i].Count; j++)
                    {
                        Console.Write($"Enter new option for Choice {j + 1} (or leave blank to keep): ");
                        string newOption = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newOption))
                        {
                            quizOptions[quizIndex][i][j] = newOption;
                        }
                    }

                    // Update the correct answer
                    Console.Write("Enter the number of the correct option (or leave blank to keep): ");
                    int newCorrectAns = Convert.ToInt32(Console.ReadLine());
                    if (newCorrectAns >= 1)
                    {
                        correctAnswers[quizIndex][i] = newCorrectAns - 1;
                    }
                }

                Console.WriteLine("Questions and answers updated successfully!\n");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        //The user is deleting some quiz
        static void RemoveQuiz()
        {
            if (quizTitles.Count == 0)
            {
                Console.WriteLine("\nNo Quizzez available to delete.\n");
                return;
            }
            else
            {
                Console.WriteLine("\nQuizzes Available: ");
                for (int i = 0; i < quizTitles.Count; i++)
                {
                    Console.WriteLine($"Quiz {i + 1}. {quizTitles[i]}");
                }

                Console.Write("Enter the number of the quiz to delete: ");
                int quizIndex = Convert.ToInt32(Console.ReadLine()) - 1;
               
                if (quizIndex < 0 || quizIndex > quizTitles.Count)
                {
                    Console.WriteLine("Invalid Choice!");
                    return;
                }

                Console.Write($"\nAre you sure you want to delete {quizTitles[quizIndex]} (yes/no)? ");
                string confirmation = Console.ReadLine().ToLower();
                if (confirmation == "yes")
                {
                    //deleting the quizIndex at the array
                    quizTitles.RemoveAt(quizIndex);
                    quizQuestions.RemoveAt(quizIndex);
                    quizOptions.RemoveAt(quizIndex);
                    correctAnswers.RemoveAt(quizIndex);

                    Console.WriteLine("Quiz deleted!\n");
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.\n");
                }
            }
        }

    }
}
