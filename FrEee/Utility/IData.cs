using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility
{
	/// <summary>
	/// Either a data object, or a scalar.
	/// </summary>
	public interface IData<out T>
	{
		/// <summary>
		/// The data value, as a string which can be used to find the object or parse into the scalar.
		/// </summary>
		string Data { get; set; }

		T Value { get; }
	}
}
