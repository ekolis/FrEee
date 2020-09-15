using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace FrEee.Utility
{
	/// <summary>
	/// A two dimensional vector of integers.
	/// </summary>
	public class IntVector2 : Vector2<int>, IEquatable<IntVector2>
	{
		public IntVector2()
			: base()
		{
		}

		public IntVector2(int x, int y)
			: base(x, y)
		{
		}

		public IntVector2(Vector2<int> v)
			: base(v)
		{
		}

		/// <summary>
		/// Length of this vector moving in 8 directions along the grid.
		/// </summary>
		public int LengthEightWay
		{
			get
			{
				var dx = Math.Abs(X);
				var dy = Math.Abs(Y);
				return Math.Max(dx, dy);
			}
		}

		/// <summary>
		/// Length of this vector moving only in the 4 cardinal directions.
		/// </summary>
		public int LengthManhattan
		{
			get
			{
				var dx = Math.Abs(X);
				var dy = Math.Abs(Y);
				return dx + dy;
			}
		}

		/// <summary>
		/// Interpolates between two vectors.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="distance"></param>
		/// <param name="skip">Should we skip moving to the endpoint? (say because it is occupied...)</param>
		/// <returns></returns>
		public static IntVector2 InterpolateEightWay(IntVector2 start, IntVector2 end, int distance, Func<IntVector2, bool>? skip = null)
		{
			if (distance <= 0)
				return start;
			var trip = end - start;
			var tripLength = trip.LengthEightWay;
			if (distance >= tripLength)
				return end;
			var dx = trip.X;
			var dy = trip.Y;
			var pos = new IntVector2(start);
			var remainingDistance = distance;
			while (remainingDistance > 0)
			{
				if (dx == 0 && dy == 0)
					break;
				if (dx != 0)
				{
					pos.X += Math.Sign(dx);
					dx -= Math.Sign(dx);
				}
				if (dy != 0)
				{
					pos.Y += Math.Sign(dy);
					dy -= Math.Sign(dy);
				}
				remainingDistance--;
			}
			if (skip != null && skip(pos))
				return InterpolateEightWay(start, end, distance - 1, skip); // TODO - find alternate endpoint rather than just stopping the journey one tile short
			return pos;
		}

		public static IntVector2 operator -(IntVector2 v)
		{
			return new IntVector2(-v.X, -v.Y);
		}

		public static IntVector2 operator -(IntVector2 v1, IntVector2 v2)
		{
			return v1 + -v2;
		}

		public static bool operator !=(IntVector2 v1, IntVector2 v2)
		{
			return !(v1 == v2);
		}

		public static IntVector2 operator *(IntVector2 v, int s)
		{
			return new IntVector2(v.X * s, v.Y * s);
		}

		public static IntVector2 operator *(int s, IntVector2 v)
		{
			return v * s;
		}

		public static IntVector2 operator /(IntVector2 v, int s)
		{
			return new IntVector2(v.X / s, v.Y / s);
		}

		public static IntVector2 operator +(IntVector2 v1, IntVector2 v2)
		{
			return new IntVector2(v1.X + v2.X, v1.Y + v2.Y);
		}

		public static bool operator ==(IntVector2 v1, IntVector2 v2)
		{
			return v1.SafeEquals(v2);
		}

		public int DistanceToEightWay(IntVector2 dest)
		{
			return (dest - this).LengthEightWay;
		}

		public int DistanceToManhattan(IntVector2 dest)
		{
			return (dest - this).LengthManhattan;
		}

		public override bool Equals(object? obj)
		{
			if (obj is IntVector2 v)
				return this.Equals(v);
			return false;
		}

		public bool Equals(IntVector2? other)
		{
			return X == other?.X && Y == other?.Y;
		}

		public override int GetHashCode()
		{
			return HashCodeMasher.Mash(X, Y);
		}

		public override string ToString()
		{
			return $"({X}, {Y})";
		}

		public static IEnumerable<IntVector2> WithinRadius(int radius)
		{
			if (!withinRadius.ContainsKey(radius))
			{
				if (radius < 0)
					withinRadius[radius] = Enumerable.Empty<IntVector2>();
				else
				{
					var list = new List<IntVector2>();
					for (var x = -radius; x <= radius; x++)
					{
						for (var y = -radius; y <= radius; y++)
							list.Add(new IntVector2(x, y));
					}
					withinRadius.Add(radius, list);
				}
			}
			return withinRadius[radius];
		}

		private static IDictionary<int, IEnumerable<IntVector2>> withinRadius = new Dictionary<int, IEnumerable<IntVector2>>();

		public static IEnumerable<IntVector2> AtRadius(int radius)
		{
			return WithinRadius(radius).Except(WithinRadius(radius - 1));
		}
	}
}
