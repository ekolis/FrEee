using FrEee.Objects.GameState;
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

	public IEnumerable<IReferrable> Referrables => [];

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
	public Empire Giver { get; set; }

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
	public Empire Receiver { get; set; }

	public double Timestamp
	{
		get;
		set;
	}

	// giver and receiver are serialized, they are used to populate Giver and Recevier when deserialized
	private GameReference<Empire> giver
	{
		get => Giver;
		set => Giver = value;
	}
	private GameReference<Empire> receiver
	{
		get => Receiver;
		set => Receiver = value;
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

	public void Dispose()
	{
		if (IsDisposed)
			return;
		Game.Current.UnassignID(this);
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

	public virtual IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
	{
		// nothing to do here...
		return this;
	}

	public override string ToString()
	{
		return BriefDescription;
	}
}