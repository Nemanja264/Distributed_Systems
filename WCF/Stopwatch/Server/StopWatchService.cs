using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class StopWatchService : IStopWatchService
    {
        private List<int> _rounds = new List<int>();
        private IStopWatchCallback _callback => OperationContext.Current.GetCallbackChannel<IStopWatchCallback>();
        public void Push()
        {
            if(_rounds.Count == 0)
            {
                _callback.OnUpdate("No rounds yet");
                return;
            }

            _callback.OnUpdate($"Fastest: {_rounds.Min()}s, Avg: {Math.Round(_rounds.Average(), 2)}s");
        }
        public int Reset()
        {
            _rounds.Clear();

            Console.WriteLine("StopWatch reseted");
            Push();
            return _rounds.Sum();
        }

        public int Round(int value)
        {
            if(value < 0)
            {
                Console.WriteLine("Value must be positive number");
                return -1;
            }

            _rounds.Add(value);
            Console.WriteLine($"Round {value}s");
            Push();
            return _rounds.Sum();
        }

        public int Undo()
        {
            if (_rounds.Count == 0)
            {
                return 0;
            }

            _rounds.RemoveAt(_rounds.Count - 1);
            Console.WriteLine("Undo Done");
            Push();
            return _rounds.Sum();
        }
    }
}
