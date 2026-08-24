using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domian.Models
{
    public class Branch : BaseObj
    {
        //public int CompanyId { get; set; }
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company Company { get; set; }

        // ignore inheretance of isCustomer from BaseObj class
        //private new bool isCustomer { get => base.isCustomer; set => base.isCustomer = value; }
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


    }
}
