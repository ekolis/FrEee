using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A record in a data file.
	/// </summary>
	public class Record
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

		public Field FindField(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = false)
		{
			return FindField(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		public Field FindField(IEnumerable<string> fieldNames, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = false)
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
		public bool TryFindFieldValue(string fieldName, out string value, ref int index, ICollection<DataParsingException> log = null, int startIndex = 0, bool allowSkip = false)
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
		public bool TryFindFieldValue(IEnumerable<string> fieldNames, out string value, ref int index, ICollection<DataParsingException> log = null, int startIndex = 0, bool allowSkip = false)
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

		public double GetDouble(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = false)
		{
			return GetDouble(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		public int GetInt(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = false)
		{
			return GetInt(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		public bool GetBool(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = false)
		{
			return GetBool(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		public string GetString(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = false)
		{
			return GetString(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		public T GetEnum<T>(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = false) where T : struct
		{
			return GetEnum<T>(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		public double GetDouble(IEnumerable<string> fieldNames, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = false)
		{
			var f = FindField(fieldNames, ref index, logErrors, startIndex, allowSkip);
			if (f == null)
				Mod.Errors.Add(new DataParsingException("Cannot find field \"" + fieldNames.First() + "\".", Mod.CurrentFileName, this, null));
			return f.DoubleValue(this);
		}

		public int GetInt(IEnumerable<string> fieldNames, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = false)
		{
			var f = FindField(fieldNames, ref index, logErrors, startIndex, allowSkip);
			if (f == null)
				Mod.Errors.Add(new DataParsingException("Cannot find field \"" + fieldNames.First() + "\".", Mod.CurrentFileName, this, null));
			return f.IntValue(this);
		}

		public bool GetBool(IEnumerable<string> fieldNames, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = false)
		{
			var f = FindField(fieldNames, ref index, logErrors, startIndex, allowSkip);
			if (f == null)
				Mod.Errors.Add(new DataParsingException("Cannot find field \"" + fieldNames.First() + "\".", Mod.CurrentFileName, this, null));
			return f.BoolValue(this);
		}

		public string GetString(IEnumerable<string> fieldNames, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = false)
		{
			return FindField(fieldNames, ref index, logErrors, startIndex, allowSkip).Value;
		}

		public T GetEnum<T>(IEnumerable<string> fieldNames, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = false) where T : struct
		{
			var f = FindField(fieldNames, ref index, logErrors, startIndex, allowSkip);
			if (f == null)
				Mod.Errors.Add(new DataParsingException("Cannot find field \"" + fieldNames.First() + "\".", Mod.CurrentFileName, this, null));
			return f.EnumValue<T>(this);
		}
	}
}
