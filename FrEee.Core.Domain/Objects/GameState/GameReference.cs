using FrEee.Extensions;
using System;
using System.Collections.Generic;
using FrEee.Utility;

namespace FrEee.Objects.GameState;

/// <summary>
/// A lightweight reference to some object in the current galaxy.
/// Can be passed around on the network as a surrogate for said object.
/// This class should be used when referencing a server side object from the client.
/// It is not necessary to use GalaxyReference when entirely within either the client or the server.
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public record GameReference<T>
    : IReference<long, T>
    where T : IReferrable
{
    private GameReference()
    {
        InitializeCache();
    }

    public GameReference(T t)
        : this()
    {
        if (t is IReferrable r)
        {
            if (Game.Current is null)
                throw new ReferenceException<int, T>("Can't create a reference to an IReferrable without a current game.");
            else if (t is null)
                ID = 0;
            else if (r.ID > 0)
                ID = r.ID;
            else
                ID = Game.Current.AssignID(r);
            if (!HasValue)
            {
                Game.Current.referrables[r.ID] = r;
                cache = null; // reset cache
                if (!HasValue)
                    throw new ArgumentException("{0} does not exist in the current game so it cannot be referenced.".F(t));
            }
        }
        else
        {
            value = t;
        }
    }

    public GameReference(long id)
        : this()
    {
        if (Game.Current is null)
            throw new ReferenceException<int, T>("Can't create a reference to an IReferrable without a current game.");
        else if (!Game.Current.referrables.ContainsKey(id))
            throw new IndexOutOfRangeException($"The id of {id} is not currently a valid reference");
        else if (Game.Current.referrables[id] is T)
            ID = id;
        else
            throw new Exception("Object with ID " + id + " is not a " + typeof(T) + ".");
    }

    private void InitializeCache()
    {
        cache = new ClientSideCache<T>(() =>
        {
            if (ID <= 0)
                return value;
            var obj = (T)Game.Current.GetReferrable(ID);
            if (obj is null)
                return default;
            return obj;
        }
        );
    }

    /// <summary>
    /// Does the reference have a valid value?
    /// </summary>
    public bool HasValue => Value is not null;

    /// <summary>
    /// The ID of the referenced <see cref="T"/>.
    /// </summary>
    public long ID { get; init; }

    /// <summary>
    /// Resolves the reference.
    /// </summary>
    /// <returns></returns>
    public T Value
    {
        get
        {
            if (cache is null)
                InitializeCache();
            return cache.Value;
        }
    }

    [field: NonSerialized]
    private T value { get; set; }

    [NonSerialized]
    private ClientSideCache<T>? cache;

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

	public override string ToString()
    {
        return "ID=" + ID + ", Value=" + Value;
    }
}
