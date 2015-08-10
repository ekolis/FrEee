using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Utility.Extensions;
using Newtonsoft.Json;

namespace FrEee.Utility
{
	/// <summary>
	/// A scalar which can be converted easily to and from a string.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DataScalar<T> : IDataScalar
	{
		public DataScalar(T value = default(T))
		{
			Value = value;
		}

		[JsonIgnore]
		public T Value { get; set; }

		object IData.Value { get { return Value; } }

		public string Data
		{
			get
			{
				return Type.Name + ":" + Convert.ToString(Value, CultureInfo.InvariantCulture);
			}
			set
			{
				var data = value.Substring(value.IndexOf(":") + 1);
				if (typeof(T).IsEnum)
					Value = (T)Enum.Parse(typeof(T), data);
				else
					Value = (T)Convert.ChangeType(data, typeof(T), CultureInfo.InvariantCulture);
			}
		}

		private SafeType Type
		{
			get
			{
				return new SafeType(typeof(T));
			}
		}
	}

	public interface IDataScalar : IData
	{

	}

	public static class DataScalar
	{
		public static IDataScalar Create<T>(T o)
		{
			if (o == null)
				return null;
			var scalarType = typeof(DataScalar<>).MakeGenericType(o.GetType());
			var scalar = scalarType.Instantiate(o);
			return (IDataScalar)scalar;
		}
	}
}
