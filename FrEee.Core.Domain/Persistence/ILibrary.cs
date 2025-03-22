using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Plugins;

namespace FrEee.Persistence;

/// <summary>
/// A library of client side objects which can be used between games.
/// </summary>
/// <typeparam name="T">The type of objets which can be stored in this library.</typeparam>
public interface ILibrary<T>
	: IPlugin<ILibrary<T>>
{
	/// <summary>
	/// The path to the file where this library should be stored.
	/// </summary>
	public string FilePath { get; }

	/// <summary>
	/// Deletes items from the library that match a condition.
	/// </summary>
	/// <param name="condition">Which items should be deleted? Null to delete all items.</param>
	/// <param name="autosave">Automatically save after deleting?</param>
	/// <returns>The number of items deleted.</returns>
	public int Delete(Func<T, bool>? condition, bool autosave = true);

	/// <summary>
	/// Adds an item to the library.
	/// </summary>
	/// <param name="obj">The item to add.</param>
	/// <param name="autosave">Automatically save after adding?</param>
	public void Add(T obj, bool autosave = true);

	/// <summary>
	/// Cleans an object in preparation for adding it to the library.
	/// </summary>
	/// <param name="obj"></param>
	public void Clean(T obj);

	/// <summary>
	/// Retrieves items from the library.
	/// </summary>
	/// <param name="condition">Which items should be retrieved? Null to retrieve all items.</param>
	/// <returns>The items retrieved.</returns>
	public IEnumerable<T> Get(Func<T, bool>? condition = null);

	/// <summary>
	/// Saves the library to disk.
	/// </summary>
	public void Save();

	/// <summary>
	/// Loads the library from disk.
	/// </summary>
	public void Load();
}
