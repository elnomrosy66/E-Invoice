using System;

namespace E_Invoice.Domain.Services;

public class result
{
	public string publicUrl { get; set; }

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

	public decimal totalSales { get; set; }

	public decimal totalDiscount { get; set; }

	public decimal netAmount { get; set; }

	public decimal total { get; set; }

	public string status { get; set; }

	public string cancelRequestDate { get; set; }

	public string rejectRequestDate { get; set; }

	public string cancelRequestDelayedDate { get; set; }

	public string rejectRequestDelayedDate { get; set; }

	public string declineCancelRequestDate { get; set; }

	public string declineRejectRequestDate { get; set; }
}
