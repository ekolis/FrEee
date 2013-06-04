using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Modding.Templates
{
	/// <summary>
	/// A template for generating planets.
	/// </summary>
	[Serializable]
	public class PlanetTemplate : ITemplate<Planet>
	{
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

		public Planet Instantiate()
		{
			var candidates = Mod.Current.StellarObjectTemplates.OfType<Planet>();
			if (Atmosphere != null)
				candidates = candidates.Where(p => p.Atmosphere == Atmosphere);
			if (Surface != null)
				candidates = candidates.Where(p => p.Surface == Surface);
			var size = Mod.Current.StellarObjectSizes.Where(sos => sos.StellarObjectType == "Planet" && !sos.IsConstructed && (StellarSize == null || sos.StellarSize == StellarSize.Value)).PickRandom();
			candidates = candidates.Where(p => p.Size == size);

			if (!candidates.Any())
				throw new Exception("No planets in SectType.txt match the criteria:\n\tAtmosphere: " + Atmosphere + "\n\tSurface: " + Surface + "\n\tSize: " + size.Name);

			var planet = candidates.PickRandom().Instantiate();

			var abil = Abilities.Instantiate();
			if (abil != null)
				planet.IntrinsicAbilities.Add(abil);

			planet.ResourceValue["Minerals"] = RandomHelper.Range(Mod.Current.MinPlanetResourceValue, Mod.Current.MaxPlanetResourceValue);
			planet.ResourceValue["Organics"] = RandomHelper.Range(Mod.Current.MinPlanetResourceValue, Mod.Current.MaxPlanetResourceValue);
			planet.ResourceValue["Radioactives"] = RandomHelper.Range(Mod.Current.MinPlanetResourceValue, Mod.Current.MaxPlanetResourceValue);

			return planet;
		}
	}
}
