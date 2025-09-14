using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FrEee.Extensions;
using FrEee.Modding.Abilities;
using FrEee.Modding.Templates;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Processes.AI;
using FrEee.Properties;
using FrEee.Utility;
using FrEee.Vehicles;

namespace FrEee.Modding;

/// <summary>
/// A lightweight reference to some object in the current mod.
/// Can be passed around on the network as a surrogate for said object.
/// This class should be used when referencing a mod object from the actual game.
/// It is not necessary to use ModReference when referring to mod objects from other mod objects.
/// Only mod objects can be referenced via mod references.
/// </summary>
/// <param name="ID">The ID of the <see cref="IModObject"/> being referenced.</param>
/// <typeparam name="T">The type of the <see cref="IModObject"/> being referenced.</typeparam>
[Serializable]
public record ModReference<T>(string ID)
	: IReference<string, T>
	where T : IModObject
{
	public ModReference(T? t)
		: this(t?.ModID)
	{
		var mobj = (IModObject)t;
		if (Mod.Current == null)
			throw new ReferenceException<int, T>("Can't create a reference to an IModObject without a mod.");
		else if (t == null)
			ID = ""; // dictionaries don't like null keys
		else if (mobj.ModID != null)
			ID = mobj.ModID;
		else
			throw new ReferenceException<string, T>("Can't create a reference to {0} because it has no ID in the current mod.".F(mobj));
		if (!HasValue)
			throw new ArgumentException("{0} does not exist in the current mod so it cannot be referenced.".F(t));
	}

	/// <summary>
	/// Does the reference have a valid value?
	/// </summary>
	public bool HasValue
	{
		get
		{
			// blank ID's are null objects, which are perfectly cromulent
			return string.IsNullOrWhiteSpace(ID) || Value != null;
		}
	}

	/// <summary>
	/// Resolves the reference.
	/// </summary>
	/// <returns></returns>
	public T Value => Mod.Current.Find<T>(ID);

	public static implicit operator ModReference<T>?(T? t)
	{
		if (t is null)
			return null;
		if (t.ModID is null)
		{
			// HACK - why are my items not registered in the mod? does it have something to do with formulas and leveled templates?
			Mod.Current.AssignID(t, Mod.Current.Objects.Select(q => q.ModID).ToList());
		}
		return new ModReference<T>(t);
	}

	public static implicit operator T?(ModReference<T>? r)
	{
		if (r is null)
			return default;
		return r.Value;
	}

	public ModReference<T> ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable>? done = null)
	{
		var result = this;
		done ??= new HashSet<IPromotable>();
		if (!done.Contains(this))
		{
			done.Add(this);
			if (HasValue && Value is IPromotable p)
				p.ReplaceClientIDs(idmap, done);
		}
		return result;
	}

	IPromotable IPromotable.ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done)
	{
		return ReplaceClientIDs(idmap, done);
	}

	public override string ToString()
	{
		return "Mod ID=" + ID + ", Value=" + Value;
	}
}
