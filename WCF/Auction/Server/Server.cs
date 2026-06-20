using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Server
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8080/WCFService");

            using (var host = new ServiceHost(typeof(AuctionServis), baseAddress))
            {
                host.Open();
                Console.WriteLine("Service is running at ", baseAddress);
                Console.WriteLine("Press Enter to stop the service.");
                Console.ReadLine();
            } 
        }
    }
}
