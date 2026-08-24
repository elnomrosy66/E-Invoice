namespace E_Invoice.Domian.Models;

public class Store : BaseName
{
	private new string Notes
	{
		get
		{
			return base.Notes;
		}
		set
		{
			base.Notes = value;
		}
	}
}
