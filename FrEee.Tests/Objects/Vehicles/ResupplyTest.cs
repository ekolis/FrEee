using System.Drawing;
using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Modding.Abilities;
using FrEee.Modding.Loaders;
using FrEee.Modding.Templates;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using NUnit.Framework;

namespace FrEee.Objects.Vehicles;

/// <summary>
/// Tests resupply of vehicles.
/// </summary>
public class ResupplyTest
{
	private static Empire empire;
	private static Fleet fleet;
	private static Ship ship1, ship2;
	private static ComponentTemplate storageComp;
	private static int supplyPerComp;
	private static StarSystem sys;

	[OneTimeSetUp]
	public static void ClassInit()
	{
		// initialize galaxy
		new Game();
		new ModLoader().Load(null);
		sys = new StarSystem(0);
		Game.Current.StarSystemLocations.Add(new ObjectLocation<StarSystem>(sys, new Point()));

		// initialize empires
		empire = new Empire();
		empire.Name = "Engi";

		// initialize components
		storageComp = Mod.Current.ComponentTemplates.FindByName("Supply Storage I");
		supplyPerComp = storageComp.GetAbilityValue("Supply Storage").ToInt();

		// initialize ships
		Assert.IsNotNull(Mod.Current);
		var dsn1 = new Design<Ship>();
		dsn1.BaseName = "Shippy McShipface";
		dsn1.CreateHull();
		dsn1.Components.Add(new MountedComponentTemplate(dsn1, storageComp));
		dsn1.Owner = empire;
		ship1 = dsn1.Instantiate();
		ship1.Owner = empire;
		var dsn2 = new Design<Ship>();
		dsn2.BaseName = "Shippy McShipface Mk2";
		dsn2.CreateHull();
		dsn2.Components.Add(new MountedComponentTemplate(dsn1, storageComp));
		dsn2.Components.Add(new MountedComponentTemplate(dsn1, storageComp));
		dsn2.Components.Add(new MountedComponentTemplate(dsn1, storageComp));
		dsn2.Owner = empire;
		ship2 = dsn2.Instantiate();
		ship2.Owner = empire;

		// initialize fleet
		fleet = new Fleet();
		fleet.Owner = empire;
		fleet.Vehicles.Add(ship1);
		fleet.Vehicles.Add(ship2);

		// place ships
		sys.Place(fleet, new Point());
	}

	/// <summary>
	/// Can ships in fleets resupply each other?
	/// </summary>
	[Test]
	public void FleetResupply()
	{
		ship1.SupplyRemaining = supplyPerComp;
		ship2.SupplyRemaining = supplyPerComp;
		fleet.ShareSupplies();
		Assert.AreEqual(supplyPerComp * 2 / 4, ship1.SupplyRemaining);
		Assert.AreEqual(supplyPerComp * 6 / 4, ship2.SupplyRemaining);
	}

	// TODO - test quantum reactors

	// TODO - test resupply depots

	// TODO - test system wide resupply
}
