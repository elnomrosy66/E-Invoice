using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace E_Invoice.Domain.Helpers;

public class CollectionFilterer
{
	public async Task<IEnumerable<T>> Filter<T>(IEnumerable<T> items, string filter)
	{
		ScriptOptions options = ScriptOptions.Default.AddReferences(typeof(T).Assembly);
		return items.Where(await CSharpScript.EvaluateAsync<Func<T, bool>>(filter, options));
	}
}
