using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domian.Models
{
  public  class Account: BaseCommercialData
    {
        //[ForeignKey(nameof(BranchId))]
        //public virtual Branch Branch { get; set; }

        public AccountType AccountType { get; set; }
        public CanonicalType CanonicalType { get; set; }
        public decimal CridetLimit { get; set; } = 0;
        public decimal InitialCridet { get; set; } = 0;
        /// </summary>
        public int ParentId { get; set; }
        public AccountNature Nature { get; set; }
        public bool HasParent { get; set; }
        /// زاد مدين نقص دائن


    }
}
