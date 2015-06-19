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
	/// A lightweight reference to some object in the current galaxy.
	/// Can be passed around on the network as a surrogate for said object.
	/// This class should be used when referencing a server side object from the client.
	/// It is not necessary to use GalaxyReference when entirely within either the client or the server.
	/// If the object is not an IReferrable, the object itself will be stored!
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class GalaxyReference<T> : IReference<long, T>, IPromotable
	{
		public GalaxyReference(T t)
		{
			if (t is IReferrable)
			{
				var r = (IReferrable)t;
				if (Galaxy.Current == null)
					throw new ReferenceException<int, T>("Can't create a reference to an IReferrable without a galaxy.");
				else if (t == null)
					ID = 0;
				else if (r.ID > 0)
					ID = r.ID;
				else
					ID = Galaxy.Current.AssignID(r);
			}
			else
			{
				value = t;
			}
		}

		public GalaxyReference(long id)
		{
			if (Galaxy.Current == null)
				throw new ReferenceException<int, T>("Can't create a reference to an IReferrable without a galaxy.");
			else if (Galaxy.Current.referrables[id] is T)
				ID = id;
			else
				throw new Exception("Object with ID " + id + " is not a " + typeof(T) + ".");
		}

		public long ID { get; internal set; }

		private T value { get; set; }

		/// <summary>
		/// Resolves the reference.
		/// </summary>
		/// <returns></returns>
		public T Value
		{
			get
			{
				if (value == null)
				{
					if (ID <= 0 || !Galaxy.Current.referrables.ContainsKey(ID))
						return default(T);
					return (T)Galaxy.Current.referrables[ID];
				}
				else
					return value;
			}
		}

		/// <summary>
		/// Does the reference have a valid value?
		/// </summary>
		public bool HasValue
		{
			get
			{
				if (value == null)
					return Galaxy.Current.referrables.ContainsKey(ID);
				else
					return true;
			}
		}

		public static implicit operator T(GalaxyReference<T> r)
		{
			if (r == null)
				return default(T);
			return r.Value;
		}

		public static implicit operator GalaxyReference<T>(T t)
		{
			return new GalaxyReference<T>(t);
		}

		public override string ToString()
		{
			return "ID=" + ID + ", Value=" + Value;
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

		public static bool operator ==(GalaxyReference<T> r1, GalaxyReference<T> r2)
		{
			if (r1.IsNull() && r2.IsNull())
				return true;
			if (r1.IsNull() || r2.IsNull())
				return false;
			return r1.ID == r2.ID;
		}

		public static bool operator !=(GalaxyReference<T> r1, GalaxyReference<T> r2)
		{
			return !(r1 == r2);
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj is GalaxyReference<T>)
				return this == (GalaxyReference<T>)obj;
			return false;
		}
	}
}