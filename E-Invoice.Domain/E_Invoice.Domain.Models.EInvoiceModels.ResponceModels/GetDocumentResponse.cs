using System;
using System.Collections.Generic;

namespace E_Invoice.Domain.Models.EInvoiceModels.ResponceModels;

public class GetDocumentResponse
{
	public string document { get; set; }

	public string transformationStatus { get; set; }

	public ValidationResults validationResults { get; set; }

	public int maxPercision { get; set; }

	public List<InvoiceLineItemCode> invoiceLineItemCodes { get; set; }

	public string uuid { get; set; }

	public string submissionUUID { get; set; }

	public string longId { get; set; }

	public string internalId { get; set; }

	public string typeName { get; set; }

	public string typeVersionName { get; set; }

	public string issuerId { get; set; }

	public string issuerName { get; set; }

	public string receiverId { get; set; }

	public string receiverName { get; set; }

	public DateTime dateTimeIssued { get; set; }

	public DateTime dateTimeReceived { get; set; }

	public double totalSales { get; set; }

	public double totalDiscount { get; set; }

	public double netAmount { get; set; }

	public double total { get; set; }

	public string status { get; set; }
}
