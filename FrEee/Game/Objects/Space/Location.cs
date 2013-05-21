using System;
using System.Drawing;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// An item and its location.
	/// </summary>
	 [Serializable] public class ObjectLocation<T>
	{
		public Point Location { get; set; }
		public T Item { get; set; }
	}
}
