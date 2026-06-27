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
    public class Reservation
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int SeatNum { get; set; }
    }

    [ServiceContract]
    public interface ICinemaService
    {
        [OperationContract] bool Reserve(string name, int seatNum);
        [OperationContract] List<Reservation> GetAllReservations();
        [OperationContract] List<int> FreeSeats();
        [OperationContract] bool Cancel(int seatNum);

    }
}
