using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace FrEee.Utility
{
	internal class SequenceEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
	{
		public bool Equals(IEnumerable<T>? x, IEnumerable<T>? y)
		{
			if (x.Count() != y.Count())
				return false;
			for (int i = 0; i < x.Count(); i++)
			{
				if (!x.ElementAt(i).Equals(y.ElementAt(i)))
					return false;
			}
			return true;
		}

		public int GetHashCode(IEnumerable<T> obj)
		{
			var hash = 0;
			foreach (var item in obj)
				hash ^= obj.GetHashCode();
			return hash;
		}
	}
}
