using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility; using Newtonsoft.Json;
using FrEee.Utility.Extensions;
using FrEee.Modding.Interfaces;

namespace FrEee.Modding.Templates
{
	/// <summary>
	/// A template for generating asteroid fields.
	/// </summary>
	 [Serializable] public class AsteroidFieldTemplate : ITemplate<AsteroidField>, IModObject
	{
		 public string Name { get; set; }

		/// <summary>
		/// Abilities to assign to the asteroid field.
		/// </summary>
		public RandomAbilityTemplate Abilities { get; set; }

		/// <summary>
		/// The size of the asteroid field, or null to choose a size randomly.
		/// </summary>
		public StellarSize? StellarSize { get; set; }

		/// <summary>
		/// The atmosphere of the asteroid field, or null to choose a planet randomly.
		/// </summary>
		public string Atmosphere { get; set; }

		/// <summary>
		/// The surface compositiion of the asteroid field, or null to choose a surface randomly.
		/// </summary>
		public string Surface { get; set; }

		public AsteroidField Instantiate()
		{
			var candidates = Mod.Current.StellarObjectTemplates.OfType<AsteroidField>();
			if (Atmosphere != null)
				candidates = candidates.Where(ast => ast.Atmosphere == Atmosphere);
			if (Surface != null)
				candidates = candidates.Where(ast => ast.Surface == Surface);
			if (StellarSize != null)
				candidates = candidates.Where(ast => ast.StellarSize == StellarSize);

			if (!candidates.Any())
				throw new Exception("No asteroid fields in SectType.txt match the criteria:\n\tAtmosphere: " + (Atmosphere ?? "Any") + "\n\tSurface: " + (Surface ?? "Any") + "\n\tStellar Size: " + (StellarSize == null ?  "Any" : StellarSize.ToString()));

			var asteroids = candidates.PickRandom().Instantiate();

			if (asteroids.Size == null)
			{
				var sizes = Mod.Current.StellarObjectSizes.Where(sos => sos.StellarObjectType == "Asteroids" && !sos.IsConstructed && (StellarSize == null || sos.StellarSize == StellarSize.Value));
				asteroids.Size = sizes.PickRandom();
			}

			var abil = Abilities.Instantiate();
			if (abil != null)
				asteroids.IntrinsicAbilities.Add(abil);

			asteroids.ResourceValue[Resource.Minerals] = RandomHelper.Range(Galaxy.Current.MinSpawnedAsteroidValue, Galaxy.Current.MaxSpawnedAsteroidValue);
			asteroids.ResourceValue[Resource.Organics] = RandomHelper.Range(Galaxy.Current.MinSpawnedAsteroidValue, Galaxy.Current.MaxSpawnedAsteroidValue);
			asteroids.ResourceValue[Resource.Radioactives] = RandomHelper.Range(Galaxy.Current.MinSpawnedAsteroidValue, Galaxy.Current.MaxSpawnedAsteroidValue);

			return asteroids;
		}

		public string ModID
		{
			get;
			set;
		}

		public void Dispose()
		{
			// nothing to do
		}
	}
}
