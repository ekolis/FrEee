using System;
using System.Collections.Generic;
using System.Drawing;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Serialization;
using FrEee.Utility;

namespace FrEee.Processes.Construction;

/// <summary>
/// A construction queue which allows vehicles and facilities to be constructed at a space object.
/// </summary>
public interface IConstructionQueue
	: IOrderable, IFoggable, IContainable<IConstructor>
{
	/// <summary>
	/// Cargo space free, counting queued items as already constructed and in cargo.
	/// </summary>
	int CargoStorageFree { get; }
	/// <summary>
	/// Cargo space free in the entire sector, counting queued items as already constructed and in cargo.
	/// </summary>
	int CargoStorageFreeInSector { get; }
	/// <summary>
	/// The colony (if any) associated with this queue.
	/// </summary>
	Colony? Colony { get; }
	/// <summary>
	/// The ETA for completion of the whole queue, in turns.
	/// Null if there is nothing being built.
	/// </summary>
	double? Eta { get; }
	/// <summary>
	/// Facility slots free, counting queued items as already constructed and on the colony.
	/// </summary>
	int FacilitySlotsFree { get; }
	/// <summary>
	/// The ETA for completion of the first item, in turns.
	/// </summary>
	double? FirstItemEta { get; }
	/// <summary>
	/// The icon for the item being constructed.
	/// </summary>
	Image FirstItemIcon { get; }
	/// <summary>
	/// The name of the first item.
	/// </summary>
	string? FirstItemName { get; }
	Image? Icon { get; }
	/// <summary>
	/// Does this queue belong to a colony?
	/// Only colony queues can build facilities.
	/// </summary>
	bool IsColonyQueue { get; }
	/// <summary>
	/// Has construction been delayed this turn due to lack of resources etc?
	/// For avoiding spamming log messages for every item in the queue.
	/// </summary>
	[DoNotSerialize]
	bool IsConstructionDelayed { get; set; }
	bool IsIdle { get; }
	/// <summary>
	/// Is this queue equipped with a space yard?
	/// Only space yard queues can build ships and bases.
	/// </summary>
	bool IsSpaceYardQueue { get; }
	string Name { get; }
	new IList<IConstructionOrder> Orders { get; }
	/// <summary>
	/// The rate at which this queue can construct.
	/// </summary>
	ResourceQuantity Rate { get; }
	// TODO: put these rate sub-properties in a view model
	[Obsolete("This property belongs in a view model.")]
	int RateMinerals { get; }
	[Obsolete("This property belongs in a view model.")]
	int RateOrganics { get; }
	[Obsolete("This property belongs in a view model.")]
	int RateRadioactives { get; }
	/// <summary>
	/// Unspent build rate for this turn.
	/// Does not update as orders are changed on the client; only during turn processing!
	/// </summary>
	ResourceQuantity UnspentRate { get; set; }
	/// <summary>
	/// Upcoming spending on construction this turn.
	/// </summary>
	ResourceQuantity UpcomingSpending { get; }

	/// <summary>
	/// Can this queue construct something?
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	bool CanConstruct(IConstructionTemplate item);
	/// <summary>
	/// Gets the reason why this queue cannot construct an item, or null if it can be constructed.
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	string? GetReasonForBeingUnableToConstruct(IConstructionTemplate item);
}