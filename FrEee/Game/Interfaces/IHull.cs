using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using System.Collections.Generic;
using System.Drawing;

namespace FrEee.Game.Interfaces
{
	public interface IHull : IModObject, IResearchable, IAbilityContainer, IPictorial, IUpgradeable<IHull>
	{
		/// <summary>
		/// Can this hull use components with the Ship Auxiliary Control ability?
		/// </summary>
		bool CanUseAuxiliaryControl { get; set; }

		string Code { get; set; }
		ResourceQuantity Cost { get; set; }
		string Description { get; set; }

		/// <summary>
		/// Maximum number of engines allowed.
		/// </summary>
		int MaxEngines { get; set; }

		/// <summary>
		/// Required number of crew quarters components.
		/// </summary>
		int MinCrewQuarters { get; set; }

		/// <summary>
		/// Required number of life support components.
		/// </summary>
		int MinLifeSupport { get; set; }

		/// <summary>
		/// Minimum percentage of space required to be used for cargo-storage components.
		/// </summary>
		int MinPercentCargoBays { get; set; }

		/// <summary>
		/// Minimum percentage of space required to be used for colonizing components.
		/// </summary>
		int MinPercentColonyModules { get; set; }

		/// <summary>
		/// Minimum percentage of space required to be used for fighter-launching components.
		/// </summary>
		int MinPercentFighterBays { get; set; }

		new string Name { get; set; }

		/// <summary>
		/// Does this hull need a component with the Ship Bridge ability?
		/// </summary>
		bool NeedsBridge { get; set; }

		IList<string> PictureNames { get; }

		string ShortName { get; set; }

		int Size { get; set; }

		/// <summary>
		/// Thrust points required to move one sector per turn.
		/// SE4 called this "engines per move" but this was a misnomer because engines were allowed to produce more than one thrust point.
		/// </summary>
		int ThrustPerMove { get; set; }

		/// <summary>
		/// The vehicle type of this hull.
		/// </summary>
		VehicleTypes VehicleType { get; }

		string VehicleTypeName { get; }

		/// <summary>
		/// Can this hull use a mount?
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		bool CanUseMount(Mount m);

		Image GetIcon(string shipsetPath);

		Image GetPortrait(string shipsetPath);
	}

	/// <summary>
	/// A vehicle hull.
	/// </summary>
	public interface IHull<out T> : IHull, IReferrable, IUpgradeable<IHull<T>> where T : IVehicle
	{
		new bool IsObsolescent { get; }

		// to deal with ambiguity between implementing IHull and IHull<T>
		new bool IsObsolete { get; }

		new IHull<T> LatestVersion { get; }
		new IEnumerable<IHull<T>> NewerVersions { get; }
		new IEnumerable<IHull<T>> OlderVersions { get; }
	}
}