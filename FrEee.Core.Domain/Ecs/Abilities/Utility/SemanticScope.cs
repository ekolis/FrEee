using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Ecs.Abilities.Utility
{
	/// <summary>
	/// Defines the default semantic scopes used by <see cref="SemanticScopeAbility"/>.
	/// </summary>
	public record SemanticScope
	(
		string Name
	)
	{
		/// <summary>
		/// Any entity.
		/// </summary>
		public static SemanticScope Entity { get; } = new SemanticScope("Entity");

		/// <summary>
		/// The entire game galaxy.
		/// </summary>
		public static SemanticScope Galaxy { get; } = new SemanticScope("Galaxy");

		/// <summary>
		/// An isolated region of space which can be connected to other star systems via warp points.
		/// </summary>
		// TODO: renme to SpaceRegion?
		public static SemanticScope StarSystem { get; } = new SemanticScope("Star System");

		/// <summary>
		/// A small region of space located within a star system.
		/// </summary>
		public static SemanticScope Sector { get; } = new SemanticScope("Sector");

		/// <summary>
		/// An object which can exist within a sector.
		/// </summary>
		public static SemanticScope SpaceObject { get; } = new SemanticScope("Space Object");

		/// <summary>
		/// An object which can exist in cargo of a colony or space vehicle.
		/// </summary>
		public static SemanticScope CargoItem { get; } = new SemanticScope("Cargo Item");

		/// <summary>
		/// Something whicn can be colonized by an empire.
		/// </summary>
		public static SemanticScope World { get; } = new SemanticScope("World");

		/// <summary>
		/// A development created by an empire on a world which allows the empire to develop it with population and facilities.
		/// </summary>
		public static SemanticScope Colony { get; } = new SemanticScope("Colony");

		/// <summary>
		/// A development on a colony which serves a specific purpose for that colony.
		/// </summary>
		public static SemanticScope Facility { get; } = new SemanticScope("Facility");

		/// <summary>
		/// A vehicle designed and built by an empire for a specific purpose.
		/// </summary>
		public static SemanticScope Vehicle { get; } = new SemanticScope("Vehicle");

		/// <summary>
		/// A vehicle which is also a space object.
		/// </summary>
		public static SemanticScope SpaceVehicle { get; } = new SemanticScope("Space Vehicle");

		/// <summary>
		/// A vehicle which is also a cargo item.
		/// </summary>
		public static SemanticScope DockableVehicle { get; } = new SemanticScope("Dockable Vehicle");

		/// <summary>
		/// An empty hull which can be used to design a vehicle.
		/// </summary>
		public static SemanticScope Hull { get; } = new SemanticScope("Hull");

		/// <summary>
		/// A component of a vehicle.
		/// </summary>
		public static SemanticScope Component { get; } = new SemanticScope("Component");

		/// <summary>
		/// A mount applied to a component or facility to modify its stats and/or abilities.
		/// </summary>
		public static SemanticScope Mount { get; } = new SemanticScope("Mount");

		public override string ToString() => Name;

	}
}
