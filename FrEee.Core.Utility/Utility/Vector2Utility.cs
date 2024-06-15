using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FrEee.Utility;
using FrEee.Utility;

namespace FrEee.Utility;

/// <summary>
/// Utilities for <see cref="Vector2{}"/>.
/// </summary>
public static class Vector2Utility
{
	/// <summary>
	/// Finds vectors that are within a particular radius from the origin on an integer grid.
	/// </summary>
	/// <param name="radius"></param>
	/// <returns></returns>
	public static IEnumerable<Vector2<T>> WithinRadius<T>(T radius)
		where T : IBinaryInteger<T>, ISignedNumber<T>
	{
		if (radius < T.Zero)
			yield break;
		else
		{
			for (var x = -radius; x <= radius; x++)
			{
				for (var y = -radius; y <= radius; y++)
					yield return new(x, y);
			}
		}
	}

	/// <summary>
	/// Finds vectors that are at a particular radius from the origin on an integer grid.
	/// </summary>
	/// <param name="radius"></param>
	/// <returns></returns>
	public static IEnumerable<Vector2<T>> AtRadius<T>(T radius)
		where T : IBinaryInteger<T>, ISignedNumber<T>
	{
		// TODO: improve performance by just checking edges
		return WithinRadius(radius).Except(WithinRadius(radius - T.One));
	}
}
