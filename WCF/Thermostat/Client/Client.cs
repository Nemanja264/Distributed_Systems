using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ThermostatCallbackHandler:IThermostatCallback
    {
        public void OnThermostatUpdate(string message) => Console.WriteLine(message);
    }
    internal class Client
    {
        static void Main(string[] args)
        {
            InstanceContext context = new InstanceContext(new ThermostatCallbackHandler());
            DuplexChannelFactory<IThermostatService> factory = new DuplexChannelFactory<IThermostatService>(context, "TService");
            IThermostatService proxy = factory.CreateChannel();

            proxy.SetDesired(21);     // Trenutno: 20, Zeljeno: 21, GREJANJE
            proxy.Increase();         // Trenutno: 21, Zeljeno: 21, STABILNO
            proxy.Increase();         // Trenutno: 22, Zeljeno: 21, HLADJENJE
            proxy.Decrease();

            Console.ReadLine();
        }
    }
}
