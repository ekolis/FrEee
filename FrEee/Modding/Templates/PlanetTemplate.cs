using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Templates
{
	/// <summary>
	/// A template for generating planets.
	/// </summary>
	public class PlanetTemplate : ITemplate<Planet>
	{
		/// <summary>
		/// Abilities to assign to the planet.
		/// </summary>
		public RandomAbilityTemplate Abilities { get; set; }

		/// <summary>
		/// The size of the planet, or null to choose a size randomly.
		/// </summary>
		public Size? Size { get; set; }

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
			if (Size != null)
				candidates = candidates.Where(p => p.Size == Size.Value);
			if (Atmosphere != null)
				candidates = candidates.Where(p => p.Atmosphere == Atmosphere);
			if (Surface != null)
				candidates = candidates.Where(p => p.Surface == Surface);
			if (!candidates.Any())
				throw new Exception("No planets in SectType.txt match the criteria!");

			var planet = candidates.PickRandom().Instantiate();

			var abil = Abilities.Instantiate();
			if (abil != null)
				planet.IntrinsicAbilities.Add(abil);

			planet.ResourceValue["minerals"] = Rng.Range(Mod.Current.MinAsteroidResourceValue, Mod.Current.MaxAsteroidResourceValue);
			planet.ResourceValue["organics"] = Rng.Range(Mod.Current.MinAsteroidResourceValue, Mod.Current.MaxAsteroidResourceValue);
			planet.ResourceValue["radioactives"] = Rng.Range(Mod.Current.MinAsteroidResourceValue, Mod.Current.MaxAsteroidResourceValue);

			return planet;
		}
	}
}
