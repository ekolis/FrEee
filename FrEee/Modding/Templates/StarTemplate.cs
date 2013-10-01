using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility; using FrEee.Utility.Serialization;
using FrEee.Utility.Extensions;

namespace FrEee.Modding.Templates
{
	/// <summary>
	/// A template for generating stars.
	/// </summary>
	 [Serializable] public class StarTemplate : ITemplate<Star>
	{
		/// <summary>
		/// Abilities to assign to the star.
		/// </summary>
		public RandomAbilityTemplate Abilities { get; set; }

		/// <summary>
		/// The size of the star, or null to choose a size randomly.
		/// </summary>
		public StellarSize? StellarSize { get; set; }

		/// <summary>
		/// The age of the star, or null to choose an age randomly.
		/// </summary>
		public string Age { get; set; }

		/// <summary>
		/// The color of the star, or null to choose a color randomly.
		/// </summary>
		public string Color { get; set; }

		/// <summary>
		/// The brightness of the star, or null to choose a brightness randomly.
		/// </summary>
		public string Brightness { get; set; }

		/// <summary>
		/// Do we want a destroyed star?
		/// </summary>
		public bool IsDestroyed { get; set; }

		public Star Instantiate()
		{
			var candidates = Mod.Current.StellarObjectTemplates.OfType<Star>();
			if (StellarSize != null)
				candidates = candidates.Where(s => s.StellarSize == StellarSize.Value);
			if (Age != null)
				candidates = candidates.Where(s => s.Age == Age);
			if (Color != null)
				candidates = candidates.Where(s => s.Color == Color);
			if (Brightness != null)
				candidates = candidates.Where(s => s.Brightness == Brightness);
			if (!candidates.Any())
				throw new Exception("No stars in SectType.txt match the criteria:\n\tStellar Size: " + (StellarSize == null ? "Any" : StellarSize.ToString()) +"\n\tAge: " + (Age ?? "Any") + "\n\tColor: " + (Color ?? "Any") + "\n\tBrightness: " + (Brightness ?? "Any"));

			var star = candidates.PickRandom().Instantiate();

			var abil = Abilities.Instantiate();
			if (abil != null)
				star.IntrinsicAbilities.Add(abil);

			return star;
		}
	}
}
