using Microsoft.AspNetCore.Mvc;
using BookManagement.Service.DtoModels;
using BookManagement.Service.Interface;
using BookManagement.Service.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookManagement.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _booksService;
        public BooksController(IBookService bookservice)
        {
            _booksService = bookservice;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public List<DtoBook> Get()
        {
            return _booksService.GetAllBooks();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpPost]
        public List<DtoBook> Getuser(int? user_id)
        {
            return _booksService.GetAllBooksUser(user_id);
        }

        [HttpPost]
        public List<DtoBook> SearchBook([FromBody] DtoBookSearch searchbook)
        {
            return _booksService.SearchBook(searchbook);
        }



        // POST api/<UsersController>
        [HttpPost]
        public void AddBoks([FromBody] DtoBook userdetails)
        {
            _booksService.AddBooks(userdetails);
        }

       
    }
}
