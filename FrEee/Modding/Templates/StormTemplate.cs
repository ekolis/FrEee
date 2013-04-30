using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Templates
{
	/// <summary>
	/// A template for generating storms.
	/// </summary>
	public class StormTemplate : ITemplate<Storm>
	{
		/// <summary>
		/// Abilities to assign to the storm.
		/// </summary>
		public RandomAbilityTemplate Abilities { get; set; }

		/// <summary>
		/// The size of the storm, or null to choose a size randomly.
		/// </summary>
		public Size? Size { get; set; }

		public Storm Instantiate()
		{
			var storm = new Storm();

			var abil = Abilities.Instantiate();
			if (abil != null)
				storm.IntrinsicAbilities.Add(abil);

			// TODO - use SectType.txt entries for instantiating storms
			storm.Size = Size ?? new Size[] { Game.Size.Tiny, Game.Size.Small, Game.Size.Medium, Game.Size.Large, Game.Size.Huge }.PickRandom();
			
			return storm;
		}
	}
}
