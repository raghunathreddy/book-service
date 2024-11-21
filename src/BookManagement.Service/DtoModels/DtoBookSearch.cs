using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Service.DtoModels
{
    public class DtoBookSearch
    {
        public string? title { get; set; }
        public string? author { get; set; }
        public string? genre { get; set; }
    }
}
