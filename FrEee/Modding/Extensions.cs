using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	// TODO - just put these methods on the Field class!
	public static class Extensions
	{
		/// <summary>
		/// Parses a field as a double and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public static double DoubleValue(this Field f, Record rec)
		{
			double d;
			if (!double.TryParse(f.Value, out d))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + f.Value + "\" as a double.", Mod.CurrentFileName, rec, f));
			return d;
		}

		/// <summary>
		/// Parses a field as an integer and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public static int IntValue(this Field f, Record rec)
		{
			int i;
			if (!int.TryParse(f.Value, out i))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + f.Value + "\" as an integer.", Mod.CurrentFileName, rec, f));
			return i;
		}

		/// <summary>
		/// Parses a field as a boolean and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public static bool BoolValue(this Field f, Record rec)
		{
			bool b;
			if (!bool.TryParse(f.Value, out b))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + f.Value + "\" as a boolean.", Mod.CurrentFileName, rec, f));
			return b;
		}

		/// <summary>
		/// Parses a field as an enum and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		public static T EnumValue<T>(this Field f, Record rec) where T : struct
		{
			T t;
			if (!Enum.TryParse<T>(f.Value, out t))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + f.Value + "\" as an enumerated value of type " + typeof(T).Name + ".", Mod.CurrentFileName, rec, f));
			return t;
		}
	}
}
