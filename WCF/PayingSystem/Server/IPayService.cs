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
    public class PayingAccount
    {
        [DataMember]
        public string AccountNumber { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }

    [DataContract]
    public class Payment
    {
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public DateTime Date { get; set; } = DateTime.Now;
    }
    [ServiceContract]
    public interface IPayService
    {
        [OperationContract]
        void AddAccount(string accountNumber, double initialAmount);
        [OperationContract]
        List<PayingAccount> GetAccounts();
        [OperationContract]
        PayingAccount GetAccount(string accountNumber);
        [OperationContract]
        bool Pay(string accountNumber, Payment payment);
        [OperationContract]
        List<Payment> GetPayments(string accountNumber);
    }
}
