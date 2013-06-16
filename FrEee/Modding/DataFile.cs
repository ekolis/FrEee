using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A data file which is part of a mod.
	/// </summary>
	 [Serializable] public class DataFile
	{
		 /// <summary>
		 /// Loads a data file from disk.
		 /// </summary>
		 /// <param name="modpath">The mod path, relative to the FrEee/Mods folder, or null to use the stock mod.</param>
		 /// <param name="filename">The file name relative to the mod's Data folder.</param>
		 /// <returns></returns>
		 public static DataFile Load(string modpath, string filename)
		 {
			 // TODO - fall back on stock data when mod data not found
			 var datapath = modpath == null ? "Data" : Path.Combine("Mods", modpath, "Data");
			 var filepath = Path.Combine(datapath, filename);
			 return new DataFile(File.ReadAllText(filepath));
		 }

		/// <summary>
		/// Creates a data file with no records
		/// </summary>
		public DataFile()
		{
			Records = new List<Record>();
		}

		/// <summary>
		/// Creates a data file by parsing some string data.
		/// </summary>
		/// <param name="data"></param>
		public DataFile(string data)
			: this()
		{
			// split data into lines
			var lines = data.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()).ToArray();

			int curLine;

			// find *BEGIN* tag
			for (curLine = 0; curLine < lines.Length; curLine++)
			{
				if (lines[curLine] == "*BEGIN*")
					break;
			}

			// skip 2 lines
			curLine += 2;

			// start reading records
			var recLines = new List<string>();
			for (; curLine < lines.Length; curLine++)
			{
				if (string.IsNullOrWhiteSpace(lines[curLine]))
				{
					// done with a record
					if (recLines.Count > 0)
					{
						Records.Add(new Record(recLines));
						recLines.Clear();
					}
				}
				else
				{
					// add line to current record
					recLines.Add(lines[curLine]);
				}
			}

			// deal with the last record
			if (recLines.Count > 0)
				Records.Add(new Record(recLines));

			// deal with degenerate records
			Records = Records.Where(rec => rec.Fields.Count > 0).ToList();
		}

		/// <summary>
		/// The records in this data file.
		/// </summary>
		public IList<Record> Records { get; private set; }

		public override string ToString()
		{
			return "(" + Records.Count + " records";
		}
	}
}
