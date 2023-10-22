using FrEee.Extensions;
using System.Collections;

namespace FrEee.Utility
{
	/// <summary>
	/// Mashes objects' hash codes together, using zeroes for nulls.
	/// </summary>
	public static class HashCodeMasher
	{
		public static int Mash(params object[] objs)
		{
			return MashList(objs);
		}

		public static int MashList(IEnumerable objs)
		{
			if (objs == null)
				return 0;
			var h = 0;
			foreach (var obj in objs)
				h ^= obj.GetSafeHashCode();
			return h;
		}
	}
}