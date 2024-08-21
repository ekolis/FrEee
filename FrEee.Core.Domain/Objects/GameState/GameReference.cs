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
public class GameReference<T> : IReference<long, T>, IPromotable
    where T : IReferrable
{

    /// <summary>
    /// Either will create a new Galaxy Reference with the given id, or return null.
    /// Useful to allow a client to store an ID locally for reference, when the server might destroy said ID. 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static GameReference<T> GetGalaxyReference(long id)
    {
        if (Game.Current.referrables.ContainsKey(id))
            return new GameReference<T>(id);

        return null;
    }

    public GameReference()
    {
        InitializeCache();
    }

    public GameReference(T t)
        : this()
    {
        if (t is IReferrable)
        {
            var r = (IReferrable)t;
            if (Game.Current == null)
                throw new ReferenceException<int, T>("Can't create a reference to an IReferrable without a galaxy.");
            else if (t == null)
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
                    throw new ArgumentException("{0} does not exist in the current galaxy so it cannot be referenced.".F(t));
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
        if (Game.Current == null)
            throw new ReferenceException<int, T>("Can't create a reference to an IReferrable without a galaxy.");
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
            if (obj == null)
                return default;
            /*if (obj is IReferrable && (obj as IReferrable).IsDisposed)
				return default(T);*/
            return obj;
        }
        );
    }

    /// <summary>
    /// Does the reference have a valid value?
    /// </summary>
    public bool HasValue => Value != null;

    public long ID { get; internal set; }

    /// <summary>
    /// Resolves the reference.
    /// </summary>
    /// <returns></returns>
    public T Value
    {
        get
        {
            if (cache == null)
                InitializeCache();
            return cache.Value;
        }
    }

    [field: NonSerialized]
    private T value { get; set; }

    [NonSerialized]
    private ClientSideCache<T> cache;

    public static implicit operator GameReference<T>(T t)
    {
        if (t == null)
            return null;
        return new GameReference<T>(t);
    }

    public static implicit operator T(GameReference<T> r)
    {
        if (r == null)
            return default;
        return r.Value;
    }

    public static bool operator !=(GameReference<T> r1, GameReference<T> r2)
    {
        return !(r1 == r2);
    }

    public static bool operator ==(GameReference<T> r1, GameReference<T> r2)
    {
        if (r1 is null && r2 is null)
            return true;
        if (r1 is null || r2 is null)
            return false;
        return r1.ID == r2.ID;
    }

    public override bool Equals(object? obj)
    {
        // TODO - upgrade equals to use "as" operator
        if (obj is GameReference<T>)
            return this == (GameReference<T>)obj;
        return false;
    }

    public override int GetHashCode()
    {
        return ID.GetHashCode();
    }

    public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
    {
        if (done == null)
            done = new HashSet<IPromotable>();
        if (!done.Contains(this))
        {
            done.Add(this);
            if (idmap.ContainsKey(ID))
                ID = idmap[ID];
            if (HasValue && Value is IPromotable)
                ((IPromotable)Value).ReplaceClientIDs(idmap, done);
        }
    }

    public override string ToString()
    {
        return "ID=" + ID + ", Value=" + Value;
    }
}
