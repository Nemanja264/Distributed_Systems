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
    public class Person
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Surname { get; set; }
        [DataMember] public string JMBG { get; set; }
    }
    [DataContract]
    public class Registration
    {
        [DataMember] public Person Person { get; set; }
        [DataMember] public Car Car { get; set; }
        [DataMember] public DateTime RegistrationDate { get; set; } = DateTime.Now;
        [DataMember] public DateTime ExpirationDate { get; set; }
        [DataMember] public int RegistrationNumber { get; set; }
    }

    [DataContract]
    public class Car
    {
        [DataMember] public string Brand { get; set; }
        [DataMember] public string Model { get; set; }
        [DataMember] public string FrameNumber { get; set; }
        [DataMember] public string Color { get; set; }
        [DataMember] public DateTime RegisteredUntil { get; set; }
    }
    [ServiceContract]
    public interface IRegisterService
    {
        [OperationContract]
        bool RegisterCar(Person person, Car car);
        [OperationContract]
        List<Car> GetCarsByPerson(Person person);
        [OperationContract]
        List<Registration> GetAllCarRegistrations(Car car);
        [OperationContract]
        List<Registration> GetAllCars();
    }
}
