using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class VotingCallbackHandler : IVotingCallback
    {
        public void OnVotesUpdated(string standings)
            => Console.WriteLine("Glasovi: " + standings);
    }
    internal class Client
    {
        static void Main(string[] args)
        {
            InstanceContext context = new InstanceContext(new VotingCallbackHandler());
            DuplexChannelFactory<IVoteService> factory =
                new DuplexChannelFactory<IVoteService>(context, "vSer");
            IVoteService proxy = factory.CreateChannel();

            Console.WriteLine(proxy.Vote("Ana"));        // Glasovi: Ana:1
            Console.WriteLine(proxy.Vote("Marko"));      // Glasovi: Ana:1, Marko:1
            Console.WriteLine(proxy.Vote("Ana"));        // Glasovi: Ana:2, Marko:1
            Console.WriteLine(proxy.UndoLastVote());      // Glasovi: Ana:1, Marko:1
            Console.WriteLine(proxy.Reset());             // Glasovi:
            Console.ReadLine();
        }
    }
}
