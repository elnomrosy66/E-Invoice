using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Invoice.Domain.Models
{
    public class invoice
    {
        public List<signeddoc> documents { get; set; }
    }
}