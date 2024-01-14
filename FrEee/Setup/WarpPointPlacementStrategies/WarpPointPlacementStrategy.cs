using FrEee.Interfaces;
using FrEee.Objects.Abilities;
using FrEee.Objects.Space;
using FrEee.Modding;
using FrEee.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Setup.WarpPointPlacementStrategies;

/// <summary>
/// Strategy for placing warp points in a star system.
/// </summary>
public abstract class WarpPointPlacementStrategy
{
	static WarpPointPlacementStrategy()
	{
		all.Add(EdgeAlignedWarpPointPlacementStrategy.Instance);
		all.Add(RandomAlignedWarpPointPlacementStrategy.Instance);
		all.Add(RandomWarpPointPlacementStrategy.Instance);
		all.Add(CenterWarpPointPlacementStrategy.Instance);
		all.Add(StarWarpPointPlacementStrategy.Instance);
		all.Add(PlanetWarpPointPlacementStrategy.Instance);
	}

	protected WarpPointPlacementStrategy(string name, string description)
	{
		Name = name;
		Description = description;
	}

	public static IEnumerable<WarpPointPlacementStrategy> All { get { return all; } }
	public string Description { get; set; }
	public string Name { get; set; }
	private static ICollection<WarpPointPlacementStrategy> all = new List<WarpPointPlacementStrategy>();

	/// <summary>
	/// Gets the sector that a warp point should be placed in.
	/// </summary>
	/// <param name="here"></param>
	/// <param name="there"></param>
	/// <returns></returns>
	public abstract Sector GetWarpPointSector(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there);

	public void PlaceWarpPoints(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there)
	{
		var abil1 = here.Item.WarpPointAbilities.Instantiate();
		var abil2 = there.Item.WarpPointAbilities.Instantiate();
		ITemplate<WarpPoint> wpTemplate;
		if (abil1 != null || abil2 != null)
		{
			// use unusual warp point templates
			wpTemplate = Mod.Current.StellarObjectTemplates.OfType<WarpPoint>().Where(wp => wp.IsUnusual).PickRandom();
		}
		else
		{
			// use normal warp point templates
			wpTemplate = Mod.Current.StellarObjectTemplates.OfType<WarpPoint>().Where(wp => !wp.IsUnusual).PickRandom();
		}

		Sector sector1, sector2;
		// HACK - for sectors with null systems
		int retries = 0;
		do
		{
			sector1 = GetWarpPointSector(here, there);
			retries++;
		} while (sector1.StarSystem == null && retries < 10);
		do
		{
			sector2 = GetWarpPointSector(there, here);
			retries++;
		} while (sector2.StarSystem == null && retries < 10);

		var wp1 = wpTemplate.Instantiate();
		wp1.IsOneWay = false;
		wp1.Name = "Warp Point to " + there.Item;
		wp1.Target = sector2;
		sector1.Place(wp1);
		var wp2 = wpTemplate.Instantiate();
		wp2.IsOneWay = false;
		wp2.Name = "Warp Point to " + here.Item;
		wp2.Target = sector1;
		sector2.Place(wp2);
		Ability abil = null;
		if (abil1 != null && abil2 != null)
		{
			var abils = new Ability[] { abil1, abil2 };
			abil = abils.PickRandom();
		}
		else if (abil1 != null)
			abil = abil1;
		else if (abil2 != null)
			abil = abil2;
		if (abil != null)
		{
			wp1.IntrinsicAbilities.Add(abil);
			wp2.IntrinsicAbilities.Add(abil);
		}
	}
}