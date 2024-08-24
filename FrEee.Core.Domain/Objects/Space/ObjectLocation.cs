using System;
using System.Drawing;

namespace FrEee.Objects.Space;

/// <summary>
/// An item and its location.
/// </summary>
[Serializable]
public record ObjectLocation<T>(T Item, Point Location)
{
	public override string ToString()
	{
		return "(" + Location.X + ", " + Location.Y + ")" + ": " + Item;
	}
}