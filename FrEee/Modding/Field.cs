using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A field in a data file.
	/// </summary>
	 [Serializable] public class Field
	{
		public const string Separator = ":=";

		/// <summary>
		/// Creates a field with no name or value.
		/// </summary>
		public Field()
		{
		}

		/// <summary>
		/// Creates a field by parsing some string data.
		/// </summary>
		/// <param name="data"></param>
		public Field(string data, Record parent = null)
		{
			var pos = data.IndexOf(Separator);
			Name = data.Substring(0, pos).Trim();
			Value = data.Substring(pos + Separator.Length).Trim();
		}

		/// <summary>
		/// The name of the field.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The value of the field.
		/// </summary>
		public string Value { get; set; }

		public override string ToString()
		{
			return Name + " := " + Value;
		}

		/// <summary>
		/// Parses a field as a double and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public double DoubleValue(Record rec)
		{
			double d;
			if (!double.TryParse(Value, out d))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + Value + "\" as a double.", Mod.CurrentFileName, rec, this));
			return d;
		}

		/// <summary>
		/// Parses a field as an integer and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public int IntValue(Record rec)
		{
			int i;
			if (!int.TryParse(Value, out i))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + Value + "\" as an integer.", Mod.CurrentFileName, rec, this));
			return i;
		}

		/// <summary>
		/// Parses a field as a long and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public long LongValue(Record rec)
		{
			long l;
			if (!long.TryParse(Value, out l))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + Value + "\" as a long.", Mod.CurrentFileName, rec, this));
			return l;
		}

		/// <summary>
		/// Parses a field as a boolean and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public bool BoolValue(Record rec)
		{
			bool b;
			if (!bool.TryParse(Value, out b))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + Value + "\" as a boolean.", Mod.CurrentFileName, rec, this));
			return b;
		}

		/// <summary>
		/// Parses a field as an enum and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public T EnumValue<T>(Record rec) where T : struct
		{
			T t;
			if (!Enum.TryParse<T>(Value, out t))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + Value + "\" as an enumerated value of type " + typeof(T).Name + ".", Mod.CurrentFileName, rec, this));
			return t;
		}

		/// <summary>
		/// Parses a field as a nullable double.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public double? NullDoubleValue()
		{
			double d;
			if (!double.TryParse(Value, out d))
				return null;
			return d;
		}

		/// <summary>
		/// Parses a field as a nullable integer.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public int? NullIntValue()
		{
			int i;
			if (!int.TryParse(Value, out i))
				return null;
			return i;
		}

		/// <summary>
		/// Parses a field as a nullable long.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public long? NullLongValue()
		{
			long l;
			if (!long.TryParse(Value, out l))
				return null;
			return l;
		}

		/// <summary>
		/// Parses a field as a nullable boolean.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public bool? NullBoolValue()
		{
			bool b;
			if (!bool.TryParse(Value, out b))
				return null;
			return b;
		}

		/// <summary>
		/// Parses a field as a nullable enum.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public T? NullEnumValue<T>() where T : struct
		{
			T t;
			if (!Enum.TryParse<T>(Value, out t))
				return null;
			return t;
		}
	}
}
