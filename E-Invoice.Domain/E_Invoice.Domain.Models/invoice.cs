using System.Collections.Generic;

namespace E_Invoice.Domain.Models;

public class invoice
{
	public List<signeddoc> documents { get; set; }
}
