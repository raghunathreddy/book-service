using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Service.DtoModels
{
    public class DtoBook
    {
        public int? book_id { get; set; }
        public int? user_id { get; set; }

        public string? title { get; set; }
        public string? author { get; set; }
        public string? genre { get; set; }
        public string? condition { get; set; }
        public string? bookAvaliable { get; set; }
        public string? AvaliableExchange { get; set; }

        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
