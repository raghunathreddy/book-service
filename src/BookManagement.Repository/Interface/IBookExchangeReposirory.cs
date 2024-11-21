using BookManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Repository.Interface
{
    public interface IBookExchangeReposirory
    {
        List<BookExchange> GetAllExchangedBookUser(int? user_id);
        Task<List<BookExchange>> GetAllExchangedBook();
        Task<List<BookExchangeTX>> GetAllexchangedBookTrx();

        void AddExchangedBook(BookExchange usersdetails);

        Task<List<BookExchange>> ExchangedBook(BookExchange searchbook);
    }
}
