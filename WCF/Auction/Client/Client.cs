using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Client
    {
        static IAuctionServis channel;
        static int count = 1;
        static void Main(string[] args)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endPoint = new EndpointAddress("http://localhost:8080/WCFService");
            ChannelFactory<IAuctionServis> factory = new ChannelFactory<IAuctionServis>(binding, endPoint);
            channel = factory.CreateChannel();

            bool exit = false;

            Console.WriteLine("Welcome to the auction! Please choose an option:");
            while (!exit)
            {
                ShowCurrentExponat();
                Console.WriteLine("1. Place a bid");
                Console.WriteLine("2. Register client");
                Console.WriteLine("3. Unregister client");
                Console.WriteLine("0. Exit\n");



                switch (Console.ReadLine())
                {
                    case "1": Bid(); break;
                    case "2": RegisterClient(); break;
                    case "3": UnregisterClient(); break;
                    case "0": exit = true; break;
                }
            }
        }

        static void Bid()
        {
            Console.WriteLine("Enter client id to place a bid: ");
            int clientId = int.Parse(Console.ReadLine());
            Console.Write("Enter amount to increase current bid: ");
            float amount = float.Parse(Console.ReadLine());

            channel.IncreasePrice(amount, clientId);
        }

        static void RegisterClient()
        {
            Console.Write("Enter client name: ");
            string name = Console.ReadLine();
            Console.Write("Enter client last name: ");
            string lastName = Console.ReadLine();
            channel.RegisterClient(new ClientInfo
            {
                Id = count++,
                Name = name,
                LastName = lastName
            });
        }

        static void UnregisterClient()
        {
            Console.Write("Enter client ID to unregister: ");
            int id = int.Parse(Console.ReadLine());
            channel.UnregisterClient(id);
        }

        static void ShowCurrentExponat()
        {
            Exponat exponat = channel.GetCurrentExponat();
            Console.WriteLine($"Current Exponat: {exponat.Name}, Price: {exponat.CurrentPrice}, Highest Bidder ID: {exponat.HighestBidderId}\n");
        }
    }
}
