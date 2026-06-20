using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AuctionServis : IAuctionServis
    {
        public List<ClientInfo> clients = new List<ClientInfo>();
        public Exponat currentExponat;

        AuctionServis()
        {
            currentExponat = new Exponat { Name = "Diamond Watch", CurrentPrice = 100.0f };
        }

        public void ChangeExponat(Exponat exponat)
        {
            currentExponat = exponat;
        }

        public Exponat GetCurrentExponat()
        {
            return currentExponat;
        }
        private bool IsBidValid(float amountToInc, int clientId)
        {
            ClientInfo client = clients.FirstOrDefault(c => c.Id == clientId);
            if (client == null || amountToInc <= 0) {
                return false;
            }
            return true;
        }

        public void IncreasePrice(float amount, int clientId)
        {
            if(!IsBidValid(amount, clientId))
            {
                Console.WriteLine($"Invalid bid client ID invalid or amount, amount must be >0");
                return;
            }
            currentExponat.CurrentPrice += amount;
            currentExponat.HighestBidderId = clientId;
            Console.WriteLine($"Current price: {currentExponat.CurrentPrice}, Highest bidder ID: {currentExponat.HighestBidderId}");
        }

        public void RegisterClient(ClientInfo clientInfo)
        {
            clients.Add(clientInfo);
            Console.WriteLine($"Client registered: {clientInfo.Name} {clientInfo.LastName}, ID: {clientInfo.Id}");
        }

        public void UnregisterClient(int clientId)
        {
            clients.RemoveAll(c => c.Id == clientId);
            Console.WriteLine($"Client unregistered ID: {clientId}");
        }
    }
}
