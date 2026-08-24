using System.Collections.Generic;

namespace E_Invoice.Domain.Models.EInvoiceModels.ResponceModels;

public class ValidationResults
{
	public string status { get; set; }

	public List<ValidationStep> validationSteps { get; set; }
}
