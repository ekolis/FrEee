using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Ecs.Abilities.Utility
{
	/// <summary>
	/// Defines the default semantic scopes used by <see cref="SemanticScopeAbility"/>.
	/// Custom scopes can be added, but these are the defaults used by the game.
	/// </summary>
	public static class SemanticScope
	{
		/// <summary>
		/// The entire game galaxy.
		/// </summary>
		public const string Galaxy = "Galaxy";

		/// <summary>
		/// An isolated region of space which can be connected to other star systems via warp points.
		/// </summary>
		// TODO: renme to SpaceRegion?
		public const string StarSystem = "Star System";

		/// <summary>
		/// A small region of space located within a star system.
		/// </summary>
		public const string Sector = "Sector";

		/// <summary>
		/// An object which can exist within a sector.
		/// </summary>
		public const string SpaceObject = "Space Object";

		/// <summary>
		/// An object which can exist in cargo of a colony or space vehicle.
		/// </summary>
		public const string CargoItem = "Cargo Item";

		/// <summary>
		/// Something whicn can be colonized by an empire.
		/// </summary>
		public const string World = "World";

		/// <summary>
		/// A development created by an empire on a world which allows the empire to develop it with population and facilities.
		/// </summary>
		public const string Colony = "Colony";

		/// <summary>
		/// A development on a colony which serves a specific purpose for that colony.
		/// </summary>
		public const string Facility = "Facility";

		/// <summary>
		/// A vehicle designed and built by an empire for a specific purpose.
		/// </summary>
		public const string Vehicle = "Vehicle";

		/// <summary>
		/// A vehicle which is also a space object.
		/// </summary>
		public const string SpaceVehicle = "Space Vehicle";

		/// <summary>
		/// A vehicle which is also a cargo item.
		/// </summary>
		public const string DockableVehicle = "Dockable Vehicle";

		/// <summary>
		/// An empty hull which can be used to design a vehicle.
		/// </summary>
		public const string Hull = "Hull";

		/// <summary>
		/// A component of a vehicle.
		/// </summary>
		public const string Component = "Component";

		/// <summary>
		/// A mount applied to a component or facility to modify its stats and/or abilities.
		/// </summary>
		public const string Mount = "Mount";

	}
}
