using AutoMapper;
using BookManagement.Service.DtoModels;
using BookManagement.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagement.Model;
using BookManagement.Repository.Implementation;
using BookManagement.Repository.Interface;

namespace BookManagement.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _booksReposiroty;
        private readonly IMapper _mapper;
        public BookService(IBookRepository booksReposiroty, IMapper mapper)
        {
            _booksReposiroty = booksReposiroty;
            _mapper = mapper;
            // _emailHelper = emailHelper;
        }

        public void AddBooks(DtoBook bookdetails)
        {
            var books = _mapper.Map<Book>(bookdetails);
            _booksReposiroty.AddBooks(books);
        }

        public List<DtoBook> SearchBook(DtoBookSearch searchbook)
        {
            var searchbooks = new Book() { title = searchbook.title, author = searchbook.author, genre = searchbook.genre };
            var result=  _booksReposiroty.SearchBook(searchbooks).Result;
            return _mapper.Map<List<DtoBook>>(result);
        }


        public List<DtoBook> GetAllBooksUser(int? user_id)
        {
            var result = _booksReposiroty.GetAllBooksUser(user_id).Result;
            return _mapper.Map<List<DtoBook>>(result);
        }

        public List<DtoBook> GetAllBooks()
        {
            var result = _booksReposiroty.GetAllBooks().Result;
            return _mapper.Map<List<DtoBook>>(result);
        }

    }
}
