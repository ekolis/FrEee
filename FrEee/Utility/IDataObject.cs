using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility
{
	/// <summary>
	/// Something which can get data about itself and reconstitute itself from said data.
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
