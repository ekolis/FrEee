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

		public static bool operator ==(ObjectLocation<T> l1, ObjectLocation<T> l2)
		{
			if ((object)l1 == null && (object)l2 == null)
				return true;
			if ((object)l1 == null || (object)l2 == null)
				return false;
			if (l1.Item == null && l2.Item == null)
				return l1.Location.Equals(l2.Location);
			if (l1.Item == null || l2.Item == null)
				return false;
			return l1.Item.Equals(l2.Item) && l1.Location.Equals(l2.Location);
		}

		public static bool operator !=(ObjectLocation<T> l1, ObjectLocation<T> l2)
		{
			return !(l1 == l2);
		}

		public override int GetHashCode()
		{
			return HashCodeMasher.Mash(Item, Location);
		}

		public override string ToString()
		{
			return "(" + Location.X + ", " + Location.Y + ")" + ": " + Item;
		}

		public override bool Equals(object obj)
		{
			if (obj is ObjectLocation<T>)
				return this == (ObjectLocation<T>)obj;
			return false;
		}
	}
}
