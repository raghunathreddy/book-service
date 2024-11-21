using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Service.DtoModels
{
    public class DtoBookExchangeTX
    {
        public int? exchange_id { get; set; }
        public int? book_id { get; set; }
        public string? username { get; set; }
        public string? book_Owner { get; set; }
        public string? Book_exchanged_To { get; set; }
        public string? title { get; set; }
        public string? request_message { get; set; }
        public string? Requested_status { get; set; }
        public string? delivery_method { get; set; }
        public DateTime? exchange_date { get; set; }

    }
}
