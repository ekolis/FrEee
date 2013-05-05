using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// Visibility of space objects.
	/// </summary>
	public enum Visibility
	{
		/// <summary>
		/// Object was never visible.
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// Object was once visible but is now invisible.
		/// </summary>
		Fogged,
		/// <summary>
		/// Object is currently visible.
		/// </summary>
		Visible,
		/// <summary>
		/// Object is currently visible and scanned.
		/// </summary>
		Scanned
	}
}
