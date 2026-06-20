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
    public class Owner
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string JMBG { get; set; }
    }
    [DataContract]
    public class Rent
    {
        [DataMember] public Owner Owner { get; set; }
        [DataMember] public int StorageId { get; set; }
        [DataMember] public DateTime OwnStart { get; set; }
        [DataMember] public DateTime OwnEnd { get; set; }
        [DataMember] public float Price { get; set; }
    }

    [ServiceContract]
    public interface IStorageService
    {
        [OperationContract]
        bool makeRent(Owner owner, int storageId, DateTime start, DateTime end, float price);
        [OperationContract]
        List<Rent> GetStorages(string jmbg);
        [OperationContract]
        List<Owner> GetOwners();
        [OperationContract]
        List<Rent> GetRentHistory();
    }
}
