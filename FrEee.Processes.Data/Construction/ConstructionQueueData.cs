using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Modding.Templates;
using FrEee.Modding.Abilities;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;
using FrEee.Objects.Civilization;

namespace FrEee.Processes.Construction;

/// <summary>
/// Data to represent a <see cref="ConstructionQueue"/>.
/// </summary>
public record ConstructionQueueData
{
	public bool IsOnHold { get; set; }

	public bool IsOnRepeat { get; set; }

	public required GameReference<IConstructor> Container { get; set; }

	public long ID { get; set; }

	public bool IsConstructionDelayed { get; set; }

	public bool IsDisposed { get; set; }

	// not a GameReferenceList, these orders are contained within the construction queue
	public IList<IConstructionOrder> Orders { get; set; } = [];

	public double Timestamp { get; set; }

	public ResourceQuantity UnspentRate { get; set; } = [];
}
