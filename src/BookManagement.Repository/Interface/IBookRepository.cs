using BookManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagement.Repository.Interface
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksUser(int? user_id);
        Task<List<Book>> GetAllBooks();
       
        void AddBooks(Book usersdetails);

        Task<List<Book>> SearchBook(Book searchbook);

    }
}
