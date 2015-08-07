using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility
{
	/// <summary>
	/// Something which can get data about itself and reconstitute itself from said data.
	/// It is probably best to implement this interface only on classes and not on interfaces,
	/// so as to avoid forcing class authors to implement this interface if they don't want to.
	/// Derived classes... well, we can't do much about that!
	/// (Don't forget to make the implementation of IDataObject virtual just in case!)
	/// </summary>
	public interface IDataObject
	{
		/// <summary>
		/// When retrived, pulls in any data needed to reconstitute this object.
		/// When set, reconstitutes the object from the data being assigned.
		/// </summary>
		IDictionary<string, object> Data { get; set; }
	}
}
