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
/// <typeparam name="TObject">The type of object which can be translated.</typeparam>
/// <typeparam name="TData">The type of data that objects will be translated to.</typeparam>
public abstract class DataTranslator<TObject, TData>
	: IDataTranslator
{
	public Type ObjectType => typeof(TObject);
	public Type DataType => typeof(TData);

	/// <summary>
	/// Translates data to an object.
	/// </summary>
	/// <param name="data"></param>
	/// <returns></returns>
	public abstract TObject FromData(TData data);

	/// <summary>
	/// Translates an object to data.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public abstract TData ToData(TObject obj);

	object IDataTranslator.FromData(object data)
	{
		return FromData((TData)data);
	}

	object IDataTranslator.ToData(object obj)
	{
		return ToData((TObject)obj);
	}
}