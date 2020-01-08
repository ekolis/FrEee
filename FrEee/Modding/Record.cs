using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding
{
	/// <summary>
	/// A record in a data file.
	/// </summary>
	[Serializable]
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

		/// <summary>
		/// Parameters from the mod meta templates.
		/// </summary>
		public IDictionary<string, object> Parameters { get; set; }

		public Field FindField(string fieldName, ref int index, bool logErrors = false, int startIndex = 0, bool allowSkip = true)
		{
			return FindField(new string[] { fieldName }, ref index, logErrors, startIndex, allowSkip);
		}

		/// <summary>
		/// Finds a field in the record.
		/// </summary>
		/// <param name="fieldNames"></param>
		/// <param name="index"></param>
		/// <param name="logErrors"></param>
		/// <param name="startIndex"></param>
		/// <param name="allowSkip"></param>
		/// <returns></returns>
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

		public Formula<T> Get<T>(string fieldName, object context = null, bool allowNulls = true)
			where T : IConvertible, IComparable
		{
			int index = 0;
			return Get<T>(fieldName, context, ref index, allowNulls);
		}

		public Formula<T> Get<T>(IEnumerable<string> fieldNames, object context = null, bool allowNulls = true)
			where T : IConvertible, IComparable
		{
			int index = 0;
			return Get<T>(fieldNames, context, ref index, allowNulls);
		}

		public Formula<T> Get<T>(string fieldName, object context, ref int index, bool allowNulls = true, int startIndex = 0, bool allowSkip = true)
			where T : IConvertible, IComparable
		{
			return Get<T>(new string[] { fieldName }, context, ref index, allowNulls, startIndex = 0, allowSkip = true);
		}

		public Formula<T> Get<T>(IEnumerable<string> fieldNames, object context, ref int index, bool allowNulls = true, int startIndex = 0, bool allowSkip = true)
			where T : IConvertible, IComparable
		{
			var f = FindField(fieldNames, ref index, !allowNulls, startIndex, allowSkip);
			if (f == null)
			{
				if (allowNulls)
					return null;
				else
				{
					Mod.Errors.Add(new DataParsingException("Cannot find field \"" + fieldNames.First() + "\".", Mod.CurrentFileName, this, null));
					return default(T);
				}
			}
			return f.CreateFormula<T>(context);
		}

		public ObjectFormula<T> GetObject<T>(string fieldName, object context = null, bool allowNulls = true)
		{
			int index = 0;
			return GetObject<T>(fieldName, context, ref index, allowNulls);
		}

		public ObjectFormula<T> GetObject<T>(IEnumerable<string> fieldNames, object context = null, bool allowNulls = true)
		{
			int index = 0;
			return GetObject<T>(fieldNames, context, ref index, allowNulls);
		}

		public ObjectFormula<T> GetObject<T>(string fieldName, object context, ref int index, bool allowNulls = true, int startIndex = 0, bool allowSkip = true)
		{
			return GetObject<T>(new string[] { fieldName }, context, ref index, allowNulls, startIndex = 0, allowSkip = true);
		}

		public ObjectFormula<T> GetObject<T>(IEnumerable<string> fieldNames, object context, ref int index, bool allowNulls = true, int startIndex = 0, bool allowSkip = true)
		{
			var f = FindField(fieldNames, ref index, !allowNulls, startIndex, allowSkip);
			if (f == null)
			{
				if (allowNulls)
					return null;
				else
				{
					Mod.Errors.Add(new DataParsingException("Cannot find field \"" + fieldNames.First() + "\".", Mod.CurrentFileName, this, null));
					return new ObjectFormula<T>("None", context, false);
				}
			}
			return f.CreateObjectFormula<T>(context);
		}

		public ObjectFormula<T> GetReferenceEnumerable<T>(IEnumerable<string> fieldNames, object context = null, bool allowNulls = true)
			where T : IReferenceEnumerable
		{
			int index = 0;
			return GetReferenceEnumerable<T>(fieldNames, context, ref index, allowNulls);
		}

		public ObjectFormula<T> GetReferenceEnumerable<T>(string fieldName, object context, ref int index, bool allowNulls = true, int startIndex = 0, bool allowSkip = true)
			where T : IReferenceEnumerable
		{
			return GetReferenceEnumerable<T>(new string[] { fieldName }, context, ref index, allowNulls);
		}

		public ObjectFormula<T> GetReferenceEnumerable<T>(IEnumerable<string> fieldNames, object context, ref int index, bool allowNulls = true, int startIndex = 0, bool allowSkip = true)
			where T : IReferenceEnumerable
		{
			var f = FindField(fieldNames, ref index, !allowNulls, startIndex, allowSkip);
			if (f == null)
			{
				if (allowNulls)
					return null;
				else
				{
					Mod.Errors.Add(new DataParsingException("Cannot find field \"" + fieldNames.First() + "\".", Mod.CurrentFileName, this, null));
					return new ObjectFormula<T>("None", context, false);
				}
			}
			return f.CreateReferenceEnumerableFormula<T>(context);
		}

		public IEnumerable<Formula<T>> GetMany<T>(string fieldName, object context, bool allowNulls = true)
			where T : IConvertible, IComparable
		{
			int index = 0;
			return GetMany<T>(fieldName, context, ref index, allowNulls);
		}

		public IEnumerable<Formula<T>> GetMany<T>(IEnumerable<string> fieldNames, object context, bool allowNulls = true)
			where T : IConvertible, IComparable
		{
			int index = 0;
			return GetMany<T>(fieldNames, context, ref index, allowNulls);
		}

		public IEnumerable<Formula<T>> GetMany<T>(string fieldName, object context, ref int index, bool allowNulls = true, int startIndex = 0, bool allowSkip = true)
			where T : IConvertible, IComparable
		{
			return GetMany<T>(new string[] { fieldName }, context, ref index, allowNulls, startIndex = 0, allowSkip = true);
		}

		public IEnumerable<Formula<T>> GetMany<T>(IEnumerable<string> fieldNames, object context, ref int index, bool allowNulls = true, int startIndex = 0, bool allowSkip = true)
			where T : IConvertible, IComparable
		{
			Field f;
			var result = new List<Formula<T>>();
			do
			{
				f = FindField(fieldNames, ref index, !allowNulls, startIndex, allowSkip);
				startIndex = index + 1;
				if (f != null)
					result.Add(f.CreateFormula<T>(context));
			} while (f != null);
			return result;
		}

		public PythonScript GetScript(string fieldName, object context = null, bool allowNulls = true)
		{
			int index = 0;
			return GetScript(fieldName, context, ref index, allowNulls);
		}

		public PythonScript GetScript(IEnumerable<string> fieldNames, object context = null, bool allowNulls = true)
		{
			int index = 0;
			return GetScript(fieldNames, context, ref index, allowNulls);
		}

		public PythonScript GetScript(string fieldName, object context, ref int index, bool allowNulls = true, bool allowSkip = true)
		{
			return GetScript(new string[] { fieldName }, context, ref index, allowNulls, allowSkip);
		}

		public PythonScript GetScript(IEnumerable<string> fieldNames, object context, ref int index, bool allowNulls = true, bool allowSkip = true)
		{
			Field f;
			List<Field> fs = new List<Field>();
			do
			{
				f = FindField(fieldNames, ref index, !allowNulls, index, allowSkip);
				if (f != null)
					fs.Add(f);
				index++;
			} while (f != null);
			return new PythonScript("Script", string.Join("\n", fs.Select(f2 => f2.Value)));
		}

		public IEnumerable<PythonScript> GetScripts(string fieldName, object context, bool allowNulls = true)
		{
			int index = 0;
			return GetScripts(fieldName, context, ref index, allowNulls);
		}

		public IEnumerable<PythonScript> GetScripts(IEnumerable<string> fieldNames, object context, bool allowNulls = true)
		{
			int index = 0;
			return GetScripts(fieldNames, context, ref index, allowNulls);
		}

		public IEnumerable<PythonScript> GetScripts(string fieldName, object context, ref int index, bool allowNulls = true, int startIndex = 0, bool allowSkip = true)
		{
			return GetScripts(new string[] { fieldName }, context, ref index, allowNulls, startIndex = 0, allowSkip = true);
		}

		public IEnumerable<PythonScript> GetScripts(IEnumerable<string> fieldNames, object context, ref int index, bool allowNulls = true, int startIndex = 0, bool allowSkip = true)
		{
			Field f;
			var result = new List<PythonScript>();
			do
			{
				f = FindField(fieldNames, ref index, !allowNulls, startIndex, allowSkip);
				startIndex = index + 1;
				if (f != null)
					result.Add(f.CreateScript(context));
			} while (f != null);
			return result;
		}

		public override string ToString()
		{
			if (Fields.Count > 0)
				return Fields.First().ToString() + " (" + Fields.Count + " fields)";
			return "(0 fields)";
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
		[Obsolete("Use the various Get methods instead (e.g. GetString).")]
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
	}
}