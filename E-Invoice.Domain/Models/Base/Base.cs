using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domian.Models
{
   public class Base
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int BranchId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public IsDelete IsDelete { get; set; }
    }
}
