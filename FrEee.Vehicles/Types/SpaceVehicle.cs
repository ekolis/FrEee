using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Modding.Abilities;

namespace FrEee.Vehicles.Types;

/// <summary>
/// A vehicle which operates in space.
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public abstract class SpaceVehicle
	: Vehicle, ISpaceVehicle
{
	protected SpaceVehicle()
	{
		Orders = new List<IOrder>();
		StoredResources = new ResourceQuantity();
	}

	/// <summary>
	/// Are this object's orders on hold?
	/// </summary>
	public bool IsOnHold { get; set; }

	/// <summary>
	/// Should this object's orders repeat once they are completed?
	/// </summary>
	public bool IsOnRepeat { get; set; }

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

	/// <summary>
	/// Space vehicles' cargo storage depends on their abilities.
	/// </summary>
	public int CargoStorage
	{
		get { return this.GetAbilityValue("Cargo Storage").ToInt(); }
	}

	public override double CombatSpeed => Mod.Current.Settings.CombatSpeedPercentPerStrategicSpeed.PercentOf(StrategicSpeed) + this.GetAbilityValue("Combat Movement").ToInt();

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

	public virtual bool IsIdle
	{
		get
		{
			return StrategicSpeed > 0 && !Orders.Any() && Container == null;
		}
	}

	public override int MaxTargets => Math.Max(1, this.GetAbilityValue("Multiplex Tracking").ToInt());

	/// <summary>
	/// Amount of movement remaining for this turn.
	/// </summary>
	[DoNotSerialize]
	public int MovementRemaining { get; set; }

	public IList<IOrder> Orders
	{
		get;
		private set;
	}

	IEnumerable<IOrder> IOrderable.Orders
		=> Orders;

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

	[DoNotCopy(false)]
	public override Sector Sector
	{
		get
		{
			if (Container != null)
				return Container.Sector;
			if (sector == null)
				sector = this.FindSector();
			return sector;
		}
		set
		{
			var oldsector = Sector;
			sector = value;
			if (value == null)
			{
				if (oldsector != null)
				{
					oldsector.Remove(this);
				}
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

			// take into account base speed plus all bonuses
			var spd =
				thrust / Design.Hull.ThrustPerMove
				+ this.GetAbilityValue("Movement Bonus").ToInt()
				+ this.GetAbilityValue("Extra Movement Generation").ToInt()
				+ this.GetAbilityValue("Vehicle Speed").ToInt()
				+ EmergencySpeed;

			// gotta go slow if you don't have supplies to move!
			if (spd > 1 && !HasInfiniteSupplies && SupplyRemaining < EngineSupplyBurnRate)
				return 1;

			return spd;
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
		get { return 1.0 / StrategicSpeed; }
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

	private Sector sector;

	public void AddOrder(IOrder order)
	{
		if (!(order is IOrder))
			throw new Exception("Can't add a " + order.GetType() + " to a space vehicle's orders.");
		Orders.Add(order);
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
		base.Dispose();
		if (!IsMemory)
			this.UpdateEmpireMemories();
	}

	public bool ExecuteOrders()
	{
		return this.ExecuteMobileSpaceObjectOrders<ISpaceVehicle>();
	}

	public override bool IsObsoleteMemory(Empire emp)
	{
		if (StarSystem == null)
			return Timestamp < Game.Current.Timestamp - 1;
		return StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Game.Current.Timestamp - 1;
	}

	public void RearrangeOrder(IOrder order, int delta)
	{
		if (order != null && !(order is IOrder))
			throw new Exception("Can't rearrange a " + order.GetType() + " in a space vehicle's orders.");
		var o = order;
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
			IsOnHold = false;
			IsOnRepeat = false;
		}

		if (vis < Visibility.Scanned)
		{
			// hide amount of supplies remaining
			SupplyRemaining = 0;
		}
	}

	public void RemoveOrder(IOrder order)
	{
		if (order != null && !(order is IOrder))
			return; // order can't exist here anyway
		Orders.Remove(order);
	}

	/// <summary>
	/// Replenishes shields normally, unless there are no supplies, in which case the shields are taken away.
	/// </summary>
	/// <param name="amount"></param>
	public override void ReplenishShields(int? amount = null)
	{
		if (HasInfiniteSupplies || SupplyRemaining > 0)
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
	public virtual void SpendTime(double timeElapsed)
	{
		TimeToNextMove += timeElapsed;
	}

	/// <summary>
	/// Space vehicles can be mobile if their design's strategic speed is greater than zero.
	/// Not their current strategic speed; they might be temporarily immobilized by, say, engine damage.
	/// </summary>
	public bool CanBeMobile => Design.StrategicSpeed > 0;
}
