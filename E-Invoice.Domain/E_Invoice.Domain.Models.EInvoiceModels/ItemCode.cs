using System;

namespace E_Invoice.Domain.Models.EInvoiceModels;

public class ItemCode
{
	public string codeType { get; set; }

	public string parentCode { get; set; }

	public string itemCode { get; set; }

	public string codeName { get; set; }

	public string codeNameAr { get; set; }

	public DateTime activeFrom { get; set; }

	public DateTime activeTo { get; set; }

	public string description { get; set; }

	public string descriptionAr { get; set; }

	public string requestReason { get; set; }
}
