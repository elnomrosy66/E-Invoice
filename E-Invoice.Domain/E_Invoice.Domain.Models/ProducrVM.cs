using System.ComponentModel;

namespace E_Invoice.Domain.Models;

public class ProducrVM
{
	[DisplayName("المعرف")]
	public double Id { get; set; }

	[DisplayName("الباركود")]
	public string Barcode { get; set; }

	[DisplayName("الصنف")]
	public string Name { get; set; }

	[DisplayName("الوحدة")]
	public string Unit { get; set; }

	[DisplayName("السعر")]
	public decimal Price { get; set; }

	[DisplayName("الكود العالمي")]
	public string Code { get; set; }
}
