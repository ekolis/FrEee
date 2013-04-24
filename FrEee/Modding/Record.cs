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
		
		/// <summary>
		/// Tries to find a field value.
		/// </summary>
		/// <param name="fieldName">The name to search for.</param>
		/// <param name="log">If an exception needs to be logged, pass it here.</param>
		/// <param name="value">The field value.</param>
		/// <param name="index">The field index.</param>
		/// <param name="startIndex"></param>
		/// <returns>true on success, false on failure</returns>
		public bool TryFindFieldValue(string fieldName, out string value, ref int index, ICollection<DataParsingException> log = null, int startIndex = 0)
		{
			return TryFindFieldValue(new string[] { fieldName }, out value, ref index, log, startIndex);
		}

		/// <summary>
		/// Tries to find a field value from the first of any of a number of fields.
		/// </summary>
		/// <param name="fieldNames">The name to search for.</param>
		/// <param name="log">If an exception needs to be logged, pass it here.</param>
		/// <param name="value">The field value.</param>
		/// <param name="index">The field index.</param>
		/// <param name="startIndex"></param>
		/// <returns>true on success, false on failure</returns>
		public bool TryFindFieldValue(IEnumerable<string> fieldNames, out string value, ref int index, ICollection<DataParsingException> log = null, int startIndex = 0)
		{
			for (var i = startIndex; i < Fields.Count; i++)
			{
				if (fieldNames.Contains(Fields[i].Name))
				{
					value = Fields[i].Value;
					index = i;
					return true;
				}
			}
			if (log != null)
				log.Add(new DataParsingException("Could not find fields: " + string.Join(", ", fieldNames.ToArray()) + ".", Mod.CurrentFileName, this));
			value = null;
			return false;
		}
	}
}
