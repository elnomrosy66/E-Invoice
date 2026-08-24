using System.Collections.Generic;

namespace E_Invoice.Domain.Services;

public class docsummary
{
	public List<result> result { get; set; }

	public metadata metadata { get; set; }

	public string error { get; set; }
}
