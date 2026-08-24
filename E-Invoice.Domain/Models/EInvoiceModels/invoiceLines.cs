using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models
{
    public class invoiceLines
    {
        public string description { get; set; }
        public string itemType { get; set; }
        public string itemCode { get; set; }
        public string unitType { get; set; }
        public decimal quantity { get; set; }
        public unitValue unitValue { get; set; }
        public decimal salesTotal { get; set; }
        public decimal total { get; set; }
        public decimal valueDifference { get; set; }
        public decimal totalTaxableFees { get; set; }
        public decimal netTotal { get; set; }
        public decimal itemsDiscount { get; set; }
        //optional
        public discount discount { get; set; }
        //optional
        public List<taxableItems> taxableItems { get; set; }
        //optional
        public string internalCode { get; set; }
    }
}
