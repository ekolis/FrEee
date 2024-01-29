using FrEee.Extensions;
using System;
using System.Numerics;

namespace FrEee.Utility;

/// <summary>
///  A generic two dimensional vector.
/// </summary>
/// <typeparam name="T"></typeparam>
public record Vector2<T>(T X, T Y)
	: IEquatable<Vector2<T>>
	where T : INumber<T>, ISignedNumber<T>
{
	/// <summary>
	/// Creates a vector with both values initialized to zero.
	/// </summary>
	public Vector2() : this(T.Zero, T.Zero) { }

	/// <summary>
	/// Copies a vector.
	/// </summary>
	/// <param name="other">The vector to copy.</param>
	public Vector2(Vector2<T> other) : base()
	{
		X = other.X;
		Y = other.Y;
	}

	/// <summary>
	/// Length of this vector moving in 8 directions along the grid.
	/// </summary>
	public T LengthEightWay
	{
		get
		{
			var dx = T.Abs(X);
			var dy = T.Abs(Y);
			return T.Max(dx, dy);
		}
	}

	/// <summary>
	/// Length of this vector moving only in the 4 cardinal directions.
	/// </summary>
	public T LengthManhattan
	{
		get
		{
			var dx = T.Abs(X);
			var dy = T.Abs(Y);
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
	public static Vector2<T> InterpolateEightWay(Vector2<T> start, Vector2<T> end, T distance, Func<Vector2<T>, bool> skip = null)
	{
		if (distance <= T.Zero)
			return start;
		var trip = end - start;
		var tripLength = trip.LengthEightWay;
		if (distance >= tripLength)
			return end;
		var dx = trip.X;
		var dy = trip.Y;
		var pos = new Vector2<T>(start);
		var remainingDistance = distance;
		while (remainingDistance > T.Zero)
		{
			if (dx == T.Zero && dy == T.Zero)
				break;
			if (dx != T.Zero)
			{
				pos = new(pos.X + TSign(dx), pos.Y);
				dx -= TSign(dx);
			}
			if (dy != T.Zero)
			{
				pos = new(pos.X, pos.Y + TSign(dy));
				dy -= TSign(dy);
			}
			remainingDistance--;
		}
		if (skip != null && skip(pos))
			return InterpolateEightWay(start, end, distance - T.One, skip); // TODO - find alternate endpoint rather than just stopping the journey one tile short
		return pos;
	}

	public static Vector2<T> operator -(Vector2<T> v)
	{
		return new Vector2<T>(-v.X, -v.Y);
	}

	public static Vector2<T> operator -(Vector2<T> v1, Vector2<T> v2)
	{
		return v1 + -v2;
	}

	public static Vector2<T> operator *(Vector2<T> v, T s)
	{
		return new Vector2<T>(v.X * s, v.Y * s);
	}

	public static Vector2<T> operator *(T s, Vector2<T> v)
	{
		return v * s;
	}

	public static Vector2<T> operator /(Vector2<T> v, T s)
	{
		return new Vector2<T>(v.X / s, v.Y / s);
	}

	public static Vector2<T> operator +(Vector2<T> v1, Vector2<T> v2)
	{
		return new Vector2<T>(v1.X + v2.X, v1.Y + v2.Y);
	}

	public T DistanceToEightWay(Vector2<T> dest)
	{
		return (dest - this).LengthEightWay;
	}

	public T DistanceToManhattan(Vector2<T> dest)
	{
		return (dest - this).LengthManhattan;
	}

	public override int GetHashCode()
	{
		return HashCodeMasher.Mash(X, Y);
	}

	public override string ToString()
	{
		return $"({X}, {Y})";
	}

	private static T TSign(T num)
	{
		if (num < T.Zero)
		{
			return T.NegativeOne;
		}
		else if (num > T.Zero)
		{
			return T.One;
		}
		else
		{
			return T.Zero;
		}
	}
}