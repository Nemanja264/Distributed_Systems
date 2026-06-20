using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        static IMixerService channel;
        static void Main()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress("http://localhost:8080/WCFService");
            ChannelFactory<IMixerService> factory = new ChannelFactory<IMixerService>(binding, endpoint);
            channel = factory.CreateChannel();

            Console.WriteLine("Add jabuka: " + channel.AddIngridient("Jabuka", 10, 1.0f));
            Console.WriteLine("Add pomorandza: " + channel.AddIngridient("Pomorandza", 5, 1.2f));
            Console.WriteLine("Add sok od sargarepe: " + channel.AddIngridient("Sargarepa", 3, 1.5f));
            Console.WriteLine("Add voda: " + channel.AddIngridient("Voda", 8, 1.0f));
            Console.WriteLine("Add limun: " + channel.AddIngridient("Limun", 4, 0.9f));

            channel.ShowMixerState();

            channel.BottleJuice(6);
            channel.ShowMixerState();

            channel.BottleJuice(10);
            channel.ShowMixerState();

            factory.Close();
            Console.WriteLine("Done. Press ENTER.");
            Console.ReadLine();
        }
    }
}
