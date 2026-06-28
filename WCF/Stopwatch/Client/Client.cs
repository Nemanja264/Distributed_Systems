using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Client
{
    internal class Client
    {
        public class StopWatchCallback : IStopWatchCallback
        {
            public void OnUpdate(string description) => Console.Write(description);
        }
        static void Main(string[] args)
        {
            InstanceContext context = new InstanceContext(new StopWatchCallback());
            DuplexChannelFactory<IStopWatchService> factory = new DuplexChannelFactory<IStopWatchService>(context, "SService");
            IStopWatchService channel = factory.CreateChannel();

            Console.WriteLine(", Total: " + channel.Round(20));   // Fastest: 20, Avg: 20   | Total: 20
            Console.WriteLine(", Total: " + channel.Round(10));   // Fastest: 10, Avg: 15   | Total: 30
            Console.WriteLine(", Total: " + channel.Round(35));   // Fastest: 10, Avg: 21.67| Total: 65
            Console.WriteLine(", Total: " + channel.Round(8));    // Fastest: 8,  Avg: 18.25| Total: 73
            Console.WriteLine(", Total: " + channel.Undo());       // Fastest: 10, Avg: 21.67| Total: 65
            Console.WriteLine(", Total: " + channel.Reset());      // No rounds yet          | Total: 0

            Console.ReadLine();
        }
    }
}
