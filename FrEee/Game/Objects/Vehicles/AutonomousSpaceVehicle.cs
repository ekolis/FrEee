using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Orders;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
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
	/// An autonomous space vehicle which does not operate in groups.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public abstract class AutonomousSpaceVehicle : Vehicle, IMobileSpaceObject<AutonomousSpaceVehicle>, ICargoContainer
	{
		public AutonomousSpaceVehicle()
		{
			IntrinsicAbilities = new List<Ability>();
			Orders = new List<IOrder<AutonomousSpaceVehicle>>();
			constructionQueue = new ConstructionQueue(this);
			Cargo = new Cargo();
		}

		public override bool RequiresSpaceYardQueue
		{
			get { return true; }
		}

		public override void Place(ISpaceObject target)
		{
			var search = Galaxy.Current.FindSpaceObjects<ISpaceObject>(sobj => sobj == target);
			if (!search.Any() || !search.First().Any() || !search.First().First().Any())
				throw new Exception("Can't place newly constructed vehicle near " + target + " because the target is not in any known sector.");
			var sys = search.First().Key.Item;
			var coords = search.First().First().First().Key;
			sys.SpaceObjectLocations.Add(new ObjectLocation<ISpaceObject>(this, coords));
		}


		public IList<Ability> IntrinsicAbilities
		{
			get;
			private set;
		}

		public IEnumerable<Ability> Abilities
		{
			get { return IntrinsicAbilities.Concat(Design.Abilities).Stack(); }
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

		public IList<IOrder<AutonomousSpaceVehicle>> Orders
		{
			get;
			private set;
		}

		public void ExecuteOrders()
		{
			TimeToNextMove -= Galaxy.Current.NextTickSize;
			while (TimeToNextMove <= 0)
			{
				if (!Orders.Any())
					break;
				Orders.First().Execute(this);
				if (Orders.First().IsComplete)
					Orders.RemoveAt(0);
			}
		}

		public int ID
		{
			get;
			set;
		}

		/// <summary>
		/// Fractional turns until the vehicle has saved up another move point.
		/// </summary>
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
		/// Refills the vehicle's movement points.
		/// </summary>
		public void RefillMovement()
		{
			MovementRemaining = Speed;
			TimeToNextMove = TimePerMove;
		}

		/// <summary>
		/// Autonomous space vehicles can warp.
		/// </summary>
		public bool CanWarp { get { return true; } }

		private ConstructionQueue constructionQueue;

		public ConstructionQueue ConstructionQueue
		{
			get
			{
				if (this.HasAbility("Space Yard"))
					return constructionQueue;
				else
					return null;
			}
		}

		/// <summary>
		/// Autonomous space vehicles can be placed in fleets.
		/// </summary>
		public bool CanBeInFleet
		{
			get { return true; }
		}

		/// <summary>
		/// Autonomous space vehicles' cargo storage depends on their abilities.
		/// </summary>
		public int CargoStorage
		{
			get { return this.GetAbilityValue("Cargo Storage").ToInt(); }
		}

		/// <summary>
		/// Autonomous space vehicles' supply storage depends on their abilities.
		/// </summary>
		public int SupplyStorage
		{
			get { return this.GetAbilityValue("Supply Storage").ToInt(); }
		}

		/// <summary>
		/// Autonomouse space vehicles do not have infinite supplies unless they have a quantum reactor.
		/// </summary>
		public bool HasInfiniteSupplies
		{
			get { return this.HasAbility("Quantum Reactor"); }
		}

		public bool CanTarget(ICombatObject target)
		{
			// TODO - alliances
			return target.Owner != Owner && Components.Any(c => !c.IsDestroyed && c.Template.ComponentTemplate.WeaponInfo != null && c.Template.ComponentTemplate.WeaponInfo.Targets.HasFlag(target.WeaponTargetType));
		}

		public abstract WeaponTargets WeaponTargetType { get; }


		public void ReplenishShields()
		{
			NormalShields = MaxNormalShields;
			PhasedShields = MaxPhasedShields;
		}

		public override ICombatObject CombatObject
		{
			get { return this; }
		}

		public Cargo Cargo { get; set; }

		public void AddOrder(IOrder order)
		{
			if (!(order is IOrder<AutonomousSpaceVehicle>))
				throw new Exception("Can't add a " + order.GetType() + " to an autonomous space vehicle's orders.");
			Orders.Add((IOrder<AutonomousSpaceVehicle>)order);
		}

		public void RemoveOrder(IOrder order)
		{
			if (!(order is IOrder<AutonomousSpaceVehicle>))
				throw new Exception("Can't remove a " + order.GetType() + " from an autonomous space vehicle's orders.");
			Orders.Remove((IOrder<AutonomousSpaceVehicle>)order);
		}

		public void RearrangeOrder(IOrder order, int delta)
		{
			if (!(order is IOrder<AutonomousSpaceVehicle>))
				throw new Exception("Can't rearrange a " + order.GetType() + " in an autonomous space vehicle's orders.");
			var o = (IOrder<AutonomousSpaceVehicle>)order;
			var newpos = Orders.IndexOf(o) + delta;
			Orders.Remove(o);
			Orders.Insert(newpos, o);
		}


		public IEnumerable<Sector> Path
		{
			get
			{
				var last = this.FindSector();
				foreach (var order in Orders)
				{
					// TODO - figure out which orders should take a move to execute and which shouldn't - assuming only move and warp orders do now
					if (order is MoveOrder<AutonomousSpaceVehicle>)
					{
						var mo = (MoveOrder<AutonomousSpaceVehicle>)order;
						foreach (var sector in mo.Pathfind(this, last))
						{
							last = sector;
							yield return last;
						}
					}
					else if (order is WarpOrder<AutonomousSpaceVehicle>)
					{
						var wo = (WarpOrder<AutonomousSpaceVehicle>)order;
						last = wo.WarpPoint.Target;
						yield return last;
					}
					else if (order is PursueOrder<AutonomousSpaceVehicle>)
					{
						var po = (PursueOrder<AutonomousSpaceVehicle>)order;
						foreach (var sector in po.Pathfind(this, last))
						{
							last = sector;
							yield return last;
						}
					}
					else if (order is EvadeOrder<AutonomousSpaceVehicle>)
					{
						var eo = (EvadeOrder<AutonomousSpaceVehicle>)order;
						foreach (var sector in eo.Pathfind(this, last))
						{
							last = sector;
							yield return last;
						}
					}
				}
			}
		}

		public int Accuracy
		{
			get
			{
				return this.GetAbilityValue("Combat To Hit Offense Plus").ToInt() - this.GetAbilityValue("Combat To Hit Offense Minus").ToInt();
			}
		}

		public int Evasion
		{
			get
			{
				return this.GetAbilityValue("Combat To Hit Defense Plus").ToInt() - this.GetAbilityValue("Combat To Hit Defense Minus").ToInt();
			}
		}

		public override void Dispose()
		{
			this.FindStarSystem().Remove(this);
			base.Dispose();
		}

		/// <summary>
		/// Resource cost per turn to maintain this vehicle.
		/// </summary>
		public ResourceQuantity MaintenanceCost
		{
			get
			{
				double pct = Mod.Current.Settings.ShipBaseMaintenanceRate;
				pct += this.GetAbilityValue("Modified Maintenance Cost").ToInt();
				pct -= this.FindStarSystem().GetSectorAbilityValue(this.FindCoordinates(), Owner, "Reduced Maintenance Cost - Sector").ToInt();
				pct -= this.FindStarSystem().GetAbilityValue(Owner, "Reduced Maintenance Cost - System").ToInt();
				if (Owner.PrimaryRace.Aptitudes.ContainsKey(Aptitude.Maintenance.Name))
					pct -= Owner.PrimaryRace.Aptitudes[Aptitude.Maintenance.Name] - 100;
				return Cost * pct / 100d;
			}
		}

		public override Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Owned;
			if (this.FindStarSystem() == null)
				return Visibility.Unknown;
			// TODO - cloaking
			var seers = this.FindStarSystem().FindSpaceObjects<ISpaceObject>(sobj => sobj.Owner == emp).Flatten();
			if (!seers.Any())
				return Visibility.Unknown; // TODO - memory sight
			var scanners = seers.Where(sobj => sobj.GetAbilityValue("Long Range Scanner").ToInt() >= Pathfinder.Pathfind(null, sobj.FindSector(), this.FindSector(), false, false).Count());
			if (scanners.Any())
				return Visibility.Scanned;
			return Visibility.Visible;
		}


		public void Redact(Empire emp)
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
			// TODO - let player see design of previously scanned ship
			if (visibility < Visibility.Scanned)
			{
				// create fake design
				var d = new Design<AutonomousSpaceVehicle>();
				d.Hull = (IHull<AutonomousSpaceVehicle>)Design.Hull;
				d.Owner = Design.Owner;
				Design = d;
			}

			if (visibility < Visibility.Visible)
				Dispose(); // TODO - memory sight
		}
	}
}
