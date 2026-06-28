using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceContract(CallbackContract =typeof(IStopWatchCallback), SessionMode = SessionMode.Required)]
    public interface IStopWatchService
    {
        [OperationContract]
        int Round(int value);
        [OperationContract]
        int Undo();
        [OperationContract]
        int Reset();
    }

    public interface IStopWatchCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnUpdate(string description);
    }
}
