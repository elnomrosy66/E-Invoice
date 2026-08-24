using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models
{
    public class OrdersVM
    {
        public long Id { get; set; }
        public string Barcode { get; set; }
        public string Customer { get; set; }
        public string Date { get; set; }
        public double Total { get; set; }
        public string UUID { get; set; }
        public OrderEinvSend Sent { get; set; }
    }
}
