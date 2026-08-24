using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Invoice.Domain.Models
{
    public class error
    {
        public string code { get; set; }
        public string message { get; set; }
        public string target { get; set; }
        public List<error> details { get; set; }
    }
}