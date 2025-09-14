using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Modding.Abilities;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.GameState;
using FrEee.Processes.Construction;
using FrEee.Utility;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;

namespace FrEee.Objects.Space;

/// <summary>
/// A galaxy in which the game is played.
/// </summary>
public class Galaxy
	: ICommonAbilityObject
{
	public static Galaxy? Current =>
		Game.Current?.Galaxy;

	public AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.Galaxy; }
	}

	public IEnumerable<IAbilityObject> Children
	{
		get { return StarSystemLocations.Select(l => l.Item); }
	}

	public int Height
	{
		get;
		set;
	}

	public IEnumerable<Ability> IntrinsicAbilities
	{
		// TODO - galaxy wide abilities?
		get { yield break; }
	}

	public int MaxX
	{
		get { return StarSystemLocations.MaxOrDefault(ssl => ssl.Location.X); }
	}

	public int MaxY
	{
		get { return StarSystemLocations.MaxOrDefault(ssl => ssl.Location.Y); }
	}

	public int MinX
	{
		get { return StarSystemLocations.MinOrDefault(ssl => ssl.Location.X); }
	}

	public int MinY
	{
		get { return StarSystemLocations.MinOrDefault(ssl => ssl.Location.Y); }
	}

	public IEnumerable<IAbilityObject> Parents
	{
		get
		{
			yield break;
		}
	}

	/// <summary>
	/// The locations of the star systems in the galaxy.
	/// </summary>
	public ICollection<ObjectLocation<StarSystem>> StarSystemLocations { get; private set; } = new List<ObjectLocation<StarSystem>>();

	/// <summary>
	/// The star systems in the galaxy.
	/// </summary>
	public IEnumerable<StarSystem> StarSystems => StarSystemLocations.Select(q => q.Item);

	/// <summary>
	/// Vertical space occupied by star systems.
	/// </summary>
	public int UsedHeight
	{
		get
		{
			if (!StarSystemLocations.Any())
				return 0;
			return StarSystemLocations.Max(ssl => ssl.Location.Y) - StarSystemLocations.Min(ssl => ssl.Location.Y) + 1;
		}
	}

	/// <summary>
	/// Horizontal space occuped by star systems.
	/// </summary>
	public int UsedWidth
	{
		get
		{
			if (!StarSystemLocations.Any())
				return 0;
			return StarSystemLocations.Max(ssl => ssl.Location.X) - StarSystemLocations.Min(ssl => ssl.Location.X) + 1;
		}
	}

	/// <summary>
	/// Searches for space objects matching criteria.
	/// </summary>
	/// <typeparam name="T">The type of space object.</typeparam>
	/// <param name="criteria">The criteria.</param>
	/// <returns>The matching space objects.</returns>
	public IEnumerable<T> FindSpaceObjects<T>(Func<T, bool>? criteria = null)
	{
		return StarSystemLocations.SelectMany(l => l.Item.FindSpaceObjects(criteria));
	}

	public IEnumerable<IAbilityObject> GetContainedAbilityObjects(Empire emp)
	{
		return StarSystemLocations.Select(ssl => ssl.Item).Concat(StarSystemLocations.SelectMany(ssl => ssl.Item.GetContainedAbilityObjects(emp)));
	}

	public Sector PickRandomSector(PRNG? prng = null)
	{
		return StarSystemLocations.PickRandom(prng).Item.PickRandomSector(prng);
	}

	// define a bunch of rules for how objects are linked together
	// TODO: put all this in a database

	/// <summary>
	/// All space objects that are in space.
	/// </summary>
	/// <remarks>
	/// Does not include space objects that are under construction or in cargo.
	/// </remarks>
	public IEnumerable<ISpaceObject> SpaceObjects =>
		FindSpaceObjects<ISpaceObject>();

	/// <summary>
	/// All construction queues belonging to space objects that are in space.
	/// </summary>
	public IEnumerable<IConstructionQueue> ConstructionQueues =>
		FindSpaceObjects<IConstructor>().Select(q => q.ConstructionQueue).ExceptNull();

	public IEnumerable<IConstructionOrder> ConstructionOrders =>
		ConstructionQueues.SelectMany(q => q.Orders);

	public IEnumerable<IConstructable> ObjectsUnderConstruction =>
		ConstructionOrders.Select(q => q.Item).ExceptNull();

	public IEnumerable<ICargoContainer> CargoContainers =>
		FindSpaceObjects<ICargoContainer>();
}
