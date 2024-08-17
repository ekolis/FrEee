using FrEee.Objects.Civilization;
using FrEee.Objects.Technology;
using FrEee.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.Civilization.Construction;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;
using FrEee.Modding.Abilities;

namespace FrEee.Objects.Space;

/// <summary>
/// A collection of ships, units, etc. that move synchronously.
/// </summary>
public class Fleet : IMobileSpaceObject<Fleet>, ICargoTransferrer, IPromotable, IIncomeProducer
{
	public Fleet()
	{
		Vehicles = new GalaxyReferenceSet<IMobileSpaceObject>();
		Orders = new List<IOrder>();
		Timestamp = Galaxy.Current?.Timestamp ?? 0;
	}

	public AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.Fleet; }
	}

	public int Accuracy
	{
		get
		{
			// TODO - fleet experience
			return 0;
		}
	}

	public IDictionary<Civilization.Race, long> AllPopulation
	{
		get
		{
			var dict = new SafeDictionary<Race, long>();
			foreach (var cc in Vehicles.OfType<ICargoContainer>())
			{
				foreach (var kvp in cc.Cargo.Population)
					dict[kvp.Key] += kvp.Value;
			}
			return dict;
		}
	}

	public IEnumerable<IUnit> AllUnits
	{
		get
		{
			return Vehicles.SelectMany(sobj =>
				{
					var list = new List<IUnit>();
					if (sobj is IUnit)
						list.Add((IUnit)sobj);
					if (sobj is ICargoContainer)
						list.AddRange(((ICargoContainer)sobj).AllUnits);
					return list.Distinct();
				});
		}
	}

	/// <summary>
	/// Are this object's orders on hold?
	/// </summary>
	public bool AreOrdersOnHold { get; set; }

	/// <summary>
	/// Should this object's orders repeat once they are completed?
	/// </summary>
	public bool AreRepeatOrdersEnabled { get; set; }

	public int ArmorHitpoints
	{
		get { return Vehicles.Sum(v => v.ArmorHitpoints); }
	}

	/// <summary>
	/// Fleets can be nested.
	/// </summary>
	public bool CanBeInFleet
	{
		get { return true; }
	}

	public bool CanBeObscured => true;

	public bool CanWarp
	{
		get { return Vehicles.All(sobj => sobj.CanWarp); }
	}

	public Cargo Cargo
	{
		get { return Vehicles.OfType<ICargoContainer>().Sum(cc => cc.Cargo); }
	}

	public int CargoStorage
	{
		get { return Vehicles.OfType<ICargoContainer>().Sum(cc => cc.CargoStorage); }
	}

	public IEnumerable<IAbilityObject> Children
	{
		get { return Vehicles; }
	}

	/// <summary>
	/// Any combatants contained in this fleet and any subfleets.
	/// </summary>
	public IEnumerable<ICombatant> Combatants
	{
		get
		{
			return Vehicles.SelectMany(sobj =>
			{
				var list = new List<ICombatant>();
				if (sobj is ICombatant)
					list.Add((ICombatant)sobj);
				if (sobj is Fleet)
					list.AddRange(((Fleet)sobj).Combatants);
				return list;
			});
		}
	}

	public double CombatSpeed => Mod.Current.Settings.CombatSpeedPercentPerStrategicSpeed.PercentOf(StrategicSpeed);

	/// <summary>
	/// Any construction queues of ships in this fleet and its subfleets.
	/// </summary>
	public IEnumerable<ConstructionQueue> ConstructionQueues
	{
		get
		{
			return Vehicles.OfType<IConstructor>().SelectMany(sobj =>
				{
					var list = new List<ConstructionQueue>();
					if (sobj.ConstructionQueue != null)
						list.Add(sobj.ConstructionQueue);
					if (sobj is Fleet)
						list.AddRange(((Fleet)sobj).ConstructionQueues);
					return list;
				});
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

	public int Evasion
	{
		get
		{
			// TODO - fleet experience
			return 0;
		}
	}

	public ResourceQuantity GrossIncome
	{
		get { return Vehicles.OfType<IIncomeProducer>().Sum(v => v.GrossIncome()); }
	}

	/// <summary>
	/// Fleets share supplies, so if any space object has infinite supplies, the fleet does.
	/// </summary>
	public bool HasInfiniteSupplies
	{
		get { return Vehicles.ExceptSingle(null).Any(sobj => sobj.HasInfiniteSupplies); }
	}

	/// <summary>
	/// Chance of hitting each ship in a fleet is equal, so this value is the number of ships in the fleet.
	/// </summary>
	public int HitChance
	{
		get
		{
			return LeafVehicles.Count();
		}
	}

	/// <summary>
	/// The hitpoints of this fleet. Cannot set this property; attempting to do so will throw a NotSupportedException.
	/// </summary>
	[DoNotSerialize(false)]
	public int Hitpoints
	{
		get
		{
			return Vehicles.Sum(sobj => sobj.Hitpoints);
		}
		set
		{
			throw new NotSupportedException("Cannot set fleet hitpoints directly. Try setting the hitpoints of individual ship components.");
		}
	}

	public int HullHitpoints
	{
		get { return Vehicles.Sum(v => v.HullHitpoints); }
	}

	public Image Icon
	{
		get
		{
			var owner = Owner ?? Empire.Current; // for client side fleets that are empty
			return Pictures.GetIcon(this, owner.ShipsetPath);
		}
	}

	public Image Icon32 => Icon.Resize(32);

	public IEnumerable<string> IconPaths
	{
		get
		{
			return GetImagePaths("Mini");
		}
	}

	public long ID
	{
		get;
		set;
	}

	public IEnumerable<Ability> IntrinsicAbilities
	{
		get
		{
			// TODO - fleet experience
			yield break;
		}
	}

	public bool IsAlive => Vehicles.Any(x => x.IsAlive);

	public bool IsDestroyed
	{
		get { return Vehicles.ExceptSingle(null).All(sobj => sobj.IsDestroyed); }
	}

	public bool IsDisposed { get; set; }

	public bool IsIdle
	{
		get
		{
			return StrategicSpeed > 0 && !Orders.Any() && Container == null || ConstructionQueues.Any(q => q.Eta < 1);
		}
	}

	public bool IsMemory
	{
		get;
		set;
	}

	public bool IsOurs { get { return Owner == Empire.Current; } }

	/// <summary>
	/// All space vehicles in this fleet and subfleets, but not counting the subfleets themselves.
	/// </summary>
	public IEnumerable<SpaceVehicle> LeafVehicles
	{
		get
		{
			return Vehicles.SelectMany(v =>
			{
				var list = new List<SpaceVehicle>();
				if (v is Fleet)
				{
					foreach (var v2 in ((Fleet)v).LeafVehicles)
						list.Add(v2);
				}
				else if (v is SpaceVehicle)
					list.Add((SpaceVehicle)v);
				return list;
			});
		}
	}

	public ResourceQuantity MaintenanceCost
	{
		get { return Vehicles.Sum(v => v.MaintenanceCost); }
	}

	public int MaxArmorHitpoints
	{
		get { return Vehicles.Sum(v => v.MaxArmorHitpoints); }
	}

	public int MaxHitpoints
	{
		get { return Vehicles.Sum(sobj => sobj.MaxHitpoints); }
	}

	public int MaxHullHitpoints
	{
		get { return Vehicles.Sum(v => v.MaxHullHitpoints); }
	}

	public int MaxNormalShields
	{
		get { return Vehicles.Sum(sobj => sobj.MaxNormalShields); }
	}

	public int MaxPhasedShields
	{
		get { return Vehicles.Sum(sobj => sobj.MaxPhasedShields); }
	}

	public int MaxShieldHitpoints
	{
		get { return Vehicles.Sum(v => v.MaxShieldHitpoints); }
	}

	/// <summary>
	/// Fleets can't fire on enemies directly; the contained ships do.
	/// </summary>
	public int MaxTargets => 0;

	public double MerchantsRatio => Owner.HasAbility("No Spaceports") ? 1.0 : 0.0;

	public int MineralsMaintenance
	{
		get { return MaintenanceCost[Resource.Minerals]; }
	}

	public int MovementRemaining { get; set; }

	public string Name
	{
		get;
		set;
	}

	/// <summary>
	/// The normal shields of this fleet. Cannot set this property; attempting to do so will throw a NotSupportedException.
	/// </summary>
	[DoNotSerialize(false)]
	public int NormalShields
	{
		get
		{
			return Vehicles.Sum(sobj => sobj.NormalShields);
		}
		set
		{
			throw new NotSupportedException("Cannot set fleet shields directly. Try setting the shields of individual ships.");
		}
	}

	public IList<IOrder> Orders { get; private set; }

	IEnumerable<IOrder> IOrderable.Orders
		=> Orders;

	public int OrganicsMaintenance
	{
		get { return MaintenanceCost[Resource.Organics]; }
	}

	[DoNotSerialize]
	public Empire Owner
	{
		get { return Vehicles.ExceptSingle(null).FirstOrDefault()?.Owner; } // assume all vehicles have the same owner
		set
		{
			foreach (var v in Vehicles)
				v.Owner = value;
		}
	}

	public IEnumerable<IAbilityObject> Parents
	{
		get
		{
			if (Container != null)
				yield return Container;
			else
			{
				if (Sector != null)
					yield return Sector;
				if (Owner != null)
					yield return Owner;
			}
		}
	}

	/// <summary>
	/// The phased shields of this fleet. Cannot set this property; attempting to do so will throw a NotSupportedException.
	/// </summary>
	[DoNotSerialize(false)]
	public int PhasedShields
	{
		get
		{
			return Vehicles.Sum(sobj => sobj.PhasedShields);
		}
		set
		{
			throw new NotSupportedException("Cannot set fleet shields directly. Try setting the shields of individual ships.");
		}
	}

	public long PopulationStorageFree
	{
		get { return 0L; }
	}

	public Image Portrait
	{
		get
		{
			var owner = Owner ?? Empire.Current; // for client side fleets that are empty
			return Pictures.GetPortrait(this, owner.ShipsetPath);
		}
	}

	public IEnumerable<string> PortraitPaths
	{
		get
		{
			return GetImagePaths("Portrait");
		}
	}

	public int RadioactivesMaintenance
	{
		get { return MaintenanceCost[Resource.Radioactives]; }
	}

	public ResourceQuantity RemoteMiningIncomePercentages
	{
		get { return Owner.PrimaryRace.IncomePercentages; }
	}

	/// <summary>
	/// Fleets have no resource value.
	/// </summary>
	public ResourceQuantity ResourceValue
	{
		get { return new ResourceQuantity(); }
	}

	public Sector Sector
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
					oldsector.Remove(this);
			}
			else
			{
				if (oldsector != value)
					value.Place(this);
			}
		}
	}

	public int ShieldHitpoints
	{
		get { return Vehicles.Sum(v => v.ShieldHitpoints); }
	}

	public int Size
	{
		get { return Vehicles.ExceptSingle((IMobileSpaceObject)null).Sum(v => v.Size); }
	}

	public ResourceQuantity StandardIncomePercentages
	{
		get { return Owner.PrimaryRace.IncomePercentages; }
	}

	public StarSystem StarSystem
	{
		get { return this.FindStarSystem(); }
	}

	/// <summary>
	/// Resources stored in this fleet.
	/// Note that modifying this value will have no effect on the individual vehicles in the fleet;
	/// we just don't have a handy read only resource quantity type.
	/// </summary>
	public ResourceQuantity StoredResources
	{
		get
		{
			return Vehicles.Sum(v => v.StoredResources);
		}
	}

	public int StrategicSpeed
	{
		get { return Vehicles.MinOrDefault(sobj => sobj.StrategicSpeed); }
	}

	/// <summary>
	/// The amount of supply which this fleet can store.
	/// </summary>
	public int SupplyCapacity
	{
		get
		{
			return this.GetAbilityValue("Supply Storage").ToInt();
		}
	}

	[DoNotSerialize(false)]
	public int SupplyRemaining
	{
		get
		{
			return Vehicles.Sum(sobj => sobj?.SupplyRemaining ?? 0);
		}
		set
		{
			var available = value;
			var storage = SupplyStorage;
			int spent = 0;

			// sharing supplies should not affect abilities
			bool wasCacheDisabled = !Galaxy.Current.IsAbilityCacheEnabled;
			if (wasCacheDisabled)
				Galaxy.Current.EnableAbilityCache();

			foreach (var sobj in Vehicles)
			{
				if (storage == 0)
				{
					sobj.SupplyRemaining = 0;
					continue;
				}
				var amount = (int)Math.Floor((double)sobj.SupplyStorage / (double)storage * available);
				sobj.SupplyRemaining = amount;
				spent += amount;
			}
			var roundingError = available - spent;
			if (storage > 0)
			{
				while (roundingError > 0)
				{
					var sobj2 = Vehicles.WithMin(sobj => (double)sobj.SupplyRemaining / (double)sobj.SupplyStorage).PickRandom();
					sobj2.SupplyRemaining += 1;
					roundingError -= 1;
				}
			}

			if (wasCacheDisabled)
				Galaxy.Current.DisableAbilityCache();
		}
	}

	public int SupplyStorage
	{
		get { return Vehicles.Sum(sobj => sobj.SupplyStorage); }
	}

	public double TimePerMove
	{
		get
		{
			if (!Vehicles.Any())
				return double.PositiveInfinity;
			return Vehicles.Max(sobj => sobj.TimePerMove);
		}
	}

	public double Timestamp { get; set; }

	[DoNotSerialize]
	public double TimeToNextMove
	{
		get;
		set;
	}

	/// <summary>
	/// The space objects in the fleet.
	/// Fleets may contain other fleets, but may not contain themselves.
	/// </summary>
	public GalaxyReferenceSet<IMobileSpaceObject> Vehicles { get; private set; }

	public IEnumerable<Component> Weapons
	{
		get { return Vehicles.SelectMany(sobj => sobj.Weapons); }
	}

	/// <summary>
	/// Fleets cannot be directly targeted by weapons. Target the individual ships instead.
	/// </summary>
	public WeaponTargets WeaponTargetType
	{
		get { return WeaponTargets.None; }
	}

	private Sector sector;

	public void AddOrder(IOrder order)
	{
		if (!(order is IOrder))
			throw new InvalidOperationException("Fleets can only accept orders of type IOrder.");
		Orders.Add((IOrder)order);
	}

	public long AddPopulation(Civilization.Race race, long amount)
	{
		foreach (var ct in Vehicles.OfType<ICargoTransferrer>())
		{
			amount = ct.AddPopulation(race, amount);
		}
		return amount;
	}

	public bool AddUnit(IUnit unit)
	{
		foreach (var ct in Vehicles.OfType<ICargoTransferrer>())
		{
			if (ct.AddUnit(unit))
				return true;
		}
		return false;
	}

	/// <summary>
	/// Burns movement supplies for all vehicles in the fleet.
	/// </summary>
	public void BurnMovementSupplies()
	{
		foreach (var v in Vehicles)
			v.BurnMovementSupplies();
	}

	public bool CanTarget(ITargetable target)
	{
		return Vehicles.Any(sobj => sobj.CanTarget(target));
	}

	/// <summary>
	/// Fleets are as visible as their most visible space object. Not that the others will actually be that visible...
	/// </summary>
	/// <param name="emp"></param>
	/// <returns></returns>
	public Visibility CheckVisibility(Empire emp)
	{
		if (!Vehicles.Any(v => v != null))
			return Visibility.Unknown;
		return Vehicles.Where(v => v != null).Max(sobj => sobj.CheckVisibility(emp));
	}

	/// <summary>
	/// Disposes of the fleet. Does not dispose of the individual ships; they are removed from the fleet instead.
	/// </summary>
	public void Dispose()
	{
		if (IsDisposed)
			return;
		IsDisposed = true;
		foreach (var v in Vehicles.ExceptSingle(null))
			v.Container = null;
		Vehicles.Clear();
		Galaxy.Current.UnassignID(this);
		Sector = null;
		Orders.Clear();
		if (!IsMemory)
			this.UpdateEmpireMemories();
	}

	public bool ExecuteOrders()
	{
		if (!Vehicles.ExceptSingle(null).Any())
			return false; // fleets with no vehicles can't execute orders
		return this.ExecuteMobileSpaceObjectOrders();
	}

	public bool IsHostileTo(Empire emp)
	{
		return Owner != null && Owner.IsEnemyOf(emp, StarSystem);
	}

	public bool IsObsoleteMemory(Empire emp)
	{
		if (StarSystem == null)
			return Timestamp < Galaxy.Current.Timestamp - 1;
		return StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Galaxy.Current.Timestamp - 1;
	}

	public void RearrangeOrder(IOrder order, int delta)
	{
		if (!(order is IOrder))
			throw new InvalidOperationException("Fleets can only accept orders of type IOrder.");
		var o = (IOrder)order;
		var newpos = Orders.IndexOf(o) + delta;
		Orders.Remove(o);
		if (newpos < 0)
			newpos = 0;
		if (newpos >= Orders.Count)
			Orders.Add(o);
		else
			Orders.Insert(newpos, o);
	}

	public void Redact(Empire emp)
	{
		// HACK - check for null (destroyed?) vehicles
		Validate();

		var vis = CheckVisibility(emp);

		// Can't see names or orders of alien fleets
		// TODO - espionage
		if (vis < Visibility.Owned)
		{
			Name = Owner + " Fleet";
			Orders.Clear();
			AreOrdersOnHold = false;
			AreRepeatOrdersEnabled = false;
		}
		if (vis < Visibility.Fogged)
			Dispose();
	}

	public void RemoveOrder(IOrder order)
	{
		if (!(order is IOrder))
			return; // order can't exist here anyway
		Orders.Remove((IOrder)order);
	}

	public long RemovePopulation(Civilization.Race race, long amount)
	{
		foreach (var ct in Vehicles.OfType<ICargoTransferrer>())
		{
			amount = ct.RemovePopulation(race, amount);
		}
		return amount;
	}

	public bool RemoveUnit(IUnit unit)
	{
		foreach (var ct in Vehicles.OfType<ICargoTransferrer>())
		{
			if (ct.RemoveUnit(unit))
				return true;
		}
		return false;
	}

	public int? Repair(int? amount = null)
	{
		// TODO - repair priority
		foreach (var sobj in Vehicles)
			amount = sobj.Repair(amount);
		return amount;
	}

	public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
	{
		if (done == null)
			done = new HashSet<IPromotable>();
		if (!done.Contains(this))
		{
			done.Add(this);
			Vehicles.ReplaceClientIDs(idmap, done);
		}
	}

	public void ReplenishShields(int? amount = null)
	{
		if (amount != null)
			throw new NotImplementedException("Can't replenish only some of a fleet's shields. Replenish individual ships' shields, or all of the fleet's shields.");
		Validate();
		foreach (var sobj in Vehicles)
			sobj.ReplenishShields();
	}

	/// <summary>
	/// Shares supplies between ships in a fleet, proprtional to their supply storage.
	/// </summary>
	public void ShareSupplies()
	{
		if (HasInfiniteSupplies)
		{
			// full refill
			foreach (var sobj in Vehicles)
				sobj.SupplyRemaining = sobj.SupplyStorage;
		}
		else
		{
			// share existing supplies
			SupplyRemaining = SupplyRemaining;
		}
	}

	/// <summary>
	/// When a fleet spends time, all its ships and subfleets do, too.
	/// </summary>
	/// <param name="timeElapsed"></param>
	public void SpendTime(double timeElapsed)
	{
		TimeToNextMove += timeElapsed;
		foreach (var sobj in Vehicles)
			sobj.SpendTime(timeElapsed);
	}

	/// <summary>
	/// Assigns damage to a random ship in the fleet. If damage is left over, it leaks to the next ship.
	/// </summary>
	public int TakeDamage(Hit hit, PRNG dice = null)
	{
		var vs = LeafVehicles.Shuffle().ToList();
		var dmg = hit.NominalDamage;
		while (vs.Any() && dmg > 0)
		{
			var vhit = new Hit(hit.Shot, vs.First(), dmg);
			var v = vs.First();
			dmg = v.TakeDamage(vhit, dice);
			vs.Remove(v);
		}
		return dmg;
	}

	public override string ToString()
	{
		return Name;
	}

	/// <summary>
	/// Remove any invalid objects from the fleet and any valid subfleets.
	/// If there are no valid objects left, the fleet is disbanded.
	/// Objects that are invalid:
	/// * Ships, etc. not owned by the owner of the fleet
	/// * This fleet (fleets may not contain themselves)
	/// * Space objects that are not located in the same sector as this fleet
	/// * Space objects that are destroyed
	/// </summary>
	public void Validate(ICollection<Fleet> ancestors = null)
	{
		if (ancestors == null)
			ancestors = new List<Fleet>();
		ancestors.Add(this);
		foreach (var sobj in Vehicles.ToArray())
		{
			if (sobj == null || sobj.Owner != Owner || (sobj is Fleet && ancestors.Contains((Fleet)sobj)) || sobj.Sector != Sector || sobj.IsDestroyed)
				Vehicles.Remove(sobj);
			else if (sobj is Fleet)
				((Fleet)sobj).Validate(ancestors);
		}
		if (!Vehicles.ExceptSingle(null).Any())
			Dispose();
	}

	private IEnumerable<string> GetImagePaths(string imagetype)
	{
		var shipsetPath = Owner?.ShipsetPath ?? Empire.Current?.ShipsetPath;

		if (shipsetPath == null)
			yield break;

		string imageName = "Fleet";
		if (LeafVehicles.All(v => v is Fighter))
			imageName = "FighterGroup";
		else if (LeafVehicles.All(v => v is Satellite))
			imageName = "SatelliteGroup";
		else if (LeafVehicles.All(v => v is Drone))
			imageName = "DroneGroup";
		else if (LeafVehicles.All(v => v is Mine))
			imageName = "MineGroup";

		if (Mod.Current.RootPath != null)
		{
			yield return Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, imagetype + "_" + imageName);
			yield return Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_" + imagetype + "_" + imageName);
		}
		yield return Path.Combine("Pictures", "Races", shipsetPath, imagetype + "_" + imageName);
		yield return Path.Combine("Pictures", "Races", shipsetPath, shipsetPath + "_" + imagetype + "_" + imageName);
	}

	public IEnumerable<Component> Components => Vehicles.SelectMany(q => q.Components);

	public bool FillsCombatTile => Vehicles.Any(q => q.FillsCombatTile);
}
