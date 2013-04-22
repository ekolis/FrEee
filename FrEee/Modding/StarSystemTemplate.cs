using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game;

namespace FrEee.Modding
{
	/// <summary>
	/// A template for creating star systems.
	/// Maps to an entry in StarSystems.json (SystemTypes.txt in SE4).
	/// </summary>
	public class StarSystemTemplate : ITemplate<StarSystem>
	{
		public StarSystemTemplate()
		{
			Abilities = new List<Ability>();
			WarpPointAbilities = new List<Ability>();
			StellarObjectLocations = new List<IStellarObjectLocation>();
		}

		/// <summary>
		/// The radius of star systems generated from this template.
		/// </summary>
		public int Radius { get; set; }

		/// <summary>
		/// A description to use for star systems generated from this template.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The path to the background image for star systems generated from this template.
		/// </summary>
		public string BackgroundImagePath { get; set; }

		/// <summary>
		/// If true, empire homeworlds can be located in systems generated from this template.
		/// </summary>
		public bool EmpiresCanStartIn { get; set; }

		/// <summary>
		/// If true, the background image for star systems generated from this template will be centered, not tiled, in combat.
		/// </summary>
		public bool NonTiledCenterCombatImage { get; set; }

		/// <summary>
		/// Any special abilities to be possessed by star systems generated from this template.
		/// </summary>
		public IList<Ability> Abilities { get; private set; }

		/// <summary>
		/// Abilities for random warp points in systems generated from this template.
		/// </summary>
		public IList<Ability> WarpPointAbilities { get; private set; }

		/// <summary>
		/// Stellar objects (such as stars and planets) and their locations in systems generated from this template.
		/// </summary>
		public IList<IStellarObjectLocation> StellarObjectLocations { get; private set; }

		public StarSystem Instantiate()
		{
			var sys = new StarSystem(Radius);
			sys.Name = "Unnamed"; // star system will be named later in galaxy generation
			sys.Description = Description;
			sys.BackgroundImagePath = BackgroundImagePath;
			sys.EmpiresCanStartIn = EmpiresCanStartIn;
			sys.NonTiledCenterCombatImage = NonTiledCenterCombatImage;
			foreach (var abil in Abilities)
				sys.Abilities.Add(abil);
			sys.WarpPointAbilities = WarpPointAbilities; // warp points will be generated later in galaxy generation
			foreach (var loc in StellarObjectLocations)
				sys.GetSector(loc.Resolve(Radius)).SpaceObjects.Add(loc.StellarObjectTemplate.Instantiate());
			return sys;
		}
	}
}
