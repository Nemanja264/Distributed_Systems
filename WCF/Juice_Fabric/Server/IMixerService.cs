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
    public class Mixer
    {
        [DataMember]
        public List<string> Ingridients { get; set; } = new List<string>();
        [DataMember]
        public float TotalVolume { get; set; } = 0;
        [DataMember]
        public float TotalDensity { get; set; } = 0;
        [DataMember]
        public float TotalMass { get; set; } = 0;
    }
    [ServiceContract]
    public interface IMixerService
    {
        [OperationContract]
        bool AddIngridient(string name, float volume, float density);
        [OperationContract]
        void BottleJuice(float volume);
        [OperationContract]
        void ShowMixerState();
    }
}
