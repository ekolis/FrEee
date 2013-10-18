using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Enumerations;
using FrEee.Modding;
using FrEee.Modding.Interfaces;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A template for a facility.
	/// </summary>
	[Serializable]
	public class FacilityTemplate : INamed, IResearchable, IAbilityObject, ITemplate<Facility>, IReferrable, IConstructionTemplate
	{
		public FacilityTemplate()
		{
			Abilities = new List<Ability>();
			TechnologyRequirements = new List<TechnologyRequirement>();
			Cost = new ResourceFormula(this);
		}

		/// <summary>
		/// The name of the facility.
		/// </summary>
		public Formula<string> Name { get; set; }

		string INamed.Name
		{
			get { return Name; }
		}

		/// <summary>
		/// A description of the facility.
		/// </summary>
		public Formula<string> Description { get; set; }

		/// <summary>
		/// The group that the facility belongs to. Used for grouping on the construction queue screen.
		/// </summary>
		public Formula<string> Group { get; set; }

		/// <summary>
		/// The family that the facility belongs to. Used for "Only Latest" on the construction queue screen.
		/// </summary>
		public Formula<string> Family { get; set; }

		/// <summary>
		/// The value of the Roman numeral that should be displayed on the facility's icon.
		/// </summary>
		public Formula<int> RomanNumeral { get; set; }

		/// <summary>
		/// Name of the picture used to represent this facility, excluding the file extension.
		/// PNG files will be searched first, then BMP.
		/// </summary>
		public Formula<string> PictureName { get; set; }

		[DoNotSerialize] public Image Icon
		{
			get { return Pictures.GetIcon(this); }
		}

		[DoNotSerialize] public Image Portrait
		{
			get { return Pictures.GetPortrait(this); }
		}

		/// <summary>
		/// The cost to build the facility.
		/// </summary>
		public ResourceFormula Cost { get; set; }

		ResourceQuantity IConstructionTemplate.Cost
		{
			get
			{
				return Cost;
			}
		}

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

		public IEnumerable<Ability> UnstackedAbilities
		{
			get { return Abilities; }
		}

		/// <summary>
		/// Creates a facility from the template.
		/// </summary>
		/// <returns></returns>
		public Facility Instantiate()
		{
			return new Facility(this);
		}

		public long ID
		{
			get;
			set;
		}

		public Empire Owner
		{
			get { return null; }
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
			get { return false; }
		}

		public override string ToString()
		{
			return Name;
		}


		public string ResearchGroup
		{
			get { return "Facility"; }
		}


		public bool HasBeenUnlockedBy(Empire emp)
		{
			return emp.HasUnlocked(this);
		}

		public void Dispose()
		{
			Galaxy.Current.UnassignID(this);
		}

		/// <summary>
		/// The latest upgraded version of this component template.
		/// </summary>
		public FacilityTemplate LatestVersion
		{
			get
			{
				var ofFamily = Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(ct => ct.Family == Family);
				var withHighestRomanNumeral = ofFamily.WithMax(ct => ct.RomanNumeral);
				return withHighestRomanNumeral.Last();
			}
		}

		/// <summary>
		/// Mod objects are fully known to everyone.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			return Visibility.Scanned;
		}
	}
}
