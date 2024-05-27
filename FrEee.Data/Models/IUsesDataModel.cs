using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Models
{
	/// <summary>
	/// A domain object which is represented by a particular type of data model.
	/// </summary>
	/// <typeparam name="TSelf">The type of domain object.<br/>
	/// Because <see cref="TSelf"> must be known at compile time,
	/// <see cref="IUsesDataModel{TSelf, TModel}"/> can't be implemented by classes in the FrEee.Data project.</typeparam>
	/// <typeparam name="TModel">The type of data model.</typeparam>
	public interface IUsesDataModel<TSelf, TModel>
		: IUsesDataModel
		where TSelf: IUsesDataModel<TSelf, TModel>
		where TModel : IDataModel
	{
		/// <summary>
		/// Converts the domain object to a data model.
		/// </summary>
		/// <returns></returns>
		new TModel ToDataModel();

		/// <summary>
		/// Converts a data model to a domain object.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		abstract static TSelf FromDataModel(TModel model);
	}

	public interface IUsesDataModel
	{
		object ToDataModel();

		Type DomainObjectType { get; }

		Type DataModelType { get; }
	}
}
