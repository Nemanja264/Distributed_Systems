using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CinemaService : ICinemaService
    {
        private int _freeSeatsCount = 50;
        private Dictionary<int, string> _reservations = new Dictionary<int, string>();
        public bool Cancel(int seatNum)
        {
            if(seatNum < 1 || seatNum > 50)
            {
                Console.WriteLine("Seat number must be in between 1-50");
                return false;
            }

            if (!_reservations.ContainsKey(seatNum))
            {
                Console.WriteLine($"Seat {seatNum} is free already");
                return false;
            }

            _reservations.Remove(seatNum);
            Console.WriteLine($"Reservation for seat {seatNum} is canceled");
            return true;
        }

        public List<int> FreeSeats()
        {
            List<int> freeSeats = new List<int>();
            for(int i=1;i<_freeSeatsCount;i++)
            {
                if(!_reservations.ContainsKey(i)) 
                    freeSeats.Add(i);
            }

            return freeSeats;
        }

        public List<Reservation> GetAllReservations()
        {
            List<Reservation> resList = new List<Reservation>();
            foreach(var res in _reservations)
            {
                resList.Add(new Reservation { SeatNum = res.Key, Name = res.Value });
            }

            return resList;
        }

        public bool Reserve(string name, int seatNum)
        {
            if (seatNum < 1 || seatNum > 50)
            {
                Console.WriteLine("Seat number must be in between 1-50");
                return false;
            }

            if (_reservations.ContainsKey(seatNum))
            {
                Console.WriteLine($"Seat {seatNum} is taken already");
                return false;
            }

            _reservations[seatNum] = name;
            Console.WriteLine($"Seat {seatNum} taken by {name}");
            return true;
        }
    }
}
