using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Invoice.Domain.Models
{
    public class recentdocs
    {
        //public List<result> data { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Datum
    {
        public string publicUrl { get; set; }
        public string uuid { get; set; }
        public string submissionUUID { get; set; }
        public string longId { get; set; }
        public string internalId { get; set; }
        public string typeName { get; set; }
        public string typeVersionName { get; set; }
        public string issuerId { get; set; }
        public string issuerName { get; set; }
        public string receiverId { get; set; }
        public string receiverName { get; set; }
        public DateTime dateTimeIssued { get; set; }
        public DateTime dateTimeReceived { get; set; }
        public double totalSales { get; set; }
        public double totalDiscount { get; set; }
        public double netAmount { get; set; }
        public double total { get; set; }
        public string status { get; set; }
        public object cancelRequestDate { get; set; }
        public object rejectRequestDate { get; set; }
        public object cancelRequestDelayedDate { get; set; }
        public object rejectRequestDelayedDate { get; set; }
        public object declineCancelRequestDate { get; set; }
        public object declineRejectRequestDate { get; set; }
    }

}