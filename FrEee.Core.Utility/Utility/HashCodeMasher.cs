using FrEee.Extensions;
using System.Collections;
using FrEee.Utility;
using FrEee.Utility;

namespace FrEee.Utility;

/// <summary>
/// Mashes objects' hash codes together, using zeroes for nulls.
/// </summary>
public static class HashCodeMasher
{
	public static int Mash(params object?[] objs)
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

	/// <summary>
	/// Returns an object's hash code, or 0 for null.
	/// </summary>
	/// <param name="o"></param>
	public static int GetSafeHashCode(this object o)
	{
		return o == null ? 0 : o.GetHashCode();
	}
}
