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
	public class Fleet : IMobileSpaceObject<Fleet>, ICargoTransferrer
	{
		public Fleet()
		{
			SpaceObjects = new HashSet<IMobileSpaceObject>();
			Orders = new List<IOrder<Fleet>>();
		}

		/// <summary>
		/// The space objects in the fleet.
		/// Fleets may contain other fleets, but may not contain themselves.
		/// </summary>
		public ISet<IMobileSpaceObject> SpaceObjects { get; private set; }

		/// <summary>
		/// Remove any invalid objects from the fleet and any valid subfleets.
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
			foreach (var sobj in SpaceObjects.ToArray())
			{
				if (sobj.Owner != Owner || ancestors.Contains(sobj) || sobj.Sector != Sector || sobj.IsDestroyed)
					SpaceObjects.Remove(sobj);
				else if (sobj is Fleet)
					((Fleet)sobj).Validate(ancestors);
			}
		}

		[DoNotSerialize]
		public double TimeToNextMove
		{
			get;
			set;
		}

		public double TimePerMove
		{
			get { return SpaceObjects.Max(sobj => sobj.TimePerMove); }
		}

		public void RefillMovement()
		{
			MovementRemaining = Speed;
			TimeToNextMove = TimePerMove;
		}

		public int MovementRemaining { get; set; }

		public int Speed
		{
			get { return SpaceObjects.Min(sobj => sobj.Speed); }
		}

		public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> DijkstraMap
		{
			get;
			set;
		}

		public bool CanTarget(ICombatObject target)
		{
			return SpaceObjects.Any(sobj => sobj.CanTarget(target));
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
			get { return SpaceObjects.SelectMany(sobj => sobj.Weapons); }
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
		public int Hitpoints
		{
			get
			{
				return SpaceObjects.Sum(sobj => sobj.Hitpoints);
			}
			set
			{
				throw new NotSupportedException("Cannot set fleet hitpoints directly. Try setting the hitpoints of individual ship components.");
			}
		}

		/// <summary>
		/// The normal shields of this fleet. Cannot set this property; attempting to do so will throw a NotSupportedException.
		/// </summary>
		public int NormalShields
		{
			get
			{
				return SpaceObjects.Sum(sobj => sobj.NormalShields);
			}
			set
			{
				throw new NotSupportedException("Cannot set fleet shields directly. Try setting the shields of individual ships.");
			}
		}

		/// <summary>
		/// The phased shields of this fleet. Cannot set this property; attempting to do so will throw a NotSupportedException.
		/// </summary>
		public int PhasedShields
		{
			get
			{
				return SpaceObjects.Sum(sobj => sobj.PhasedShields);
			}
			set
			{
				throw new NotSupportedException("Cannot set fleet shields directly. Try setting the shields of individual ships.");
			}
		}

		public int MaxHitpoints
		{
			get { return SpaceObjects.Sum(sobj => sobj.MaxHitpoints); }
		}

		public int MaxNormalShields
		{
			get { return SpaceObjects.Sum(sobj => sobj.MaxNormalShields); }
		}

		public int MaxPhasedShields
		{
			get { return SpaceObjects.Sum(sobj => sobj.MaxPhasedShields); }
		}

		public void ReplenishShields()
		{
			foreach (var sobj in SpaceObjects)
				ReplenishShields();
		}

		public int? Repair(int? amount = null)
		{
			// TODO - repair priority
			foreach (var sobj in SpaceObjects)
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
			get { return SpaceObjects.All(sobj => sobj.IsDestroyed); }
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
			SpaceObjects.Clear();
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
			get { return SpaceObjects.Sum(sobj => sobj.SupplyStorage); }
		}

		public int SupplyRemaining
		{
			get
			{
				return SpaceObjects.Sum(sobj => sobj.SupplyRemaining);
			}
			set
			{
				var available = SupplyRemaining;
				var storage = SupplyStorage;
				int spent = 0;
				foreach (var sobj in SpaceObjects)
				{
					var amount = (int)Math.Floor((double)sobj.SupplyStorage / (double)storage * available);
					sobj.SupplyRemaining = amount;
					spent += amount;
				}
				var roundingError = available - spent;
				while (roundingError > 0)
				{
					var sobj2 = SpaceObjects.WithMin(sobj => (double)sobj.SupplyRemaining / (double)sobj.SupplyStorage).PickRandom();
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
			get { return SpaceObjects.Any(sobj => sobj.HasInfiniteSupplies); }
		}

		/// <summary>
		/// Shares supplies between ships in a fleet, proprtional to their supply storage.
		/// </summary>
		public void ShareSupplies()
		{
			if (HasInfiniteSupplies)
			{
				// full refill
				foreach (var sobj in SpaceObjects)
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
			get { return SpaceObjects.All(sobj => sobj.CanWarp); }
		}

		public bool IsIdle
		{
			get
			{
				return Speed > 0 && !Orders.Any() || ConstructionQueues.Any(q => q.Eta < 1);
			}
		}

		/// <summary>
		/// Any construction queues of ships in this fleet.
		/// </summary>
		public IEnumerable<ConstructionQueue> ConstructionQueues
		{
			get
			{
				return SpaceObjects.SelectMany(sobj =>
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
			get { return SpaceObjects.SelectMany(sobj => sobj.Abilities); }
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
			return SpaceObjects.Max(sobj => sobj.CheckVisibility(emp));
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
			get { return SpaceObjects.OfType<ICargoContainer>().Sum(cc => cc.Cargo); }
		}

		public int CargoStorage
		{
			get { return SpaceObjects.OfType<ICargoContainer>().Sum(cc => cc.CargoStorage); }
		}

		public long PopulationStorageFree
		{
			get { return 0L; }
		}

		public long AddPopulation(Civilization.Race race, long amount)
		{
			foreach (var ct in SpaceObjects.OfType<ICargoTransferrer>())
			{
				amount = ct.AddPopulation(race, amount);
			}
			return amount;
		}

		public long RemovePopulation(Civilization.Race race, long amount)
		{
			foreach (var ct in SpaceObjects.OfType<ICargoTransferrer>())
			{
				amount = ct.RemovePopulation(race, amount);
			}
			return amount;
		}

		public bool AddUnit(Vehicles.Unit unit)
		{
			foreach (var ct in SpaceObjects.OfType<ICargoTransferrer>())
			{
				if (ct.AddUnit(unit))
					return true;
			}
			return false;
		}

		public bool RemoveUnit(Vehicles.Unit unit)
		{
			foreach (var ct in SpaceObjects.OfType<ICargoTransferrer>())
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
				foreach (var cc in SpaceObjects.OfType<ICargoContainer>())
				{
					foreach (var kvp in cc.Cargo.Population)
						dict[kvp.Key] += kvp.Value;
				}
				return dict;
			}
		}

		public Fleet Container
		{
			get { return Galaxy.Current.FindSpaceObjects<Fleet>(f => f.SpaceObjects.Contains(this)).Flatten().Flatten().SingleOrDefault(); }
		}

		/// <summary>
		/// When a fleet spends time, all its ships and subfleets do, too.
		/// </summary>
		/// <param name="timeElapsed"></param>
		public void SpendTime(double timeElapsed)
		{
			TimeToNextMove += timeElapsed;
			foreach (var sobj in SpaceObjects)
				sobj.SpendTime(timeElapsed);
		}
	}
}
