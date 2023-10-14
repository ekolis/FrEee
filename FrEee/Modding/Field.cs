using FrEee.Interfaces;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FrEee.Modding
{
	/// <summary>
	/// A field in a data file.
	/// </summary>
	[Serializable]
	public class Field
	{
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
		/// The text value of the field.
		/// </summary>
		public string Value { get; set; }

		public const string Separator = ":=";

		/// <summary>
		/// Parses a field as a boolean and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		[Obsolete("Use CreateFormula instead.")]
		public bool BoolValue(Record rec)
		{
			bool b;
			if (!bool.TryParse(Value, out b))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + Value + "\" as a boolean.", The.ModFileName, rec, this));
			return b;
		}

		public Formula<T> CreateFormula<T>(object context)
					where T : IConvertible, IComparable
		{
			var txt = Value;

			// deal with replacement strings
			txt = Regex.Replace(txt, @"\[\%(.*?)\%?\]", @"{{$1}}");

			if (txt.StartsWith("=="))
				return new ComputedFormula<T>(txt.TrimStart('='), context, true); // dynamic
			else if (txt.StartsWith("="))
				return new ComputedFormula<T>(txt.TrimStart('='), context, false); // static
																	// TODO - take into account quotes when seeing if we have string interpolation?
			else if (txt.Contains("{") && txt.Substring(txt.IndexOf("{")).Contains("}"))
			{
				// string interpolation formula
				var isDynamic = txt.Contains("{{") && txt.Substring(txt.IndexOf("{{")).Contains("}}");
				var replacedText = txt;
				replacedText = "'" + replacedText + "'"; // make it a string
				replacedText = replacedText.Replace("{{", "' + str(");
				replacedText = replacedText.Replace("}}", ").strip('\"') + '"); // strip to trim start/end quotes that get added by str
				replacedText = replacedText.Replace("{", "' + str(");
				replacedText = replacedText.Replace("}", ").strip('\"') + '");
				return new ComputedFormula<T>(replacedText, context, isDynamic);
			}
			return new LiteralFormula<T>(txt);
		}

		public ObjectFormula<T> CreateObjectFormula<T>(object context)
		{
			return new ObjectFormula<T>(Value.TrimStart('='), context, true);
		}

		public ObjectFormula<T> CreateReferenceEnumerableFormula<T>(object context)
			where T : IReferenceEnumerable
		{
			var typename = typeof(T).Name;
			if (typename.Contains("`"))
				typename = typename.Substring(0, typename.IndexOf('`'));
			var f = new ObjectFormula<T>($"{typename}[{string.Join(", ", typeof(T).GetGenericArguments().Select(x => x.Name).ToArray())}]({Value.TrimStart('=')})", context, true);
			var import = $"from {typeof(T).Namespace} import {typename};";
			var imports = new List<string>();
			imports.Add(import);
			foreach (var genparm in typeof(T).GetGenericArguments())
			{
				var typename2 = genparm.Name;
				if (typename2.Contains("`"))
					typename2 = typename.Substring(0, typename2.IndexOf('`'));
				imports.Add($"from {genparm.Namespace} import {typename2};");
			}
			var script = new PythonScript("Import", string.Join("\n", imports));
			if (f.ExternalScripts == null)
				f.ExternalScripts = new PythonScript[] { script };
			else
				f.ExternalScripts = f.ExternalScripts.Append(script).ToArray();
			return f;
		}

		public PythonScript CreateScript(object context)
		{
			return new PythonScript("DynamicScript", Value.TrimStart('='));
		}

		/// <summary>
		/// Parses a field as a double and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		[Obsolete("Use CreateFormula instead.")]
		public double DoubleValue(Record rec)
		{
			double d;
			if (!double.TryParse(Value, out d))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + Value + "\" as a double.", The.ModFileName, rec, this));
			return d;
		}

		/// <summary>
		/// Parses a field as an enum and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		[Obsolete("Use CreateFormula instead.")]
		public T EnumValue<T>(Record rec) where T : struct
		{
			T t;
			if (!Enum.TryParse<T>(Value, out t))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + Value + "\" as an enumerated value of type " + typeof(T).Name + ".", The.ModFileName, rec, this));
			return t;
		}

		/// <summary>
		/// Parses a field as an integer and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		[Obsolete("Use CreateFormula instead.")]
		public int IntValue(Record rec)
		{
			int i;
			if (!int.TryParse(Value, out i))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + Value + "\" as an integer.", The.ModFileName, rec, this));
			return i;
		}

		/// <summary>
		/// Parses a field as a long and logs any error in the mod loading error log.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		[Obsolete("Use CreateFormula instead.")]
		public long LongValue(Record rec)
		{
			long l;
			if (!long.TryParse(Value, out l))
				Mod.Errors.Add(new DataParsingException("Cannot parse \"" + Value + "\" as a long.", The.ModFileName, rec, this));
			return l;
		}

		/// <summary>
		/// Parses a field as a nullable boolean.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		[Obsolete("Use CreateFormula instead.")]
		public bool? NullBoolValue()
		{
			bool b;
			if (!bool.TryParse(Value, out b))
				return null;
			return b;
		}

		/// <summary>
		/// Parses a field as a nullable double.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		[Obsolete("Use CreateFormula instead.")]
		public double? NullDoubleValue()
		{
			double d;
			if (!double.TryParse(Value, out d))
				return null;
			return d;
		}

		/// <summary>
		/// Parses a field as a nullable enum.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		[Obsolete("Use CreateFormula instead.")]
		public T? NullEnumValue<T>() where T : struct
		{
			T t;
			if (!Enum.TryParse<T>(Value, out t))
				return null;
			return t;
		}

		/// <summary>
		/// Parses a field as a nullable integer.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rec"></param>
		/// <returns></returns>
		[Obsolete("Use CreateFormula instead.")]
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
		[Obsolete("Use CreateFormula instead.")]
		public long? NullLongValue()
		{
			long l;
			if (!long.TryParse(Value, out l))
				return null;
			return l;
		}

		public override string ToString()
		{
			return Name + " := " + Value;
		}
	}
}
