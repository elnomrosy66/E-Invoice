using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domian.Models
{
    public class Company : BaseCommercialData
    {
        

        // ignore inheretance of BranchId from base class
        private new int BranchId { get => base.BranchId; set => base.BranchId = value; }
    }
}
