using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domian.Models
{
    public class Order : Base
    {
        public string OrderBarcode { get; set; } // باركود الفاتورة
        public long OrderNumber { get; set; }
        public OrderType OrderType { get; set; }

        public DateTime Date { get; set; }
        public OrderPayment OrderPayment { get; set; }
        public long? bankTreasuryId { get; set; }
        public int? AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public virtual Account account { get; set; }
        //العملات
        public long CurrencyId { get; set; }
        public double CurrencyRate { get; set; }

        public int StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }
        public long? ToStoreId { get; set; }

        public double NetBeforeTax { get; set; }//الصافي قبل الضريبة شامل الخصم
        public double TotalVat { get; set; }//الضريبة
        public double DiscountRate { get; set; }
        public double TotalDiscount { get; set; } // order details
        public double TotalExtra { get; set; }//  order details
        public double NetInvoice { get; set; } //الصافي
        public double Paid { get; set; }
        public double Rest { get; set; }
        public double TotalCost { get; set; }
        public double TotalProfit { get; set; } // اجمالي ربح الفاتورة
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public DateTime? DateSent { get; set; }
        public string Status { get; set; }
        public string uuid { get; set; }
        [Display(Name = "حالةالارسال")]
        public OrderEinvSend sent { get; set; }
        [Display(Name = "المرسل")]
        public int userSent { get; set; }
        public bool CustomerNakdy { get; set; }
        public string CustumerName { get; set; }
        public string CustumerCity { get; set; }
        public string CustumerGovernate { get; set; }
        public string CustumerStreet { get; set; }
        public string CustumerBuilding { get; set; }
        public string CustomerId { get; set; }
        public double Tax4 { get; set; }
        private new string Code { get => base.Code; set => base.Code = value; }
    }
}
