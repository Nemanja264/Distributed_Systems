using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Client
    {
        static void Main(string[] args)
        {
            ChannelFactory<ILibraryService> factory = new ChannelFactory<ILibraryService>("LService");
            ILibraryService proxy = factory.CreateChannel();

            proxy.AddBook("Na Drini cuprija", "Ivo Andric", "111");
            proxy.AddBook("Prokleta avlija", "Ivo Andric", "222");
            Console.WriteLine(proxy.AddBook("Duplikat", "X", "111"));  // false - ISBN postoji

            Console.WriteLine(proxy.RentBook("111", "Marko"));   // true
            Console.WriteLine(proxy.RentBook("111", "Ana"));     // false - zauzeta
            proxy.ReturnBook("111");
            Console.WriteLine(proxy.RentBook("111", "Ana"));     // true - sad prolazi

            Console.WriteLine("Iznajmljene:");
            foreach (Book b in proxy.RentedBooks())
                Console.WriteLine(b.Title + " -> " + b.RentedBy);

            Console.WriteLine("Slobodne:");
            foreach (Book b in proxy.GetAvailableBooks())
                Console.WriteLine(b.Title + " (" + b.ISBN + ")");

            Console.ReadLine();
        }
    }
}
