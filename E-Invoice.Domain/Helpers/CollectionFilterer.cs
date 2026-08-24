using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace E_Invoice.Domain.Helpers
{
	public class CollectionFilterer
	{
		public async Task<IEnumerable<T>> Filter<T>(IEnumerable<T> items, string filter)
		{
			//var discountFilter = "album => album.Quantity > 0";
			var options = ScriptOptions.Default.AddReferences(typeof(T).Assembly);

			Func<T, bool> filterExpression = await CSharpScript.EvaluateAsync<Func<T, bool>>(filter, options);

			var filteredItems = items.Where(filterExpression);
			return filteredItems;
		}
	}
}
