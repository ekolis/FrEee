﻿using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Modding.Interfaces;

namespace FrEee.Modding.Templates
{
	/// <summary>
	/// A template for generating planets.
	/// </summary>
	[Serializable]
	public class PlanetTemplate : ITemplate<Planet>, IModObject
	{
		public string Name { get; set; }

		/// <summary>
		/// Abilities to assign to the planet.
		/// </summary>
		public RandomAbilityTemplate Abilities { get; set; }

		/// <summary>
		/// The size of the planet, or null to choose a size randomly.
		/// </summary>
		public StellarSize? StellarSize { get; set; }

		/// <summary>
		/// The atmosphere of the planet, or null to choose a planet randomly.
		/// </summary>
		public string Atmosphere { get; set; }

		/// <summary>
		/// The surface compositiion of the planet, or null to choose a surface randomly.
		/// </summary>
		public string Surface { get; set; }

		/// <summary>
		/// Do we want a constructed world (such as a ringworld or sphereworld)?
		/// </summary>
		public bool IsConstructed { get; set; }

		public Planet Instantiate()
		{
			var candidates = Mod.Current.StellarObjectTemplates.OfType<Planet>();
			if (Atmosphere != null)
				candidates = candidates.Where(p => p.Atmosphere == Atmosphere);
			if (Surface != null)
				candidates = candidates.Where(p => p.Surface == Surface);
			if (StellarSize != null)
				candidates = candidates.Where(p => p.StellarSize == StellarSize);
			candidates = candidates.Where(p => p.Size.IsConstructed == IsConstructed);

			if (!candidates.Any())
				throw new Exception("No planets in SectType.txt match the criteria:\n\tAtmosphere: " + (Atmosphere ?? "Any") + "\n\tSurface: " + (Surface ?? "Any") + "\n\tStellar Size: " + (StellarSize == null ? "Any" : StellarSize.ToString()));

			var planet = candidates.PickRandom().Instantiate();

			if (planet.Size == null)
			{
				var sizes = Mod.Current.StellarObjectSizes.Where(sos => sos.StellarObjectType == "Planet" && !sos.IsConstructed && (StellarSize == null || sos.StellarSize == StellarSize.Value));
				planet.Size = sizes.PickRandom();
			}

			var abil = Abilities.Instantiate();
			if (abil != null)
				planet.IntrinsicAbilities.Add(abil);

			planet.ResourceValue[Resource.Minerals] = RandomHelper.Range(Galaxy.Current.MinSpawnedPlanetValue, Galaxy.Current.MaxSpawnedPlanetValue);
			planet.ResourceValue[Resource.Organics] = RandomHelper.Range(Galaxy.Current.MinSpawnedPlanetValue, Galaxy.Current.MaxSpawnedPlanetValue);
			planet.ResourceValue[Resource.Radioactives] = RandomHelper.Range(Galaxy.Current.MinSpawnedPlanetValue, Galaxy.Current.MaxSpawnedPlanetValue);

			return planet;
		}

		public string ModID
		{
			get;
			set;
		}
	}
}
