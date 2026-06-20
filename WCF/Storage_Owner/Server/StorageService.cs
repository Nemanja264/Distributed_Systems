using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class StorageService : IStorageService
    {
        public List<Rent> rents = new List<Rent>();

        public List<Owner> GetOwners()
        {
            return rents
                .Where(r => r.OwnEnd > DateTime.Now)
                .Select(r => r.Owner)
                .ToList();
        }

        public List<Rent> GetRentHistory()
        {
            return rents;
        }

        public List<Rent> GetStorages(string jmbg)
        {
            return rents.Where(r => r.Owner.JMBG == jmbg).ToList();
        }

        public bool makeRent(Owner owner, int storageId, DateTime start, DateTime end, float price)
        {
            if(rents.Any(r => r.StorageId == storageId && r.OwnEnd > start))
            {
                Console.WriteLine($"Storage {storageId} is already rented during the requested period.");
                return false;
            }

            Console.WriteLine($"Storage {storageId} rented to {owner.Name} from {start} to {end} for {price}.");
            rents.Add(new Rent
            {
                Owner = owner,
                StorageId = storageId,
                OwnStart = start,
                OwnEnd = end,
                Price = price
            });
            return true;
        }
    }
}
