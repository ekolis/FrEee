using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A field in a data file.
	/// </summary>
	public class Field
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
	}
}
