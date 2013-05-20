using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;

namespace FrEee.Modding.Templates
{
	/// <summary>
	/// A template for generating asteroid fields.
	/// </summary>
	public class AsteroidFieldTemplate : ITemplate<AsteroidField>
	{
		/// <summary>
		/// Abilities to assign to the asteroid field.
		/// </summary>
		public RandomAbilityTemplate Abilities { get; set; }

		/// <summary>
		/// The size of the asteroid field, or null to choose a size randomly.
		/// </summary>
		public Size? Size { get; set; }

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
			if (Size != null)
				candidates = candidates.Where(ast => ast.Size == Size.Value);
			if (Atmosphere != null)
				candidates = candidates.Where(ast => ast.Atmosphere == Atmosphere);
			if (Surface != null)
				candidates = candidates.Where(ast => ast.Surface == Surface);
			if (!candidates.Any())
				throw new Exception("No asteroid fields in SectType.txt match the criteria!");

			var asteroids = candidates.PickRandom().Instantiate();

			var abil = Abilities.Instantiate();
			if (abil != null)
				asteroids.IntrinsicAbilities.Add(abil);

			asteroids.ResourceValue["Minerals"] = RandomIntHelper.Range(Mod.Current.MinAsteroidResourceValue, Mod.Current.MaxAsteroidResourceValue);
			asteroids.ResourceValue["Organics"] = RandomIntHelper.Range(Mod.Current.MinAsteroidResourceValue, Mod.Current.MaxAsteroidResourceValue);
			asteroids.ResourceValue["Radioactives"] = RandomIntHelper.Range(Mod.Current.MinAsteroidResourceValue, Mod.Current.MaxAsteroidResourceValue);

			return asteroids;
		}
	}
}
