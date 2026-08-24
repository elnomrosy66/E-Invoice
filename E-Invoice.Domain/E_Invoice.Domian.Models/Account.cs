namespace E_Invoice.Domian.Models;

public class Account : BaseCommercialData
{
	public AccountType AccountType { get; set; }

	public CanonicalType CanonicalType { get; set; }

	public decimal CridetLimit { get; set; } = default(decimal);

	public decimal InitialCridet { get; set; } = default(decimal);

	public int ParentId { get; set; }

	public AccountNature Nature { get; set; }

	public bool HasParent { get; set; }
}
