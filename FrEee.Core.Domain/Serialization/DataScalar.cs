using FrEee.Extensions;
using FrEee.Utility;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace FrEee.Serialization;

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

/// <summary>
/// A scalar which can be converted easily to and from a string.
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public class DataScalar<T> : IDataScalar
{
	public DataScalar(T value = default(T))
	{
		Value = value;
	}

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

	[JsonIgnore]
	public T Value { get; set; }

	object IData.Value { get { return Value; } }

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
