using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [DataContract]
    public class ClientInfo
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string LastName { get; set; }
    }
    [DataContract]
    public class Exponat
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public float CurrentPrice { get; set; }
        [DataMember]
        public int HighestBidderId { get; set; }
    }
    [ServiceContract]
    public interface IAuctionServis
    {
        [OperationContract]
        void IncreasePrice(float amount, int clientId);
        [OperationContract]
        void RegisterClient(ClientInfo clientInfo);
        [OperationContract]
        void UnregisterClient(int clientId);
        [OperationContract]
        Exponat GetCurrentExponat();
    }
}
