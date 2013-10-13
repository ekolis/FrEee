using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.LogMessages;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to colonize an uninhabited planet.
	/// </summary>
	[Serializable]
	public class ColonizeOrder : IOrder<SpaceVehicle>
	{
		public ColonizeOrder(Planet planet)
		{
			Owner = Empire.Current;
			Planet = planet;
		}

		/// <summary>
		/// The planet we are colonizing.
		/// </summary>
		[DoNotSerialize]
		public Planet Planet { get { return planet; } set { planet = value; } }

		private Reference<Planet> planet { get; set; }

		public void Execute(SpaceVehicle sobj)
		{
			// error checking
			var errors = GetErrors(sobj);
			foreach (var error in errors)
				sobj.Owner.Log.Add(error);

			if (!errors.Any())
			{
				// colonize now!!!
				Planet.Colony = new Colony { Owner = sobj.Owner };
				Planet.Colony.ConstructionQueue = new ConstructionQueue(Planet);
				foreach (var kvp in sobj.Cargo.Population)
				{
					// place population on planet
					Planet.AddPopulation(kvp.Key, kvp.Value);
				}
				foreach (var unit in sobj.Cargo.Units)
				{
					// planet unit on planet
					Planet.AddUnit(unit);
				}

				// log it!
				sobj.Owner.Log.Add(Planet.CreateLogMessage(sobj + " has founded a new colony on " + Planet + "."));

				// bye bye colony ship
				sobj.Dispose();
			}

			// either done colonizing, or we failed
			IsComplete = true;

			// spend time
			sobj.SpendTime(sobj.TimePerMove);
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

		public void Dispose()
		{
			// TODO - remove from queue, but we don't know which object we're on...
			Galaxy.Current.UnassignID(this);
		}

		private Reference<Empire> owner { get; set; }

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get { return owner; } set { owner = value; } }

		/// <summary>
		/// Orders are visible only to their owners.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Visible;
			return Visibility.Unknown;
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			// This type does not use client objects, so nothing to do here.
		}

		public long ID { get; set; }


		public IEnumerable<LogMessage> GetErrors(SpaceVehicle sobj)
		{
			if (sobj.Sector != Planet.Sector)
			{
				// can't colonize here, maybe the GUI should have issued a move order?
				yield return sobj.CreateLogMessage(sobj + " cannot colonize " + Planet + " because it is not currently located at the planet.");
			}
			if (Planet.Colony != null)
			{
				// planet is already colonized!
				yield return Planet.CreateLogMessage(Planet + " cannot be colonized by " + sobj + " because there is already a colony there belonging to the " + Planet.Colony.Owner + ".");
			}
			if (!sobj.HasAbility(Planet.ColonizationAbilityName))
			{
				// no such colony module
				yield return sobj.CreateLogMessage(sobj + " cannot colonize " + Planet + " because it lacks a " + Planet.Surface + " colony module.");
			}
			if (Galaxy.Current.Settings.CanColonizeOnlyBreathable && Planet.Atmosphere != sobj.Owner.PrimaryRace.NativeAtmosphere)
			{
				// can only colonize breathable atmosphere (due to game setup option)
				yield return sobj.CreateLogMessage(sobj + " cannot colonize " + Planet + " because we can only colonize " + sobj.Owner.PrimaryRace.NativeAtmosphere + " planets.");
			}
			if (Galaxy.Current.Settings.CanColonizeOnlyHomeworldSurface && Planet.Surface != sobj.Owner.PrimaryRace.NativeSurface)
			{
				// can only colonize breathable atmosphere (due to game setup option)
				yield return sobj.CreateLogMessage(sobj + " cannot colonize " + Planet + " because we can only colonize " + sobj.Owner.PrimaryRace.NativeSurface + " planets.");
			}
		}

		public bool CheckCompletion(SpaceVehicle v)
		{
			return IsComplete;
		}

		public bool IsModObject
		{
			get { return false; }
		}
	}
}
