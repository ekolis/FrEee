using FrEee.Extensions;
using System;
using System.Collections.Generic;
using FrEee.Utility;
using FrEee.Vehicles;
using System.Linq;
using FrEee.Objects.Civilization;

namespace FrEee.Objects.GameState;

/// <summary>
/// A lightweight reference to some object in the current galaxy.
/// Can be passed around on the network as a surrogate for said object.
/// This class should be used when referencing a server side object from the client.
/// It is not necessary to use GalaxyReference when entirely within either the client or the server.
/// </summary>
/// <param name="ID">The ID of the <see cref="IReferrable"/> being referenced.</param>
/// <typeparam name="T">The type of the <see cref="IReferrable"/> being referenced.</typeparam>
[Serializable]
public record GameReference<T>(long ID)
	: IReference<long, T>
	where T : IReferrable
{
	public GameReference(T? t)
		: this(t?.ID ?? 0)
	{
		if (t is null)
			ID = 0;
		else if (t.ID > 0)
			ID = t.ID;
		else
			ID = Game.Current.AssignID(t);
	}
	/// <summary>
	/// Does the reference have a valid value?
	/// </summary>
	public bool HasValue => Value is not null;

	/// <summary>
	/// Resolves the reference.
	/// </summary>
	/// <returns></returns>
	public T Value => Game.Current.Find<T>(ID);

	public static implicit operator GameReference<T>?(T t)
	{
		if (t is null)
			return default;
		return new GameReference<T>(t);
	}

	public static implicit operator T?(GameReference<T> r)
	{
		if (r is null)
			return default;
		return r.Value;
	}

	public GameReference<T> ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
	{
		var result = this;
		if (done == null)
			done = new HashSet<IPromotable>();
		if (!done.Contains(this))
		{
			done.Add(this);
			if (idmap.ContainsKey(ID))
				result = result with { ID = idmap[ID] };
			if (HasValue && Value is IPromotable)
				((IPromotable)Value).ReplaceClientIDs(idmap, done);
		}
		return result;
	}

	IPromotable IPromotable.ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done)
	{
		return ReplaceClientIDs(idmap, done);
	}
}