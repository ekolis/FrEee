using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A template for generating asteroid fields.
	/// </summary>
	public class AsteroidFieldTemplate : ITemplate<AsteroidField>
	{
		public AsteroidFieldTemplate()
		{
			Abilities = new List<Ability>();
		}
		
		/// <summary>
		/// Abilities to assign to the asteroid field.
		/// </summary>
		public IList<Ability> Abilities { get; private set; }

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
			var asteroids = new AsteroidField();
			foreach (var abil in Abilities)
				asteroids.IntrinsicAbilities.Add(abil);

			// TODO - use SectType.txt entries for instantiating planets
			asteroids.Size = Size ?? new Size[] { Game.Size.Tiny, Game.Size.Small, Game.Size.Medium, Game.Size.Large, Game.Size.Huge }.PickRandom();
			asteroids.Atmosphere = Atmosphere ?? new string[] { "None", "Methane", "Oxygen", "Hydrogen", "Carbon Dioxide" }.PickRandom();
			asteroids.Surface = Surface ?? new string[] { "Rock", "Ice", "Gas Giant" }.PickRandom();

			return asteroids;
		}
	}
}
