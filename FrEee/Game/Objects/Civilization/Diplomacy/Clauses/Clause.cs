using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization.Diplomacy.Clauses
{
	/// <summary>
	/// A clause in a treaty.
	/// </summary>
	public abstract class Clause : IOwnable, IFoggable
	{
		protected Clause(Empire giver, Empire receiver)
		{
			Giver = giver;
			Receiver = receiver;
		}

		/// <summary>
		/// The empire that is offering something in this clause.
		/// </summary>
		public Empire Giver { get; set; }

		/// <summary>
		/// The empire that is receiving a benefit from this clause.
		/// </summary>
		public Empire Receiver { get; set; }

		/// <summary>
		/// Performs any per-turn action for this treaty clause, if applicable.
		/// </summary>
		public abstract void PerformAction();

		/// <summary>
		/// A description of the effects of this clause.
		/// </summary>
		public abstract string Description { get; }

		public Empire Owner
		{
			get { return Receiver; }
		}

		public Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Owned;
			if (emp == Giver)
				return Visibility.Scanned;
			// TODO - espionage on treaties
			return Visibility.Unknown;
		}

		public void Redact(Empire emp)
		{
			if (CheckVisibility(emp) < Visibility.Fogged)
				Dispose();
		}

		public bool IsMemory
		{
			get;
			set;
		}

		public double Timestamp
		{
			get;
			set;
		}

		public bool IsObsoleteMemory(Empire emp)
		{
			return false;
		}

		public long ID
		{
			get;
			set;
		}

		public void Dispose()
		{
			Galaxy.Current.UnassignID(this);
		}

		public override string ToString()
		{
			return Description;
		}
	}
}
