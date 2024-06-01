using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Entities.References
{
	/// <summary>
	/// A base type for all identifiers.
	/// </summary>
	public interface IIdentifier
	{
	}

	/// <summary>
	/// A value which can be used to uniquely identify an entity.
	/// </summary>
	/// <typeparam name="TValue">The type of identifier value.</typeparam>
	public interface IIdentifier<out TValue>
		: IIdentifier
		where TValue: IEquatable<TValue>
	{
		/// <summary>
		/// The unique identifier value.
		/// </summary>
		TValue Value { get; }
	}

	/// <summary>
	/// A value which can be used to uniquely identify an entity.
	/// </summary>
	/// <typeparam name="TValue">The type of identifier value.</typeparam>
	/// <typeparam name="TEntity">The type of entity.</typeparam>
	public interface IIdentifier<out TValue, out TEntity>
		: IIdentifier<TValue>
        where TEntity : IEntity<TValue>
		where TValue : IEquatable<TValue>
	{
    }
}
