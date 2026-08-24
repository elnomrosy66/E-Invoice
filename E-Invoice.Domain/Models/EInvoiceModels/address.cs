using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models
{
    public class address
    {
        public string branchId { get; set; }
        public string country { get; set; }
        public string governate { get; set; }
        public string regionCity { get; set; }
        public string street { get; set; }
        public string buildingNumber { get; set; }
        //Optional
        public string postalCode { get; set; }
        //Optional
        public string floor { get; set; }
        //Optional
        public string room  { get; set; }
        //Optional
        public string landmark { get; set; }
        //Optional
        public string additionalInformation { get; set; }
    }
}
