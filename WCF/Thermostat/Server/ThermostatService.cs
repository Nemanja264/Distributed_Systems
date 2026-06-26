using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ThermostatService : IThermostatService
    {
        private int _currentTemp = 20;
        private int _desiredTemp = 0;
        private IThermostatCallback _callback => OperationContext.Current.GetCallbackChannel<IThermostatCallback>();

        public int Decrease()
        {
            _currentTemp--;
            Push();
            return _currentTemp;
        }

        public int Increase()
        {
            _currentTemp++;
            Push();
            return _currentTemp;
        }

        public int SetDesired(int value)
        {
            if(value < 10 || value > 30)
            {
                Console.WriteLine("Minimum temperature is 10, Maximum is 30");
                return -1;
            }
            _desiredTemp = value;

            Push();
            return _currentTemp;
        }

        public void Push()
        {
            string direction;
            if (_currentTemp > _desiredTemp)
                direction = "Heat";
            else if (_desiredTemp > _currentTemp)
                direction = "Cooling";
            else
                direction = "Stable";

            string message = $"Current: {_currentTemp}, Desired: {_desiredTemp}, {direction}";
            _callback.OnThermostatUpdate(message);
        }
    }
}
