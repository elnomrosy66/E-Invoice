using System.Collections.Generic;

namespace E_Invoice.Domain.Models.EInvoiceModels;

public class PackageRequests
{
	public List<Result> result { get; set; }

	public Metadata metadata { get; set; }
}
