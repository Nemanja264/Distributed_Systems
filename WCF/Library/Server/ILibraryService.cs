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
    public class Book
    { 
        [DataMember] public string Title { get; set; }
        [DataMember] public string Author { get; set; }
        [DataMember] public string ISBN { get; set; }
        [DataMember] public string RentedBy { get; set; }
    }


    [ServiceContract]
    public interface ILibraryService
    {
        [OperationContract] bool AddBook(string name, string author, string ISBN);
        [OperationContract] bool RentBook(string ISBN, string name);
        [OperationContract] bool ReturnBook(string ISBN);
        [OperationContract] List<Book> RentedBooks();

        [OperationContract] List<Book> GetAvailableBooks();
    }
}
