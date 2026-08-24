namespace E_Invoice.Domain.Models.Accounting;

internal class ChartOfAccount
{
	public int Id { get; set; }

	public string Name { get; set; }

	public int ParentId { get; set; }

	public bool HasParent { get; set; }

	public AccountNature Nature { get; set; }
}
