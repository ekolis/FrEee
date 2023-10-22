using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;

namespace FrEee.Serialization
{
	// TODO: make this a record or maybe a struct so that it's a value type and caches can be reused even when spamming multiple references to the same object
	/// <summary>
	/// A lightweight reference to some object in the current game.
	/// Can be passed around on the network as a surrogate for said object.
	/// This class should be used when referencing a server side object from the client.
	/// It is not necessary to use <see cref="GameReference{T}"/> when entirely within either the client or the server.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class GameReference<T> : IReference<long, T>, IPromotable
		where T : IReferrable
	{

		/// <summary>
		/// Either will create a new <see cref="GameReference{T}"/> with the given id, or return null.
		/// Useful to allow a client to store an ID locally for reference, when the server might destroy said ID. 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static GameReference<T> GetGameReference(long id)
		{
			if (The.Game.ReferrableRepository.ContainsKey(id))
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
				if (The.Game == null)
					throw new ReferenceException<int, T>("Can't create a reference to an IReferrable without a game.");
				else if (t == null)
					ID = 0;
				else if (r.ID > 0)
					ID = r.ID;
				else
					ID = The.ReferrableRepository.Add(r);
				if (!HasValue)
				{
					The.ReferrableRepository.Add(r, r.ID);
					cache = new(() => t);
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
			if (The.Game == null)
				throw new ReferenceException<int, T>("Can't create a reference to an IReferrable without a game.");
			else if (!The.ReferrableRepository.ContainsKey(id))
				throw new IndexOutOfRangeException($"The id of {id} is not currently a valid reference");
			else if (The.ReferrableRepository[id] is T)
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
				var obj = The.ReferrableRepository.GetReferrable<T>(ID);
				return obj ?? default;
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
		public T? Value
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
		private ClientSideCache<T>? cache = null;

		public static implicit operator GameReference<T>?(T? t)
		{
			if (t == null)
				return null;
			return new GameReference<T>(t);
		}

		public static implicit operator T?(GameReference<T>? r)
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
			if (r1.IsNull() && r2.IsNull())
				return true;
			if (r1.IsNull() || r2.IsNull())
				return false;
			return r1.ID == r2.ID;
		}

		public override bool Equals(object obj)
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
}
