using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MixerService: IMixerService
    {
        private Mixer _mixer;
        MixerService()
        {
            _mixer = new Mixer();
        }

        public bool AddIngridient(string name, float volume, float density)
        {
            if(volume <= 0 || density <= 0)
            {
                return false;
            }
            _mixer.TotalVolume += volume;
            _mixer.TotalMass += density*volume;
            _mixer.TotalDensity = _mixer.TotalMass / _mixer.TotalVolume;


            if (!_mixer.Ingridients.Contains(name))
            {
                _mixer.Ingridients.Add(name);
            }
            Console.WriteLine($"Added ingredient: {name}, Volume: {volume}, Density: {density}");
            return true;
        }

        public void BottleJuice(float volume)
        {
            if (volume <= 0 || volume > _mixer.TotalVolume)
            {
                Console.WriteLine("Volume must be greater than zero and less than or equal to the total volume.");
                return;
            }
            _mixer.TotalVolume -= volume;

            _mixer.TotalMass = _mixer.TotalDensity * _mixer.TotalVolume;
            Console.WriteLine($"Bottled {volume} of juice. Remaining volume: {_mixer.TotalVolume}");
        }

        public void ShowMixerState()
        {
            Console.WriteLine("Mixer State:");
            Console.WriteLine($"Total Volume: {_mixer.TotalVolume}");
            Console.WriteLine($"Total Density: {_mixer.TotalDensity}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in _mixer.Ingridients)
            {
                Console.WriteLine($"- {ingredient}");
            }
        }
    }
}
