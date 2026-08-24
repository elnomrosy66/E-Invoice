using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domian.Models
{
    public class OrderDetail : Base
    {
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
        public int? ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        public int ProdcutUnitId { get; set; }
        //[ForeignKey(nameof(ProdcutUnitId))]
        //public virtual ProductUnites ProductUnit { get; set; }
        public double QtyConvert { get; set; }
        public double QtySum { get; set; }  //  علشان بتاخد موجب وسالب علي حسب الفاتورة مبيعات سالب مشتريات موجب

        public double Price { get; set; }
        public double Quantity { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; }//قيمة
        public double Extra { get; set; } //مصاريف اضافية مشار اكرامية وهكذا
        public double NetBeforeTax { get; set; }//الاجمالي قبل الضريبة شامل الخصم 
        public double Vat { get; set; } // نسبة الفاتورة
        public double VatPrice { get; set; }//القيمة
        public double NetAfterTax { get; set; }//الاجمالي بعد الضريبة
        public double AvgPrice { get; set; }//سعر التكلفة
        public double TotalAvgPrice { get; set; }//الكمية * تاكنزسط
        public double Profits { get; set; }//الربح


        public DateTime? ExpireDate { get; set; }
        public DateTime? ProductionDate { get; set; }



        

        public double Cost { get; set; }
        public string ProductDesc { get; set; }

        private new string Code { get => base.Code; set => base.Code = value; }

    }
}
