using System.Collections.Generic;

namespace E_Invoice.Domain.Models;

public class respo
{
	public string submissionUUID { get; set; }

	public List<acceptedDocuments> acceptedDocuments { get; set; }

	public List<rejectedDocuments> rejectedDocuments { get; set; }

	public string Message { get; set; }

	public bool succeded { get; set; }
}
