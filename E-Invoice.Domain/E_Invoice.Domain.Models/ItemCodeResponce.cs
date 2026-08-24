using System.Collections.Generic;

namespace E_Invoice.Domain.Models;

public class ItemCodeResponce
{
	public int passedItemsCount { get; set; }

	public List<FailedItem> failedItems { get; set; }

	public List<PassedItem> passedItems { get; set; }
}
