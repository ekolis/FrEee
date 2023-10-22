using FrEee.Enumerations;
using FrEee.Interfaces;
using FrEee.Objects.Space;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Extensions;

namespace FrEee.Modding.Templates
{
	/// <summary>
	/// A template for generating planets.
	/// </summary>
	[Serializable]
	public class PlanetTemplate : ITemplate<Planet>, IModObject
	{
		/// <summary>
		/// Abilities to assign to the planet.
		/// </summary>
		public RandomAbilityTemplate Abilities { get; set; }

		/// <summary>
		/// The atmosphere of the planet, or null to choose a planet randomly.
		/// </summary>
		public string Atmosphere { get; set; }

		/// <summary>
		/// Do we want a constructed world (such as a ringworld or sphereworld)?
		/// </summary>
		public bool IsConstructed { get; set; }

		public bool IsDisposed
		{
			get; private set;
		}

		public string ModID
		{
			get;
			set;
		}

		public string Name { get; set; }

		/// <summary>
		/// The size of the planet, or null to choose a size randomly.
		/// </summary>
		public StellarSize? StellarSize { get; set; }

		/// <summary>
		/// The surface compositiion of the planet, or null to choose a surface randomly.
		/// </summary>
		public string Surface { get; set; }

		/// <summary>
		/// Parameters from the mod meta templates.
		/// </summary>
		public IDictionary<string, object> TemplateParameters { get; set; }

		public void Dispose()
		{
			// TODO - remove it from somewhere?
			IsDisposed = true;
		}

		public Planet Instantiate(Game game)
		{
			var candidates = game.Mod.StellarObjectTemplates.OfType<Planet>();
			if (Atmosphere != null)
				candidates = candidates.Where(p => p.Atmosphere == Atmosphere);
			if (Surface != null)
				candidates = candidates.Where(p => p.Surface == Surface);
			if (StellarSize != null)
				candidates = candidates.Where(p => p.StellarSize == StellarSize);
			candidates = candidates.Where(p => p.Size.IsConstructed == IsConstructed);

			if (!candidates.Any())
				throw new Exception("No planets in SectType.txt match the criteria:\n\tAtmosphere: " + (Atmosphere ?? "Any") + "\n\tSurface: " + (Surface ?? "Any") + "\n\tStellar Size: " + (StellarSize == null ? "Any" : StellarSize.ToString()));

			var planet = candidates.PickRandom().Instantiate(game);

			if (planet.Size == null)
			{
				var sizes = game.Mod.StellarObjectSizes.Where(sos => sos.StellarObjectType == "Planet" && !sos.IsConstructed && (StellarSize == null || sos.StellarSize == StellarSize.Value));
				planet.Size = sizes.PickRandom();
			}

			var abil = Abilities.Instantiate(game);
			if (abil != null)
				planet.IntrinsicAbilities.Add(abil);

			planet.ResourceValue[Resource.Minerals] = RandomHelper.Range(game.Setup.MinSpawnedPlanetValue, game.Setup.MaxSpawnedPlanetValue);
			planet.ResourceValue[Resource.Organics] = RandomHelper.Range(game.Setup.MinSpawnedPlanetValue, game.Setup.MaxSpawnedPlanetValue);
			planet.ResourceValue[Resource.Radioactives] = RandomHelper.Range(game.Setup.MinSpawnedPlanetValue, game.Setup.MaxSpawnedPlanetValue);

			planet.ConditionsAmount = RandomHelper.Range(game.Mod.Settings.MinRandomPlanetConditions, game.Mod.Settings.MaxRandomPlanetConditions);

			return planet;
		}
	}
}
