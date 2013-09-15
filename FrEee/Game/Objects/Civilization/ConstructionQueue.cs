using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
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
	[Serializable]
	public class ConstructionQueue : IOrderable, IOwnable, IFoggable
	{
		public ConstructionQueue(ISpaceObject sobj)
		{
			Orders = new List<IConstructionOrder>();
			SpaceObject = sobj;
			UnspentRate = new ResourceQuantity();
		}

		/// <summary>
		/// Is this a space yard queue?
		/// </summary>
		public bool IsSpaceYardQueue { get { return SpaceObject.HasAbility("Space Yard"); } }

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
				if (SpaceObject is Planet)
					return ((Planet)SpaceObject).Colony;
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
					double aptmod = 0d;
					foreach (var ratio in ratios)
						aptmod += ((ratio.Race.Aptitudes[Aptitude.Construction.Name] / 100d)) * ratio.Ratio;
					rate *= aptmod;

				}
				if (rate == null)
					rate = new ResourceQuantity();
				if (Colony == null)
				{
					// apply aptitude modifier for empire's primary race
					rate *= Owner.PrimaryRace.Aptitudes[Aptitude.Construction.Name] / 100d + 1d;
				}
				

				return rate;
			}
		}

		private ResourceQuantity ComputeSYAbilityRate()
		{
			if (SpaceObject.HasAbility("Space Yard"))
			{
				var rate = new ResourceQuantity();
				// TODO - moddable resources?
				for (int i = 1; i <= 3; i++)
				{
					var amount = SpaceObject.GetAbilityValue("Space Yard", 2, a => a.Value1 == i.ToString()).ToInt();
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

		public ISpaceObject SpaceObject { get; set; }

		[DoNotSerialize] public Image Icon
		{
			get
			{
				return SpaceObject.Icon;
			}
		}

		public Empire Owner
		{
			get { return SpaceObject.Owner; }
		}

		public string Name
		{
			get { return SpaceObject.Name; }
		}

		/// <summary>
		/// Executes orders for a turn.
		/// </summary>
		public void ExecuteOrders()
		{
			UnspentRate = Rate;
			var empty = new ResourceQuantity();
			while (UnspentRate > empty && Orders.Any())
			{
				var numOrders = Orders.Count;

				foreach (var order in Orders.Cast<IConstructionOrder>().ToArray())
				{
					var reasonForNotBuilding = GetReasonForBeingUnableToConstruct(order.Template);
					if (reasonForNotBuilding != null)
					{
						// can't build that here!
						Orders.RemoveAt(0);
						Owner.Log.Add(SpaceObject.CreateLogMessage(order.Template + " cannot be built at " + this + " because " + reasonForNotBuilding));
					}
					else
					{
						order.Execute(this);
						if (order.IsComplete)
						{
							order.Item.Place(SpaceObject);
							Orders.Remove(order);
							Owner.Log.Add(order.Item.CreateLogMessage(order.Item + " has been constructed at " + Name + "."));
						}
					}
				}

				if (Orders.Count == numOrders)
					break; // couldn't accomplish any orders
			}
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
		public int? FirstItemEta
		{
			get
			{
				if (!Orders.Any())
					return null;
				var remainingCost = Orders[0].Template.Cost - (Orders[0].Item == null ? new ResourceQuantity() : Orders[0].Item.ConstructionProgress);
				return (int)Math.Ceiling(remainingCost.Max(kvp => (double)kvp.Value / (double)Rate[kvp.Key]));
			}
		}

		/// <summary>
		/// The ETA for completion of the whole queue, in turns.
		/// </summary>
		public int? Eta
		{
			get
			{
				if (!Orders.Any())
					return null;
				var remainingCost = Orders.Select(o => o.Template.Cost - (o.Item == null ? new ResourceQuantity() : o.Item.ConstructionProgress)).Aggregate((r1, r2) => r1 + r2);
				return (int)Math.Ceiling(remainingCost.Max(kvp => (double)kvp.Value / (double)Rate[kvp.Key]));
			}
		}


		public void AddOrder(IOrder order)
		{
			if (!(order is IConstructionOrder))
				throw new Exception("Can't add a " + order.GetType() + " to a construction queue's orders.");
			Orders.Add((IConstructionOrder)order);
		}

		public void RemoveOrder(IOrder order)
		{
			if (order != null && !(order is IConstructionOrder))
				throw new Exception("Can't remove a " + order.GetType() + " from a construction queue's orders.");
			Orders.Remove((IConstructionOrder)order);
		}

		public void RearrangeOrder(IOrder order, int delta)
		{
			if (!(order is IConstructionOrder))
				throw new Exception("Can't rearrange a " + order.GetType() + " in a construction queue's orders.");
			var o = (IConstructionOrder)order;
			var newpos = Orders.IndexOf(o) + delta;
			Orders.Remove(o);
			Orders.Insert(newpos, o);
		}

		public void Dispose()
		{
			Galaxy.Current.UnassignID(this);
		}

		/// <summary>
		/// Only the owner of a space object can see its construction queue.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			var vis = SpaceObject.CheckVisibility(emp);
			if (vis == Visibility.Owned)
				return vis;
			return Visibility.Unknown;
		}

		public void Redact(Empire emp)
		{
			if (CheckVisibility(emp) < Visibility.Visible)
				Dispose();
		}
	}
}
