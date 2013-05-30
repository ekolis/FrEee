using System;
using System.Collections.Generic;
using System.Drawing;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Space;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A large immobile installation on a colony.
	/// </summary>
	[Serializable]
	public class Facility : INamed, IResearchable, IAbilityObject, ITemplate<Facility>, IConstructable
	{
		public Facility()
		{
			Abilities = new List<Ability>();
			TechnologyRequirements = new List<TechnologyRequirement>();
			Cost = new Resources();
			ConstructionProgress = new Resources();
		}

		/// <summary>
		/// The name of the facility.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A description of the facility.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The group that the facility belongs to. Used for grouping on the construction queue screen.
		/// </summary>
		public string Group { get; set; }

		/// <summary>
		/// The family that the facility belongs to. Used for "Only Latest" on the construction queue screen.
		/// </summary>
		public string Family { get; set; }

		/// <summary>
		/// The value of the Roman numeral that should be displayed on the facility's icon.
		/// </summary>
		public int RomanNumeral { get; set; }

		/// <summary>
		/// Name of the picture used to represent this facility, excluding the file extension.
		/// PNG files will be searched first, then BMP.
		/// </summary>
		public string PictureName { get; set; }

		public Image Icon
		{
			get { return Pictures.GetIcon(this); }
		}

		public Image Portrait
		{
			get { return Pictures.GetPortrait(this); }
		}

		/// <summary>
		/// The cost to build the facility.
		/// </summary>
		public Resources Cost { get; set; }

		/// <summary>
		/// The technology requirements for this facility.
		/// </summary>
		public IList<TechnologyRequirement> TechnologyRequirements { get; private set; }

		/// <summary>
		/// Abilities possessed by this facility.
		/// </summary>
		public IList<Ability> Abilities { get; private set; }

		IEnumerable<Ability> IAbilityObject.Abilities
		{
			get { return Abilities; }
		}

		/// <summary>
		/// Facilities must be built on a colony.
		/// </summary>
		public bool RequiresColonyQueue
		{
			get { return true; }
		}

		/// <summary>
		/// Facilities do not require a space yard.
		/// </summary>
		public bool RequiresSpaceYardQueue
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>
		/// Just clones the facility.
		/// </summary>
		/// <returns></returns>
		public Facility Instantiate()
		{
			return this.Clone();
		}

		public Resources ConstructionProgress
		{
			get;
			set;
		}

		/// <summary>
		/// Places the facility.
		/// </summary>
		/// <param name="sobj">Must be a colonized planet.</param>
		public void Place(ISpaceObject sobj)
		{
			if (sobj is Planet)
			{
				var planet = (Planet)sobj;
				if (planet.Colony == null)
					throw new ArgumentException("Facilities can only be placed on colonized planets.");
				planet.Colony.Facilities.Add(this);
			}
			else
				throw new ArgumentException("Facilities can only be placed on colonized planets.");
		}
	}
}
