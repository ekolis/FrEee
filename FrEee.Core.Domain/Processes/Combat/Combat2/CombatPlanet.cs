using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility;
using FrEee.Utility.Extensions;

using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;

using NewtMath.f16;
using FixMath.NET;
using FrEee.Game.Objects.Space;

namespace FrEee.Game.Objects.Combat2
{
	public class CombatPlanet : CombatControlledObject
	{
		/// <summary>
		/// use this constructor when creating a 'Start' combatPlanet.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="working"></param>
		/// <param name="battleseed"></param>
		/// <param name="OrigionalID"></param>
		/// <param name="IDPrefix"></param>
		public CombatPlanet(Planet start, Planet working, int battleseed, long OriginalID, string IDPrefix = "PLN")
			: base(start, working, new PointXd(0, 0, 0), new PointXd(0, 0, 0), battleseed, IDPrefix)
		{
			ID = OriginalID;
			// TODO - planets that can move in combat? 
			//- I don't think there's much point in allowing planets to move in combat. combat shouldnt be long enough that it would make any noticible difference. 
			cmbt_mass = Fix16.MaxValue;
			maxfowardThrust = 0;
			maxStrafeThrust = 0;
			maxRotate.Radians = 0;
			strategy = new StragegyObject_Default();
		}

		#region fields & properties

		/// <summary>
		/// The planet's state at the start of combat.
		/// </summary>
		[DoNotAssignID]
		[DoNotSerialize]
		public Planet StartPlanet { get { return (Planet)StartCombatant; } private set { StartCombatant = value; } }

		/// <summary>
		/// The current state of the planet.
		/// </summary>
		[DoNotSerialize]
		public Planet WorkingPlanet { get { return (Planet)WorkingObject; } private set { WorkingObject = value; } }

		#endregion

		#region methods and functions
		public override void renewtoStart()
		{

#if DEBUG
			Console.WriteLine("renewtoStart for CombatPlanet");
#endif
			var planet = StartPlanet.Copy();
			planet.IsMemory = true;
			if (planet.Owner != StartPlanet.Owner)
				planet.Owner.Dispose(); // don't need extra empires!
			if (planet.Colony != null)
			{
				planet.Colony.Owner = StartPlanet.Owner;

				// copy over the facilities individually so they can take damage without affecting the starting state
				planet.Colony.Facilities.Clear();
#if DEBUG
				Console.WriteLine("copying facilities");
#endif
				foreach (var f in (StartPlanet.Colony.Facilities))
				{
					var fcopy = f.Copy();
					planet.Colony.Facilities.Add(fcopy);
#if DEBUG
					Console.Write(".");
#endif
				}
#if DEBUG
				Console.WriteLine("Done");
#endif
			}

			WorkingPlanet = planet;
			RefreshWeapons();

			foreach (var w in Weapons)
				w.nextReload = 1;

			base.renewtoStart();
#if DEBUG
			Console.WriteLine("Done");
#endif
		}

		protected override void RefreshWeapons()
		{
			var weapons = new List<CombatWeapon>();
			weapons = new List<CombatWeapon>();
#if DEBUG
			Console.WriteLine("RefreshingWeapons");
#endif
			foreach (Component weapon in WorkingPlanet.Weapons)
			{
				CombatWeapon wpn = new CombatWeapon(weapon);
				weapons.Add(wpn);
#if DEBUG
				Console.Write(".");
#endif
			}
			Weapons = weapons;
#if DEBUG
			Console.WriteLine("Done");
#endif
		}


		public override void TakeSpecialDamage(Battle_Space battle, Hit hit, PRNG dice)
		{
			// find out who hit us
			var atkr = battle.FindCombatObject(hit.Shot.Attacker);

			// find out how too
			var dmgType = hit.Shot.DamageType;

			// conditions damage
			{
				var cdmg = dmgType.ConditionsDamage.Value * hit.Shot.DamageLeft / 100;
				// TODO - conditions damage, once we have conditions
			}

			// plagues
			{
				var plague = dmgType.PlagueLevel;
				// TODO - plaguing planets, once we have plagues
			}

			// population damage
			{
				var popdmg = dmgType.PopulationDamage.Value * hit.Shot.DamageLeft / 100;
				WorkingPlanet.TakePopulationDamage(popdmg, dice);
			}

			// disrupt reload
			{
				var disrupt = dmgType.DisruptReload.Value * hit.Shot.DamageLeft / 100;
				foreach (var w in Weapons)
				{
					w.nextReload += disrupt;
					if (w.nextReload - battle.CurrentTick > w.reloadRate)
						w.nextReload = battle.CurrentTick + w.reloadRate; // this damage type can't increase past normal reload time
				}
			}

			// increase reload (doesn't work againts master computers, even disabled ones)
			if (WorkingPlanet.Colony != null)
			{
				if (!WorkingPlanet.Colony.Facilities.Cast<IAbilityObject>().Union(WorkingPlanet.Cargo.Units.SelectMany(u => u.Components).Cast<IAbilityObject>()).Any(c => c.HasAbility("Master Computer")))
				{
					var inc = dmgType.IncreaseReload.Value * hit.Shot.DamageLeft / 100;
					foreach (var w in Weapons)
						w.nextReload += inc; // this damage type can increase past normal reload time
				}
			}
		}

		#endregion
	}
}
