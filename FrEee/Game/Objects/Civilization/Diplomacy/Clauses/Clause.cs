using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System.Collections.Generic;

#nullable enable

namespace FrEee.Game.Objects.Civilization.Diplomacy.Clauses
{
	/// <summary>
	/// A clause in a treaty.
	/// </summary>
	public abstract class Clause : IOwnable, IFoggable, IPromotable, IReferrable
	{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		// initialized via property
		protected Clause(Empire giver, Empire receiver)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		{
			Giver = giver;
			Receiver = receiver;
		}

		/// <summary>
		/// A description of the effects of this clause.
		/// </summary>
		public abstract string BriefDescription { get; }

		/// <summary>
		/// A description of the effects of this clause.
		/// </summary>
		public abstract string FullDescription { get; }

		/// <summary>
		/// The empire that is offering something in this clause.
		/// </summary>
		[DoNotSerialize]
		public Empire Giver { get => giver; set => giver = value; }

		public long ID { get; set; }

		public bool IsDisposed { get; set; }

		/// <summary>
		/// Is this clause in effect, or is it still a proposal?
		/// </summary>
		public bool IsInEffect { get; set; }

		public bool IsMemory { get; set; }

		public Empire Owner => Receiver;

		/// <summary>
		/// The empire that is receiving a benefit from this clause.
		/// </summary>
		[DoNotSerialize]
		public Empire Receiver { get => receiver; set => receiver = value; }

		public double Timestamp { get; set; }

		private GalaxyReference<Empire> giver { get; set; }
		private GalaxyReference<Empire> receiver { get; set; }

		public Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Owned;
			if (emp == Giver)
				return Visibility.Scanned;
			// TODO - espionage on treaties
			return Visibility.Unknown;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			Galaxy.Current.UnassignID(this);
			IsInEffect = false;
		}

		public bool IsObsoleteMemory(Empire emp) => false;

		/// <summary>
		/// Performs any per-turn action for this treaty clause, if applicable.
		/// </summary>
		public abstract void PerformAction();

		public void Redact(Empire emp)
		{
			if (CheckVisibility(emp) < Visibility.Fogged)
				Dispose();
		}

		public virtual void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable>? done = null)
		{
			// nothing to do here...
		}

		public override string ToString() => BriefDescription;
	}
}
