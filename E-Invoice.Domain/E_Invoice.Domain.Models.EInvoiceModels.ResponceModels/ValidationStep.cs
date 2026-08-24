namespace E_Invoice.Domain.Models.EInvoiceModels.ResponceModels;

public class ValidationStep
{
	public string name { get; set; }

	public string status { get; set; }

	public object error { get; set; }
}
