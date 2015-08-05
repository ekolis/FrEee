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
using FrEee.Modding.Interfaces;

namespace FrEee.Modding
{
	/// <summary>
	/// A lightweight reference to some object in the current mod.
	/// Can be passed around on the network as a surrogate for said object.
	/// This class should be used when referencing a mod object from the actual game.
	/// It is not necessary to use ModReference when referring to mod objects from other mod objects.
	/// Only mod objects can be referenced via mod references.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class ModReference<T> : IReference<string, T> where T : IModObject
	{
		public ModReference(T t)
		{
			var mobj = (IModObject)t;
			if (Mod.Current == null)
				throw new ReferenceException<int, T>("Can't create a reference to an IModObject without a mod.");
			else if (t == null)
				ID = ""; // dictionaries don't like null keys
			else if (mobj.ModID != null)
				ID = mobj.ModID;
			else
				throw new ReferenceException<string, T>("Can't create a reference to {0} because it has no ID.".F(mobj));
			if (!HasValue)
				throw new ArgumentException("{0} does not exist in the current mod so it cannot be referenced.".F(t));
		}

		public ModReference(string id)
		{
			if (Mod.Current == null)
				throw new ReferenceException<int, T>("Can't create a reference to an IModObject without a mod.");
			else if (Mod.Current.Find(id) is T)
				ID = id;
			else
				throw new Exception("Object with ID " + id + " is not a " + typeof(T) + ".");
		}

		public string ID { get; internal set; }

		/// <summary>
		/// Resolves the reference.
		/// </summary>
		/// <returns></returns>
		public T Value
		{
			get
			{
				if (ID == null || ID == "")
					return default(T);
				return (T)Mod.Current.Find(ID);
			}
		}

		/// <summary>
		/// Does the reference have a valid value?
		/// </summary>
		public bool HasValue
		{
			get
			{
				return Value != null;
			}
		}

		public static implicit operator T(ModReference<T> r)
		{
			if (r == null)
				return default(T);
			return r.Value;
		}

		public static implicit operator ModReference<T>(T t)
		{
			if (t == null)
				return null;
			return new ModReference<T>(t);
		}

		public override string ToString()
		{
			return "Mod ID=" + ID + ", Value=" + Value;
		}

		public static bool operator ==(ModReference<T> r1, ModReference<T> r2)
		{
			if (r1.IsNull() && r2.IsNull())
				return true;
			if (r1.IsNull() || r2.IsNull())
				return false;
			return r1.ID == r2.ID;
		}

		public static bool operator !=(ModReference<T> r1, ModReference<T> r2)
		{
			return !(r1 == r2);
		}

		public override int GetHashCode()
		{
			return ID == null || ID == "" ? 0 : ID.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj is ModReference<T>)
				return this == (ModReference<T>)obj;
			return false;
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				if (HasValue && Value is IPromotable)
					((IPromotable)Value).ReplaceClientIDs(idmap, done);
			}
		}
	}
}