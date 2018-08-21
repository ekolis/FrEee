﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;

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
		public GalaxyReference()
		{
			cache = new ClientSideCache<T>(() =>
			{
				if (ID <= 0)
					return value;
				var obj = (T)Galaxy.Current.GetReferrable(ID);
				if (obj == null)
					return default(T);
				if (obj is IReferrable && (obj as IReferrable).IsDisposed)
					return default(T);
				return obj;
			}
			);
		}

		public GalaxyReference(T t)
			: this()
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
				if (!HasValue)
					throw new ArgumentException("{0} does not exist in the current galaxy so it cannot be referenced.".F(t));
			}
			else
			{
				value = t;
			}
		}

		public GalaxyReference(long id)
			: this()
		{
			if (Galaxy.Current == null)
				throw new ReferenceException<int, T>("Can't create a reference to an IReferrable without a galaxy.");
			else if (Galaxy.Current.referrables[id] is T)
				ID = id;
			else
				throw new Exception("Object with ID " + id + " is not a " + typeof(T) + ".");
		}

		/// <summary>
		/// Does the reference have a valid value?
		/// </summary>
		public bool HasValue
		{
			get
			{
				if (value == null)
					return ID <= 0 || Galaxy.Current.referrables.ContainsKey(ID); // nonpositive ID's are null objects
				else
					return true;
			}
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
				return cache.Value;
			}
		}

		private T value { get; set; }
		private ClientSideCache<T> cache;

		public static implicit operator GalaxyReference<T>(T t)
		{
			if (t == null)
				return null;
			return new GalaxyReference<T>(t);
		}

		public static implicit operator T(GalaxyReference<T> r)
		{
			if (r == null)
				return default(T);
			return r.Value;
		}

		public static bool operator !=(GalaxyReference<T> r1, GalaxyReference<T> r2)
		{
			return !(r1 == r2);
		}

		public static bool operator ==(GalaxyReference<T> r1, GalaxyReference<T> r2)
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
			if (obj is GalaxyReference<T>)
				return this == (GalaxyReference<T>)obj;
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