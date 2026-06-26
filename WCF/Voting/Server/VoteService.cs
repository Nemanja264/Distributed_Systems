using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class VoteService : IVoteService
    {
        private Dictionary<string, int> _votes = new Dictionary<string, int>();
        private List<string> _history = new List<string>();

        private IVotingCallback _callback => OperationContext.Current.GetCallbackChannel<IVotingCallback>();
        public int Reset()
        {
            _votes.Clear();
            _history.Clear();
            return 0;
        }

        public int UndoLastVote()
        {
            if (_history.Count > 0)
            {
                string last = _history[_history.Count - 1];
                _history.RemoveAt(_history.Count - 1);
                _votes[last]--;
            }
            Push();
            return _history.Count;
        }

        public int Vote(string candidate)
        {
            if (!_votes.ContainsKey(candidate)) _votes[candidate] = 0;
            _votes[candidate]++;
            _history.Add(candidate);
            Push();
            return _history.Count;
        }


        public void Push()
        {
            List<string> parts = new List<string>();
            foreach (var p in _votes) parts.Add(p.Key + ":" + p.Value);
            _callback.OnVotesUpdated(string.Join(", ", parts));
        }
    }
}
