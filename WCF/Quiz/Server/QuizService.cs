using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class QuizService : IQuizService
    {
        List<Question> questions = new List<Question>();
        public void AddQuestion(Question question)
        {
            questions.Add(question);
        }

        public List<Question> GetQuestions()
        {
            return questions;
        }

        public bool ModifyQuestion(int rBr, string newTxt, string newRightAnswer)
        {
            if(rBr < 1 || rBr > questions.Count)
            {
                return false;
            }
            questions[rBr - 1].Text = newTxt;
            questions[rBr - 1].RightAnswer = newRightAnswer;
            return true;
        }

        public Result SubmitAnswers(List<string> answers)
        {
            int correct = 0;
            int n = questions.Count;
            for(int i = 0; i < n && i < answers.Count; i++)
            {
                if (answers[i].Trim().Equals(questions[i].RightAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    correct++;
                }
            }

            return new Result
            {
                CorrectNum = correct,
                QuestionCount = n,
                Percent = n > 0 ? (double)correct / n * 100 : 0
            };
        }
    }
}
