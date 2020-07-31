using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Orders;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

#nullable enable

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// Something which can construct objects.
	/// </summary>
	[Serializable]
	public class ConstructionQueue : IOrderable, IOwnable, IFoggable, IContainable<IConstructor>
	{
		public ConstructionQueue(IConstructor c)
		{
			Orders = new List<IConstructionOrder?>();
			Container = c;
			UnspentRate = new ResourceQuantity();
		}

		/// <summary>
		/// Are this object's orders on hold?
		/// </summary>
		public bool AreOrdersOnHold { get; set; }

		/// <summary>
		/// Should this object's orders repeat once they are completed?
		/// </summary>
		public bool AreRepeatOrdersEnabled { get; set; }

		/// <summary>
		/// Cargo space free, counting queued items as already constructed and in cargo.
		/// </summary>
		public int CargoStorageFree
		{
			get
			{
				if (Container is ICargoContainer cargoContainer)
					return cargoContainer.CargoStorageFree() - Orders.Select(o => o?.Template).OfType<IDesign<IUnit>>().Sum(t => t.Hull.Size);
				return 0;
			}
		}

		/// <summary>
		/// Cargo space free in the entire sector, counting queued items as already constructed and in cargo.
		/// </summary>
		public int CargoStorageFreeInSector
		{
			get
			{
				var storage = Container.Sector.SpaceObjects.Where(sobj => sobj.Owner == Owner)
					.OfType<ICargoContainer>().Sum(cc => cc.CargoStorageFree());
				var queues = Container.Sector.SpaceObjects.OfType<IConstructor>().Where
					(sobj => sobj.Owner == Owner && sobj.ConstructionQueue != null)
					.Select(sobj => sobj.ConstructionQueue);
				return storage - queues.Sum(q => q.Orders.Select(o => o?.Template).OfType<IDesign<IUnit>>().Sum(t => t.Hull.Size));
			}
		}

		/// <summary>
		/// The colony (if any) associated with this queue.
		/// </summary>
		public Colony? Colony
		{
			get
			{
				if (Container is Planet planet)
					return planet.Colony;
				return null;
			}
		}

		[DoNotCopy]
		public IConstructor Container { get; set; }

		/// <summary>
		/// The ETA for completion of the whole queue, in turns.
		/// Null if there is nothing being built.
		/// </summary>
		public double? Eta
		{
			get
			{
				if (!Orders.Any())
					return null;
				if (!Rate.Any(kvp => kvp.Value > 0))
					return double.PositiveInfinity;
				if (!Orders.Any())
					return 0d;
				var remainingCost = Orders.Select(o => o?.Cost - (o?.Item == null ? new ResourceQuantity() : o.Item.ConstructionProgress)).Aggregate((r1, r2) => r1 + r2);
				return remainingCost.Max(kvp => (double)kvp.Value / (double)Rate[kvp.Key]);
			}
		}

		/// <summary>
		/// Facility slots free, counting queued items as already constructed and on the colony.
		/// </summary>
		public int FacilitySlotsFree
		{
			get
			{
				if (Colony == null)
					return 0;
				// TODO - storage racial trait
				return ((Planet)Container).MaxFacilities - Colony.Facilities.Count - Orders.OfType<ConstructionOrder<Facility, FacilityTemplate>>().Count();
			}
		}

		/// <summary>
		/// The ETA for completion of the first item, in turns.
		/// </summary>
		public double? FirstItemEta
		{
			get
			{
				if (!Orders.Any())
					return null;
				var remainingCost = Orders[0]?.Cost - (Orders[0]?.Item == null ? new ResourceQuantity() : Orders[0]?.Item.ConstructionProgress);
				return remainingCost.Max(kvp => (double)kvp.Value / (double)Rate[kvp.Key]);
			}
		}

		/// <summary>
		/// The icon for the item being constructed.
		/// </summary>
		public Image? FirstItemIcon
		{
			get
			{
				if (!Orders.Any())
					return Pictures.GetSolidColorImage(Color.Transparent);
				return Orders?.First()?.Template.Icon;
			}
		}

		/// <summary>
		/// The name of the first item.
		/// </summary>
		public string? FirstItemName
		{
			get
			{
				if (!Orders.Any())
					return null;
				return Orders[0]?.Template.Name;
			}
		}

		[DoNotSerialize]
		public Image? Icon => Container?.Icon;

		public long ID { get; set; }

		/// <summary>
		/// Is this a colony queue?
		/// </summary>
		public bool IsColonyQueue => Colony != null;

		/// <summary>
		/// Has construction been delayed this turn due to lack of resources etc?
		/// For avoiding spamming log messages for every item in the queue.
		/// </summary>
		[DoNotSerialize]
		public bool IsConstructionDelayed { get; set; }

		public bool IsDisposed { get; set; }

		public bool IsIdle
		{
			get
			{
				var unlockedHulls = Mod.Current.Hulls.OfType<IHull<IUnit>>().Where(h => h.IsUnlocked());
				return (Eta == null || Eta < 1 && !AreRepeatOrdersEnabled)
					&& (IsSpaceYardQueue || FacilitySlotsFree > 0 || unlockedHulls.Any() && unlockedHulls.Min(h => h.Size) <= CargoStorageFreeInSector);
			}
		}

		// TODO - make this a DoNotSerialize property after the game ends
		public bool IsMemory
		{
			get => Container?.IsMemory ?? true;
			set
			{
				if (Container == null)
					return;
				Container.IsMemory = value;
			}
		}

		/// <summary>
		/// Is this a space yard queue?
		/// </summary>
		public bool IsSpaceYardQueue => Container.HasAbility("Space Yard");

		public string Name => Container.Name;

		public IList<IConstructionOrder?> Orders { get; private set; }

		public Empire Owner => Container.Owner;

		/// <summary>
		/// The rate at which this queue can construct.
		/// </summary>
		public ResourceQuantity Rate
		{
			get
			{
				if (Empire.Current != null)
				{
					// try to use cache, rate can't change client side!
					if (rate == null)
						rate = ComputeRate();
					return rate;
				}
				else
					return ComputeRate();
			}
		}

		public int RateMinerals => Rate[Resource.Minerals];
		public int RateOrganics => Rate[Resource.Organics];
		public int RateRadioactives => Rate[Resource.Radioactives];
		public double Timestamp { get; set; }

		/// <summary>
		/// Unspent build rate for this turn.
		/// Does not update as orders are changed on the client; only during turn processing!
		/// </summary>
		public ResourceQuantity UnspentRate { get; set; }

		/// <summary>
		/// Upcoming spending on construction this turn.
		/// </summary>
		public ResourceQuantity UpcomingSpending
		{
			get
			{
				var spent = new ResourceQuantity();
				if (AreOrdersOnHold)
					return spent;
				do
				{
					var spentThisRound = new ResourceQuantity();
					foreach (var o in Orders)
					{
						var left = o?.Cost;
						if (o?.Item != null)
							left -= o.Item.ConstructionProgress;
						left = ResourceQuantity.Min(left, Rate - spent);
						spent += left;
						spentThisRound += left;
					}
					if (!spentThisRound.Any(kvp => kvp.Value > 0))
						break;
				} while (AreRepeatOrdersEnabled);
				return spent;
			}
		}

		IList<IOrder> IOrderable.Orders => Orders.Cast<IOrder>().ToList();

		private ResourceQuantity? rate;

		public void AddOrder(IOrder order)
		{
			if (order == null)
				Owner.Log.Append(Container.CreateLogMessage($"Can't add a null order to {this}. Probably a bug...",logMessageType: LogMessages.LogMessageType.Error));
			else if (!(order is IConstructionOrder))
				Owner.Log.Append(Container.CreateLogMessage($"Can't add a {order.GetType()} to {this}. Probably a bug...", logMessageType: LogMessages.LogMessageType.Error));
			else
			{
				var co = (IConstructionOrder)order;
				if (co.Template == null)
					Owner.Log.Append(Container.CreateLogMessage($"Can't add an order with no template to {this}. Probably a bug...", logMessageType: LogMessages.LogMessageType.Error));
				else
					Orders.Add(co);
			}
		}

		/// <summary>
		/// Can this queue construct something?
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool CanConstruct(IConstructionTemplate item) => GetReasonForBeingUnableToConstruct(item) == null;

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
			return Visibility.Unknown;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			if (!IsMemory && Mod.Current != null)
				this.UpdateEmpireMemories();
			Galaxy.Current.UnassignID(this);
			Orders.Clear();
			IsDisposed = true;
		}

		/// <summary>
		/// Executes orders for a turn.
		/// </summary>
		public bool ExecuteOrders()
		{
			bool didStuff = false;

			if (AreOrdersOnHold)
				return didStuff;

			UnspentRate = Rate;
			var empty = new ResourceQuantity();
			var builtThisTurn = new HashSet<IConstructable?>();
			bool done = false;
			while (!done && Orders.Any() && (Owner.StoredResources > empty || UpcomingSpending.IsEmpty))
			{
				var numOrders = Orders.Count;
				var spentThisRound = new ResourceQuantity();

				foreach (var order in Orders.Cast<IConstructionOrder>().ToArray())
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
						Owner.Log.Add(Container.CreateLogMessage(order.Template + " cannot be built at " + this + " because " + reasonForNotBuilding, LogMessages.LogMessageType.Error));
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
								order?.Item?.Place(Container);
							Orders.Remove(order);
							if (AreRepeatOrdersEnabled)
							{
								var copy = order?.Copy();
								copy?.Reset();
								Orders.Add(copy);
							}
							builtThisTurn.Add(order?.Item);
							if (order?.Item is Ship || order?.Item is Base)
							{
								// trigger ship built happiness changes
								Owner.TriggerHappinessChange(hm => hm.AnyShipConstructed);
								if (Container is Planet p)
									p.Colony.TriggerHappinessChange(hm => hm.ShipConstructed);

							}
							if (order?.Item is Facility)
							{
								// trigger facility built happiness changes
								if (Container is Planet p)
									p.Colony.TriggerHappinessChange(hm => hm.FacilityConstructed);

							}
						}
					}
				}

				didStuff = true;

				if (!AreRepeatOrdersEnabled)
					done = true;
			}
			foreach (var g in builtThisTurn.GroupBy(i => i?.Template))
			{
				if (g.Count() == 1)
					Owner.Log.Add(g.First().CreateLogMessage(g.First() + " has been constructed at " + Name + ".", logMessageType: LogMessages.LogMessageType.ConstructionComplete));
				else
					Owner.Log.Add(g.First().CreateLogMessage(g.Count() + "x " + g.Key + " have been constructed at " + Name + ".", logMessageType: LogMessages.LogMessageType.ConstructionComplete));
			}
			return didStuff;
		}

		/// <summary>
		/// Gets the reason why this queue cannot construct an item, or null if it can be constructed.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
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
			return Container == null || Container.StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Galaxy.Current.Timestamp - 1;
		}

		public void RearrangeOrder(IOrder order, int delta)
		{
			if (order != null && !(order is IConstructionOrder))
				throw new Exception("Can't rearrange a " + order.GetType() + " in a construction queue's orders.");
			var o = (IConstructionOrder?)order;
			var newpos = Orders.IndexOf(o) + delta;
			if (newpos < 0)
				newpos = 0;
			Orders.Remove(o);
			if (newpos >= Orders.Count)
				Orders.Add(o);
			else
				Orders.Insert(newpos, o);
		}

		public void Redact(Empire emp)
		{
			// TODO - see first order in queue if queue is scanned?
			// need to add design being built to known designs too?
			if (CheckVisibility(emp) < Visibility.Owned)
			{
				Orders.DisposeAll();
				Orders.Clear();
				AreOrdersOnHold = false;
				AreRepeatOrdersEnabled = false;
			}
			if (CheckVisibility(emp) < Visibility.Fogged)
				Dispose();
		}

		public void RemoveOrder(IOrder order)
		{
			if (order == null)
				Owner.Log.Add(Container.CreateLogMessage("Attempted to remove a null order from " + this + ". This is likely a game bug.", LogMessages.LogMessageType.Error));
			else if (!(order is IConstructionOrder))
				return; // order can't exist here anyway
			else
				Orders.Remove((IConstructionOrder)order);
		}

		public override string ToString() => Container + "'s construction queue";

		private ResourceQuantity ComputeRate()
		{
			var rate = ComputeSYAbilityRate();
			if (Colony != null)
			{
				if (rate == null)
					rate = Mod.Current.Settings.DefaultColonyConstructionRate;

				// apply population modifier
				var pop = Colony.Population.Sum(p => p.Value);
				if (pop == 0)
					return new ResourceQuantity();
				rate *= Mod.Current.Settings.GetPopulationConstructionFactor(pop);

				// apply mood modifier
				// TODO - load mood modifier from mod
				var moodModifier = Colony.Mood == Mood.Rioting ? 0 : 100;
				rate *= moodModifier / 100d;

				var ratios = Colony.Population.Select(p => new { Race = p.Key, Ratio = (double)p.Value / (double)pop });

				// apply racial trait planetary SY modifier
				// TODO - should Planetary SY Rate apply only to planets that have space yards, or to all planetary construction queues?
				double traitmod = 1d;
				foreach (var ratio in ratios)
					traitmod += (ratio.Race.GetAbilityValue("Planetary SY Rate").ToDouble() / 100d) * ratio.Ratio;
				rate *= traitmod;

				// apply aptitude modifier
				if (IsSpaceYardQueue)
				{
					double aptmod = 0d;
					foreach (var ratio in ratios)
						aptmod += ((ratio.Race.Aptitudes[Aptitude.Construction.Name] / 100d)) * ratio.Ratio;
					rate *= aptmod;

					// apply culture modifier
					rate *= (100d + (Owner?.Culture?.Construction ?? 0)) / 100d;
				}
			}
			if (rate == null)
				rate = new ResourceQuantity();
			if (Container is IVehicle)
			{
				// apply aptitude modifier for empire's primary race
				rate *= Owner?.PrimaryRace?.Aptitudes[Aptitude.Construction.Name] ?? 100 / 100d;
			}

			return rate;
		}

		private ResourceQuantity? ComputeSYAbilityRate()
		{
			if (Container.HasAbility("Space Yard"))
			{
				var rate = new ResourceQuantity();
				// TODO - moddable resources?
				for (int i = 1; i <= 3; i++)
				{
					var amount = Container.GetAbilityValue("Space Yard", 2, true, true, a => a.Value1 == i.ToString()).ToInt();
					Resource? res = null;
					if (i == 1)
						res = Resource.Minerals;
					else if (i == 2)
						res = Resource.Organics;
					else if (i == 3)
						res = Resource.Radioactives;
					rate[res] = amount;
				}
				return rate;
			}
			else
				return null;
		}
	}
}
