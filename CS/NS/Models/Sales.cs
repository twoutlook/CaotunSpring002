using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NS.Models
{

    class Sales
    {
        public int Id { get; set; }
        public string SalesNum { get; set; }
        public DateTime SalesDate { get; set; }
        public string PartNum { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public string Currency { get; set; }
        public string CustomerCode { get; set; }
        public string FromWH { get; set; }
    }
}
