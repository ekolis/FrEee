using System.Collections.Generic;
using System.Linq;
using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Modding.Abilities;
using FrEee.Modding.Loaders;
using FrEee.Modding.Templates;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.Technology;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;
using NUnit.Framework;

namespace FrEee.Objects.Vehicles;

/// <summary>
/// Tests damage to vehicles.
/// </summary>
public class DamageTest
{
	/// <summary>
	/// Number of engines to give the ship.
	/// </summary>
	private const int numEngines = 3;

	/// <summary>
	/// They're controlling the ship.
	/// </summary>
	private static Empire empire;

	/// <summary>
	/// The template for the engine component.
	/// </summary>
	private static ComponentTemplate engineTemplate;

	/// <summary>
	/// The ship that is taking damage.
	/// </summary>
	private static Ship ship;

	[OneTimeSetUp]
	public static void ClassInit()
	{
		// initialize galaxy
		new ModLoader().Load(null);
		new Game();

		// initialize empire
		empire = new Empire();
		empire.Name = "Masochists";

		// initialize engine template
		engineTemplate = new ComponentTemplate();
		engineTemplate.Name = "Gotta-Go-Fast Engine";
		engineTemplate.Abilities.Add(new Ability(engineTemplate, Mod.Current.AbilityRules.FindByName("Standard Ship Movement"), "Lets the ship go fast.", "1"));
		engineTemplate.Abilities.Add(new Ability(engineTemplate, Mod.Current.AbilityRules.FindByName("Movement Bonus"), "Lets the ship go REALLY fast.", "1"));
		engineTemplate.Abilities.Add(new Ability(engineTemplate, Mod.Current.AbilityRules.FindByName("Extra Movement Generation"), "Lets the ship go REALLY REALLY fast.", "1"));
		engineTemplate.Abilities.Add(new Ability(engineTemplate, Mod.Current.AbilityRules.FindByName("Vehicle Speed"), "Lets the ship go REALLY FREAKIN' UNGODLY LUDICROUSLY fast.", "1"));
		engineTemplate.Abilities.Add(new Ability(engineTemplate, Mod.Current.AbilityRules.FindByName("Quantum Reactor"), "Infinite supplies! Wheeee!"));
		engineTemplate.SupplyUsage = 0;
		engineTemplate.Durability = 10;

		// initialize ship's design
		var design = new Design<Ship>();
		design.BaseName = "Punching Bag";
		var hull = new Hull<Ship>();
		Mod.Current.AssignID(hull, new List<string>());
		design.Hull = hull;
		design.Hull.ThrustPerMove = 1;
		design.Owner = empire;
		for (var i = 0; i < numEngines; i++)
			design.Components.Add(new MountedComponentTemplate(design, engineTemplate));

		// TODO - account for C&C and supply requirements once those are a thing

		// initialize ship
		ship = design.Instantiate();
		ship.Owner = empire;
	}

	/// <summary>
	/// Ship speed should degrade as engines take damage.
	/// Also, ships should go the proper speed when undamaged.
	/// </summary>
	[Test]
	public void EngineDamage()
	{
		// sanity check
		Assert.AreNotEqual(0, GetExpectedSpeed(ship));

		Assert.AreEqual(GetExpectedSpeed(ship), ship.StrategicSpeed);

		for (var i = 0; i < numEngines; i++)
		{
			// ouchies!
			ship.Components.Where(c => c.Template.ComponentTemplate == engineTemplate && c.Hitpoints > 0).First().Hitpoints = 0;

			Assert.AreEqual(GetExpectedSpeed(ship), ship.StrategicSpeed);
		}
	}

	private int GetExpectedSpeed(Ship ship)
	{
		// add up thrust of all working engines, and divide by hull mass (engines per move, not tonnage)
		// HACK - assumes standard ability rules!
		// TODO - worry about supplies
		var thrust = ship.Components.Where(c => c.Hitpoints > 0).Sum(c => c.GetAbilityValue("Standard Ship Movement").ToInt());
		if (thrust < ship.Hull.ThrustPerMove)
			return 0;
		return thrust / ship.Hull.ThrustPerMove
			+ ship.Components.Where(c => c.Hitpoints > 0).MaxOrDefault(c => c.GetAbilityValue("Movement Bonus").ToInt())
			+ ship.Components.Where(c => c.Hitpoints > 0).MaxOrDefault(c => c.GetAbilityValue("Extra Movement Generation").ToInt())
			+ ship.Components.Where(c => c.Hitpoints > 0).MaxOrDefault(c => c.GetAbilityValue("Vehicle Speed").ToInt());
	}
}
