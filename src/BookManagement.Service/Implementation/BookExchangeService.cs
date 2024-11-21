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
using System.Text.Json;
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
            Random random = new Random();

            // Generate a random integer between 0 and 100 (exclusive)
            int randomNumber = random.Next(0, 1001);
            var booktransaction = new DtoBookTransaction()
            {
                Book_transactionid= randomNumber,
                Book_Id = searchbook.book_id,
                //Book_Name=searchbook.na
                transaction_date = DateTime.Now,
                transaction_status="Requested"
            };
            SendRequestThansactionserviceAsync(booktransaction);
            var searchbooks = new BookExchange() { requester_id = searchbook.requester_id};
            var result = _bookexchangeReposiroty.ExchangedBook(searchbooks).Result;
            return _mapper.Map<List<DtoBookExchange>>(result);
        }
        private async  Task SendRequestThansactionserviceAsync(DtoBookTransaction transactionhistory)
        {

            // Create an instance of HttpClient
            using var client = new HttpClient();

            // Define the API URL
            string url = "http://localhost:8080/api/BookTransaction/AddBookTransaction";

            // Serialize the C# object to JSON
            string jsonData = JsonSerializer.Serialize(transactionhistory);

            // Create the content for the POST request
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Send the POST request
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Data: " + responseContent);
            }
            else
            {
                Console.WriteLine("Request failed with status: " + response.StatusCode);
            }

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
