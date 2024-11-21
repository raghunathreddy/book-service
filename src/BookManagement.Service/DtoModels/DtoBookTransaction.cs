using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Service.DtoModels
{
    public class DtoBookTransaction
    {
        public int? Book_transactionid { get; set; }
        public int? Book_Id { get; set; }
        public string Book_Name { get; set; }
        public DateTime? transaction_date { get; set; }
        public string transaction_status { get; set; }
    }
}
