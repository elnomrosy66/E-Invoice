using System;

namespace E_Invoice.Domain.Models.EInvoiceModels;

public class QueryParams
{
	public DateTime dateFrom { get; set; }

	public DateTime dateTo { get; set; }

	public string statuses { get; set; }

	public string productsInternalCodes { get; set; }

	public string receiverSenderId { get; set; }

	public string receiverSenderType { get; set; }

	public string documentTypeName { get; set; }

	public string documentFormat { get; set; }

	public string branchNumber { get; set; }

	public object itemCodes { get; set; }
}
