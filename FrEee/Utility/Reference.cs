using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility.Extensions;

namespace FrEee.Utility
{
	/// <summary>
	/// A reference to some shared object that can be passed around on the network as a surrogate for said object.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class Reference<T> : IReference<T>, IPromotable where T : IReferrable
	{
		public Reference(T t)
		{
			if (Galaxy.Current == null)
				throw new ReferenceException("Can't create a reference to an object without a galaxy.", 0, typeof(T));
			else if (t == null)
				ID = 0;
			else if (t.ID > 0)
				ID = t.ID;
			else if (Empire.Current == null || t.GetType().IsClientSafe())
				ID = Galaxy.Current.AssignID(t);
			else
				throw new ReferenceException("Tried to create a new reference to a non-client-safe object " + t + " on the client side.", -1, t.GetType());
		}

		public Reference(long id)
		{
			if (Galaxy.Current == null)
				throw new ReferenceException("Can't create a reference to an object without a galaxy.", 0, typeof(T));
			else if (Galaxy.Current.referrables[id] is T)
				ID = id;
			else
				throw new Exception("Object with ID " + id + " is not a " + typeof(T) + ".");
		}

		public long ID { get; internal set; }

		/// <summary>
		/// Resolves the reference.
		/// </summary>
		/// <returns></returns>
		public T Value
		{
			get
			{
				if (ID <= 0)
					return default(T);
				if (!Galaxy.Current.referrables.ContainsKey(ID))
					throw new ReferenceException("ID " + ID + " does not exist in the current galaxy. " + typeof(T) + ". Trying to reference a newly created object, or one that was not assigned an ID by the server?", ID, typeof(T));
				return (T)Galaxy.Current.referrables[ID];
			}
		}

		/// <summary>
		/// Does the reference have a valid value?
		/// </summary>
		public bool HasValue
		{
			get
			{
				return Galaxy.Current.referrables.ContainsKey(ID);
			}
		}

		public static implicit operator T(Reference<T> r)
		{
			return r.Value;
		}

		public static implicit operator Reference<T>(T t)
		{
			return new Reference<T>(t);
		}

		public override string ToString()
		{
			return "ID=" + ID + ", Value=" + Value;
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			if (idmap.ContainsKey(ID))
				ID = idmap[ID];
			if (HasValue && Value is IPromotable)
				((IPromotable)Value).ReplaceClientIDs(idmap);
		}
	}

	public class ReferenceException : Exception
	{
		public ReferenceException(string message, long id, Type type)
			: base(message)
		{
			ID = id;
			Type = type;
		}

		public long ID { get; private set; }

		public Type Type { get; private set; }
	}
}