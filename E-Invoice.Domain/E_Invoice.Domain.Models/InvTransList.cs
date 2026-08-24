using System.Collections.Generic;

namespace E_Invoice.Domain.Models;

public class InvTransList
{
	public double Doc_No { get; set; }

	public string Message { get; set; }

	public bool Status { get; set; }

	public List<validationErrors> errors { get; set; }
}
