
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Combat2;
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
			StoredResources = new ResourceQuantity();
		}

		/// <summary>
		/// Amount of movement remaining for this turn.
		/// </summary>
		[DoNotSerialize]
		public int MovementRemaining { get; set; }

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
		/// Makes sure there aren't more supplies than we can store, or fewer than zero
		/// </summary>
		/// <returns>Leftover supplies (or a negative number if somehow we got negative supplies in this vehicle)</returns>
		public int NormalizeSupplies()
		{
			if (SupplyRemaining > SupplyStorage)
			{
				var leftover = SupplyRemaining - SupplyStorage;
				SupplyRemaining = SupplyStorage;
				return leftover;
			}
			if (SupplyRemaining < 0)
			{
				var deficit = SupplyRemaining;
				SupplyRemaining = 0;
				return deficit;
			}
			return 0;
		}

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
			while (TimeToNextMove <= 1e-15 && Orders.Any())
			{
				Orders.First().Execute(this);
				if (Orders.First().IsComplete)
					Orders.RemoveAt(0);
				didStuff = true;
			}
			while (Orders.Any() && !Orders.First().ConsumesMovement)
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

		public Progress SupplyFill { get { return new Progress(SupplyRemaining, SupplyStorage); } }

		/// <summary>
		/// Space vehicles do not have infinite supplies unless they have a quantum reactor or they are bases.
		/// </summary>
		public virtual bool HasInfiniteSupplies
		{
			// TODO - what about Supply Generation (resupply depot) ability? or is it alias for QR ability?
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
			if (newpos >= Orders.Count)
				Orders.Add(o);
			else
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

		public override Visibility CheckVisibility(Empire emp)
		{
			return this.CheckSpaceObjectVisibility(emp);
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
			// and can't see the design either if we haven't scanned it before
			if (visibility < Visibility.Scanned)
			{
				if (Design.CheckVisibility(emp) < Visibility.Scanned)
				{
					// create fake design
					var d = Vehicles.Design.Create(Design.VehicleType);
					d.Hull = Design.Hull;
					d.Owner = Design.Owner;
					Design = d;
				}

				// clear component list
				Components.Clear();

				// hide amount of supplies remaining
				SupplyRemaining = 0;
			}

			if (visibility < Visibility.Fogged)
				Dispose();
		}

		public bool IsIdle
		{
			get
			{
				return (Speed > 0 && !Orders.Any() && Container == null) || (ConstructionQueue != null && ConstructionQueue.IsIdle);
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

		public IDictionary<Race, long> AllPopulation
		{
			get { return Cargo.Population; }
		}

		public Fleet Container
		{
			get
			{
				var fleets = Galaxy.Current.FindSpaceObjects<Fleet>(f => f.Vehicles.Contains(this));
				if (!fleets.Any())
					return null;
				if (fleets.Count() == 1)
					return fleets.Single();
				throw new Exception("Vehicle belongs to more than one fleet?!");
				//return null; // probably busy copying a fleet to memory sight or something
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

		[DoNotSerialize(false)]
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

		/// <summary>
		/// Resources stored on this space vehicle.
		/// </summary>
		public ResourceQuantity StoredResources { get; private set; }

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

		/// <summary>
		/// The speed of the vehicle, taking into account hull mass, thrust, speed bonuses, and supply.
		/// </summary>
		public override int Speed
		{
			get
			{
				// no Engines Per Move rating? then no movement
				if (Design.Hull.Mass == 0)
					return 0;

				// can't go anywhere without thrust!
				var thrust = this.GetAbilityValue("Standard Ship Movement").ToInt();
				if (thrust < Design.Hull.Mass)
					return 0;

				// gotta go slow if you don't have supplies!
				if (!HasInfiniteSupplies && SupplyStorage < EngineSupplyBurnRate)
					return 1;

				// take into account base speed plus all bonuses
				return
					thrust / Design.Hull.Mass
					+ this.GetAbilityValue("Movement Bonus").ToInt()
					+ this.GetAbilityValue("Extra Movement Generation").ToInt()
					+ this.GetAbilityValue("Vehicle Speed").ToInt();
			}
		}

		/// <summary>
		/// Burns supplies required to move one sector.
		/// If this would put the vehicle below zero supplies,
		/// the move is still successful, but the vehicle's supplies are set to zero.
		/// </summary>
		public void BurnMovementSupplies()
		{
			SupplyRemaining -= EngineSupplyBurnRate;
			NormalizeSupplies();
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
	}
}
