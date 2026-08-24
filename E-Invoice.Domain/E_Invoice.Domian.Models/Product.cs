using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Invoice.Domian.Models;

public class Product : BaseName
{
	public decimal SalePrice { get; set; }

	public decimal BuyPrice { get; set; }

	[Display(Name = "نوع التكويد")]
	public ItemType itemType { get; set; }

	[Display(Name = "الكود العالمي")]
	public string itemCode { get; set; }

	[Display(Name = "حد الطلب")]
	public int requestLimit { get; set; }

	public int CategoryId { get; set; }

	[ForeignKey("CategoryId")]
	public virtual Category Category { get; set; }

	public virtual ICollection<ProductUnites> ProductUnites { get; set; }

	public double SmallUnitCost { get; set; }

	public bool IsService { get; set; }

	public string GPCCode { get; set; }
}
