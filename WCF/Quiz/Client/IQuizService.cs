using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Server
{
    [DataContract]
    public class Question
    {
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string RightAnswer { get; set; }
    }

    public class Result
    {
        [DataMember]
        public int CorrectNum { get; set; }
        [DataMember]
        public int QuestionCount { get; set; }
        [DataMember]
        public double Percent { get; set; }
    }

    [ServiceContract]
    internal interface IQuizService
    {
        [OperationContract]
        List<Question> GetQuestions();
        [OperationContract]
        void AddQuestion(Question question);
        [OperationContract]
        bool ModifyQuestion(int rBr, string newTxt, string newRightAnswer);
        [OperationContract]
        Result SubmitAnswers(List<string> answers);

    }
}
