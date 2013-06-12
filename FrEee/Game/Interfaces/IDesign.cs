using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
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
	[ClientSafe]
	public interface IDesign : INamed, IPictorial, IAbilityObject, IConstructionTemplate, IOwnable
	{
		/// <summary>
		/// The name of the design.
		/// </summary>
		new string Name { get; set; }

		/// <summary>
		/// The empire which created this design.
		/// </summary>
		new Empire Owner { get; set; }

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

		/// <summary>
		/// Creates a command to create this design on the server.
		/// </summary>
		/// <returns></returns>
		ICommand<Empire> CreateCreationCommand();

		/// <summary>
		/// Creates an order to build this design.
		/// </summary>
		/// <returns></returns>
		IConstructionOrder CreateConstructionOrder(ConstructionQueue queue);

		int VehiclesBuilt { get; set; }

		int SupplyStorage { get; }

		int CargoStorage { get; }
	}

	public interface IDesign<T> : IDesign, IPictorial, IReferrable where T : IVehicle
	{
	}
}
