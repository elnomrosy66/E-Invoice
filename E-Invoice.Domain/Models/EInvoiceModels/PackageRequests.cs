using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models.EInvoiceModels
{


    public class PackageRequests
    {
        public List<Result> result { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Metadata
    {
        public int totalPages { get; set; }
        public int totalCount { get; set; }
    }

    public class QueryParams
    {
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public string statuses { get; set; }
        public string productsInternalCodes { get; set; }
        public string receiverSenderId { get; set; }
        public string receiverSenderType { get; set; }
        public string documentTypeName { get; set; }
        public string documentFormat { get; set; }
        public string branchNumber { get; set; }
        public object itemCodes { get; set; }
    }

    public class Result
    {
        public string packageId { get; set; }
        public DateTime submissionDate { get; set; }
        public int status { get; set; }
        public DateTime? deletionDate { get; set; }
        public int type { get; set; }
        public int format { get; set; }
        public string requestorUserId { get; set; }
        public string requestorTaxpayerRIN { get; set; }
        public int requestorTypeId { get; set; }
        public string requestorTaxpayerName { get; set; }
        public QueryParams queryParams { get; set; }
        public bool isExpired { get; set; }
    }

    


}
