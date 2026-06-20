using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PayService : IPayService
    {
        List<PayingAccount> accounts = new List<PayingAccount>();
        public void AddAccount(string accountNumber, double initialAmount)
        {
            accounts.Add(new PayingAccount { AccountNumber = accountNumber, Amount = initialAmount });
        }

        public PayingAccount GetAccount(string accountNumber)
        {
            return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public List<PayingAccount> GetAccounts()
        {
            return accounts;
        }

        public List<Payment> GetPayments(string accountNumber)
        {
            var account = GetAccount(accountNumber);
            return account?.Payments;
        }

        public bool Pay(string accountNumber, Payment payment)
        {
            var account = GetAccount(accountNumber);
            if (account == null || account.Amount < payment.Amount)
            {
                Console.WriteLine($"Payment failed for account {accountNumber}. Insufficient funds or account not found.");
                return false;
            }

            account.Amount -= payment.Amount;
            account.Payments.Add(payment);
            Console.WriteLine($"Payment of {payment.Amount} successful for account {accountNumber}. Remaining balance: {account.Amount}");
            return true;
        }
    }
}
