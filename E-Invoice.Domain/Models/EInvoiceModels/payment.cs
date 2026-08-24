using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models
{
    public class payment
    {
        // all optional
        public string bankName { get; set; }
        public string bankAddress { get; set; }
        public string bankAccountNo { get; set; }
        public string bankAccountIBAN { get; set; }
        public string swiftCode { get; set; }
        public string terms { get; set; }
    }
}
