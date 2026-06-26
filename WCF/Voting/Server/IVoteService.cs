using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceContract(CallbackContract = typeof(IVotingCallback), SessionMode = SessionMode.Required)]
    public interface IVoteService
    {
        [OperationContract] int Vote(string candidate);
        [OperationContract] int UndoLastVote();
        [OperationContract] int Reset();
    }


    public interface IVotingCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnVotesUpdated(string standings);
    }
}
