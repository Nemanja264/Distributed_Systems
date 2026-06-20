using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RegisterService : IRegisterService
    {
        public List<Registration> registrations = new List<Registration>();

        public List<Registration> GetAllCarRegistrations(Car car)
        {
            return registrations.Where(r => r.Car.FrameNumber == car.FrameNumber).ToList();
        }

        public List<Registration> GetAllCars()
        {
            return registrations;
        }

        public List<Car> GetCarsByPerson(Person person)
        {
            return registrations.
                Where(r => r.Person.JMBG == person.JMBG && r.ExpirationDate > DateTime.Now).
                Select(r => r.Car).
                ToList();
        }

        public bool RegisterCar(Person person, Car car)
        {
            if (registrations.Any(r => r.Car.FrameNumber == car.FrameNumber && r.ExpirationDate > DateTime.Now))
            {
                Console.WriteLine($"Car with frame number {car.FrameNumber} is already registered.");
                return false;
            }

            DateTime expDate = DateTime.Now.AddYears(1);
            registrations.Add(new Registration
            {
                Person = person,
                Car = car,
                ExpirationDate = expDate,
                RegistrationNumber = registrations.Count()+1
            });

            car.RegisteredUntil = expDate;
            Console.WriteLine($"Car with frame number {car.FrameNumber} registered successfully for person {person.Name}.");

            return true;
        }
    }
}
