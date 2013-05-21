using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Space;

namespace FrEee.Modding.Templates
{
	/// <summary>
	/// A template for creating star systems.
	/// Maps to a record in SystemTypes.txt.
	/// </summary>
	 [Serializable] public class StarSystemTemplate : ITemplate<StarSystem>, INamed
	{
		/// <summary>
		/// Creates an empty star system template.
		/// </summary>
		public StarSystemTemplate()
		{
			Abilities = new List<Ability>();
			WarpPointAbilities = new List<Ability>();
			StellarObjectLocations = new List<IStellarObjectLocation>();
			Radius = 8;
		}

		/// <summary>
		/// The name of this star system template.
		/// </summary>
		public string Name { get; set; }

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

			var planets = new Dictionary<IStellarObjectLocation, Planet>();

			foreach (var loc in StellarObjectLocations)
			{
				// create object
				var sobj = loc.StellarObjectTemplate.Instantiate();
				
				// place object
				sys.GetSector(loc.Resolve(sys)).SpaceObjects.Add(sobj);

				// for planets with moons
				if (sobj is Planet)
					planets.Add(loc, (Planet)sobj);

				// set flags for naming
				sobj.Index = sys.FindSpaceObjects<StellarObject>(s => s.GetType() == sobj.GetType()).Count + 1;
				sobj.IsUnique = StellarObjectLocations.Where(l => typeof(ITemplate<>).MakeGenericType(sobj.GetType()).IsAssignableFrom(l.StellarObjectTemplate.GetType())).Count() == 1;
				if (sobj is Planet && loc is SameAsStellarObjectLocation)
				{
					var planet = (Planet)sobj;
					var loc2 = (SameAsStellarObjectLocation)loc;
					planet.MoonOf = planets[StellarObjectLocations[loc2.TargetIndex - 1]];
				}
			}
			return sys;
		}
	}
}
