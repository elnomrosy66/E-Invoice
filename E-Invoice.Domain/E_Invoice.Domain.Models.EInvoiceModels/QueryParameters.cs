using System.Collections.Generic;

namespace E_Invoice.Domain.Models.EInvoiceModels;

public class QueryParameters
{
	public string dateFrom { get; set; }

	public string dateTo { get; set; }

	public List<string> statuses { get; set; }

	public List<string> productsInternalCodes { get; set; }

	public string receiverSenderId { get; set; }

	public string receiverSenderType { get; set; }

	public List<string> documentTypeNames { get; set; }
}
