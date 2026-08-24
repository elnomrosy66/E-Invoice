using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Invoice.Domian.Models;

public class OrderDetail : Base
{
	public int OrderId { get; set; }

	[ForeignKey("OrderId")]
	public virtual Order Order { get; set; }

	public int? ProductId { get; set; }

	[ForeignKey("ProductId")]
	public virtual Product Product { get; set; }

	public int ProdcutUnitId { get; set; }

	public string ProductDesc { get; set; }

	public double QtyConvert { get; set; }

	public double QtySum { get; set; }

	public double Price { get; set; }

	public double Quantity { get; set; }

	public double TotalPrice { get; set; }

	public double Discount { get; set; }

	public double Extra { get; set; }

	public double NetBeforeTax { get; set; }

	public double Vat { get; set; }

	public double VatPrice { get; set; }

	public double NetAfterTax { get; set; }

	public double AvgPrice { get; set; }

	public double TotalAvgPrice { get; set; }

	public double Profits { get; set; }

	public DateTime? ExpireDate { get; set; }

	public DateTime? ProductionDate { get; set; }

	public double Cost { get; set; }

	private new string Code
	{
		get
		{
			return base.Code;
		}
		set
		{
			base.Code = value;
		}
	}
}
