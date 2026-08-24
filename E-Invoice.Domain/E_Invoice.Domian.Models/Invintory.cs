using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Invoice.Domian.Models;

public class Invintory : Base
{
	public int? OrderId { get; set; }

	public DateTime Date { get; set; }

	public int StoreId { get; set; }

	public int? StoreToId { get; set; }

	public int OrderNumber { get; set; }

	public int ProductUnitId { get; set; }

	public int ProductId { get; set; }

	public OrderType OrderType { get; set; }

	public DateTime? ExpierDate { get; set; }

	public DateTime? ProductionDate { get; set; }

	public double Qty { get; set; }

	[ForeignKey("StoreId")]
	public virtual Store Store { get; set; }

	[ForeignKey("ProductId")]
	public virtual Product Product { get; set; }

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
