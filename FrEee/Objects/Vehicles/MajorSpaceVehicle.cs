using FrEee.Enumerations;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;

namespace FrEee.Objects.Vehicles
{
	/// <summary>
	/// A space vehicle which can contain cargo and is not a unit.
	/// </summary>
	public abstract class MajorSpaceVehicle : SpaceVehicle, ICargoTransferrer, IConstructor
	{

		protected MajorSpaceVehicle() : base()
		{
			Cargo = new Cargo();
			constructionQueue = new ConstructionQueue(this);
		}

		public override void Place(ISpaceObject target)
		{
			var search = The.Galaxy.FindSpaceObjects<ISpaceObject>(sobj => sobj == target);
			if (!search.Any())
				throw new Exception("Can't place newly constructed vehicle near " + target + " because the target is not in any known sector.");
			var sys = search.First().StarSystem;
			var coords = search.First().Sector.Coordinates;
			sys.SpaceObjectLocations.Add(new ObjectLocation<ISpaceObject>(this, coords));
		}

		public long AddPopulation(Race race, long amount)
		{
			var canCargo = Math.Min(amount, (long)(this.CargoStorageFree() / The.Mod.Settings.PopulationSize));
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

		public override IMobileSpaceObject RecycleContainer
		{
			get { return this; }
		}

		public override WeaponTargets WeaponTargetType
		{
			// NOTE - SE4 made ships and bases use both the ship target type; maybe we should add an override field in settings.txt to change that optionally?
			get { return WeaponTargets.Ship; }
		}

		public Cargo Cargo { get; set; }

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
		public override void Dispose()
		{
			base.Dispose();
			if (Cargo != null)
			{
				foreach (var u in Cargo.Units)
					u.Dispose();
			}
			constructionQueue.SafeDispose();
		}

		public override void Redact(Empire emp)
		{
			base.Redact(emp);

			var vis = CheckVisibility(emp);

			if (vis < Visibility.Scanned)
			{
				// can't see cargo at all
				Cargo.SetFakeSize(false);
			}
			else if (vis < Visibility.Owned)
			{
				// can only see cargo size if scanned but unowed
				Cargo.SetFakeSize(true);
			}
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

		public override void SpendTime(double timeElapsed)
		{
			base.SpendTime(timeElapsed);

			foreach (var u in Cargo.Units.OfType<IMobileSpaceObject>())
				u.SpendTime(timeElapsed);
		}

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

		public override bool IsIdle => base.IsIdle || ConstructionQueue != null && ConstructionQueue.IsIdle;

		public override bool FillsCombatTile => true;
	}
}

