using BookManagement.Service.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Service.Interface
{
    public interface IBookExchangeService
    {
        public DtoBookExchange GetAllExchangedBooksUser(int? user_id);

        public List<DtoBookExchange> RequesterExchangedBooks(DtoBookExchange searchbook);
        public List<DtoBookExchange> GetAllExchangedBooks();
        public List<DtoBookExchangeTX> GetAllExchangedBooksTrx();

        void AddExchangedBooks(DtoBookExchange booksdetails);
    }
}
