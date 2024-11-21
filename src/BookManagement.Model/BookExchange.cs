using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Model
{
    public class BookExchange
    {
        public int? exchange_id { get; set; }

        public int? book_id { get; set; }
        public int? requester_id { get; set; }
        public string? request_message { get; set; }
        public int? owner_id { get; set; }
        public string? status { get; set; }
        public string? delivery_method { get; set; }
        public DateTime? exchange_date { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
