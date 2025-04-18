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
using FrEee.Processes.Construction;

namespace FrEee.Plugins.Default.Processes.Construction;

/// <summary>
/// Something which can construct objects.
/// </summary>
[Serializable]
public class ConstructionQueue : IConstructionQueue
{
	public ConstructionQueue(IConstructor c)
	{
		Orders = new List<IConstructionOrder>();
		Container = c;
		UnspentRate = new ResourceQuantity();
	}

	public bool IsOnHold { get; set; }

	public bool IsOnRepeat { get; set; }

	public int CargoStorageFree
	{
		get
		{
			if (Container is ICargoContainer cc)
			{
				return cc.CargoStorageFree() -
					Orders.Select(o => o.Template)
					.OfType<IDesign<IUnit>>()
					.Sum(t => t.Hull.Size);
			}
			else
			{
				return 0;
			}
		}
	}

	public int CargoStorageFreeInSector
	{
		get
		{
			var storage = Container.Sector.SpaceObjects
				.Where(sobj => sobj.Owner == Owner)
				.OfType<ICargoContainer>()
				.Sum(cc => cc.CargoStorageFree());
			var queues = Container.Sector.SpaceObjects
				.OfType<IConstructor>()
				.Where(sobj => sobj.Owner == Owner && sobj.ConstructionQueue != null)
				.Select(sobj => sobj.ConstructionQueue);
			var queuedSize = queues.Sum(q =>
				q.Orders.Select(o => o.Template)
				.OfType<IDesign<IUnit>>()
				.Sum(t => t.Hull.Size));
			return storage - queuedSize;
		}
	}

	public Colony? Colony
	{
		get
		{
			if (Container is Planet p)
			{
				return p.Colony;
			}
			else
			{
				return null;
			}
		}
	}

	[DoNotCopy]
	public IConstructor Container { get; set; }

	public double? Eta
	{
		get
		{
			if (!Orders.Any())
				return null;
			if (!Rate.Any(kvp => kvp.Value > 0))
				return double.PositiveInfinity;
			var remainingCost = Orders.Select(o =>
				o.Cost - (o.Item?.ConstructionProgress ?? []))
				.Sum();
			return remainingCost.Max(kvp => kvp.Value / (double)Rate[kvp.Key]);
		}
	}

	public int FacilitySlotsFree
	{
		get
		{
			if (Colony is null)
			{
				return 0;
			}
			else
			{
				// TODO - storage racial trait
				return ((Planet)Container).MaxFacilities - Colony.Facilities.Count - Orders.OfType<ConstructionOrder<Facility, FacilityTemplate>>().Count();
			}
		}
	}

	public double? FirstItemEta
	{
		get
		{
			if (!Orders.Any())
			{
				return null;
			}
			var remainingCost = Orders[0].Cost - (Orders[0].Item?.ConstructionProgress ?? []);
			return remainingCost.Max(kvp => kvp.Value / (double)Rate[kvp.Key]);
		}
	}

	public Image FirstItemIcon
	{
		get
		{
			return Orders.FirstOrDefault()?.Template.Icon ?? Pictures.GetSolidColorImage(Color.Transparent);
		}
	}

	public string? FirstItemName
	{
		get
		{
			return Orders.FirstOrDefault()?.Template.Name;
		}
	}

	[DoNotSerialize]
	public Image? Icon
	{
		get
		{
			return (Container as ISpaceObject)?.Icon;
		}
	}

	public long ID
	{
		get;
		set;
	}

	public bool IsColonyQueue { get { return Colony is not null; } }

	public bool IsConstructionDelayed { get; set; }

	public bool IsDisposed { get; set; }

	public bool IsIdle
	{
		get
		{
			var unlockedHulls = Mod.Current.Hulls
				.OfType<IHull<IUnit>>()
				.Where(h => h.IsUnlocked());
			return (Eta == null || Eta < 1 && !IsOnRepeat)
				&& (IsSpaceYardQueue || FacilitySlotsFree > 0 || unlockedHulls.Any() && unlockedHulls.Min(h => h.Size) <= CargoStorageFreeInSector);
		}
	}

	[DoNotSerialize]
	public bool IsMemory
	{
		get
		{
			return Container?.IsMemory ?? true;
		}
		set
		{
			if (Container == null)
				return;
			Container.IsMemory = value;
		}
	}

