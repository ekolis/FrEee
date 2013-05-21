using System;
using System.Collections.Generic;
using System.Drawing;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Research;
using FrEee.Utility;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A large immobile installation on a colony.
	/// Note that colonies do not actually contain facilities, since facilities store no data
	/// that is specific to a single instance of the given facility.
	/// Thus instances are not needed, and each facility can serve as a "singleton" of sorts.
	/// Instead colonies store quantities of facilities.
	/// </summary>
	 [Serializable] public class Facility : INamed, IResearchable, IAbilityObject
	{
		public Facility()
		{
			Abilities = new List<Ability>();
			TechnologyRequirements = new List<TechnologyRequirement>();
			Cost = new Resources();
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
		/// Name of the picture used to represent this stellar object, excluding the file extension.
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
	}
}
