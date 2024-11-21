using BookManagement.Service.DtoModels;
using BookManagement.Service.Implementation;
using BookManagement.Service.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookManagement.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookExchangeController : ControllerBase
    {
        private readonly IBookExchangeService _bookExchangeService;

        public BookExchangeController(IBookExchangeService bookexchangeservice)
        {
            _bookExchangeService = bookexchangeservice;
        }
        // GET: api/<BookExchangeController>
        
        [HttpGet]
        public List<DtoBookExchange> Get()
        {
            return _bookExchangeService.GetAllExchangedBooks();
            //return new string[] { "value1", "value2" };
        }


        // GET api/<BookExchangeController>/5
        [HttpPost]
        public DtoBookExchange Getuser(int? user_id)
        {
            return _bookExchangeService.GetAllExchangedBooksUser(user_id);
        }


        // POST api/<BookExchangeController>
        [HttpPost]
        public List<DtoBookExchange> GetRequesterExchangedBooks([FromBody] DtoBookExchange searchbook)
        {
            return _bookExchangeService.RequesterExchangedBooks(searchbook);
        }

        // PUT api/<BookExchangeController>/5
        [HttpPost]
        public void AddBoks([FromBody] DtoBookExchange addExchangebook)
        {
            _bookExchangeService.AddExchangedBooks(addExchangebook);
        }

        [HttpGet]
        public List<DtoBookExchangeTX> GetExchangeBookdetails()
        {
            return _bookExchangeService.GetAllExchangedBooksTrx();
            //return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public List<DtoBookExchangeTX> GetBookfilterTitle(string? title)
        {
            
              var resultbooks=  _bookExchangeService.GetAllExchangedBooksTrx();
             return resultbooks.Where(x => x.title.Contains(title)).ToList();
            //return new string[] { "value1", "value2" };
        }
    }
}
