using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Invoice.Domain.Models
{
    public class respo
    {
        public string submissionUUID { get; set; }
        public List<acceptedDocuments> acceptedDocuments { get; set; }
        public List<rejectedDocuments> rejectedDocuments { get; set; }
        public string Message { get; set; }
        public bool succeded { get; set; }
    }
}