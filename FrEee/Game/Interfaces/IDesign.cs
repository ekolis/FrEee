using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Templates;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A vehicle design.
	/// </summary>
	public interface IDesign : INamed, IPictorial, IAbilityObject
	{
		/// <summary>
		/// The name of the design.
		/// </summary>
		new string Name { get; set; }

		/// <summary>
		/// The empire which created this design.
		/// </summary>
		Empire Owner { get; set; }

		/// <summary>
		/// The vehicle's components.
		/// </summary>
		IList<MountedComponentTemplate> Components { get; }

		/// <summary>
		/// The vehicle type.
		/// </summary>
		VehicleTypes VehicleType { get; }

		/// <summary>
		/// The name of the vehicle type.
		/// </summary>
		string VehicleTypeName { get; }

		/// <summary>
		/// The vehicle's hull.
		/// </summary>
		IHull Hull { get; set; }

		/// <summary>
		/// The ship's role (design type in SE4).
		/// </summary>
		string Role { get; set; }

		/// <summary>
		/// The turn this design was created (for our designs) or discovered (for alien designs).
		/// </summary>
		int TurnNumber { get; set; }

		/// <summary>
		/// Is this design obsolete?
		/// Note that foreign designs will never be obsoleted, since you don't know when their owner obsoleted them.
		/// </summary>
		bool IsObsolete { get; set; }

		/// <summary>
		/// Warnings that need to be resolved before the design can be saved.
		/// </summary>
		IEnumerable<string> Warnings { get; }

		/// <summary>
		/// Unused space on the design.
		/// </summary>
		int SpaceFree { get; }

		/// <summary>
		/// The resource cost to build the design.
		/// </summary>
		Resources Cost { get; }

		/// <summary>
		/// The movement speed of the design, in sectors per turn.
		/// </summary>
		int Speed { get; }

		/// <summary>
		/// Supply used for each sector of movement.
		/// </summary>
		int SupplyUsagePerSector { get; }

		int ShieldHitpoints { get; }

		int ShieldRegeneration { get; }

		int ArmorHitpoints { get; }

		int HullHitpoints { get; }

		int Accuracy { get; }

		int Evasion { get; }

		int CargoCapacity { get; }
	}
}
