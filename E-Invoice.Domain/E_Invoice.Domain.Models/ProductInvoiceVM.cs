using System.Collections.Generic;

namespace E_Invoice.Domain.Models;

public class ProductInvoiceVM
{
	public int Id { get; set; }

	public string Barcode { get; set; }

	public string Name { get; set; }

	public int UnitId { get; set; }

	public string Unit { get; set; }

	public decimal UnitValue { get; set; }

	public decimal Qty { get; set; }

	public decimal UnitQty { get; set; }

	public decimal ItemTotal { get; set; }

	public decimal DiscountRate { get; set; }

	public decimal NetTotal { get; set; }

	public string Taxes { get; set; }

	public List<taxableItems> taxableItems { get; set; }
}
