using System;

namespace FrEee.Modding;

/// <summary>
/// Exceptiion thrown when parsing mod data.
/// </summary>
[Serializable]
public class DataParsingException : Exception
{
	public DataParsingException(string message, string filename, Record record = null, Field field = null)
		: base(message)
	{
		Filename = filename;
		Record = record;
		Field = field;
	}

	public DataParsingException(string message, Exception inner, string filename, Record record = null, Field field = null)
		: base(message, inner)
	{
		Filename = filename;
		Record = record;
		Field = field;
	}

	public Field Field { get; private set; }
	public string Filename { get; private set; }
	public Record Record { get; private set; }
}