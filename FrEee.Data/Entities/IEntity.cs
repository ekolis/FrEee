using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data.Entities.Identification;

namespace FrEee.Data.Entities
{
	/// <summary>
	/// A general purpose entity.
	/// </summary>
	public interface IEntity
	{
		// TODO: ECS stuff is cool! Add child entities and parent entities and repositories and abilities and stats...
		// Ability scopes can be functions that take in an entity and return a boolean whether is is of that scope or not
		// so they can depend on abilities, etc...
		// Savegames and mods can be repositories...
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
