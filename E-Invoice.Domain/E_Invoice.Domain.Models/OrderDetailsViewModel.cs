namespace E_Invoice.Domain.Models;

public class OrderDetailsViewModel
{
	public string ItemId { get; set; }

	public string ItemName { get; set; }

	public string barcode { get; set; }

	public double ItemPrice { get; set; }

	public double Quantity { get; set; }

	public double Total { get; set; }
}
