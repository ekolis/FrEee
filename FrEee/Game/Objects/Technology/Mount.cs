using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A mount that can be applied to a component.
	/// </summary>
	public class Mount : IResearchable
	{
		public Mount()
		{
			AbilityPercentages = new Dictionary<string, IDictionary<int, int>>();
			AbilityModifiers = new Dictionary<string, IDictionary<int, int>>();
			TechnologyRequirements = new List<TechnologyRequirement>();
		}

		/// <summary>
		/// No one owns mounts; they are shared.
		/// </summary>
		public Empire Owner { get { return null; } }

		/// <summary>
		/// The full name of this mount.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A shorter name for this mount.
		/// </summary>
		public string ShortName { get; set; }

		/// <summary>
		/// A description of this mount.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// An abbreviation for this mount.
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// The name of the picture to use for this mount.
		/// </summary>
		public string PictureName { get; set; }

		/// <summary>
		/// Percentage of normal component cost.
		/// </summary>
		public int CostPercent { get; set; }

		/// <summary>
		/// Percentage of normal component size.
		/// </summary>
		public int SizePercent { get; set; }

		/// <summary>
		/// Percentage of normal component hitpoints.
		/// </summary>
		public int DurabilityPercent { get; set; }

		/// <summary>
		/// Percentage of normal weapon damage.
		/// </summary>
		public int WeaponDamagePercent { get; set; }

		/// <summary>
		/// Percentage of normal component supply usage.
		/// </summary>
		public int SupplyUsagePercent { get; set; }

		/// <summary>
		/// Range modifier for weapons.
		/// </summary>
		public int WeaponRangeModifier { get; set; }

		/// <summary>
		/// Accuracy modifier for weapons.
		/// </summary>
		public int WeaponAccuracyModifier { get; set; }

		/// <summary>
		/// Minimum vehicle size to use this mount. Null means no restriction.
		/// </summary>
		public int? MinimumVehicleSize { get; set; }

		/// <summary>
		/// Maximum vehicle size to use this mount. Null means no restriction.
		/// </summary>
		public int? MaximumVehicleSize { get; set; }

		/// <summary>
		/// Required component family. Null means no restriction.
		/// </summary>
		public string RequiredComponentFamily { get; set; }

		/// <summary>
		/// Weapon types which can use this mount.
		/// </summary>
		public WeaponTypes WeaponTypes { get; set; }

		/// <summary>
		/// Vehicle types which can use this mount.
		/// </summary>
		public VehicleTypes VehicleTypes { get; set; }

		/// <summary>
		/// Percentage factors for abilities.
		/// </summary>
		public IDictionary<string, IDictionary<int, int>> AbilityPercentages
		{
			get;
			set;
		}

		/// <summary>
		/// Additive modifiers for abilities.
		/// </summary>
		public IDictionary<string, IDictionary<int, int>> AbilityModifiers
		{
			get;
			set;
		}

		/// <summary>
		/// Technology requirements to use this mount.
		/// </summary>
		public IList<TechnologyRequirement> TechnologyRequirements
		{
			get;
			set;
		}

		public string ResearchGroup
		{
			get { return "Mount"; }
		}

		public System.Drawing.Image Icon
		{
			get { return Pictures.GetIcon(this); }
		}

		public System.Drawing.Image Portrait
		{
			get { return Pictures.GetPortrait(this); }
		}

		public void Dispose()
		{
			Galaxy.Current.UnassignID(this);
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

		public long ID { get; set; }

		public bool IsModObject
		{
			get { return true; }
		}
	}
}
