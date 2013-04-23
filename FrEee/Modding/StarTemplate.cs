using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A template for generating stars.
	/// </summary>
	public class StarTemplate : ITemplate<Star>
	{
		public StarTemplate()
		{
			Abilities = new List<Ability>();
		}

		/// <summary>
		/// Abilities to assign to the star.
		/// </summary>
		public IList<Ability> Abilities { get; private set; }

		/// <summary>
		/// The size of the star, or null to choose a size randomly.
		/// </summary>
		public Size? Size { get; set; }

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

		public Star Instantiate()
		{
			var star = new Star();
			foreach (var abil in Abilities)
				star.IntrinsicAbilities.Add(abil);

			// TODO - use SectType.txt entries for instantiating stars
			star.Size = Size ?? new Size[] { Game.Size.Tiny, Game.Size.Small, Game.Size.Medium, Game.Size.Large, Game.Size.Huge }.PickRandom();
			star.Age = Age ?? new string[] { "Young", "Average", "Old", "Ancient" }.PickRandom();
			star.Color = Color ?? new string[] { "Yellow", "Red", "Purple", "Green", "Blue", "White", "Orange" }.PickRandom();
			star.Brightness = Brightness ?? new string[] { "Dim", "Average", "Bright", "Super Bright" }.PickRandom();

			return star;
		}
	}
}
