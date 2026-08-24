using System;

namespace E_Invoice.Domain.Models.EInvoiceModels;

public class Result
{
	public string packageId { get; set; }

	public DateTime submissionDate { get; set; }

	public int status { get; set; }

	public DateTime? deletionDate { get; set; }

	public int type { get; set; }

	public int format { get; set; }

	public string requestorUserId { get; set; }

	public string requestorTaxpayerRIN { get; set; }

	public int requestorTypeId { get; set; }

	public string requestorTaxpayerName { get; set; }

	public QueryParams queryParams { get; set; }

	public bool isExpired { get; set; }
}
