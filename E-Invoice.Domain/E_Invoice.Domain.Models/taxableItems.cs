namespace E_Invoice.Domain.Models;

public class taxableItems
{
	public string taxType { get; set; }

	public decimal amount { get; set; }

	public string subType { get; set; }

	public decimal rate { get; set; }
}
