using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility
{
	/// <summary>
	/// A scalar which can be converted easily to and from a string.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DataScalar : IData<object>
	{
		public DataScalar(object value = null)
		{
			Value = value;
		}

		public object Value { get; set; }

		public string Data
		{
			get
			{
				return Convert.ToString(Value, CultureInfo.InvariantCulture);
			}
			set
			{
				Value = Convert.ChangeType(value, typeof(object), CultureInfo.InvariantCulture);
			}
		}
	}
}
