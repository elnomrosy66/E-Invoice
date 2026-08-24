using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models
{
    public class documents
    {
        public issuer issuer { get; set; }
        public receiver receiver { get; set; }
        public string documentType { get; set; }
        public string documentTypeVersion { get; set; }
        public string dateTimeIssued { get; set; }
        public string taxpayerActivityCode { get; set; }
        public string internalID { get; set; }
        public string purchaseOrderReference { get; set; }
        public string purchaseOrderDescription { get; set; }
        public string salesOrderReference { get; set; }
        public string salesOrderDescription { get; set; }
        public string proformaInvoiceNumber { get; set; }
        public List<string> references { get; set; }
        public payment payment { get; set; }
        public delivery delivery { get; set; }
        public List<invoiceLines> invoiceLines { get; set; }
        public decimal totalSalesAmount { get; set; }
        public decimal totalDiscountAmount { get; set; }
        public decimal netAmount { get; set; }
        public List<taxTotals> taxTotals { get; set; }
        public decimal extraDiscountAmount { get; set; }
        public decimal totalItemsDiscountAmount { get; set; }
        public decimal totalAmount { get; set; }
        
    }
}
