using FrEee.Enumerations;
using FrEee.Interfaces;
using FrEee.Objects.Space;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

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

			planet.ResourceValue[Resource.Minerals] = RandomHelper.Range(The.Setup.MinSpawnedPlanetValue, The.Setup.MaxSpawnedPlanetValue);
			planet.ResourceValue[Resource.Organics] = RandomHelper.Range(The.Setup.MinSpawnedPlanetValue, The.Setup.MaxSpawnedPlanetValue);
			planet.ResourceValue[Resource.Radioactives] = RandomHelper.Range(The.Setup.MinSpawnedPlanetValue, The.Setup.MaxSpawnedPlanetValue);

			planet.ConditionsAmount = RandomHelper.Range(Mod.Current.Settings.MinRandomPlanetConditions, Mod.Current.Settings.MaxRandomPlanetConditions);

			return planet;
		}
	}
}
