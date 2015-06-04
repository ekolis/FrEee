using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	public interface IHull : IModObject, IResearchable, IAbilityContainer, IReferrable, IPictorial
	{
		string ShortName { get; set; }

		string Description { get; set; }

		string Code { get; set; }

		IList<string> PictureNames { get; }

		Image GetIcon(string shipsetPath);

		Image GetPortrait(string shipsetPath);

		int Size { get; set; }

		/// <summary>
		/// Thrust points required to move one sector per turn.
		/// SE4 called this "engines per move" but this was a misnomer because engines were allowed to produce more than one thrust point.
		/// </summary>
		int Mass { get; set; }

		ResourceQuantity Cost { get; set; }

		/// <summary>
		/// Does this hull need a component with the Ship Bridge ability?
		/// </summary>
		bool NeedsBridge { get; set; }

		/// <summary>
		/// Can this hull use components with the Ship Auxiliary Control ability?
		/// </summary>
		bool CanUseAuxiliaryControl { get; set; }

		/// <summary>
		/// Required number of life support components.
		/// </summary>
		int MinLifeSupport { get; set; }

		/// <summary>
		/// Required number of crew quarters components.
		/// </summary>
		int MinCrewQuarters { get; set; }

		/// <summary>
		/// Maximum number of engines allowed.
		/// </summary>
		int MaxEngines { get; set; }

		/// <summary>
		/// Minimum percentage of space required to be used for fighter-launching components.
		/// </summary>
		int MinPercentFighterBays { get; set; }

		/// <summary>
		/// Minimum percentage of space required to be used for colonizing components.
		/// </summary>
		int MinPercentColonyModules { get; set; }

		/// <summary>
		/// Minimum percentage of space required to be used for cargo-storage components.
		/// </summary>
		int MinPercentCargoBays { get; set; }

		/// <summary>
		/// The vehicle type of this hull.
		/// </summary>
		VehicleTypes VehicleType { get; }

		new string Name { get; set; }

		/// <summary>
		/// Can this hull use a mount?
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		bool CanUseMount(Mount m);

		string VehicleTypeName { get; }
	}

	/// <summary>
	/// A vehicle hull.
	/// </summary>
	public interface IHull<out T> : IHull, IReferrable, IUpgradeable<IHull<T>> where T : IVehicle
	{
		
	}
}
