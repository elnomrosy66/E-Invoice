namespace E_Invoice.Domain.Models.Accounting;

internal class Austaz
{
	public int Id { get; set; }

	public double Debit { get; set; }

	public double Credit { get; set; }

	public int AccountId { get; set; }

	public string Description { get; set; }
}
