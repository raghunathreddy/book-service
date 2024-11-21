using AutoMapper;
using BookManagement.Model;
using BookManagement.Repository.Implementation;
using BookManagement.Repository.Interface;
using BookManagement.Service.DtoModels;
using BookManagement.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Service.Implementation
{
    public class BookExchangeService : IBookExchangeService
    {
        private readonly IBookExchangeReposirory _bookexchangeReposiroty;
        private readonly IMapper _mapper;
        public BookExchangeService(IBookExchangeReposirory bookexchangeReposiroty, IMapper mapper)
        {
            _bookexchangeReposiroty = bookexchangeReposiroty;
            _mapper = mapper;
            // _emailHelper = emailHelper;
        }
        public void AddExchangedBooks(DtoBookExchange booksdetails)
        {
            var books = _mapper.Map<BookExchange>(booksdetails);
            _bookexchangeReposiroty.AddExchangedBook(books);
        }

        public List<DtoBookExchange> RequesterExchangedBooks(DtoBookExchange searchbook)
        {
            var searchbooks = new BookExchange() { requester_id = searchbook.requester_id};
            var result = _bookexchangeReposiroty.ExchangedBook(searchbooks).Result;
            return _mapper.Map<List<DtoBookExchange>>(result);
        }

        public List<DtoBookExchange> GetAllExchangedBooks()
        {
            var result = _bookexchangeReposiroty.GetAllExchangedBook().Result;
            return _mapper.Map<List<DtoBookExchange>>(result);
        }

        public List<DtoBookExchangeTX> GetAllExchangedBooksTrx()
        {
            var result = _bookexchangeReposiroty.GetAllexchangedBookTrx().Result;
            return _mapper.Map<List<DtoBookExchangeTX>>(result);
        }


        public DtoBookExchange GetAllExchangedBooksUser(int? user_id)
        {
            var result = _bookexchangeReposiroty.GetAllExchangedBookUser(user_id);
            return _mapper.Map<DtoBookExchange>(result);
        }
    }
}
