using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FrEee.Utility
{
	/// <summary>
	/// A reference to some shared object that can be passed around on the network as a surrogate for said object.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	[ClientSafe]
	public class Reference<T> : DynamicObject where T : IReferrable<object>
	{
		public Reference(T t)
		{
			if (t == null)
				ID = -1;
			else if (Galaxy.Current.Referrables.Contains(t))
				ID = t.ID;
			else
			{
				Galaxy.Current.Register(t);
				ID = t.ID;
			}
		}

		public Reference(int id)
		{
			if (Galaxy.Current.Referrables[id] is T)
				ID = id;
			else
				throw new Exception("Object with ID " + id + " is not a " + typeof(T) + ".");
		}


		public int ID { get; private set; }

		/// <summary>
		/// Resolves the reference.
		/// </summary>
		/// <returns></returns>
		public T Value
		{
			get
			{
				if (ID < 0)
					return default(T);
				if (ID >= Galaxy.Current.Referrables.Count)
					throw new ReferenceException("ID " + ID +  " is too high for " + typeof(T) + ". Trying to reference a newly created object, or one that was not assigned an ID by the server?", ID, typeof(T));
				return (T)Galaxy.Current.Referrables[ID];
			}
		}

		public static implicit operator Reference<T>(T t)
		{
			return new Reference<T>(t);
		}

		public static implicit operator T(Reference<T> r)
		{
			return r.Value;
		}

		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return typeof(T).GetProperties().Select(p => p.Name);
		}

		/// <summary>
		/// Gets a value from the referenced object.
		/// </summary>
		/// <param name="binder"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			var prop = typeof(T).GetProperty(binder.Name);
			if (prop == null || prop.GetIndexParameters().Length > 0)
			{
				result = null;
				return false;
			}
			result = prop.GetValue(Value, new object[] { });
			return true;
		}

		/// <summary>
		/// Sets a value on the referenced object.
		/// </summary>
		/// <param name="binder"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			var prop = typeof(T).GetProperty(binder.Name);
			if (prop == null || prop.GetIndexParameters().Length > 0)
				return false;
			prop.SetValue(Value, value, new object[] { });
			return true;
		}
	}

	public class ReferenceException : Exception
	{
		public ReferenceException(string message, int id, Type type)
			: base(message)
		{
			ID = id;
			Type = type;
		}

		public int ID { get; private set; }

		public Type Type { get; private set; }
	}
}
