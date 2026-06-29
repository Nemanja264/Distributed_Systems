using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class LibraryService : ILibraryService
    {
        private List<Book> _books = new List<Book>();
        public bool AddBook(string name, string author, string ISBN)
        {
            if(_books.Any(b => string.Equals(b.ISBN, ISBN, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Book already exist");
                return false;
            }

            _books.Add(new Book { Author= author, ISBN=ISBN , Title = name});
            Console.WriteLine("Book added");
            return true;
        }

        public List<Book> GetAvailableBooks()
        {
            return _books.Where(b => b.RentedBy==null).ToList();
        }

        public bool RentBook(string ISBN, string name)
        {
            Book b = _books.Find(bk => string.Equals(bk.ISBN, ISBN));
            if (b != null && b.RentedBy == null)
            {
                b.RentedBy = name;
                return true;
            }

            return false;
        }

        public List<Book> RentedBooks()
        {
            return _books.Where(b => b.RentedBy!=null).ToList();
        }

        public bool ReturnBook(string ISBN)
        {
            Book b = _books.Find(bk => string.Equals(bk.ISBN, ISBN));
            if (b != null)
            {
                b.RentedBy = null;
                return true;
            }

            return false;
        }
    }
}
