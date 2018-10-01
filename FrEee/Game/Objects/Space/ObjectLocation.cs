using FrEee.Utility;
using System;
using System.Drawing;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// An item and its location.
	/// </summary>
	[Serializable]
	public class ObjectLocation<T>
	{
		public ObjectLocation()
		{
		}

		public ObjectLocation(T item, Point location)
		{
			Item = item;
			Location = location;
		}

		public T Item { get; set; }
		public Point Location { get; set; }

		public static bool operator !=(ObjectLocation<T> l1, ObjectLocation<T> l2)
		{
			return !(l1 == l2);
		}

		public static bool operator ==(ObjectLocation<T> l1, ObjectLocation<T> l2)
		{
			if (object.ReferenceEquals(l1, l2))
				return true;
			if (l1 is null || l2 is null)
				return false;
			if (object.ReferenceEquals(l1, l2))
				return l1.Location.Equals(l2.Location);
			if (object.ReferenceEquals(l1.Item, null) || object.ReferenceEquals(l2.Item, null))
				return false;
			return l1.Item.Equals(l2.Item) && l1.Location.Equals(l2.Location);
		}

		public override bool Equals(object obj)
		{
			return this == obj as ObjectLocation<T>;
		}

		public override int GetHashCode()
		{
			return HashCodeMasher.Mash(Item, Location);
		}

		public override string ToString()
		{
			return "(" + Location.X + ", " + Location.Y + ")" + ": " + Item;
		}
	}
}