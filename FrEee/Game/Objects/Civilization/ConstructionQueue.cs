﻿using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.LogMessages;
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
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// Something which can construct objects.
	/// </summary>
	[Serializable]
	public class ConstructionQueue : IOrderable, IOwnable, IFoggable, IContainable<ISpaceObject>
	{
		public ConstructionQueue(ISpaceObject sobj)
		{
			Orders = new List<IConstructionOrder>();
			Container = sobj;
			UnspentRate = new ResourceQuantity();
		}

		/// <summary>
		/// Is this a space yard queue?
		/// </summary>
		public bool IsSpaceYardQueue { get { return Container.HasAbility("Space Yard"); } }

		/// <summary>
		/// Is this a colony queue?
		/// </summary>
		public bool IsColonyQueue { get { return Colony != null; } }

		/// <summary>
		/// The colony (if any) associated with this queue.
		/// </summary>
		public Colony Colony
		{
			get
			{
				if (Container is Planet)
					return ((Planet)Container).Colony;
				return null;
			}
		}

		/// <summary>
		/// Can this queue construct something?
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool CanConstruct(IConstructionTemplate item)
		{
			return GetReasonForBeingUnableToConstruct(item) == null;
		}

		/// <summary>
		/// Gets the reason why this queue cannot construct an item, or null if it can be constructed.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public string GetReasonForBeingUnableToConstruct(IConstructionTemplate item)
		{
			if (!item.HasBeenUnlockedBy(Owner))
				return Owner + " has not yet unlocked " + item + ".";
			if (!IsSpaceYardQueue && item.RequiresSpaceYardQueue)
				return item + " requires a space yard queue.";
			if (!IsColonyQueue && item.RequiresColonyQueue)
				return item + " requires a colony queue.";
			return null;
		}

		/// <summary>
		/// The rate at which this queue can construct.
		/// </summary>
		public ResourceQuantity Rate
		{
			get
			{
				var rate = ComputeSYAbilityRate();
				if (Colony != null)
				{
					if (rate == null)
						rate = Mod.Current.Settings.DefaultColonyConstructionRate;

					// apply population modifier
					var pop = Colony.Population.Sum(p => p.Value);
					rate *= Mod.Current.Settings.GetPopulationConstructionFactor(pop);

					// TODO - apply happiness modifier

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
						rate *= (100d + Owner.Culture.Construction) / 100d;
					}

				}
				if (rate == null)
					rate = new ResourceQuantity();
				if (Container is IVehicle)
				{
					// apply aptitude modifier for empire's primary race
					rate *= Owner.PrimaryRace.Aptitudes[Aptitude.Construction.Name] / 100d + 1d;
				}
				

				return rate;
			}
		}

		public int RateMinerals { get { return Rate[Resource.Minerals]; } }

		public int RateOrganics { get { return Rate[Resource.Organics]; } }

		public int RateRadioactives { get { return Rate[Resource.Radioactives]; } }

		private ResourceQuantity ComputeSYAbilityRate()
		{
			if (Container.HasAbility("Space Yard"))
			{
				var rate = new ResourceQuantity();
				// TODO - moddable resources?
				for (int i = 1; i <= 3; i++)
				{
					var amount = Container.GetAbilityValue("Space Yard", 2, true, a => a.Value1 == i.ToString()).ToInt();
					Resource res = null;
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

		/// <summary>
		/// Unspent build rate for this turn.
		/// Does not update as orders are changed on the client; only during turn processing!
		/// </summary>
		public ResourceQuantity UnspentRate { get; set; }

		IEnumerable<IOrder> IOrderable.Orders
		{
			get
			{
				return Orders;
			}
		}

		public IList<IConstructionOrder> Orders
		{
			get;
			private set;
		}

		public long ID
		{
			get;
			set;
		}

		[DoNotCopy]
		public ISpaceObject Container { get; set; }

		[DoNotSerialize] public Image Icon
		{
			get
			{
				return Container.Icon;
			}
		}

		public Empire Owner
		{
			get { return Container.Owner; }
		}

		public string Name
		{
			get { return Container.Name; }
		}

		/// <summary>
		/// Executes orders for a turn.
		/// </summary>
		public bool ExecuteOrders()
		{
			UnspentRate = Rate;
			bool didStuff = false;
			var empty = new ResourceQuantity();
			var builtThisTurn = new HashSet<IConstructable>();
			while (Orders.Any() && ResourceQuantity.Min(Owner.StoredResources, UpcomingSpending) > empty)
			{
				var numOrders = Orders.Count;

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
						Owner.Log.Add(Container.CreateLogMessage(order.Template + " cannot be built at " + this + " because " + reasonForNotBuilding));
					}
					else
					{
						order.Execute(this);
						if (order.CheckCompletion(this))
						{
							order.Item.Place(Container);
							Orders.Remove(order);
							builtThisTurn.Add(order.Item);
						}
					}
				}

				didStuff = true;

				if (Orders.Count == numOrders)
					break; // couldn't accomplish any orders
			}
			foreach (var g in builtThisTurn.GroupBy(i => i.Template))
			{
				if (g.Count() == 1)
					Owner.Log.Add(g.First().CreateLogMessage(g.First() + " has been constructed at " + Name + "."));
				else
					Owner.Log.Add(g.First().CreateLogMessage(g.Count() + "x " + g.Key + " have been constructed at " + Name + "."));
			}
			return didStuff;
		}

		/// <summary>
		/// The name of the first item.
		/// </summary>
		public string FirstItemName
		{
			get
			{
				if (!Orders.Any())
					return null;
				return Orders[0].Template.Name;
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
				var remainingCost = Orders[0].Template.Cost - (Orders[0].Item == null ? new ResourceQuantity() : Orders[0].Item.ConstructionProgress);
				return remainingCost.Max(kvp => (double)kvp.Value / (double)Rate[kvp.Key]);
			}
		}

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
				var remainingCost = Orders.Select(o => o.Template.Cost - (o.Item == null ? new ResourceQuantity() : o.Item.ConstructionProgress)).Aggregate((r1, r2) => r1 + r2);
					return remainingCost.Max(kvp => (double)kvp.Value / (double)Rate[kvp.Key]);
			}
		}

		public void AddOrder(IOrder order)
		{
			if (order == null)
				throw new Exception("Can't add a null order to a construction queue's orders.");
			if (!(order is IConstructionOrder))
				throw new Exception("Can't add a " + order.GetType() + " to a construction queue's orders.");
			Orders.Add((IConstructionOrder)order);
		}

		public void RemoveOrder(IOrder order)
		{
			if (order == null)
				Owner.Log.Add(Container.CreateLogMessage("Attempted to remove a null order from " + this + ". This is likely a game bug."));
			else if (!(order is IConstructionOrder))
				throw new Exception("Can't remove a " + order.GetType() + " from a construction queue's orders.");
			else
				Orders.Remove((IConstructionOrder)order);
		}

		public void RearrangeOrder(IOrder order, int delta)
		{
			if (order != null && !(order is IConstructionOrder))
				throw new Exception("Can't rearrange a " + order.GetType() + " in a construction queue's orders.");
			var o = (IConstructionOrder)order;
			var newpos = Orders.IndexOf(o) + delta;
			if (newpos < 0)
				newpos = 0;
			Orders.Remove(o);
			if (newpos >= Orders.Count)
				Orders.Add(o);
			else
				Orders.Insert(newpos, o);
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			if (!IsMemory && Mod.Current != null) // don't update memories if patching mod
				this.UpdateEmpireMemories();
			Galaxy.Current.UnassignID(this);
		}

		/// <summary>
		/// Only the owner of a space object can see its construction queue.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			var vis = Container.CheckVisibility(emp);
			if (vis == Visibility.Owned)
				return vis;
			return Visibility.Unknown;
		}

		public void Redact(Empire emp)
		{
			if (CheckVisibility(emp) < Visibility.Visible)
				Dispose();
		}

		public override string ToString()
		{
			return Container + "'s construction queue";
		}

		/// <summary>
		/// Upcoming spending on construction this turn.
		/// </summary>
		public ResourceQuantity UpcomingSpending
		{
			get
			{
				var spent = new ResourceQuantity();
				foreach (var o in Orders)
				{
					var left = o.Template.Cost;
					if (o.Item != null)
						left -= o.Item.ConstructionProgress;
					left = ResourceQuantity.Min(left, Rate - spent);
					spent += left;
				}
				return spent;
			}
		}

		/// <summary>
		/// Cargo space free, counting queued items as already constructed and in cargo.
		/// </summary>
		public int CargoStorageFree
		{
			get
			{
				if (!(Container is ICargoContainer))
					return 0;
				return ((ICargoContainer)Container).CargoStorageFree() - Orders.Select(o => o.Template).OfType<IDesign<IUnit>>().Sum(t => t.Hull.Size);
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
				var queues = Container.Sector.SpaceObjects.Where
					(sobj => sobj.Owner == Owner && sobj.ConstructionQueue != null)
					.Select(sobj => sobj.ConstructionQueue);
				return storage - queues.Sum(q => q.Orders.Select(o => o.Template).OfType<IDesign<IUnit>>().Sum(t => t.Hull.Size));
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
				return ((Planet)Container).MaxFacilities - Colony.Facilities.Count - Orders.Select(o => o.Template).OfType<FacilityTemplate>().Count();
			}
		}

		/// <summary>
		/// The icon for the item being constructed.
		/// </summary>
		public Image FirstItemIcon
		{
			get
			{
				if (!Orders.Any())
					return Pictures.GetSolidColorImage(Color.Transparent);
				return Orders.First().Template.Icon;
			}
		}

		public bool IsIdle
		{
			get { return Eta == null || Eta < 1; }
		}

		// TODO - make this a DoNotSerialize property after the game ends
		public bool IsMemory
		{
			get
			{
				return Container.IsMemory;
			}
			set
			{
				Container.IsMemory = value;
			}
		}

		public double Timestamp { get; set; }

		public bool IsObsoleteMemory(Empire emp)
		{
			return Container == null || Container.StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Galaxy.Current.Timestamp - 1;
		}

		public bool IsDisposed { get; set; }
	}
}
