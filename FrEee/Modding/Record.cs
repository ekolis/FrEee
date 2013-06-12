using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A record in a data file.
	/// </summary>
	 [Serializable] public class Record
	{
		/// <summary>
		/// Creates a record with no fields.
		/// </summary>
		public Record()
		{
			Fields = new List<Field>();
		}

		/// <summary>
		/// Creates a record by parsing some string data.
		/// </summary>
		/// <param name="lines"></param>
		public Record(IEnumerable<string> lines)
			: this()
		{
			foreach (var line in lines.Where(l => l.Contains(":=")))
				Fields.Add(new Field(line, this));
		}
		
		/// <summary>
		/// The fields in this record.
		/// </summary>
		public IList<Field> Fields { get; private set; }

		public override string ToString()
		{
			if (Fields.Count > 0)
				return Fields.First().ToString() + " (" + Fields.Count + " fields)";
			return "(0 fields)";
		}

		public Field FindField(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true)
		{
			return FindField(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		public Field FindField(IEnumerable<string> fieldNames, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true)
		{
			for (var i = startIndex; i < Fields.Count; i++)
			{
				if (fieldNames.Contains(Fields[i].Name))
				{
					index = i;
					return Fields[i];
				}
				if (!allowSkip)
					break;
			}
			if (logErrors)
				Mod.Errors.Add(new DataParsingException("Could not find field: " + string.Join(", ", fieldNames.ToArray()) + ".", Mod.CurrentFileName, this));
			return null;
		}
		
		/// <summary>
		/// Tries to find a field value.
		/// </summary>
		/// <param name="fieldName">The name to search for.</param>
		/// <param name="log">If an exception needs to be logged, pass it here.</param>
		/// <param name="value">The field value.</param>
		/// <param name="index">The field index.</param>
		/// <param name="startIndex">Where to start in the field list.</param>
		/// <param name="allowSkip">Allow skipping fields to find the one we want?</param>
		/// <returns>true on success, false on failure</returns>
		public bool TryFindFieldValue(string fieldName, out string value, ref int index, ICollection<DataParsingException> log = null, int startIndex = 0, bool allowSkip = true)
		{
			return TryFindFieldValue(new string[] { fieldName }, out value, ref index, log, startIndex, allowSkip);
		}

		/// <summary>
		/// Tries to find a field value from the first of any of a number of fields.
		/// </summary>
		/// <param name="fieldNames">The name to search for.</param>
		/// <param name="log">If an exception needs to be logged, pass it here. Note that now all errors are logged to Mod.Errors.</param>
		/// <param name="value">The field value.</param>
		/// <param name="index">The field index.</param>
		/// <param name="startIndex">Where to start in the field list.</param>
		/// <param name="allowSkip">Allow skipping fields to find the one we want?</param>
		/// <returns>true on success, false on failure</returns>
		// TODO - change method signature to take a bool instead of a collection of errors
		public bool TryFindFieldValue(IEnumerable<string> fieldNames, out string value, ref int index, ICollection<DataParsingException> log = null, int startIndex = 0, bool allowSkip = true)
		{
			var field = FindField(fieldNames, ref index, log != null, startIndex, allowSkip);
			if (field == null)
			{
				value = null;
				return false;
			}
			else
			{
				value = field.Value;
				return true;
			}
		}

		public double GetDouble(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true)
		{
			return GetDouble(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		public int GetInt(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true)
		{
			return GetInt(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		public bool GetBool(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true)
		{
			return GetBool(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		public string GetString(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true)
		{
			return GetString(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		public T GetEnum<T>(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true) where T : struct
		{
			return GetEnum<T>(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		public double GetDouble(IEnumerable<string> fieldNames, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true)
		{
			var f = FindField(fieldNames, ref index, logErrors, startIndex, allowSkip);
			if (f == null)
			{
				Mod.Errors.Add(new DataParsingException("Cannot find field \"" + fieldNames.First() + "\".", Mod.CurrentFileName, this, null));
				return 0;
			}
			return f.DoubleValue(this);
		}

		public int GetInt(IEnumerable<string> fieldNames, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true)
		{
			var f = FindField(fieldNames, ref index, logErrors, startIndex, allowSkip);
			if (f == null)
			{
				Mod.Errors.Add(new DataParsingException("Cannot find field \"" + fieldNames.First() + "\".", Mod.CurrentFileName, this, null));
				return 0;
			}
			return f.IntValue(this);
		}

		public bool GetBool(IEnumerable<string> fieldNames, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true)
		{
			var f = FindField(fieldNames, ref index, logErrors, startIndex, allowSkip);
			if (f == null)
			{
				Mod.Errors.Add(new DataParsingException("Cannot find field \"" + fieldNames.First() + "\".", Mod.CurrentFileName, this, null));
				return false;
			}
			return f.BoolValue(this);
		}

		public string GetString(IEnumerable<string> fieldNames, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true)
		{
			var f = FindField(fieldNames, ref index, logErrors, startIndex, allowSkip);
			if (f == null)
			{
				Mod.Errors.Add(new DataParsingException("Cannot find field \"" + fieldNames.First() + "\".", Mod.CurrentFileName, this, null));
				return null;
			}
			return f.Value;
		}

		public T GetEnum<T>(IEnumerable<string> fieldNames, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true) where T : struct
		{
			var f = FindField(fieldNames, ref index, logErrors, startIndex, allowSkip);
			if (f == null)
			{
				Mod.Errors.Add(new DataParsingException("Cannot find field \"" + fieldNames.First() + "\".", Mod.CurrentFileName, this, null));
				return default(T);
			}
			return f.EnumValue<T>(this);
		}

		public double? GetNullDouble(string fieldName, ref int index, int startIndex = 0, bool allowSkip = true)
		{
			return GetNullDouble(new string[] { fieldName }, ref index, startIndex, allowSkip);
		}

		public int? GetNullInt(string fieldName, ref int index, int startIndex = 0, bool allowSkip = true)
		{
			return GetNullInt(new string[] { fieldName }, ref index, startIndex, allowSkip);
		}

		public bool? GetNullBool(string fieldName, ref int index, int startIndex = 0, bool allowSkip = true)
		{
			return GetNullBool(new string[] { fieldName }, ref index, startIndex, allowSkip);
		}

		public string GetNullString(string fieldName, ref int index, int startIndex = 0, bool allowSkip = true)
		{
			return GetNullString(new string[] { fieldName }, ref index, startIndex, allowSkip);
		}

		public T? GetNullEnum<T>(string fieldName, ref int index, int startIndex = 0, bool allowSkip = true) where T : struct
		{
			return GetNullEnum<T>(new string[] { fieldName }, ref index, startIndex, allowSkip);
		}

		public double? GetNullDouble(IEnumerable<string> fieldNames, ref int index, int startIndex = 0, bool allowSkip = true)
		{
			var f = FindField(fieldNames, ref index, false, startIndex, allowSkip);
			if (f == null)
				return null;
			return f.NullDoubleValue();
		}

		public int? GetNullInt(IEnumerable<string> fieldNames, ref int index, int startIndex = 0, bool allowSkip = true)
		{
			var f = FindField(fieldNames, ref index, false, startIndex, allowSkip);
			if (f == null)
				return null;
			return f.NullIntValue();
		}

		public bool? GetNullBool(IEnumerable<string> fieldNames, ref int index, int startIndex = 0, bool allowSkip = true)
		{
			var f = FindField(fieldNames, ref index, false, startIndex, allowSkip);
			if (f == null)
				return null;
			return f.NullBoolValue();
		}

		public string GetNullString(IEnumerable<string> fieldNames, ref int index, int startIndex = 0, bool allowSkip = true)
		{
			var f = FindField(fieldNames, ref index, false, startIndex, allowSkip);
			if (f == null)
				return null;
			return f.Value;
		}

		public T? GetNullEnum<T>(IEnumerable<string> fieldNames, ref int index, int startIndex = 0, bool allowSkip = true) where T : struct
		{
			var f = FindField(fieldNames, ref index, false, startIndex, allowSkip);
			if (f == null)
				return null;
			return f.NullEnumValue<T>();
		}
	}
}
