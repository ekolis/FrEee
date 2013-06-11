using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FrEee.Game.Objects.Civilization;

namespace FrEee.Utility
{
	/// <summary>
	/// A reference to some shared object that can be passed around on the network as a surrogate for said object.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	[ClientSafe]
	public class Reference<T> : IReference<T> where T : IReferrable
	{
		public Reference(Empire empire, T t)
		{
			EmpireNumber = Galaxy.Current.Empires.IndexOf(empire) + 1;
			if (t == null)
			{
				ID = -1;
				IsGlobal = false;
			}
			else if (Galaxy.Current.Referrables[0].Contains(t))
			{
				ID = t.ID;
				IsGlobal = true;
			}
			else if (Galaxy.Current.Referrables.Count - 1 >= EmpireNumber && Galaxy.Current.Referrables[EmpireNumber].Contains(t))
			{
				ID = t.ID;
				IsGlobal = false;
			}
			else
			{
				if (Empire.Current == null)
				{
					ID = t.ID = Galaxy.Current.Register(t);
					IsGlobal = true;
				}
				else
				{
					ID = t.ID = Galaxy.Current.Register(t, Empire.Current);
					IsGlobal = false;
				}
			}
		}

		public Reference(bool isGlobal, int id)
		{
			if (isGlobal)
			{
				EmpireNumber = 0;
				IsGlobal = true;
				if (Galaxy.Current.Referrables[EmpireNumber][id] is T)
					ID = id;
				else
					throw new Exception("Object with ID " + id + " is not a " + typeof(T) + ".");
				IsGlobal = false;
			}
			else
			{
				EmpireNumber = Galaxy.Current.Empires.IndexOf(Empire.Current);
				IsGlobal = false;
				if (Galaxy.Current.Referrables[EmpireNumber][id] is T)
					ID = id;
				else
					throw new Exception("Object with ID " + id + " is not a " + typeof(T) + ".");
				IsGlobal = false;
			}
		}

		public int EmpireNumber { get; private set; }

		public int ID { get; private set; }

		/// <summary>
		/// Referencing a global referrable? Or one that's only accessible to this player?
		/// </summary>
		public bool IsGlobal { get; private set; }

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
				if (IsGlobal)
				{
					if (ID >= Galaxy.Current.Referrables[0].Count)
						throw new ReferenceException("ID " + ID + " is too high for global reference to " + typeof(T) + ". Trying to reference a newly created object, or one that was not assigned an ID by the server?", ID, typeof(T));
					return (T)Galaxy.Current.Referrables[0][ID];
				}
				else
				{
					if (ID >= Galaxy.Current.Referrables[EmpireNumber].Count)
						throw new ReferenceException("ID " + ID + " is too high for empire reference to " + typeof(T) + ". Trying to reference a newly created object, or one that was not assigned an ID by the server?", ID, typeof(T));
					return (T)Galaxy.Current.Referrables[EmpireNumber][ID];
				}
			}
		}

		public static implicit operator T(Reference<T> r)
		{
			return r.Value;
		}

		public static implicit operator Reference<T>(T t)
		{
			return new Reference<T>(Empire.Current, t);
		}

		public override string ToString()
		{
			if (IsGlobal)
				return "Global ID=" + ID + ", Value=" + Value;
			else
				return "ID=" + ID + ", Value=" + Value;
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