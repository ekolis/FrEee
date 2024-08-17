using FrEee.Extensions;
using FrEee.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace FrEee.Utility;

/// <summary>
/// A library of client side objects which can be used between games.
/// </summary>
public static class Library
{
	static Library()
	{
		Items = new HashSet<object>();
	}

	/// <summary>
	/// The full path to the library file.
	/// </summary>
	public static string FilePath
	{
		get
		{
			return Path.Combine(ClientUtilities.ApplicationDataPath, "Library.dat");
		}
	}

	/// <summary>
	/// The items in the library.
	/// </summary>
	private static ISet<object> Items { get; set; }

	/// <summary>
	/// Deletes objects from the library.
	/// </summary>
	/// <typeparam name="T">The type of object to delete.</typeparam>
	/// <param name="condition">Condition to apply when selecting objects to delete.</param>
	/// <returns>Number of objects deleted.</returns>
	public static int Delete<T>(Func<T, bool> condition = null, bool autosave = true)
	{
		// defaults to deleting all
		if (condition == null)
			condition = t => true;

		int count = 0;
		foreach (var o in Items.OfType<T>().Where(condition).ToArray())
		{
			Items.Remove(o);
			count++;
		}

		if (autosave)
			Save();

		return count;
	}

	/// <summary>
	/// Exports an object to the library.
	/// </summary>
	/// <param name="cleaner">Anything special to do to the copied object before saving it.</param>
	/// <param name="autosave"></param>
	/// <param name="o"></param>
	public static void Export<T>(T o, Action<T> cleaner = null, bool autosave = true)
	{
		var c = o.CopyAndAssignNewID();
		// TODO - unassign ID in galaxy? does it matter?
		if (cleaner != null)
			cleaner(c);
		Items.Add(c);
		if (autosave)
			Save();
	}

	/// <summary>
	/// Imports objects from the library into the game.
	/// </summary>
	/// <typeparam name="T">The type of object to import.</typeparam>
	/// <param name="condition">Condition to apply when selecting objects to import.</param>
	/// <returns>Copied objects imported.</returns>
	public static IEnumerable<T> Import<T>(Func<T, bool> condition = null)
	{
		// defaults to loading all
		if (condition == null)
			condition = t => true;

		// copy objects so they're distinct from the library versions when importing
		return Items.OfType<T>().Where(condition).Select(o => o.CopyAndAssignNewID()).ToArray();
	}

	public static void Load()
	{
		// load library from disk
		try
		{
			// HACK - move old Library.dat that was in the app folder
			if (File.Exists("Library.dat"))
				File.Move("Library.dat", FilePath);

			var fs = File.OpenRead(FilePath);
			Items = Serializer.Deserialize<ISet<object>>(fs);
			fs.Close(); fs.Dispose();
		}
		catch (IOException)
		{
			// file not found, leave library empty
			// TODO - log somewhere?
			Items = new HashSet<object>();
		}
		catch (SerializationException)
		{
			// bad data, leave library empty
			// TODO - log somewhere?
			Items = new HashSet<object>();
		}
	}

	public static void Save()
	{
		var path = Path.GetDirectoryName(FilePath);
		if (!Directory.Exists(path))
			Directory.CreateDirectory(path);
		var fs = File.Create(FilePath);
		Serializer.Serialize(Items, fs);
		fs.Close(); fs.Dispose();
	}
}
