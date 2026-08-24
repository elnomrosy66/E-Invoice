using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domian.Models
{
   public class ProductUnites : Base
    {
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product product { get; set; }
        public int UnitId { get; set; }
        [ForeignKey(nameof(UnitId))]
        public virtual Unit Unit { get; set; }
        public decimal UnitConvert { get; set; }//50 //20
        //rate
        public decimal QtySmallUnit { get; set; }//50, 100
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public string Barcode { get; set; }
        //احفظ المتوسط هنا
        public decimal Avg { get; set; }
        

        // ignore Code of BranchId from base class
        private new string Code { get => base.Code; set => base.Code = value; }
    }
}