	public bool IsSpaceYardQueue { get { return Container.HasAbility("Space Yard"); } }

	public string Name
		=> Container.Name;

	public IList<IConstructionOrder> Orders
	{
		get;
		private set;
	}

	IEnumerable<IOrder> IOrderable.Orders
		=> Orders;

	public Empire Owner
		=> Container.Owner;

	public ResourceQuantity Rate
	{
		get
		{
			if (Empire.Current != null)
			{
				// try to use cache, rate can't change client side!
				return rate ??= Services.ConstructionQueues.ComputeRate(this);
			}
			else
				return Services.ConstructionQueues.ComputeRate(this);
		}
	}

	public int RateMinerals { get { return Rate[Resource.Minerals]; } }
	public int RateOrganics { get { return Rate[Resource.Organics]; } }
	public int RateRadioactives { get { return Rate[Resource.Radioactives]; } }

	public double Timestamp { get; set; }

	public ResourceQuantity UnspentRate { get; set; }

	public ResourceQuantity UpcomingSpending
	{
		get
		{
			var spent = new ResourceQuantity();
			if (IsOnHold)
			{
				return spent;
			}
			do
			{
				var spentThisRound = new ResourceQuantity();
				foreach (var o in Orders)
				{
					var left = o.Cost;
					if (o.Item != null)
						left -= o.Item.ConstructionProgress;
					left = ResourceQuantity.Min(left, Rate - spent);
					spent += left;
					spentThisRound += left;
				}
				if (!spentThisRound.Any(kvp => kvp.Value > 0))
					break;
			} while (IsOnRepeat);
			return spent;
		}
	}

	private ResourceQuantity? rate;

	public void AddOrder(IOrder order)
	{
		if (order == null)
			Owner.Log.Append(Container.CreateLogMessage($"Can't add a null order to {this}. Probably a bug...", logMessageType: Objects.LogMessages.LogMessageType.Error));
		else if (!(order is IConstructionOrder))
			Owner.Log.Append(Container.CreateLogMessage($"Can't add a {order.GetType()} to {this}. Probably a bug...", logMessageType: Objects.LogMessages.LogMessageType.Error));
		else
		{
			var co = (IConstructionOrder)order;
			if (co.Template == null)
				Owner.Log.Append(Container.CreateLogMessage($"Can't add an order with no template to {this}. Probably a bug...", logMessageType: Objects.LogMessages.LogMessageType.Error));
			else
				Orders.Add(co);
		}
	}

	public bool CanConstruct(IConstructionTemplate item)
	{
		return GetReasonForBeingUnableToConstruct(item) is null;
	}

	/// <summary>
	/// Only the owner of a space object can see its construction queue.
	/// </summary>
	/// <param name="emp"></param>
	/// <returns></returns>
	public Visibility CheckVisibility(Empire emp)
	{
		if (IsMemory && this.MemoryOwner() != emp)
			return Visibility.Unknown; // can't see from opponents' memories!
		var vis = Container.CheckVisibility(emp);
		if (vis == Visibility.Owned)
			return vis;
		// TODO: espionage
		return Visibility.Unknown;
	}

	public void Dispose()
	{
		if (IsDisposed)
		{
			return;
		}

		if (!IsMemory && Mod.Current is not null)
		{
			this.UpdateEmpireMemories();
		}

		Game.Current.UnassignID(this);
		Orders.Clear();
		IsDisposed = true;
	}

