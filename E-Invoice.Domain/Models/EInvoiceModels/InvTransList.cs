using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Invoice.Domain.Models
{
    public class InvTransList
    {
        public double Doc_No { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public List<validationErrors> errors { get; set; }
    }
}