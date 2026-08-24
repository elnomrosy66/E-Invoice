using E_Invoice.Domian.Models;

namespace E_Invoice.Domain.Models;

public class JournalDetails : Base
{
	public int JournalId { get; set; }

	public double Depet { get; set; }

	public double Credit { get; set; }

	public double AccountId { get; set; }

	public string Descreption { get; set; }

	public int CostCenter { get; set; }
}
