using FrEee.Enumerations;
using FrEee.Interfaces;
using FrEee.Objects.Space;
using FrEee.Serialization;
using System.Collections.Generic;

namespace FrEee.Objects.Civilization.Diplomacy.Clauses;

/// <summary>
/// A clause in a treaty.
/// </summary>
public abstract class Clause : IOwnable, IFoggable, IPromotable, IReferrable
{
	protected Clause(Empire giver, Empire receiver)
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
	public Empire Giver { get { return giver; } set { giver = value; } }

	public long ID
	{
		get;
		set;
	}

	public bool IsDisposed { get; set; }

	/// <summary>
	/// Is this clause in effect, or is it still a proposal?
	/// </summary>
	public bool IsInEffect { get; set; }

	public bool IsMemory
	{
		get;
		set;
	}

	public Empire Owner
	{
		get { return Receiver; }
	}

	/// <summary>
	/// The empire that is receiving a benefit from this clause.
	/// </summary>
	[DoNotSerialize]
	public Empire Receiver { get { return receiver; } set { receiver = value; } }

	public double Timestamp
	{
		get;
		set;
	}

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

	public bool IsObsoleteMemory(Empire emp)
	{
		return false;
	}

	/// <summary>
	/// Performs any per-turn action for this treaty clause, if applicable.
	/// </summary>
	public abstract void PerformAction();

	public void Redact(Empire emp)
	{
		if (CheckVisibility(emp) < Visibility.Fogged)
			Dispose();
	}

	public virtual void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
	{
		// nothing to do here...
	}

	public override string ToString()
	{
		return BriefDescription;
	}
}