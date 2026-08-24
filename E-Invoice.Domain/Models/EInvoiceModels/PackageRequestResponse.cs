using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models.EInvoiceModels
{
    public class PackageRequestResponse
    {
        public string requestId { get; set; }
        public error error { get; set; }
    }
}
