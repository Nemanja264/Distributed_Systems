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
            using(ServiceHost host = new ServiceHost(typeof(ThermostatService)))
            {
                host.Open();
                Console.WriteLine("Service is running...");
                Console.WriteLine("Press Enter to stop");
                Console.ReadLine();
            }
        }
    }
}
