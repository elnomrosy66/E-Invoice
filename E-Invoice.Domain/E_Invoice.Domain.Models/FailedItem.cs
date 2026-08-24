using System.Collections.Generic;

namespace E_Invoice.Domain.Models;

public class FailedItem
{
	public int index { get; set; }

	public List<string> errors { get; set; }
}
