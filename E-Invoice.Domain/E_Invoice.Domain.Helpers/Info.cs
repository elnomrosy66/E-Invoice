using System.Collections.Generic;
using E_Invoice.Domain.Models;
using E_Invoice.Domian.Models;

namespace E_Invoice.Domain.Helpers;

public static class Info
{
	public static User CurrentUser { get; set; }

	public static Branch CurrenBranch { get; set; }

	public static string Connectionstring { get; set; } = "Data Source=.\\SQLEXPRESS;Initial Catalog=InventoryModuleDB;Integrated Security=true";

	public static IEnumerable<idAndValue> ItemTypesList { get; } = new List<idAndValue>
	{
		new idAndValue
		{
			Id = 0,
			Name = "GS1"
		},
		new idAndValue
		{
			Id = 1,
			Name = "EGS"
		}
	};

	public static IEnumerable<idAndValue> CanonicalTypesList { get; } = new List<idAndValue>
	{
		new idAndValue
		{
			Id = 0,
			Name = "شخص"
		},
		new idAndValue
		{
			Id = 1,
			Name = "نشاط/شركة"
		},
		new idAndValue
		{
			Id = 2,
			Name = "أجنبي"
		}
	};

	public static setting _setting { get; set; }

	public static Company _Company { get; set; }

	public static int _SelectedProduct { get; set; }

	public static bool _ProductSelected { get; set; }

	public static int _SelectedAccount { get; set; }
}
