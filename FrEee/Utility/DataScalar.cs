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
	public class DataScalar<T> : IData
	{
		public DataScalar(T value = default(T))
		{
			Value = value;
		}

		public T Value { get; set; }

		public string Data
		{
			get
			{
				return Convert.ToString(Value, CultureInfo.InvariantCulture);
			}
			set
			{
				Value = (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
			}
		}
	}
}
