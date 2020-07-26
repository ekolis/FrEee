using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Templates;
using FrEee.Utility;
using System.Collections.Generic;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A vehicle design.
	/// </summary>
	public interface IDesign : INamed, IPictorial, IOwnableAbilityObject, IConstructionTemplate, IPromotable, IFoggable, IUpgradeable<IDesign>, ICleanable
	{
		int Accuracy { get; }

		int ArmorHitpoints { get; }

		/// <summary>
		/// The base name of the design, without the iteration number.
		/// </summary>
		string BaseName { get; set; }

		int CargoCapacity { get; }

		int CargoStorage { get; }

		/// <summary>
		/// The movement speed of the design in combat.
		/// </summary>
		int CombatSpeed { get; }

		/// <summary>
		/// The vehicle's components.
		/// </summary>
		IList<MountedComponentTemplate> Components { get; }

		int Evasion { get; }

		/// <summary>
		/// The vehicle's hull.
		/// </summary>
		IHull Hull { get; set; }

		int HullHitpoints { get; }

		/// <summary>
		/// Is this a newly created design on the client side that needs to be sent to the server?
		/// </summary>
		bool IsNew { get; set; }

		/// <summary>
		/// Is this design obsolete?
		/// Note that foreign designs will never be obsoleted, since you don't know when their owner obsoleted them.
		/// </summary>
		new bool IsObsolete { get; set; }

		/// <summary>
		/// Is this design valid in the current mod? Or is it using techs from other mods?
		/// </summary>
		bool IsValidInMod { get; }

		int Iteration { get; set; }

		ResourceQuantity MaintenanceCost { get; }

		/// <summary>
		/// The name of the design.
		/// </summary>
		new string Name { get; }

		/// <summary>
		/// The empire which created this design.
		/// </summary>
		new Empire Owner { get; set; }

		/// <summary>
		/// The ship's role (design type in SE4).
		/// </summary>
		string Role { get; set; }

		int ShieldHitpoints { get; }

		int ShieldRegeneration { get; }

		/// <summary>
		/// Unused space on the design.
		/// </summary>
		int SpaceFree { get; }

		/// <summary>
		/// The movement speed of the design, in sectors per turn.
		/// </summary>
		int StrategicSpeed { get; }

		int SupplyStorage { get; }

		/// <summary>
		/// Supply used for each sector of movement.
		/// </summary>
		int SupplyUsagePerSector { get; }

		/// <summary>
		/// The turn this design was created (for our designs) or discovered (for alien designs).
		/// </summary>
		int TurnNumber { get; set; }

		int VehiclesBuilt { get; set; }

		/// <summary>
		/// The vehicle type.
		/// </summary>
		VehicleTypes VehicleType { get; }

		/// <summary>
		/// The name of the vehicle type.
		/// </summary>
		string VehicleTypeName { get; }

		/// <summary>
		/// Warnings that need to be resolved before the design can be saved.
		/// </summary>
		IEnumerable<string> Warnings { get; }

		void AddComponent(ComponentTemplate ct, Mount m = null);

		/// <summary>
		/// Creates an order to build this design.
		/// </summary>
		/// <returns></returns>
		IConstructionOrder CreateConstructionOrder(ConstructionQueue queue);

		/// <summary>
		/// Creates a command to create this design on the server.
		/// </summary>
		/// <returns></returns>
		ICreateDesignCommand CreateCreationCommand();

		IVehicle Instantiate();
	}

	public interface IDesign<out T> : IDesign, IPictorial, IReferrable, IUpgradeable<IDesign<T>> where T : IVehicle
	{
		new T Instantiate();
	}
}
