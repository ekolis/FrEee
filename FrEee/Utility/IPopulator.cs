using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility
{
	public interface IPopulator
	{
		/// <summary>
		/// Performs potentially expensive lookup logic to retrieve that data which needs to be populated
		/// into the cache.
		/// </summary>
		/// <param name="context"></param>
		/// <returns>The data retrieved, or null if it was not found.</returns>
		public object? Populate(object? context);
	}
}
