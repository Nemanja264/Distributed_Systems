using Server;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Client
{
    class Client
    {
        static IRegisterService channel;

        static void Main()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint =
                new EndpointAddress("http://localhost:8080/WCFService");

            ChannelFactory<IRegisterService> factory =
                new ChannelFactory<IRegisterService>(binding, endpoint);
            channel = factory.CreateChannel();

            Person p1 = new Person { Name = "Petar", Surname = "Petrovic", JMBG = "111" };
            Person p2 = new Person { Name = "Jovan", Surname = "Jovanovic", JMBG = "222" };

            Car c1 = new Car { Brand = "VW", Model = "Golf", FrameNumber = "VIN1", Color = "Crna" };
            Car c2 = new Car { Brand = "Fiat", Model = "Punto", FrameNumber = "VIN2", Color = "Bela" };
            Car c3 = new Car { Brand = "Audi", Model = "A4", FrameNumber = "VIN3", Color = "Siva" };

            channel.RegisterCar(p1, c1);
            channel.RegisterCar(p1, c2);
            channel.RegisterCar(p2, c3);

            Console.WriteLine("\nCars of person 111:");
            foreach (Car c in channel.GetCarsByPerson(p1))
                Console.WriteLine($"   {c.Brand} {c.Model} ({c.FrameNumber})");

            Console.WriteLine("\nRegistration history of car VIN1:");
            foreach (Registration r in channel.GetAllCarRegistrations(c1))
                Console.WriteLine($"   No.{r.RegistrationNumber} | {r.Person.Name} {r.Person.Surname} | {r.RegistrationDate.ToShortDateString()} - {r.ExpirationDate.ToShortDateString()}");

            Console.WriteLine("\nAll cars and their registrations:");
            foreach (Registration c in channel.GetAllCars())
            {
                Console.WriteLine($"   {c.Car.Brand} {c.Car.Model} ({c.Car.FrameNumber}):");
                foreach (Registration r in channel.GetAllCarRegistrations(c.Car))
                    Console.WriteLine($"      No.{r.RegistrationNumber} | {r.Person.Name} {r.Person.Surname} | {r.RegistrationDate.ToShortDateString()} - {r.ExpirationDate.ToShortDateString()}");
            }

            factory.Close();
            Console.WriteLine("\nDone. Press ENTER.");
            Console.ReadLine();
        }
    }
}