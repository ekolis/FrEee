using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
	/// <summary>
	/// A reference to some shared object that can be passed around on the network as a surrogate for said object.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Reference<T> where T : IReferrable<object>
	{
		public Reference(T t)
		{
			if (Galaxy.Current.Referrables.Contains(t))
				ID = t.ID;
			if (Galaxy.Current.CurrentEmpire == null)
			{
				Galaxy.Current.Register(t);
				ID = t.ID;
			}
			throw new Exception("Can't create a reference to a brand-new object from the client side. Pass the object itself to the server instead.");
		}


		public int ID { get; private set; }

		/// <summary>
		/// Resolves the reference.
		/// </summary>
		/// <returns></returns>
		public T Resolve()
		{
			return (T)Galaxy.Current.Referrables[ID];
		}

		public static implicit operator Reference<T>(T t)
		{
			return new Reference<T>(t);
		}

		public static implicit operator T(Reference<T> r)
		{
			return r.Resolve();
		}
	}
}
