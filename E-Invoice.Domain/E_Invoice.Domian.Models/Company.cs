namespace E_Invoice.Domian.Models;

public class Company : BaseCommercialData
{
	private new int BranchId
	{
		get
		{
			return base.BranchId;
		}
		set
		{
			base.BranchId = value;
		}
	}
}
