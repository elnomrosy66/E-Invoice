namespace E_Invoice.Domain.Models.EInvoiceModels.ResponceModels;

public class PrintInvoiceResponse
{
	public byte[] Pdf { get; set; }

	public string Message { get; set; }

	public bool Success { get; set; }
}
