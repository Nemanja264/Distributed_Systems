using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Client
{
    public class Client
    {
        static IStorageService channel;
        static void Main(string[] args)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress("http://localhost:8080/WCFService");
            ChannelFactory<IStorageService> factory = new ChannelFactory<IStorageService>(binding, endpoint);
            channel = factory.CreateChannel();

            Owner o1 = new Owner { Name = "Petar", Surname = "Petrovic", JMBG = "111" };
            Owner o2 = new Owner { Name = "Jovan", Surname = "Jovanovic", JMBG = "222" };

            channel.makeRent(o1, 1, DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-10), 4000);
            channel.makeRent(o2, 1, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(20), 5000);
            channel.makeRent(o1, 2, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(15), 7000);

            Console.WriteLine("Storages for JMBG 111:");
            foreach (Rent r in channel.GetStorages("111"))
                Console.WriteLine($"   Storage {r.StorageId} | {r.OwnStart.ToShortDateString()} - {r.OwnEnd.ToShortDateString()} | {r.Price}");

            Console.WriteLine("\nOwners of active storages:");
            foreach (Owner o in channel.GetOwners())
                Console.WriteLine($"   {o.Name} {o.Surname} ({o.JMBG})");

            Console.WriteLine("\nRent history per storage:");
            foreach (var g in channel.GetRentHistory().GroupBy(r => r.StorageId).OrderBy(g => g.Key))
            {
                Console.WriteLine($"   Storage {g.Key}:");
                foreach (Rent r in g.OrderBy(r => r.OwnStart))
                    Console.WriteLine($"      {r.Owner.Name} {r.Owner.Surname} | {r.OwnStart.ToShortDateString()} - {r.OwnEnd.ToShortDateString()} | {r.Price}");
            }

            factory.Close();
            Console.WriteLine("\nDone. Press ENTER.");
            Console.ReadLine();

        }
    }
}
