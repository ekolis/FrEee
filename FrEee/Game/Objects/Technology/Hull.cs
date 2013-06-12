using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A vehicle hull.
	/// </summary>
	/// <typeparam name="T">The type of vehicle.</typeparam>
	[Serializable]
	public class Hull<T> : IHull<T> where T : Vehicle
	{
		public Hull()
		{
			PictureNames = new List<string>();
			TechnologyRequirements = new List<TechnologyRequirement>();
			Abilities = new List<Ability>();
			Cost = new Resources();
		}

		/// <summary>
		/// The name of the hull.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A shorter name for use in some places.
		/// </summary>
		public string ShortName { get; set; }

		/// <summary>
		/// A description of the hull.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// An abbeviation for the ship's name.
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Names of pictures from the approrpriate shipset to use for the hull.
		/// </summary>
		public IList<string> PictureNames { get; private set; }

		/// <summary>
		/// The icon for this hull.
		/// </summary>
		public Image GetIcon(string shipsetPath)
		{
			return Pictures.GetIcon(this, shipsetPath);
		}

		/// <summary>
		/// The portrait for this hull.
		/// </summary>
		public Image GetPortrait(string shipsetPath)
		{
			return Pictures.GetPortrait(this, shipsetPath);
		}

		/// <summary>
		/// The amount of space available for components.
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// The cost to build this hull.
		/// </summary>
		public Resources Cost { get; set; }

		/// <summary>
		/// The number of thrust points required to generate 1 movement point.
		/// Also known as Engines Per Move, though technically engines can generate more than 1 thrust point.
		/// </summary>
		public int Mass { get; set; }

		/// <summary>
		/// Technology requirements to build ships using this hull.
		/// </summary>
		public IList<TechnologyRequirement> TechnologyRequirements
		{
			get;
			private set;
		}

		IEnumerable<Abilities.Ability> IAbilityObject.Abilities
		{
			get { return Abilities; }
		}

		public ICollection<Ability> Abilities { get; private set; }

		/// <summary>
		/// Does this hull need a component with the Ship Bridge ability?
		/// </summary>
		public bool NeedsBridge { get; set; }

		/// <summary>
		/// Can this hull use components with the Ship Auxiliary Control ability?
		/// </summary>
		public bool CanUseAuxiliaryControl { get; set; }

		/// <summary>
		/// Required number of life support components.
		/// </summary>
		public int MinLifeSupport { get; set; }

		/// <summary>
		/// Required number of crew quarters components.
		/// </summary>
		public int MinCrewQuarters { get; set; }

		/// <summary>
		/// Maximum number of engines allowed.
		/// </summary>
		public int MaxEngines { get; set; }

		/// <summary>
		/// Minimum percentage of space required to be used for fighter-launching components.
		/// </summary>
		public int MinPercentFighterBays { get; set; }

		/// <summary>
		/// Minimum percentage of space required to be used for colonizing components.
		/// </summary>
		public int MinPercentColonyModules { get; set; }

		/// <summary>
		/// Minimum percentage of space required to be used for cargo-storage components.
		/// </summary>
		public int MinPercentCargoBays { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public VehicleTypes VehicleType
		{
			get
			{
				if (typeof(T) == typeof(Ship))
					return VehicleTypes.Ship;
				if (typeof(T) == typeof(Base))
					return VehicleTypes.Base;
				if (typeof(T) == typeof(Fighter))
					return VehicleTypes.Fighter;
				if (typeof(T) == typeof(Troop))
					return VehicleTypes.Troop;
				if (typeof(T) == typeof(Satellite))
					return VehicleTypes.Satellite;
				if (typeof(T) == typeof(Drone))
					return VehicleTypes.Drone;
				if (typeof(T) == typeof(Mine))
					return VehicleTypes.Mine;
				if (typeof(T) == typeof(WeaponPlatform))
					return VehicleTypes.WeaponPlatform;
				return VehicleTypes.Invalid;
			}
		}

		[DoNotSerialize] public Image Icon
		{
			get
			{
				return GetIcon(Empire.Current.ShipsetPath);
			}
		}

		[DoNotSerialize] public Image Portrait
		{
			get
			{
				return GetPortrait(Empire.Current.ShipsetPath);
			}
		}

		public int ID
		{
			get;
			set;
		}

		public Empire Owner
		{
			get { return null; }
		}


		public string ResearchGroup
		{
			get { return "Hull"; }
		}

		public IDesign<T> CreateDesign()
		{
			return new Design<T> { Hull = this };
		}

		public void Dispose()
		{
			Galaxy.Current.Unregister(this);
			foreach (var emp in Galaxy.Current.Empires)
				Galaxy.Current.Unregister(this, emp);
		}
	}
}
