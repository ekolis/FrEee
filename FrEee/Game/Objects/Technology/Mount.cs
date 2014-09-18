using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
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
	public class Mount : IResearchable, IModObject
	{
		public Mount()
		{
			AbilityPercentages = new Dictionary<AbilityRule, IDictionary<int, Formula<int>>>();
			AbilityModifiers = new Dictionary<AbilityRule, IDictionary<int, Formula<int>>>();
			UnlockRequirements = new List<Requirement<Empire>>();
		}

		/// <summary>
		/// No one owns mounts; they are shared.
		/// </summary>
		public Empire Owner { get { return null; } }

		/// <summary>
		/// The full name of this mount.
		/// </summary>
		public Formula<string> Name { get; set; }

		/// <summary>
		/// A shorter name for this mount.
		/// </summary>
		public Formula<string> ShortName { get; set; }

		/// <summary>
		/// A description of this mount.
		/// </summary>
		public Formula<string> Description { get; set; }

		/// <summary>
		/// An abbreviation for this mount.
		/// </summary>
		public Formula<string> Code { get; set; }

		/// <summary>
		/// The name of the picture to use for this mount.
		/// </summary>
		public Formula<string> PictureName { get; set; }

		/// <summary>
		/// Percentage of normal component cost.
		/// </summary>
		public Formula<int> CostPercent { get; set; }

		/// <summary>
		/// Percentage of normal component size.
		/// </summary>
		public Formula<int> SizePercent { get; set; }

		/// <summary>
		/// Percentage of normal component hitpoints.
		/// </summary>
		public Formula<int> DurabilityPercent { get; set; }

		/// <summary>
		/// Percentage of normal weapon damage.
		/// </summary>
		public Formula<int> WeaponDamagePercent { get; set; }

		/// <summary>
		/// Percentage of normal component supply usage.
		/// </summary>
		public Formula<int> SupplyUsagePercent { get; set; }

		/// <summary>
		/// Range modifier for weapons.
		/// </summary>
		public Formula<int> WeaponRangeModifier { get; set; }

		/// <summary>
		/// Accuracy modifier for weapons.
		/// </summary>
		public Formula<int> WeaponAccuracyModifier { get; set; }

		/// <summary>
		/// Minimum vehicle size to use this mount. Null means no restriction.
		/// </summary>
		public Formula<int> MinimumVehicleSize { get; set; }

		/// <summary>
		/// Maximum vehicle size to use this mount. Null means no restriction.
		/// </summary>
		public Formula<int> MaximumVehicleSize { get; set; }

		/// <summary>
		/// Required component family. Null means no restriction.
		/// </summary>
		public Formula<string> RequiredComponentFamily { get; set; }

		/// <summary>
		/// Weapon types which can use this mount.
		/// </summary>
		public Formula<WeaponTypes> WeaponTypes { get; set; }

		/// <summary>
		/// Vehicle types which can use this mount.
		/// </summary>
		public VehicleTypes VehicleTypes { get; set; }

		/// <summary>
		/// Percentage factors for abilities.
		/// </summary>
		public IDictionary<AbilityRule, IDictionary<int, Formula<int>>> AbilityPercentages
		{
			get;
			set;
		}

		/// <summary>
		/// Additive modifiers for abilities.
		/// </summary>
		public IDictionary<AbilityRule, IDictionary<int, Formula<int>>> AbilityModifiers
		{
			get;
			set;
		}

		/// <summary>
		/// Requirements to unlock this mount.
		/// </summary>
		public IList<Requirement<Empire>> UnlockRequirements
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
			if (IsDisposed)
				return;
			Galaxy.Current.UnassignID(this);
			if (Mod.Current != null)
				Mod.Current.Mounts.Remove(this);
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

		string INamed.Name
		{
			get { return Name; }
		}

		public void Redact(Empire emp)
		{
			// TODO - tech items that aren't visible until some requirements are met
		}

		public bool IsMemory
		{
			get;
			set;
		}

		public double Timestamp
		{
			get;
			set;
		}

		public bool IsObsoleteMemory(Empire emp)
		{
			return false;
		}

		public string ModID { get; set; }

		public bool IsDisposed { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
