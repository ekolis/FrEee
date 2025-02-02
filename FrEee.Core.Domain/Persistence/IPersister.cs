using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Persistence;

/// <summary>
/// Tool to save and load objects.
/// </summary>
/// <typeparam name="T">The type of object which can be persisted.</typeparam>
/// <typeparam name="TID">The type of object used to identify which object is being persisted.</typeparam>
public interface IPersister<T, TID>
{
	/// <summary>
	/// Saves an object.
	/// </summary>
	/// <param name="obj">The object to save.</param>
	/// <returns>The ID of the saved object.</returns>
	TID Save(T obj);

	/// <summary>
	/// Loads an object.
	/// </summary>
	/// <param name="id">The ID of the object to load.</param>
	/// <returns>The loaded object.</returns>
	T Load(TID id);
}
