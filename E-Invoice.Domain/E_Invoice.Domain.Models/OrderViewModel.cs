using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Invoice.Domain.Models;

public class OrderViewModel
{
	public int DOC_NO { get; set; }

	public int br_id { get; set; }

	[Required]
	public int CustomerId { get; set; }

	public string CustomerName { get; set; }

	public DateTime DATE { get; set; }

	public double Discount { get; set; }

	public double Total { get; set; }

	public string Notes { get; set; }

	public IEnumerable<OrderDetailsViewModel> orderDetails { get; set; }
}
