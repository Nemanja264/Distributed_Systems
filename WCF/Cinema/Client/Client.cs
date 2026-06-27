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
        static void Main(string[] args)
        {
            ChannelFactory<ICinemaService> factory = new ChannelFactory<ICinemaService>("CService");
            ICinemaService proxy = factory.CreateChannel();

            Console.WriteLine(proxy.Reserve("Marko", 5));   // true
            Console.WriteLine(proxy.Reserve("Ana", 5));     // false - zauzeto
            Console.WriteLine(proxy.Reserve("Ana", 8));     // true

            proxy.Cancel(5);                                 // oslobadja 5
            Console.WriteLine(proxy.Reserve("Jelena", 5));  // true - sad prolazi

            Console.WriteLine("Slobodna sedista:");
            foreach (int s in proxy.FreeSeats())
                Console.Write(s + " ");
            Console.WriteLine();

            Console.WriteLine("Sve rezervacije:");
            foreach (Server.Reservation r in proxy.GetAllReservations())
                Console.WriteLine("Sediste " + r.SeatNum + " -> " + r.Name);

            Console.ReadLine();
        }
    }
}
