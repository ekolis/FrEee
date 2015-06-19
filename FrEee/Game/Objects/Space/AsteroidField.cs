using System;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Modding.Templates;
using FrEee.Modding;
using FrEee.Game.Objects.Civilization;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// An asteroid field. Asteroids can be mined or converted to planets.
	/// </summary>
	[Serializable]
	public class AsteroidField : StellarObject, ITemplate<AsteroidField>, IMineableSpaceObject
	{
		public AsteroidField()
		{
			ResourceValue = new ResourceQuantity();
		}

		/// <summary>
		/// The PlanetSize.txt entry for this asteroid field's size.
		/// </summary>
		[DoNotSerialize]
		public StellarObjectSize Size { get { return size; } set { size = value; } }

		private ModReference<StellarObjectSize> size { get; set; }

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
		public ResourceQuantity ResourceValue { get; set; }

		/// <summary>
		/// Just copy the asteroid field's data.
		/// </summary>
		/// <returns>A copy of the asteroid field.</returns>
		public AsteroidField Instantiate()
		{
			return this.Copy();
		}

		public double MineralsValue { get { return ResourceValue[Resource.Minerals]; } }
		public double OrganicsValue { get { return ResourceValue[Resource.Organics]; } }
		public double RadioactivesValue { get { return ResourceValue[Resource.Radioactives]; } }

		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.AsteroidField; }
		}

		[DoNotSerialize(false)]
		public override Empire Owner
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException("Cannot set the owner of an asteroid field; it is always null.");
			}
		}
	}
}
