using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace FrEee.Tests.Game.Objects.Vehicles
{
	/// <summary>
	/// Tests cloaking of vehicles.
	/// </summary>
	[TestClass]
	public class CloakingTest
	{
		#region variables
		/// <summary>
		/// The ship that is looking for an enemy ship.
		/// </summary>
		Ship destroyer;

		/// <summary>
		/// They're controlling the destroyer.
		/// </summary>
		Empire seekers;

		/// <summary>
		/// The ship that is hiding.
		/// </summary>
		Ship submarine;

		/// <summary>
		/// They're controlling the submarine.
		/// </summary>
		Empire hiders;

		/// <summary>
		/// Where the ships are.
		/// </summary>
		StarSystem sys;
		#endregion

		[TestInitialize]
		public void Setup()
		{
			// initialize galaxy
			Mod.Load(null);
			new Galaxy();
			sys = new StarSystem(0);
			Galaxy.Current.StarSystemLocations.Add(new ObjectLocation<StarSystem>(sys, new Point()));

			// initialize empires
			seekers = new Empire();
			seekers.Name = "Seekers";
			hiders = new Empire();
			hiders.Name = "Hiders";

			// initialize ships
			var dsDesign = new Design<Ship>();
			dsDesign.BaseName = "Destroyer";
			dsDesign.Hull = new Hull<Ship>();
			dsDesign.Owner = seekers;
			destroyer = dsDesign.Instantiate();
			destroyer.Owner = seekers;
			var subDesign = new Design<Ship>();
			subDesign.BaseName = "Submarine";
			subDesign.Hull = new Hull<Ship>();
			subDesign.Owner = hiders;
			submarine = subDesign.Instantiate();
			submarine.Owner = hiders;

			// place ships
			sys.Place(destroyer, new Point());
			sys.Place(submarine, new Point());
		}

		/// <summary>
		/// If we have no cloaks, and they have no sensors, they should still be able to see us.
		/// </summary>
		[TestMethod]
		public void NoSensorNoCloakCanSee()
		{
			// by default the ships will have no abilities, so let's just test
			Assert.IsFalse(submarine.IsHiddenFrom(seekers), "Submarine should be visible.");
		}

		/// <summary>
		/// If we have a cloak, and they have no sensors, they should not be able to see us.
		/// </summary>
		[TestMethod]
		public void NoSensorAnyCloakCantSee()
		{
			AddCloakAbility(submarine.Hull, "Foobar", 1);
			Assert.IsTrue(submarine.IsHiddenFrom(seekers), "Submarine should be hidden.");
		}

		/// <summary>
		/// If we have no cloaks, and they have a sensor, they should still be able to see us.
		/// </summary>
		[TestMethod]
		public void AnySensorNoCloakCanSee()
		{
			AddSensorAbility(destroyer.Hull, "Foobar", 1);
			Assert.IsFalse(submarine.IsHiddenFrom(seekers), "Submarine should be visible.");
		}

		/// <summary>
		/// If we have a cloak, and they have a lower level sensor of the same type, we should be hidden.
		/// </summary>
		[TestMethod]
		public void LowLevelSensorCantSee()
		{
			AddSensorAbility(destroyer.Hull, "Foobar", 1);
			AddCloakAbility(submarine.Hull, "Foobar", 2);
			Assert.IsTrue(submarine.IsHiddenFrom(seekers), "Submarine should be hidden.");
		}

		/// <summary>
		/// If we have a cloak, and they have the same level sensor of the same type, they should be able to see us.
		/// </summary>
		[TestMethod]
		public void SameLevelSensorCanSee()
		{
			AddSensorAbility(destroyer.Hull, "Foobar", 1);
			AddCloakAbility(submarine.Hull, "Foobar", 1);
			Assert.IsFalse(submarine.IsHiddenFrom(seekers), "Submarine should be visible.");
		}

		/// <summary>
		/// If we have a cloak, and they have a higher level sensor of the same type, they should be able to see us.
		/// </summary>
		[TestMethod]
		public void HighLevelSensorCanSee()
		{
			AddSensorAbility(destroyer.Hull, "Foobar", 2);
			AddCloakAbility(submarine.Hull, "Foobar", 1);
			Assert.IsFalse(submarine.IsHiddenFrom(seekers), "Submarine should be visible.");
		}

		/// <summary>
		/// If we have a cloak, and they have a different type of sensor that we don't have a cloak in, they should be able to see us.
		/// </summary>
		[TestMethod]
		public void MismatchedSensorTypeCanSee()
		{
			AddSensorAbility(destroyer.Hull, "Narf", 1);
			AddCloakAbility(submarine.Hull, "Foobar", 1);
			Assert.IsFalse(submarine.IsHiddenFrom(seekers), "Submarine should be visible.");
		}

		#region helper methods
		private void AddCloakAbility(IHull hull, string sightType, int level)
		{
			var a = new Ability(hull);
			hull.Abilities.Add(a);
			a.Rule = Mod.Current.AbilityRules.FindByName("Cloak Level");
			a.Values.Add(sightType);
			a.Values.Add(level.ToString());
		}

		private void AddSensorAbility(IHull hull, string sightType, int level)
		{
			var a = new Ability(hull);
			hull.Abilities.Add(a);
			a.Rule = Mod.Current.AbilityRules.FindByName("Sensor Level");
			a.Values.Add(sightType);
			a.Values.Add(level.ToString());
		}
		#endregion
	}
}
