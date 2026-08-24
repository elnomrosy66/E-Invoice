namespace E_Invoice.Domain.Models;

public class DownloadPackageResponse
{
	public byte[] zip { get; set; }

	public error error { get; set; }
}
