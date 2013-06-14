using System;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Modding.Templates;
using FrEee.Modding;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// An asteroid field. Asteroids can be mined or converted to planets.
	/// </summary>
	[Serializable]
	public class AsteroidField : StellarObject, ITemplate<AsteroidField>
	{
		public AsteroidField()
		{
			ResourceValue = new Resources();
		}

		/// <summary>
		/// The PlanetSize.txt entry for this asteroid field's size.
		/// </summary>
		public StellarObjectSize Size { get; set; }

		/// <summary>
		/// The surface composition (e.g. rock, ice, gas) of this asteroid field.
		/// </summary>
		public string Surface { get; set; }

		/// <summary>
		/// The atmospheric composition (e.g. methane, oxygen, carbon dioxide) of this asteroid field.
		/// </summary>
		public string Atmosphere { get; set; }

		/// <summary>
		/// Some sort of combat image? Where are these stored anyway?
		/// </summary>
		public string CombatTile { get; set; }
		/// <summary>
		/// The resource value of this asteroid field, in %.
		/// </summary>
		public Resources ResourceValue { get; set; }

		/// <summary>
		/// Just copy the asteroid field's data.
		/// </summary>
		/// <returns>A copy of the asteroid field.</returns>
		public new AsteroidField Instantiate()
		{
			return this.Copy();
		}

		public StellarSize StellarSize
		{
			get { return Size.StellarSize; }
		}
	}
}
