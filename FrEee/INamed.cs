using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee
{
	/// <summary>
	/// Something that has a name.
	/// </summary>
	public interface INamed
	{
		/// <summary>
		/// The name of the object.
		/// </summary>
		string Name { get; set; }
	}
}
