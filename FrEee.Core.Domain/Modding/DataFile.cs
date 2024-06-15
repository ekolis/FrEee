using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FrEee.Modding;

/// <summary>
/// A data file which is part of a mod.
/// </summary>
[Serializable]
public class DataFile
{
	/// <summary>
	/// Creates a data file with no records
	/// </summary>
	public DataFile()
	{
		MetaRecords = new List<MetaRecord>();
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
		// TODO - if no *BEGIN* tag present, don't require it
		for (curLine = 0; curLine < lines.Length; curLine++)
		{
			if (lines[curLine] == "*BEGIN*")
				break;
		}

		// skip 2 lines
		curLine += 2;

		List<DataFile> includeDataFiles = new List<DataFile>();
		while (curLine < lines.Length && (lines[curLine].StartsWith("#include") ||
			string.IsNullOrEmpty(lines[curLine])))
		{
			string line = lines[curLine];
			if (string.IsNullOrEmpty(line))
			{
				++curLine;
				continue;
			}

			if (line.StartsWith("#include-from"))
			{
				var regex = new Regex("#include-from \"(.*)\" \"(.*)\"");
				var match = regex.Match(line);
				var modName = match.Groups[1].Captures[0].Value;
				var fileName = match.Groups[2].Captures[0].Value;
				if (string.IsNullOrEmpty(modName))
					throw new FileNotFoundException($"#include-from at line {curLine} does not contain mod name.");
				if (string.IsNullOrEmpty(fileName))
					throw new FileNotFoundException($"#include-from at line {curLine} does not contain file name.");
				includeDataFiles.Add(Load(modName, fileName));
			}
			else
			{
				int firstPQ = line.IndexOf('"') + 1;
				int lastPQ = line.LastIndexOf('"') - 1;
				string includePath = line.Substring(firstPQ, (lastPQ - firstPQ) + 1);
				string filename = Path.GetFileName(includePath);
				if (string.IsNullOrEmpty(filename))
					throw new FileNotFoundException($"#include path \"{includePath}\" at line {curLine} does not contain filename.");
				string subpath = Path.GetDirectoryName(includePath);
				includeDataFiles.Add(Load(DataPath, filename, subpath));
			}

			++curLine;
		}

		// start reading records
		var recLines = new List<string>();
		for (; curLine < lines.Length; curLine++)
		{
			if (string.IsNullOrWhiteSpace(lines[curLine]))
			{
				// done with a record
				if (recLines.Count > 0)
				{
					MetaRecords.Add(new MetaRecord(recLines));
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
			MetaRecords.Add(new MetaRecord(recLines));

		// deal with degenerate records
		MetaRecords = MetaRecords.Where(rec => rec.Fields.Count > 0).ToList();

		List<MetaRecord> compiledRecords = new List<MetaRecord>();
		foreach (var includeDataFile in includeDataFiles)
		{
			foreach (var record in includeDataFile.MetaRecords)
			{
				compiledRecords.Add(record);
			}
		}

		compiledRecords.AddRange(MetaRecords);

		MetaRecords = compiledRecords;
	}

	public static string DataPath { get; private set; }

	/// <summary>
	/// The meta records in this data file.
	/// Meta records are records that can contain parameters and static formulas.
	/// </summary>
	public IList<MetaRecord> MetaRecords { get; private set; }

	/// <summary>
	/// The individual records generated from the meta records.
	/// </summary>
	public IEnumerable<Record> Records
	{
		get
		{
			return MetaRecords.SelectMany(mr => mr.Instantiate());
		}
	}

	/// <summary>
	/// Loads a data file from disk.
	/// </summary>
	/// <param name="modpath">The mod path, relative to the FrEee/Mods folder, or null to use the stock mod.</param>
	/// <param name="filename">The file name relative to the mod's Data folder.</param>
	/// <returns></returns>
	public static DataFile Load(string modpath, string filename, string subpath = "")
	{
		// TODO - fall back on stock data when mod data not found
		var datapath = modpath == null ? "Data" : Path.Combine("Mods", modpath, "Data", subpath);

		DataPath = modpath;

		var filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), datapath, filename);
		if (File.Exists(filepath))
			return new DataFile(File.ReadAllText(filepath));
		// got here? then try the stock data file instead if we were loading a mod
		if (modpath != null)
		{
			filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data", filename);
			if (File.Exists(filepath))
				return new DataFile(File.ReadAllText(filepath));
		}
		// got here? then data file was not found even in stock
		throw new FileNotFoundException($"Could not find data file: {filename} at {filepath}.", filename);
	}

	public override string ToString()
	{
		return "(" + MetaRecords.Count + " meta records)";
	}
}