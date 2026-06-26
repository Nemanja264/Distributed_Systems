using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceContract(CallbackContract=typeof(IThermostatCallback),SessionMode = SessionMode.Required)]
    public interface IThermostatService
    {
        [OperationContract]
        int Increase();
        [OperationContract]
        int Decrease();
        [OperationContract]
        int SetDesired(int value);
    }

    public interface IThermostatCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnThermostatUpdate(string message);
    }
}
