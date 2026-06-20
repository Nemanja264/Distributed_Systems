using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        static IQuizService channel;
        static void Main(string[] args)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress("http://localhost:8080/QuizService");

            ChannelFactory<IQuizService> factory = new ChannelFactory<IQuizService>(binding, endpoint);
            channel = factory.CreateChannel();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Add question");
                Console.WriteLine("2. Modify question");
                Console.WriteLine("3. Quiz");
                Console.WriteLine("4. Exit");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddQuestion();
                        break;
                    case "2":
                        ModifyQuestion();
                        break;
                    case "3":
                        StartQuiz();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void GetQuestions()
        {
            List<Question> questions = channel.GetQuestions();
            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {questions[i].Text}");
            }
        }

        static void AddQuestion()
        {
            Console.WriteLine("\nEnter question text:");
            string text = Console.ReadLine();
            Console.WriteLine("Enter right answer:");
            string rightAnswer = Console.ReadLine();
            channel.AddQuestion(new Question { Text = text, RightAnswer = rightAnswer });
        }

        static void ModifyQuestion()
        {
            List<Question> questions = channel.GetQuestions();
            if (questions.Count == 0)
            {
                Console.WriteLine("No questions available to modify.");
                return;
            }

            GetQuestions();

            Console.WriteLine("\nEnter question number to modify:");
            int rBr = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new question text:");
            string newTxt = Console.ReadLine();
            Console.WriteLine("Enter new right answer:");
            string newRightAnswer = Console.ReadLine();
            if (!channel.ModifyQuestion(rBr, newTxt, newRightAnswer))
            {
                Console.WriteLine("Invalid question number.");
            }
        }

        static void StartQuiz()
        {
            List<Question> questions = channel.GetQuestions();
            if (questions.Count == 0)
            {
                Console.WriteLine("No questions available for quiz.");
                return;
            }

            List<string> answers = new List<string>();
            foreach (var question in questions)
            {
                Console.WriteLine(question.Text);
                string answer = Console.ReadLine();
                answers.Add(answer);
            }

            Result result = channel.SubmitAnswers(answers);
            Console.WriteLine($"You got {result.CorrectNum} out of {result.QuestionCount} correct. ({result.Percent:F2}%)");
        }
    }
}