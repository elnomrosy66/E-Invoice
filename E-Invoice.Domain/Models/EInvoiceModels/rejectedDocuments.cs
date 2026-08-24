using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Invoice.Domain.Models
{
    public class rejectedDocuments
    {
        public string internalId { get; set; }
        public error error { get; set; }
    }
}