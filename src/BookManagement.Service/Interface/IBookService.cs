using BookManagement.Service.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Service.Interface
{
    public interface IBookService
    {
        public List<DtoBook> GetAllBooksUser(int? user_id);

        public List<DtoBook> SearchBook(DtoBookSearch searchbook);
        public List<DtoBook> GetAllBooks();

        void AddBooks(DtoBook booksdetails);
    }
}
