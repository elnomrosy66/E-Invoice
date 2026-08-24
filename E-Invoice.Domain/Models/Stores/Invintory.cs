using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domian.Models
{
    public class Invintory : Base
    {
        public int? OrderId { get; set; }
        public DateTime Date { get; set; }
        public int StoreId { get; set; }
        public int? StoreToId { get; set; }
        public int OrderNumber { get; set; } // مسلسل الفاتورة
        public int ProductUnitId { get; set; }
        public int ProductId { get; set; }
        public OrderType OrderType { get; set; }
        public DateTime? ExpierDate { get; set; }
        public DateTime? ProductionDate { get; set; }
        public double Qty { get; set; }
        //[ForeignKey(nameof(BranchId))]
        //public virtual Branch Branch { get; set; }
        //[ForeignKey(nameof(ProductUnitId))]
        //public virtual ProductUnites ProductUnit { get; set; }
        [ForeignKey(nameof(StoreId))]
        public virtual Store Store { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        public double Cost { get; set; }
        private new string Code { get => base.Code; set => base.Code = value; }

    }
}
