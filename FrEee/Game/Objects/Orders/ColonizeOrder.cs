using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Civilization;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to colonize an uninhabited planet.
	/// </summary>
	[Serializable]
	public class ColonizeOrder : IMobileSpaceObjectOrder<AutonomousSpaceVehicle>
	{
		public ColonizeOrder(Planet planet)
		{
			Planet = planet;
		}

		/// <summary>
		/// The planet we are colonizing.
		/// </summary>
		[DoNotSerialize]
		public Planet Planet { get { return planet; } set { planet = value; } }

		private Reference<Planet> planet {get; set;}

		public void Execute(AutonomousSpaceVehicle sobj)
		{
			var here = sobj.FindSector();
			if (here == Planet.FindSector())
			{
				// make sure we can still colonize
				if (Planet.Colony != null)
				{
					// planet is already colonized!
					((ISpaceObject)sobj).Owner.Log.Add(Planet.CreateLogMessage(Planet + " cannot be colonized by " + sobj + " because there is already a colony there belonging to the " + Planet.Colony.Owner + "."));
				}
				else if (!sobj.HasAbility(Planet.ColonizationAbilityName))
				{
					// no such colony module
					((ISpaceObject)sobj).Owner.Log.Add(sobj.CreateLogMessage(sobj + " cannot colonize " + Planet + " because it lacks a " + Planet.Surface + " colony module."));
				}
				else if (Galaxy.Current.CanColonizeOnlyBreathable && Planet.Atmosphere != sobj.Owner.PrimaryRace.NativeAtmosphere)
				{
					// can only colonize breathable atmosphere (due to game setup option)
					sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " cannot colonize " + Planet + " because we can only colonize " + sobj.Owner.PrimaryRace.NativeAtmosphere + " planets."));
				}
				else if (Galaxy.Current.CanColonizeOnlyHomeworldSurface && Planet.Surface != sobj.Owner.PrimaryRace.NativeSurface)
				{
					// can only colonize breathable atmosphere (due to game setup option)
					sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " cannot colonize " + Planet + " because we can only colonize " + sobj.Owner.PrimaryRace.NativeSurface + " planets."));
				}
				else
				{
					// colonize now!!!
					Planet.Colony = new Colony { Owner = sobj.Owner };
					Planet.Colony.ConstructionQueue = new ConstructionQueue(Planet);
					foreach (var kvp in sobj.Cargo.Population)
					{
						// place population on planet
						Planet.Colony.Population.Add(kvp);
					}
					foreach (var unit in sobj.Cargo.Units)
					{
						// planet unit on planet
						Planet.Colony.Cargo.Units.Add(unit);
					}

					// bye bye colony ship
					sobj.FindSector().SpaceObjects.Remove(sobj);

					// done colonizing
					IsComplete = true;
				}
			}
			else
			{
				// can't colonize here, maybe the GUI should have issued a move order?
				((ISpaceObject)sobj).Owner.Log.Add(sobj.CreateLogMessage(sobj + " cannot colonize " + Planet + " because it is not currently located at the planet."));
			}

			// spend time
			sobj.TimeToNextMove += sobj.TimePerMove;
		}

		public bool IsComplete
		{
			get;
			private set;
		}

		public override string ToString()
		{
			return "Colonize " + Planet.Name;
		}
	}
}
