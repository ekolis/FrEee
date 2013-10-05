using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Technology;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A collection of ships, units, etc. that move synchronously.
	/// </summary>
	public class Fleet : ISpaceVehicle<Fleet>, ICargoTransferrer
	{
		public Fleet()
		{
			Vehicles = new HashSet<ISpaceVehicle>();
			Orders = new List<IOrder<Fleet>>();
		}

		/// <summary>
		/// The space objects in the fleet.
		/// Fleets may contain other fleets, but may not contain themselves.
		/// </summary>
		public ISet<ISpaceVehicle> Vehicles { get; private set; }

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
				if (sobj.Owner != Owner || ancestors.Contains(sobj) || sobj.Sector != Sector || sobj.IsDestroyed)
					Vehicles.Remove(sobj);
				else if (sobj is Fleet)
					((Fleet)sobj).Validate(ancestors);
			}
			if (!Vehicles.Any())
				Dispose();
		}

		[DoNotSerialize]
		public double TimeToNextMove
		{
			get;
			set;
		}

		public double TimePerMove
		{
			get { return Vehicles.Max(sobj => sobj.TimePerMove); }
		}

		public void RefillMovement()
		{
			MovementRemaining = Speed;
			TimeToNextMove = TimePerMove;
		}

		public int MovementRemaining { get; set; }

		public int Speed
		{
			get { return Vehicles.Min(sobj => sobj.Speed); }
		}

		public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> DijkstraMap
		{
			get;
			set;
		}

		public bool CanTarget(ICombatObject target)
		{
			return Vehicles.Any(sobj => sobj.CanTarget(target));
		}

		/// <summary>
		/// Fleets cannot be directly targeted by weapons. Target the individual ships instead.
		/// </summary>
		public WeaponTargets WeaponTargetType
		{
			get { return WeaponTargets.None; }
		}

		public IEnumerable<Component> Weapons
		{
			get { return Vehicles.SelectMany(sobj => sobj.Weapons); }
		}

		public bool IsHostileTo(Empire emp)
		{
			return Owner.IsHostileTo(emp);
		}

		public int Accuracy
		{
			get
			{
				// TODO - fleet experience
				return 0;
			}
		}

		public int Evasion
		{
			get
			{
				// TODO - fleet experience
				return 0;
			}
		}

		public Image Icon
		{
			get
			{
				return Pictures.GetIcon(this, Owner.ShipsetPath);
			}
		}

		public Image Portrait
		{
			get
			{
				return Pictures.GetPortrait(this, Owner.ShipsetPath);
			}
		}

		public Empire Owner
		{
			get;
			set;
		}

		/// <summary>
		/// The hitpoints of this fleet. Cannot set this property; attempting to do so will throw a NotSupportedException.
		/// </summary>
		[DoNotSerialize]
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

		/// <summary>
		/// The normal shields of this fleet. Cannot set this property; attempting to do so will throw a NotSupportedException.
		/// </summary>
		[DoNotSerialize]
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

		/// <summary>
		/// The phased shields of this fleet. Cannot set this property; attempting to do so will throw a NotSupportedException.
		/// </summary>
		[DoNotSerialize]
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

		public int MaxHitpoints
		{
			get { return Vehicles.Sum(sobj => sobj.MaxHitpoints); }
		}

		public int MaxNormalShields
		{
			get { return Vehicles.Sum(sobj => sobj.MaxNormalShields); }
		}

		public int MaxPhasedShields
		{
			get { return Vehicles.Sum(sobj => sobj.MaxPhasedShields); }
		}

		public void ReplenishShields()
		{
			foreach (var sobj in Vehicles)
				sobj.ReplenishShields();
		}

		public int? Repair(int? amount = null)
		{
			// TODO - repair priority
			foreach (var sobj in Vehicles)
				amount = sobj.Repair(amount);
			return amount;
		}

		/// <summary>
		/// Fleets cannot take damage directly, so this method will throw a NotSupportedException.
		/// </summary>
		public int TakeDamage(Combat.DamageType dmgType, int damage, Combat.Battle battle)
		{
			throw new NotSupportedException("Fleets cannot take damage directly. Try assigning damage to the individual ships.");
		}

		public bool IsDestroyed
		{
			get { return Vehicles.All(sobj => sobj.IsDestroyed); }
		}

		/// <summary>
		/// Fleets cannot take damage directly, so this method will throw a NotSupportedException.
		/// </summary>
		public int HitChance
		{
			get { throw new NotSupportedException("Fleets cannot take damage directly, so a hit chance is meaningless."); }
		}

		/// <summary>
		/// Disposes of the fleet. Does not dispose of the individual ships; they are removed from the fleet instead.
		/// </summary>
		public void Dispose()
		{
			Vehicles.Clear();
			Galaxy.Current.UnassignID(this);
		}

		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Fleets don't have intrinsic abilities, though it would be cool if they could gain them through experience like in SE5.
		/// </summary>
		public IList<Ability> IntrinsicAbilities
		{
			get { return new List<Ability>(); }
		}

		/// <summary>
		/// Fleets don't have construction queues.
		/// </summary>
		public Civilization.ConstructionQueue ConstructionQueue
		{
			get { return null; }
		}

		/// <summary>
		/// Fleets can be nested.
		/// </summary>
		public bool CanBeInFleet
		{
			get { return true; }
		}

		public int SupplyStorage
		{
			get { return Vehicles.Sum(sobj => sobj.SupplyStorage); }
		}

		[DoNotSerialize]
		public int SupplyRemaining
		{
			get
			{
				return Vehicles.Sum(sobj => sobj.SupplyRemaining);
			}
			set
			{
				var available = SupplyRemaining;
				var storage = SupplyStorage;
				int spent = 0;
				foreach (var sobj in Vehicles)
				{
					var amount = (int)Math.Floor((double)sobj.SupplyStorage / (double)storage * available);
					sobj.SupplyRemaining = amount;
					spent += amount;
				}
				var roundingError = available - spent;
				while (roundingError > 0)
				{
					var sobj2 = Vehicles.WithMin(sobj => (double)sobj.SupplyRemaining / (double)sobj.SupplyStorage).PickRandom();
					sobj2.SupplyRemaining += 1;
					roundingError += 1;
				}
			}
		}

		/// <summary>
		/// Fleets share supplies, so if any space object has infinite supplies, the fleet does.
		/// </summary>
		public bool HasInfiniteSupplies
		{
			get { return Vehicles.Any(sobj => sobj.HasInfiniteSupplies); }
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

		public bool CanWarp
		{
			get { return Vehicles.All(sobj => sobj.CanWarp); }
		}

		public bool IsIdle
		{
			get
			{
				return Speed > 0 && !Orders.Any() || ConstructionQueues.Any(q => q.Eta < 1);
			}
		}

		/// <summary>
		/// Any construction queues of ships in this fleet and its subfleets.
		/// </summary>
		public IEnumerable<ConstructionQueue> ConstructionQueues
		{
			get
			{
				return Vehicles.SelectMany(sobj =>
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

		public IEnumerable<Ability> Abilities
		{
			// TODO - stacking rules, so movement abilities and such don't stack on fleets
			get { return UnstackedAbilities.Stack(); }
		}

		public IEnumerable<Ability> UnstackedAbilities
		{
			get { return Vehicles.SelectMany(sobj => sobj.Abilities); }
		}

		public long ID
		{
			get;
			set;
		}

		/// <summary>
		/// Fleets are as visible as their most visible space object. Not that the others will actually be that visible...
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			return Vehicles.Max(sobj => sobj.CheckVisibility(emp));
		}

		public void Redact(Empire emp)
		{
			var vis = CheckVisibility(emp);

			// Can't see names of alien fleets
			if (vis < Visibility.Owned)
				Name = Owner + " Fleet";
		}

		public Sector Sector
		{
			get { return this.FindSector(); }
		}

		public StarSystem StarSystem
		{
			get { return this.FindStarSystem(); }
		}

		public IList<IOrder<Fleet>> Orders { get; private set; }

		IEnumerable<IOrder> IOrderable.Orders
		{
			get { return Orders; }
		}

		public void AddOrder(IOrder order)
		{
			if (!(order is IOrder<Fleet>))
				throw new InvalidOperationException("Fleets can only accept orders of type IOrder<Fleet>.");
			Orders.Add((IOrder<Fleet>)order);
		}

		public void RemoveOrder(IOrder order)
		{
			if (!(order is IOrder<Fleet>))
				throw new InvalidOperationException("Fleets can only accept orders of type IOrder<Fleet>.");
			Orders.Remove((IOrder<Fleet>)order);
		}

		public void RearrangeOrder(IOrder order, int delta)
		{
			if (!(order is IOrder<Fleet>))
				throw new InvalidOperationException("Fleets can only accept orders of type IOrder<Fleet>.");
			var o = (IOrder<Fleet>)order;
			var newpos = Orders.IndexOf(o) + delta;
			Orders.Remove(o);
			if (newpos < 0)
				newpos = 0;
			if (newpos > Orders.Count)
				newpos = Orders.Count;
			Orders.Insert(newpos, o);
		}

		public void ExecuteOrders()
		{
			if (Galaxy.Current.NextTickSize == double.PositiveInfinity)
				TimeToNextMove = 0;
			else
				TimeToNextMove -= Galaxy.Current.NextTickSize;
			while (TimeToNextMove <= 0 && Orders.Any())
			{
				Orders.First().Execute(this);
				if (Orders.First().IsComplete)
					Orders.RemoveAt(0);
			}
		}

		public Cargo Cargo
		{
			get { return Vehicles.OfType<ICargoContainer>().Sum(cc => cc.Cargo); }
		}

		public int CargoStorage
		{
			get { return Vehicles.OfType<ICargoContainer>().Sum(cc => cc.CargoStorage); }
		}

		public long PopulationStorageFree
		{
			get { return 0L; }
		}

		public long AddPopulation(Civilization.Race race, long amount)
		{
			foreach (var ct in Vehicles.OfType<ICargoTransferrer>())
			{
				amount = ct.AddPopulation(race, amount);
			}
			return amount;
		}

		public long RemovePopulation(Civilization.Race race, long amount)
		{
			foreach (var ct in Vehicles.OfType<ICargoTransferrer>())
			{
				amount = ct.RemovePopulation(race, amount);
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

		public bool RemoveUnit(IUnit unit)
		{
			foreach (var ct in Vehicles.OfType<ICargoTransferrer>())
			{
				if (ct.RemoveUnit(unit))
					return true;
			}
			return false;
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

		public Fleet Container
		{
			get { return Galaxy.Current.FindSpaceObjects<Fleet>(f => f.Vehicles.Contains(this)).Flatten().Flatten().SingleOrDefault(); }
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
		/// Any combat objects contained in this fleet and any subfleets.
		/// </summary>
		public IEnumerable<ICombatObject> CombatObjects
		{
			get
			{
				return Vehicles.SelectMany(sobj =>
				{
					var list = new List<ICombatObject>();
					if (sobj is ICombatObject)
						list.Add((ICombatObject)sobj);
					if (sobj is Fleet)
						list.AddRange(((Fleet)sobj).CombatObjects);
					return list;
				});
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
		/// All space vehicles in this fleet and subfleets, but not counting the subfleets themselves.
		/// </summary>
		public IEnumerable<ISpaceVehicle> LeafVehicles
		{
			get
			{
				return Vehicles.SelectMany(v =>
				{
					var list = new List<ISpaceVehicle>();
					if (v is Fleet)
					{
						foreach (var v2 in ((Fleet)v).LeafVehicles)
							list.Add(v2);
					}
					else
						list.Add(v);
					return list;
				});
			}
		}
	}
}
