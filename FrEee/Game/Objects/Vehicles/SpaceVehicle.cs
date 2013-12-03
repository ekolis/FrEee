using AutoMapper;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Orders;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	/// <summary>
	/// A vehicle which operates in space.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public abstract class SpaceVehicle : Vehicle, ICargoTransferrer, IMobileSpaceObject<SpaceVehicle>
	{
		public SpaceVehicle()
		{
			Orders = new List<IOrder<SpaceVehicle>>();
			constructionQueue = new ConstructionQueue(this);
			Cargo = new Cargo();
		}

		/// <summary>
		/// Amount of movement remaining for this turn.
		/// </summary>
		public int MovementRemaining { get; set; }

		/// <summary>
		/// The amount of supply present on this vehicle.
		/// </summary>
		public int SupplyRemaining { get; set; }

		IEnumerable<IOrder> IOrderable.Orders
		{
			get { return Orders; }
		}

		public IList<IOrder<SpaceVehicle>> Orders
		{
			get;
			private set;
		}

		public bool ExecuteOrders()
		{
			bool didStuff = false;
			if (Galaxy.Current.NextTickSize == double.PositiveInfinity)
				TimeToNextMove = 0;
			else
				TimeToNextMove -= Galaxy.Current.NextTickSize;
			while (TimeToNextMove <= 0 && Orders.Any())
			{
				Orders.First().Execute(this);
				if (Orders.First().IsComplete)
					Orders.RemoveAt(0);
				didStuff = true;
			}
			return didStuff;
		}

		/// <summary>
		/// Fractional turns until the vehicle has saved up another move point.
		/// </summary>
		[DoNotSerialize]
		public double TimeToNextMove
		{
			get;
			set;
		}

		/// <summary>
		/// The fraction of a turn that moving one sector takes.
		/// </summary>
		public double TimePerMove
		{
			get { return 1.0 / (double)Speed; }
		}

		/// <summary>
		/// Can this space vehicle warp?
		/// </summary>
		public abstract bool CanWarp { get; }

		private ConstructionQueue constructionQueue { get; set; }

		public ConstructionQueue ConstructionQueue
		{
			get
			{
				// only vehicles with a space yard that are not under construction have a construction queue
				if (this.HasAbility("Space Yard") && Sector != null)
					return constructionQueue;
				else
					return null;
			}
		}

		/// <summary>
		/// Space vehicles can be placed in fleets.
		/// </summary>
		public bool CanBeInFleet
		{
			get { return true; }
		}

		/// <summary>
		/// Space vehicles' cargo storage depends on their abilities.
		/// </summary>
		public int CargoStorage
		{
			get { return this.GetAbilityValue("Cargo Storage").ToInt(); }
		}

		/// <summary>
		/// Space vehicles' supply storage depends on their abilities.
		/// </summary>
		public int SupplyStorage
		{
			get { return this.GetAbilityValue("Supply Storage").ToInt(); }
		}

		/// <summary>
		/// Space vehicles do not have infinite supplies unless they have a quantum reactor or they are bases.
		/// </summary>
		public virtual bool HasInfiniteSupplies
		{
			get { return this.HasAbility("Quantum Reactor"); }
		}

		public Cargo Cargo { get; set; }

		public void AddOrder(IOrder order)
		{
			if (!(order is IOrder<SpaceVehicle>))
				throw new Exception("Can't add a " + order.GetType() + " to an autonomous space vehicle's orders.");
			Orders.Add((IOrder<SpaceVehicle>)order);
		}

		public void RemoveOrder(IOrder order)
		{
			if (order != null && !(order is IOrder<SpaceVehicle>))
				throw new Exception("Can't remove a " + order.GetType() + " from an autonomous space vehicle's orders.");
			Orders.Remove((IOrder<SpaceVehicle>)order);
		}

		public void RearrangeOrder(IOrder order, int delta)
		{
			if (order != null && !(order is IOrder<SpaceVehicle>))
				throw new Exception("Can't rearrange a " + order.GetType() + " in an autonomous space vehicle's orders.");
			var o = (IOrder<SpaceVehicle>)order;
			var newpos = Orders.IndexOf(o) + delta;
			Orders.Remove(o);
			if (newpos < 0)
				newpos = 0;
			if (newpos > Orders.Count)
				newpos = Orders.Count;
			Orders.Insert(newpos, o);
		}

		[DoNotSerialize]
		public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> DijkstraMap
		{
			get;
			set;
		}

		public override void Dispose()
		{
			if (IsDisposed)
				return;
			var sys = this.FindStarSystem();
			if (sys != null)
				sys.Remove(this);
			base.Dispose();
			this.UpdateEmpireMemories();
		}

		public override Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Owned;
			if (this.FindStarSystem() == null)
				return Visibility.Unknown;

			// You can always scan ships you are in combat with.
			if (Battle.Current.Any(b => b.Combatants.Contains(this) && b.Combatants.Any(c => c.Owner == emp)))
				return Visibility.Scanned;

			// TODO - cloaking
			var seers = this.FindStarSystem().FindSpaceObjects<ISpaceObject>(sobj => sobj.Owner == emp).Flatten();
			if (!seers.Any())
			{
				var known = emp.Memory[ID];
				if (known != null && this.GetType() == known.GetType())
					return Visibility.Fogged;
				else if(Battle.Previous.Any(b => b.Combatants.Contains(this) && b.Combatants.Any(c => c.Owner == emp)))
					return Visibility.Fogged;
				else
					return Visibility.Unknown;
			}
			var scanners = seers.Where(sobj => sobj.GetAbilityValue("Long Range Scanner").ToInt() >= Pathfinder.Pathfind(null, sobj.FindSector(), this.FindSector(), false, false, DijkstraMap).Count());
			if (scanners.Any())
				return Visibility.Scanned;
			return Visibility.Visible;
		}


		public override void Redact(Empire emp)
		{
			var visibility = CheckVisibility(emp);

			if (visibility < Visibility.Owned)
			{
				// can't see orders unless it's your vehicle
				Orders.Clear();

				// can only see space used by cargo, not actual cargo
				Cargo.SetFakeSize();
			}

			// Can't see the ship's components if it's not scanned
			// TODO - let player see design of previously scanned ship if the ship has not been refit
			if (visibility < Visibility.Scanned)
			{
				// create fake design and clear component list
				var d = new Design<SpaceVehicle>();
				d.Hull = (IHull<SpaceVehicle>)Design.Hull;
				d.Owner = Design.Owner;
				Design = d;
				Components.Clear();
			}

			if (visibility < Visibility.Fogged)
				Dispose();
			else if (visibility == Visibility.Fogged)
			{
				var known = emp.Memory[ID];
				if (known != null && known.GetType() == GetType())
					known.CopyTo(this);
			}
		}

		public bool IsIdle
		{
			get
			{
				return (Speed > 0 && !Orders.Any()) || (ConstructionQueue != null && ConstructionQueue.IsIdle);
			}
		}

		/// <summary>
		/// Vehicles cannot have population per se.
		/// </summary>
		public long PopulationStorageFree
		{
			get { return 0; }
		}

		public long AddPopulation(Race race, long amount)
		{
			var canCargo = Math.Min(amount, (long)(this.CargoStorageFree() / Mod.Current.Settings.PopulationSize));
			amount -= canCargo;
			Cargo.Population[race] += canCargo;
			return amount;
		}

		public long RemovePopulation(Race race, long amount)
		{
			var canCargo = Math.Min(amount, Cargo.Population[race]);
			amount -= canCargo;
			Cargo.Population[race] -= canCargo;
			return amount;
		}

		public bool AddUnit(IUnit unit)
		{
			if (this.CargoStorageFree() >= unit.Design.Hull.Size)
			{
				Cargo.Units.Add(unit);
				return true;
			}
			return false;
		}

		public bool RemoveUnit(IUnit unit)
		{
			if (Cargo.Units.Contains(unit))
			{
				Cargo.Units.Remove(unit);
				return true;
			}
			return false;
		}

		Sector ILocated.Sector
		{
			get { return Sector; }
		}

		public IDictionary<Race, long> AllPopulation
		{
			get { return Cargo.Population; }
		}

		public Fleet Container
		{
			get { return Galaxy.Current.FindSpaceObjects<Fleet>(f => f.Vehicles.Contains(this)).Flatten().Flatten().SingleOrDefault(); }
		}

		/// <summary>
		/// When a space vehicle spends time, all of its units in cargo that can fly in space should spend time too.
		/// TODO - It should also perform construction here...
		/// </summary>
		/// <param name="timeElapsed"></param>
		public void SpendTime(double timeElapsed)
		{
			TimeToNextMove += timeElapsed;
			foreach (var u in Cargo.Units.OfType<IMobileSpaceObject>())
				u.SpendTime(timeElapsed);
		}


		public IEnumerable<IUnit> AllUnits
		{
			get
			{
				if (this is IUnit)
					yield return (IUnit)this;
				if (Cargo != null)
				{
					foreach (var u in Cargo.Units)
						yield return u;
				}
			}
		}

		[DoNotSerialize]
		[IgnoreMap]
		public override Sector Sector
		{
			get
			{
				return this.FindSector();
			}
			set
			{
				if (value == null)
				{
					if (Sector != null)
						Sector.Remove(this);
				}
				else
					value.Place(this);
				foreach (var v in Cargo.Units.OfType<IMobileSpaceObject>())
					v.Sector = value;
			}
		}

		public override StarSystem StarSystem
		{
			get { return Sector == null ? null : Sector.StarSystem; }
		}

		public int? Size
		{
			get { return Design.Hull.Size; }
		}

		public override bool IsObsoleteMemory(Empire emp)
		{
			if (StarSystem == null)
				return Timestamp < Galaxy.Current.Timestamp - 1;
			return StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Galaxy.Current.Timestamp - 1;
		}

		public override IAbilityObject Parent
		{
			get
			{
				if (Container != null)
					return Container;
				return base.Parent;
			}
		}
	}
}
