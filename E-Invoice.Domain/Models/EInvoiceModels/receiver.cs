using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models
{
    public class receiver
    {
        public string type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public address address { get; set; }
    }
}
