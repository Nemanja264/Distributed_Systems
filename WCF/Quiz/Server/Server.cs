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
            Uri baseAddress = new Uri("http://localhost:8080/QuizService");
            using(ServiceHost host = new ServiceHost(typeof(QuizService), baseAddress))
            {
                host.Open();
                Console.WriteLine("Server is running...");
                Console.ReadLine();
            }
        }
    }
}