	/// <summary>
	/// Executes orders for a turn.
	/// </summary>
	public bool ExecuteOrders()
	{
		bool didStuff = false;

		if (IsOnHold)
			return didStuff;

		UnspentRate = Rate;
		var empty = new ResourceQuantity();
		var builtThisTurn = new HashSet<IConstructable>();
		bool done = false;
		while (!done && Orders.Any() && (Owner.StoredResources > empty || UpcomingSpending.IsEmpty))
		{
			foreach (var order in Orders.ToArray())
			{
				if (order == null)
				{
					// WTF
					Orders.Remove(order);
					continue;
				}
				var reasonForNotBuilding = GetReasonForBeingUnableToConstruct(order.Template);
				if (reasonForNotBuilding != null)
				{
					// can't build that here!
					Orders.RemoveAt(0);
					Owner.Log.Add(Container.CreateLogMessage(order.Template + " cannot be built at " + this + " because " + reasonForNotBuilding, Objects.LogMessages.LogMessageType.Error));
				}
				else
				{
					var oldProgress = new ResourceQuantity(order.Item?.ConstructionProgress);
					order.Execute(this);
					var newProgress = new ResourceQuantity(order.Item?.ConstructionProgress);
					if (newProgress < (order.Item?.Cost ?? new ResourceQuantity()) && newProgress == oldProgress && order == Orders.Last())
						done = true; // made no progress and nothing else to try and build
					if (order.CheckCompletion(this))
					{
						// upgrade facility orders place their own facilities
						if (!(order is UpgradeFacilityOrder))
						{
							order.Item.Place(Container);
						}
						Orders.Remove(order);
						if (IsOnRepeat)
						{
							var copy = order.Copy();
							copy.Reset();
							Orders.Add(copy);
						}
						builtThisTurn.Add(order.Item);
						if (order.Item is IMajorSpaceVehicle)
						{
							// trigger ship built happiness changes
							Owner.TriggerHappinessChange(hm => hm.AnyShipConstructed);
							if (Container is Planet p)
							{
								p.Colony.TriggerHappinessChange(hm => hm.ShipConstructed);
							}

						}
						if (order.Item is Facility)
						{
							// trigger facility built happiness changes
							if (Container is Planet p)
							{
								p.Colony.TriggerHappinessChange(hm => hm.FacilityConstructed);
							}

						}
					}
				}
			}

			didStuff = true;

			if (!IsOnRepeat)
				done = true;
		}
		foreach (var g in builtThisTurn.GroupBy(i => i.Template))
		{
			if (g.Count() == 1)
				Owner.Log.Add(g.First().CreateLogMessage(g.First() + " has been constructed at " + Name + ".", logMessageType: Objects.LogMessages.LogMessageType.ConstructionComplete));
			else
				Owner.Log.Add(g.First().CreateLogMessage(g.Count() + "x " + g.Key + " have been constructed at " + Name + ".", logMessageType: Objects.LogMessages.LogMessageType.ConstructionComplete));
		}
		return didStuff;
	}

	public string? GetReasonForBeingUnableToConstruct(IConstructionTemplate item)
	{
		if (item == null)
			return "Construction template does not exist.";
		if (!item.HasBeenUnlockedBy(Owner))
			return Owner + " has not yet unlocked " + item + ".";
		if (!IsSpaceYardQueue && item.RequiresSpaceYardQueue)
			return item + " requires a space yard queue.";
		if (!IsColonyQueue && item.RequiresColonyQueue)
			return item + " requires a colony queue.";
		return null;
	}

	public bool IsObsoleteMemory(Empire emp)
	{
		return Container == null
			|| Container.StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Game.Current.Timestamp - 1;
	}

	public void RearrangeOrder(IOrder order, int delta)
	{
		if (order is not null && order is not IConstructionOrder)
		{
			throw new Exception("Can't rearrange a " + order.GetType() + " in a construction queue's orders.");
		}
		var o = (IConstructionOrder)order;
		var newpos = Orders.IndexOf(o) + delta;
		if (newpos < 0)
		{
			newpos = 0;
		}
		Orders.Remove(o);
		if (newpos >= Orders.Count)
		{
			Orders.Add(o);
		}
		else
		{
			Orders.Insert(newpos, o);
		}
	}

	public void Redact(Empire emp)
	{
		// TODO - see first order in queue if queue is scanned?
		// need to add design being built to known designs too?
		if (CheckVisibility(emp) < Visibility.Owned)
		{
			Orders.DisposeAll();
			Orders.Clear();
			IsOnHold = false;
			IsOnRepeat = false;
		}
		if (CheckVisibility(emp) < Visibility.Fogged)
		{
			Dispose();
		}
	}

	public void RemoveOrder(IOrder order)
	{
		if (order is null)
		{
			Owner.Log.Add(Container.CreateLogMessage("Attempted to remove a null order from " + this + ". This is likely a game bug.", Objects.LogMessages.LogMessageType.Error));
		}
		else if (order is IConstructionOrder co)
		{
			Orders.Remove(co);
		}
		else
		{
			// do nothing, non-construction order can't exist here anyway	
		}
	}

	public override string ToString()
	{
		return Container + "'s construction queue";
	}
}
