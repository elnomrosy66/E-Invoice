using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domian.Models
{
    public class BaseCommercialData : BaseName
    {
        public string TaxReg { get; set; }
        public string CommercialRegNo { get; set; }
        public string ActivityCode { get; set; }
        public string Country { get; set; }
        public string Governate { get; set; }
        public string RegionCity { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        //Optional
        public string PostalCode { get; set; }
        //Optional
        public string Floor { get; set; }
        //Optional
        public string Room { get; set; }
        //Optional
        public string Landmark { get; set; }
        //Optional
        public string AdditionalInformation { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


    }
}
