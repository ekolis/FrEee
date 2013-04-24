using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A template for generating planets.
	/// </summary>
	public class PlanetTemplate : ITemplate<Planet>
	{
		/// <summary>
		/// Abilities to assign to the planet.
		/// </summary>
		public IList<Ability> Abilities { get; private set; }

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
			var planet = new Planet();
			foreach (var abil in Abilities)
				planet.IntrinsicAbilities.Add(abil);

			// TODO - use SectType.txt entries for instantiating planets
			planet.Size = Size ?? new Size[] { Game.Size.Tiny, Game.Size.Small, Game.Size.Medium, Game.Size.Large, Game.Size.Huge }.PickRandom();
			planet.Atmosphere = Atmosphere ?? new string[] { "None", "Methane", "Oxygen", "Hydrogen", "Carbon Dioxide" }.PickRandom();
			planet.Surface = Surface ?? new string[] { "Rock", "Ice", "Gas Giant" }.PickRandom();

			return planet;
		}
	}
}
