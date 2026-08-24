using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models
{
    public class taxTotals
    {
        public string taxType { get; set; }
        public decimal amount { get; set; }
    }
}
