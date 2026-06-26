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
            using (ServiceHost host = new ServiceHost(typeof(VoteService)))
            {
                host.Open();
                Console.WriteLine("The service is running..");
                Console.WriteLine("Press ENTER to stop");
                Console.ReadLine();
            }
        }
    }
}
