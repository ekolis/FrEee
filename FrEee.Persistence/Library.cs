using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Utility;

namespace FrEee.Persistence;

public abstract class Library<T>
	: ILibrary<T>
{
	public static readonly string RootPath = ClientUtilities.ApplicationDataPath;

	public abstract string FilePath { get; }

	private ISet<T> Items = new HashSet<T>();

	public void Add(T obj, bool autosave = true)
	{
		var copy = obj.CopyAndAssignNewID();
		Clean(copy);
		Items.Add(copy);
		if (autosave)
		{
			Save();
		}
	}

	public abstract void Clean(T obj);

	public int Delete(Func<T, bool>? condition, bool autosave = true)
	{
		// default to deleting all objects
		condition ??= t => true;

		int count = 0;
		foreach (var o in Items.Where(condition).ToArray())
		{
			Items.Remove(o);
			count++;
		}

		if (autosave)
		{
			Save();
		}

		return count;
	}

	public IEnumerable<T> Get(Func<T, bool>? condition = null)
	{
		// default to loading all objects
		condition ??= t => true;

		// copy objects so they're distinct from the library versions when importing
		return Items.Where(condition).Select(o => o.CopyAndAssignNewID()).ToArray();
	}

	public void Load()
	{
		// load library from disk
		try
		{
			// HACK: move old Library.dat
			var oldFilePath = Path.Combine(Path.GetDirectoryName(FilePath), "Library.dat");
			if (File.Exists(oldFilePath))
			{
				File.Move(oldFilePath, FilePath);
			}
			else if (File.Exists("Library.dat"))
			{
				File.Move("Library.dat", FilePath);
			}

			using var fs = File.OpenRead(FilePath);
			Items = Serializer.Deserialize<ISet<T>>(fs);
		}
		catch (IOException)
		{
			// file not found, leave library empty
			// TODO - log somewhere?
			Items = new HashSet<T>();
		}
		catch (SerializationException)
		{
			// bad data, leave library empty
			// TODO - log somewhere?
			Items = new HashSet<T>();
		}
	}

	public void Save()
	{
		var path = Path.GetDirectoryName(FilePath);
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		using var fs = File.Create(FilePath);
		Serializer.Serialize(Items, fs);
	}
}
