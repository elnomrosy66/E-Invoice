using E_Invoice.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models
{
    public class Journal : Base
    {
        public double Doc_No { get; set; }
        public JournalType Type { get; set; }
        public string Descreption { get; set; }
    }
}
