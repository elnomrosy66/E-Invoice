namespace E_Invoice.Domain.Models.EInvoiceModels;

public class PackageRequest
{
	public string type { get; set; }

	public string format { get; set; }

	public QueryParameters queryParameters { get; set; }
}
