﻿using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

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
			StoredResources = new ResourceQuantity();
		}

		public IDictionary<Race, long> AllPopulation
		{
			get { return Cargo.Population; }
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

		/// <summary>
		/// Space vehicles can be placed in fleets.
		/// </summary>
		public bool CanBeInFleet
		{
			get { return true; }
		}

		public bool CanBeObscured => true;

		/// <summary>
		/// Can this space vehicle warp?
		/// </summary>
		public abstract bool CanWarp { get; }

		public Cargo Cargo { get; set; }

		/// <summary>
		/// Space vehicles' cargo storage depends on their abilities.
		/// </summary>
		public int CargoStorage
		{
			get { return this.GetAbilityValue("Cargo Storage").ToInt(); }
		}

		public override int CombatSpeed => Mod.Current.Settings.CombatSpeedPercentPerStrategicSpeed.PercentOfRounded(StrategicSpeed) + this.GetAbilityValue("Combat Movement").ToInt();

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

		public Fleet Container
		{
			get; set;
		}

		[DoNotSerialize]
		public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> DijkstraMap
		{
			get;
			set;
		}

		/// <summary>
		/// Supply usage per sector to move.
		/// Sum of all thrust-generating components' Supply Usage values.
		/// This means that ships with more engines burn more supplies per sector!
		/// </summary>
		public int EngineSupplyBurnRate
		{
			get
			{
				return Components.Where(c =>
					!c.IsDestroyed &&
					(
						c.HasAbility("Standard Ship Movement") ||
						c.HasAbility("Movement Bonus") ||
						c.HasAbility("Extra Movement Generation") ||
						c.HasAbility("Vehicle Speed")
					)).Sum(c => c.Template.SupplyUsage);
			}
		}

		public ResourceQuantity GrossIncome
		{
			get
			{
				return Owner.RemoteMiners.Where(m => m.Key.Item1 == this).Sum(m => m.Value) + this.RawResourceIncome();
			}
		}

		/// <summary>
		/// Space vehicles do not have infinite supplies unless they have a quantum reactor or they are bases.
		/// </summary>
		public virtual bool HasInfiniteSupplies
		{
			// TODO - what about Supply Generation (resupply depot) ability? or is it alias for QR ability?
			get { return this.HasAbility("Quantum Reactor"); }
		}

		public bool IsIdle
		{
			get
			{
				return (StrategicSpeed > 0 && !Orders.Any() && Container == null) || (ConstructionQueue != null && ConstructionQueue.IsIdle);
			}
		}

		public override int MaxTargets => Math.Max(1, this.GetAbilityValue("Multiplex Tracking").ToInt());

		/// <summary>
		/// Amount of movement remaining for this turn.
		/// </summary>
		[DoNotSerialize]
		public int MovementRemaining { get; set; }

		IEnumerable<IOrder> IOrderable.Orders
		{
			get { return Orders; }
		}

		public IList<IOrder<SpaceVehicle>> Orders
		{
			get;
			private set;
		}

		public override IEnumerable<IAbilityObject> Parents
		{
			get
			{
				if (Container != null)
					yield return Container;
				else
				{
					if (Sector != null)
						yield return Sector;
					yield return Owner;
				}
			}
		}

		/// <summary>
		/// Vehicles cannot have population per se.
		/// </summary>
		public long PopulationStorageFree
		{
			get { return 0; }
		}

		public override Sector Sector
		{
			get
			{
				if (sector == null)
					sector = this.FindSector();
				return sector;
			}
			set
			{
				var oldsector = sector;
				sector = value;
				if (value == null)
				{
					if (oldsector != null)
						oldsector.Remove(this);
				}
				else
				{
					if (oldsector != value)
						value.Place(this);
				}
			}
		}

		public override StarSystem StarSystem
		{
			get { return Sector == null ? null : Sector.StarSystem; }
		}

		/// <summary>
		/// Resources stored on this space vehicle.
		/// </summary>
		public ResourceQuantity StoredResources { get; private set; }

		/// <summary>
		/// The speed of the vehicle, taking into account hull mass, thrust, speed bonuses, and supply.
		/// </summary>
		public override int StrategicSpeed
		{
			get
			{
				// no Engines Per Move rating? then no movement
				if (Design.Hull.ThrustPerMove == 0)
					return 0;

				// can't go anywhere without thrust!
				var thrust = this.GetAbilityValue("Standard Ship Movement").ToInt();
				if (thrust < Design.Hull.ThrustPerMove)
					return 0;

				// gotta go slow if you don't have supplies!
				if (!HasInfiniteSupplies && SupplyStorage < EngineSupplyBurnRate)
					return 1;

				// take into account base speed plus all bonuses
				return
					thrust / Design.Hull.ThrustPerMove
					+ this.GetAbilityValue("Movement Bonus").ToInt()
					+ this.GetAbilityValue("Extra Movement Generation").ToInt()
					+ this.GetAbilityValue("Vehicle Speed").ToInt()
					+ EmergencySpeed;
			}
		}

		public Progress SupplyFill { get { return new Progress(SupplyRemaining, SupplyStorage); } }

		/// <summary>
		/// The amount of supply present on this vehicle.
		/// </summary>
		[DoNotSerialize]
		public int SupplyRemaining
		{
			get { return StoredResources[Resource.Supply]; }
			set { StoredResources[Resource.Supply] = value; }
		}

		/// <summary>
		/// Space vehicles' supply storage depends on their abilities.
		/// </summary>
		public int SupplyStorage
		{
			get { return this.GetAbilityValue("Supply Storage").ToInt(); }
		}

		/// <summary>
		/// The fraction of a turn that moving one sector takes.
		/// </summary>
		public double TimePerMove
		{
			get { return 1.0 / (double)StrategicSpeed; }
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

		private ConstructionQueue constructionQueue { get; set; }
		private Sector sector;

		public void AddOrder(IOrder order)
		{
			if (!(order is IOrder<SpaceVehicle>))
				throw new Exception("Can't add a " + order.GetType() + " to a space vehicle's orders.");
			Orders.Add((IOrder<SpaceVehicle>)order);
		}

		public long AddPopulation(Race race, long amount)
		{
			var canCargo = Math.Min(amount, (long)(this.CargoStorageFree() / Mod.Current.Settings.PopulationSize));
			amount -= canCargo;
			Cargo.Population[race] += canCargo;
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

		/// <summary>
		/// Burns supplies required to move one sector.
		/// If this would put the vehicle below zero supplies,
		/// the move is still successful, but the vehicle's supplies are set to zero.
		/// </summary>
		public void BurnMovementSupplies()
		{
			SupplyRemaining -= EngineSupplyBurnRate;
			this.NormalizeSupplies();
		}

		public override Visibility CheckVisibility(Empire emp)
		{
			return this.CheckSpaceObjectVisibility(emp);
		}

		public override void Dispose()
		{
			if (IsDisposed)
				return;
			var sys = this.FindStarSystem();
			if (sys != null)
				sys.Remove(this);
			if (Cargo != null)
			{
				foreach (var u in Cargo.Units)
					u.Dispose();
			}
			constructionQueue.SafeDispose();
			base.Dispose();
			if (!IsMemory)
				this.UpdateEmpireMemories();
		}

		public bool ExecuteOrders()
		{
			return this.ExecuteMobileSpaceObjectOrders();
		}

		public override bool IsObsoleteMemory(Empire emp)
		{
			if (StarSystem == null)
				return Timestamp < Galaxy.Current.Timestamp - 1;
			return StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Galaxy.Current.Timestamp - 1;
		}

		public void RearrangeOrder(IOrder order, int delta)
		{
			if (order != null && !(order is IOrder<SpaceVehicle>))
				throw new Exception("Can't rearrange a " + order.GetType() + " in a space vehicle's orders.");
			var o = (IOrder<SpaceVehicle>)order;
			var newpos = Orders.IndexOf(o) + delta;
			Orders.Remove(o);
			if (newpos < 0)
				newpos = 0;
			if (newpos >= Orders.Count)
				Orders.Add(o);
			else
				Orders.Insert(newpos, o);
		}

		public override void Redact(Empire emp)
		{
			base.Redact(emp);

			var vis = CheckVisibility(emp);

			if (vis < Visibility.Owned)
			{
				// can't see orders unless it's your vehicle
				Orders.Clear();

				// can only see cargo size if scanned but unowed
				Cargo.SetFakeSize(true);
			}

			if (vis < Visibility.Scanned)
			{
				// can't see cargo at all
				Cargo.SetFakeSize(false);

				// hide amount of supplies remaining
				SupplyRemaining = 0;
			}
		}

		public void RemoveOrder(IOrder order)
		{
			if (order != null && !(order is IOrder<SpaceVehicle>))
				return; // order can't exist here anyway
			Orders.Remove((IOrder<SpaceVehicle>)order);
		}

		public long RemovePopulation(Race race, long amount)
		{
			var canCargo = Math.Min(amount, Cargo.Population[race]);
			amount -= canCargo;
			Cargo.Population[race] -= canCargo;
			return amount;
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

		/// <summary>
		/// Replenishes shields normally, unless there are no supplies, in which case the shields are taken away.
		/// </summary>
		/// <param name="amount"></param>
		public override void ReplenishShields(int? amount = null)
		{
			if (SupplyRemaining > 0)
				base.ReplenishShields(amount);
			else
			{
				NormalShields = 0;
				PhasedShields = 0;
			}
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
	}
}