using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models.Accounting
{
    class Austaz
    {
        public int Id { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
    }
}
