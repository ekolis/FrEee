using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data;

/// <summary>
/// Translates between objects and simplified representations of those objects
/// which can be used for serialization.
/// </summary>
public interface IDataTranslator
{
	/// <summary>
	/// The type of object which can be translated by this <see cref="IDataTranslator"/>.
	/// </summary>
	Type ObjectType { get; }

	/// <summary>
	/// The type of data that this <see cref="IDataTranslator"/> translates objects to.
	/// </summary>
	Type DataType { get; }

	/// <summary>
	/// Translates an object to data.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	object ToData(object obj);

	/// <summary>
	/// Translates data to an object.
	/// </summary>
	/// <param name="data"></param>
	/// <returns></returns>
	object FromData(object data);
}
