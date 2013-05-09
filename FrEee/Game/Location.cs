using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// An item and its location.
	/// </summary>
	public class ObjectLocation<T>
	{
		public Point Location { get; set; }
		public T Item { get; set; }
	}
}
