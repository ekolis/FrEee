using FrEee.Extensions;
using System;

namespace FrEee.Utility;

/// <summary>
///  A generic two dimensional vector.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Vector2<T> : IEquatable<Vector2<T>>
{
	public Vector2()
		: this(default(T), default(T))
	{
	}

	public Vector2(T x, T y)
	{
		X = x;
		Y = y;
	}

	public Vector2(Vector2<T> v)
		: this(v.X, v.Y)
	{
	}

	public T X { get; set; }
	public T Y { get; set; }

	public bool Equals(Vector2<T> other)
	{
		return X.SafeEquals(other.X) && Y.SafeEquals(other.Y);
	}
}