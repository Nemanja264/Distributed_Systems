using Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Client
    {
        static IPayService channel;
        static int counter = 100000;
        static void Main(string[] args)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress("http://localhost:8080/WCFService");

            ChannelFactory<IPayService> factory = new ChannelFactory<IPayService>(binding, endpoint);
            channel = factory.CreateChannel();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("  1) Plati troškove");
                Console.WriteLine("  2) Dodaj račun");
                Console.WriteLine("  3) Prikaži listing plaćanja");
                Console.WriteLine("  0) Exit");
                Console.Write("Izbor: ");

                switch (Console.ReadLine())
                {
                    case "1": StartPaymentProcess(); break;
                    case "2": AddAccount(); break;
                    case "3": ShowListing(); break;
                    case "0": exit = true; break;
                    default: Console.WriteLine("Nepoznat izbor."); break;
                }
            }
        }

        static void StartPaymentProcess()
        {
            List<PayingAccount> payingAccounts = channel.GetAccounts();
            Console.WriteLine("Choose paying account:");
            for (int i = 0; i < payingAccounts.Count; i++)
            {
                Console.WriteLine($"{i+1}. {payingAccounts[i].AccountNumber}, AMOUNT: {payingAccounts[i].Amount}");
            }
            Console.Write("Enter account number to pay: ");
            int accNum = int.Parse(Console.ReadLine());

            channel.Pay(payingAccounts[accNum - 1].AccountNumber, new Payment
            {
                Amount = 100,
                Description = "Test payment"
            });
        }

        static void AddAccount()
        {
            Random random = new Random();
            channel.AddAccount($"ACC{counter++}", random.Next(50, 500));
        }

        static void ShowListing()
        {   


        }
    }
}
