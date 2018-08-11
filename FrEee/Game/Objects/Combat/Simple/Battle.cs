using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat.Simple
{
	public class Battle : INamed, IPictorial, ILocated
	{
		public Battle(Sector location)
		{
			if (location == null)
				throw new Exception("Battles require a sector location.");
			Sector = location;
			Log = new List<LogMessage>();
			Empires = Sector.SpaceObjects.OfType<ICombatSpaceObject>().Select(sobj => sobj.Owner).Where(emp => emp != null).Distinct().ToArray();
			Combatants = new HashSet<ICombatant>(Sector.SpaceObjects.OfType<ICombatant>().Where(o => o.Owner != null).Union(Sector.SpaceObjects.OfType<Fleet>().SelectMany(f => f.Combatants)).Where(o => !(o is Fleet)));

			double stardate = Galaxy.Current.Timestamp;
			int moduloID = (int)(Sector.StarSystem.ID % 100000);
			Dice = new PRNG((int)(moduloID / stardate * 10));
		}

		static Battle()
		{
			Current = new HashSet<Battle>();
			Previous = new HashSet<Battle>();
		}

		public PRNG Dice { get; set; }

		/// <summary>
		/// Any battles that are currently ongoing.
		/// This is a collection so we can multithread battle resolution if so desired.
		/// </summary>
		public static ICollection<Battle> Current { get; private set; }

		/// <summary>
		/// Any battles that have completed this turn.
		/// </summary>
		public static ICollection<Battle> Previous { get; private set; }

		/// <summary>
		/// The sector in which this battle took place.
		/// </summary>
		public Sector Sector { get; set; }

		/// <summary>
		/// The star system in which this battle took place.
		/// </summary>
		public StarSystem StarSystem { get { return Sector.StarSystem; } }

		/// <summary>
		/// The empires engagaed in battle.
		/// </summary>
		public IEnumerable<Empire> Empires { get; private set; }

		/// <summary>
		/// The combatants in this battle.
		/// </summary>
		public ISet<ICombatant> Combatants { get; private set; }

		/// <summary>
		/// Battles are named after any stellar objects in their sector; failing that, they are named after the star system and sector coordinates.
		/// </summary>
		public string Name
		{
			get
			{
				if (Sector.SpaceObjects.OfType<StellarObject>().Any())
					return "Battle at " + Sector.SpaceObjects.OfType<StellarObject>().Largest();
				var coords = Sector.Coordinates;
				return "Battle at " + Sector.StarSystem + " sector (" + coords.X + ", " + coords.Y + ")";
			}
		}

		/// <summary>
		/// Resolves the battle.
		/// </summary>
		public void Resolve()
		{
			// update memories
			foreach (var sobj in StarSystem.SpaceObjects.ToArray())
				sobj.UpdateEmpireMemories();

			Current.Add(this);
			var reloads = new SafeDictionary<Component, double>();
			var seekers = new Dictionary<Seeker, int>();
			
			// let all combatants scan each other
			foreach (var c in Combatants)
				c.UpdateEmpireMemories();

			for (int i = 0; i < Mod.Current.Settings.SpaceCombatTurns; i++)
			{
				LogRound(i + 1);
				// TODO - real 2D combat mechanics
				foreach (var seeker in seekers.Keys.ToArray())
				{
					seekers[seeker]--;
					if (seekers[seeker] <= 0)
					{
						seekers.Remove(seeker);
						Log.Add(seeker.CreateLogMessage(seeker + " detonates!"));
						var minrng = seeker.LaunchingComponent.Template.WeaponMinRange;
						var maxrng = seeker.LaunchingComponent.Template.WeaponMinRange;
						var range = Dice.Next(maxrng - minrng) + minrng; // just pick a random valid range
						var shot = new Shot(seeker.LaunchingCombatant, seeker.LaunchingComponent, seeker.Target, range);
						seeker.Target.TakeDamage(new Hit(shot, seeker.Target, seeker.Damage.Evaluate(shot)));
					}
					else
						Log.Add(seeker.CreateLogMessage(seeker + " moves closer to " + seeker.Target + " (" + seekers[seeker] + " rounds to detonation)"));
				}
				foreach (var launcher in Combatants.ToArray())
				{
					// find launchable units
					var unitsToLaunch = new List<SpaceVehicle>();
					if (launcher is Planet)
					{
						// planets can launch infinite units per turn
						var p = (Planet)launcher;
						if (p.Cargo != null && p.Cargo.Units != null)
							unitsToLaunch.AddRange(p.Cargo.Units.OfType<SpaceVehicle>());
					}
					else if (launcher is ICargoTransferrer)
					{
						// ships, etc. can launch units based on abilities
						var ct = (ICargoTransferrer)launcher;
						foreach (var vt in Enum.GetValues(typeof(VehicleTypes)).Cast<VehicleTypes>().Distinct())
						{
							var rate = ct.GetAbilityValue("Launch/Recover " + vt.ToSpacedString() + "s").ToInt();
							unitsToLaunch.AddRange(ct.Cargo.Units.Where(u => u.Design.VehicleType == vt).OfType<SpaceVehicle>().Take(rate));
						}
					}

					// launch them temporarily for combat
					foreach (var unit in unitsToLaunch)
						Combatants.Add(unit);
				}
				foreach (var attacker in Combatants.Shuffle(Dice).Where(sobj => sobj.Weapons.Any()).ToArray())
				{
					if (attacker.IsDestroyed)
						continue;

					var defenders = Combatants.Where(sobj => attacker.CanTarget(sobj) && !sobj.IsDestroyed);
					if (!defenders.Any())
						continue; // no one to shoot at
					var defender = defenders.PickRandom(Dice);

					LogSalvo(attacker, defender);
					foreach (var weapon in attacker.Weapons.Where(w => w.CanTarget(defender)))
					{
						while (reloads[weapon] <= 0)
						{
							// fire
							var winfo = weapon.Template.ComponentTemplate.WeaponInfo;
							if (winfo is SeekingWeaponInfo)
							{
								// launch a seeker
								var swinfo = (SeekingWeaponInfo)winfo;
								var seeker = new Seeker(this, attacker.Owner, attacker, weapon, defender);
								seekers.Add(seeker, 20 / swinfo.SeekerSpeed);
								LogLaunch(seeker);
							}
							else
							{
								// direct fire
								var minrng = weapon.Template.WeaponMinRange;
								var maxrng = weapon.Template.WeaponMinRange;
								var range = Dice.Next(maxrng - minrng) + minrng; // just pick a random valid range
								var shot = new Shot(attacker, weapon, defender, range);
								defender.TakeDamage(new Hit(shot, defender, weapon.Template.GetWeaponDamage(range)));
							}
							// TODO - mounts that affect reload rate?
							reloads[weapon] += weapon.Template.ComponentTemplate.WeaponInfo.ReloadRate;
						}

						// reload
						reloads[weapon] -= 1;
					}
				}
			}

			// replenish combatants' shields
			foreach (var combatant in Sector.SpaceObjects.OfType<ICombatant>())
				combatant.ReplenishShields();

			// validate fleets
			foreach (var fleet in Sector.SpaceObjects.OfType<Fleet>())
				fleet.Validate();
			Current.Remove(this);
			Previous.Add(this);

			// update memories
			foreach (var sobj in StarSystem.SpaceObjects.Where(x => !x.IsMemory).ToArray())
				sobj.UpdateEmpireMemories();
		}

		public IList<LogMessage> Log { get; private set; }

		public void LogRound(int i)
		{
			Log.Add(new GenericLogMessage("Begin round " + i + "!"));
		}

		public void LogSalvo(ICombatant attacker, ICombatant defender)
		{
			Log.Add(new PictorialLogMessage<ICombatant>(attacker + " attacks " + defender + "!", attacker));
		}

		public void LogShot(Component weapon, bool hit)
		{
			Log.Add(new PictorialLogMessage<Component>("Fires " + weapon + " and " + (hit ? "hits" : "misses") + "!", weapon));
		}

		public void LogLaunch(ICombatant craft)
		{
			Log.Add(new PictorialLogMessage<ICombatant>("Launches " + craft + "!", craft));
		}

		public void LogShieldDamage(ICombatant defender, int damage)
		{
			Log.Add(new PictorialLogMessage<ICombatant>(defender + "'s shields take " + damage + " damage!", defender));
		}

		public void LogComponentDamage(Component component, int damage)
		{
			if (component.IsDestroyed)
				Log.Add(new PictorialLogMessage<Component>(component + " takes " + damage + " damage and is destroyed!", component));
			else
				Log.Add(new PictorialLogMessage<Component>(component + " takes " + damage + " damage!", component));

		}

		public void LogPopulationDamage(Race race, long casualties)
		{
			Log.Add(race.CreateLogMessage(casualties.ToUnitString(true) + " " + race + " are killed!"));
		}

		public void LogSeekerDamage(Seeker seeker, int damage)
		{
			Log.Add(seeker.CreateLogMessage(seeker + " takes " + damage + " damage!"));
		}

		public void LogTargetDeath(ICombatant defender)
		{
			Log.Add(new PictorialLogMessage<ICombatant>(defender + " is destroyed!", defender));
		}

		public System.Drawing.Image Icon
		{
			get { return Combatants.OfType<ISpaceObject>().Largest().Icon; }
		}

		public System.Drawing.Image Portrait
		{
			get { return Combatants.OfType<ISpaceObject>().Largest().Portrait; }
		}

		public IEnumerable<string> IconPaths
		{
			get
			{
				return Combatants.OfType<ISpaceObject>().Largest().IconPaths;
			}
		}

		public IEnumerable<string> PortraitPaths
		{
			get
			{
				return Combatants.OfType<ISpaceObject>().Largest().PortraitPaths;
			}
		}

		/// <summary>
		/// Battles are named after any stellar objects in their sector; failing that, they are named after the star system and sector coordinates.
		/// </summary>
		public string NameFor(Empire emp)
		{
			return ResultFor(emp).Capitalize() + " at " + Sector;
		}

		/// <summary>
		/// The result (victory/defeat/stalemate) for a given empire.
		/// If empire or its allies are not involved or no empire specified, just say "battle".
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public string ResultFor(Empire emp)
		{
			if (emp == null)
				return "battle"; // no empire specified
			if (!Combatants.Any(c => c.Owner == emp || (c.Owner?.IsAllyOf(emp, StarSystem) ?? false))) // TODO - determine prior owner of glassed planets
				return "battle"; // empire/allies not involved
			var survivors = Combatants.Where(c => !c.IsDestroyed && c.Owner != null); // glassed planets aren't destroyed but they do have null owners
			var ourSurvivors = survivors.Where(c => c.Owner == emp);
			var allySurvivors = survivors.Where(c => c.Owner.IsAllyOf(emp, StarSystem));
			var friendlySurvivors = ourSurvivors.Union(allySurvivors);
			var enemySurvivors = survivors.Where(c => c.Owner.IsEnemyOf(emp, StarSystem));
			if (friendlySurvivors.Any() && enemySurvivors.Any())
				return "stalemate";
			if (friendlySurvivors.Any())
				return "victory";
			if (enemySurvivors.Any())
				return "defeat";
			return "Pyrrhic victory"; // mutual annihilation!
		}
	}
}
