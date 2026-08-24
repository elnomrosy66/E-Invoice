using System.ComponentModel.DataAnnotations.Schema;

namespace E_Invoice.Domian.Models;

public class ProductUnites : Base
{
	public int ProductId { get; set; }

	[ForeignKey("ProductId")]
	public virtual Product product { get; set; }

	public int UnitId { get; set; }

	[ForeignKey("UnitId")]
	public virtual Unit Unit { get; set; }

	public decimal UnitConvert { get; set; }

	public decimal QtySmallUnit { get; set; }

	public decimal BuyPrice { get; set; }

	public decimal SellPrice { get; set; }

	public string Barcode { get; set; }

	public decimal Avg { get; set; }

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
