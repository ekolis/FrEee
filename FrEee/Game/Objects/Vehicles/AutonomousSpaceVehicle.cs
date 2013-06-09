using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;
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
	public abstract class AutonomousSpaceVehicle : Vehicle, IMobileSpaceObject<AutonomousSpaceVehicle>
	{
		public AutonomousSpaceVehicle()
		{
			IntrinsicAbilities = new List<Ability>();
			Orders = new List<IMobileSpaceObjectOrder<AutonomousSpaceVehicle>>();
			constructionQueue = new ConstructionQueue(this);
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
			var sector = sys.GetSector(coords);
			sector.SpaceObjects.Add(this);
		}


		public IList<Ability> IntrinsicAbilities
		{
			get;
			private set;
		}

		public Visibility CheckVisibility(Galaxy galaxy, StarSystem starSystem)
		{
			if (galaxy.CurrentEmpire == null)
				return Visibility.Scanned; // host can see everything

			if (galaxy.CurrentEmpire == Owner)
				return Visibility.Scanned;

			// TODO - check for cloaking vs. sensors

			// TODO - check for long range scanners

			if (galaxy.OmniscientView || starSystem.FindSpaceObjects<ISpaceObject>(sobj => sobj.Owner == galaxy.CurrentEmpire).SelectMany(g => g).Any())
				return Visibility.Visible; // player can see vehicles in systems he owns stuff in, or if he has an omniscient view

			// no fog of war for space vehicles...
			
			return Visibility.Unknown;
		}

		public void Redact(Galaxy galaxy, StarSystem starSystem, Visibility visibility)
		{
			if (visibility == Visibility.Unknown)
				throw new ArgumentException("If a space vehicle is not visible at all, it should be removed from the player's savegame rather than redacted.", "visibility");

			// Can't see the ship's components if it's not scanned
			if (visibility < Visibility.Scanned)
			{
				// create fake design
				var d = new Design<AutonomousSpaceVehicle>();
				d.Hull = (IHull<AutonomousSpaceVehicle>)Design.Hull;
				d.Owner = Design.Owner;
				Design = d;
			}
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

		public IList<IMobileSpaceObjectOrder<AutonomousSpaceVehicle>> Orders
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
				Orders.First().Execute();
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

		public bool CanTarget(ISpaceObject target)
		{
			// TODO - alliances
			return target.Owner != Owner && Components.Any(c => !c.IsDestroyed && c.Template.ComponentTemplate.WeaponInfo != null && c.Template.ComponentTemplate.WeaponInfo.Targets.HasFlag(WeaponTargetType));
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
	}
}
