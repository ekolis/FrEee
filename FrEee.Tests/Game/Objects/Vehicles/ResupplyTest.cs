using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Linq;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using System.Drawing;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Interfaces;
using FrEee.Modding;
using FrEee.Modding.Templates;

namespace FrEee.Tests.Game.Objects.Vehicles
{
	/// <summary>
	/// Tests resupply of vehicles.
	/// </summary>
	[TestClass]
	public class ResupplyTest
	{
		#region variables
		Ship ship1, ship2;
		Fleet fleet;
		Empire empire;
		StarSystem sys;
		ComponentTemplate storageComp;
		int supplyPerComp;
		#endregion

		[TestInitialize]
		public void Setup()
		{
			// initialize galaxy
			new Galaxy();
			Mod.Load(null);
			sys = new StarSystem(0);
			Galaxy.Current.StarSystemLocations.Add(new ObjectLocation<StarSystem>(sys, new Point()));

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
			dsn1.Components.Add(new MountedComponentTemplate(dsn1, storageComp));
			dsn1.Owner = empire;
			ship1 = dsn1.Instantiate();
			ship1.Owner = empire;
			var dsn2 = new Design<Ship>();
			dsn2.BaseName = "Shippy McShipface Mk2";
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
		[TestMethod]
		public void FleetResupply()
		{
			ship1.SupplyRemaining = supplyPerComp;
			ship2.SupplyRemaining = supplyPerComp;
			fleet.ShareSupplies();
			AreEqual(supplyPerComp / 4 , ship1.SupplyRemaining);
			AreEqual(supplyPerComp * 3 / 4, ship2.SupplyRemaining);
		}

		// TODO - test quantum reactors

		// TODO - test resupply depots

		// TODO - test system wide resupply
	}
}
