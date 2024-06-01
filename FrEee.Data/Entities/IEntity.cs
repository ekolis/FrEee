using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data.Entities.References;

namespace FrEee.Data.Entities
{
	/// <summary>
	/// A general purpose entity.
	/// </summary>
	public interface IEntity
	{
		// TODO: ECS stuff is cool!
	}

	/// <summary>
	/// An entity which can be identified by an ID.
	/// </summary>
	/// <typeparam name="TIDValue">The type of ID value.</typeparam>
	public interface IEntity<out TIDValue>
		: IEntity
		where TIDValue : IEquatable<TIDValue>
	{
		IIdentifier<TIDValue> ID { get; }
	}
}
