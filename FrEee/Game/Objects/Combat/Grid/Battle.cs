﻿using FrEee.Game.Enumerations;
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
using static System.Math;

namespace FrEee.Game.Objects.Combat.Grid
{
	public class Battle : IBattle
	{
		#region Public Constructors

		static Battle()
		{
			Current = new HashSet<Battle>();
			Previous = new HashSet<Battle>();
		}

		public Battle(Sector location)
		{
			Sector = location ?? throw new Exception("Battles require a sector location.");
			Log = new List<LogMessage>();
			Empires = Sector.SpaceObjects.OfType<ICombatSpaceObject>().Select(sobj => sobj.Owner).Where(emp => emp != null).Distinct().ToArray();
			Combatants = new HashSet<ICombatant>(Sector.SpaceObjects.OfType<ICombatant>().Where(o => o.Owner != null).Union(Sector.SpaceObjects.OfType<Fleet>().SelectMany(f => f.Combatants)).Where(o => !(o is Fleet)));
			foreach (var c in Combatants)
			{
				OriginalHitpoints[c] = c.Hitpoints;
				OriginalOwners[c] = c.Owner;
			}

			double stardate = Galaxy.Current.Timestamp;
			int moduloID = (int)(Sector.StarSystem.ID % 100000);
			Dice = new PRNG((int)(moduloID / stardate * 10));
			Timestamp = stardate;
		}

		#endregion Public Constructors

		#region Public Properties

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
		/// The combatants in this battle.
		/// </summary>
		public ISet<ICombatant> Combatants { get; private set; }

		public PRNG Dice { get; set; }

		/// <summary>
		/// The number of rounds this battle lasted.
		/// </summary>
		public int Duration => Events.Count;

		/// <summary>
		/// The empires engagaed in battle.
		/// </summary>
		public IEnumerable<Empire> Empires { get; private set; }

		public List<IList<IBattleEvent>> Events
		{
			get; private set;
		}

		public System.Drawing.Image Icon
		{
			get { return Combatants.OfType<ISpaceObject>().Largest().Icon; }
		}

		public IEnumerable<string> IconPaths
		{
			get
			{
				return Combatants.OfType<ISpaceObject>().Largest().IconPaths;
			}
		}

		public IList<LogMessage> Log { get; private set; }

		public IList<IntVector2> LowerRight { get; private set; } = new List<IntVector2>();

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
		/// Starting HP of all combatants.
		/// </summary>
		public SafeDictionary<ICombatant, int> OriginalHitpoints { get; private set; } = new SafeDictionary<ICombatant, int>();

		/// <summary>
		/// Who originally owned each combatant?
		/// </summary>
		public SafeDictionary<ICombatant, Empire> OriginalOwners { get; private set; } = new SafeDictionary<ICombatant, Empire>();

		public System.Drawing.Image Portrait
		{
			get { return Combatants.OfType<ISpaceObject>().Largest().Portrait; }
		}

		public IEnumerable<string> PortraitPaths
		{
			get
			{
				return Combatants.OfType<ISpaceObject>().Largest().PortraitPaths;
			}
		}

		/// <summary>
		/// The sector in which this battle took place.
		/// </summary>
		public Sector Sector { get; set; }

		/// <summary>
		/// The star system in which this battle took place.
		/// </summary>
		public StarSystem StarSystem { get { return Sector.StarSystem; } }

		public double Timestamp { get; private set; }
		public IList<IntVector2> UpperLeft { get; private set; } = new List<IntVector2>();

		#endregion Public Properties

		#region Public Methods

		public int GetDiameter(int round)
		{
			return UpperLeft[round].DistanceToEightWay(LowerRight[round]) + 1;
		}

		/// <summary>
		/// Resolves the battle.
		/// </summary>
		public void Resolve()
		{
			// update memories
			foreach (var sobj in StarSystem.SpaceObjects.Where(x => !x.IsMemory).ToArray())
				sobj.UpdateEmpireMemories();

			Current.Add(this);

			var reloads = new SafeDictionary<Component, double>();
			var locations = new SafeDictionary<ICombatant, IntVector2>();
			var multiplex = new SafeDictionary<ICombatant, HashSet<ICombatant>>(true);

			// place all combatants at the points of a regular polygon
			var sideLength = 21; // make sure no one can shoot each other at the start
								 // https://stackoverflow.com/questions/32169875/calculating-the-coordinates-of-a-regular-polygon-given-its-center-and-its-side-l
			var radius = sideLength / (2 * Sin(PI / Empires.Count()));
			var combs = Combatants.ToArray();
			for (var i = 0; i < Empires.Count(); i++)
			{
				var x = radius * Cos(PI / Empires.Count() * (1 + 2 * i));
				var y = radius * Sin(PI / Empires.Count() * (1 + 2 * i));
				foreach (var comb in Combatants.Where(q => q.Owner == Empires.ElementAt(i)))
					locations.Add(comb, new IntVector2((int)x, (int)y));
			}

			Events = new List<IList<IBattleEvent>>();

			UpdateBounds(0, locations.Values);

			// let all combatants scan each other
			foreach (var c in Combatants)
				c.UpdateEmpireMemories();

			for (int i = 0; i < Mod.Current.Settings.SpaceCombatTurns; i++)
			{
				Events.Add(new List<IBattleEvent>());
				if (i == 0)
				{
					// first round, all combatants appear
					foreach (var c in Combatants)
					{
						Events.Last().Add(new CombatantAppearsEvent(c, locations[c]));
					}
				}

				var turnorder = Combatants.Where(x => x.IsAlive).OrderBy(x => x is Seeker ? 1 : 0).ThenBy(x => x.CombatSpeed).ThenShuffle(Dice).ToArray();

				// phase 0: reload weapons
				foreach (var w in turnorder.SelectMany(q => q.Weapons))
				{
					reloads[w]--;
					if (reloads[w] < 0)
						reloads[w] = 0;
				}

				// phase 1: combatants move starting with the slowest (so the faster ships get to react to their moves) - but seekers go last so they get a chance to hit
				foreach (var c in turnorder)
				{
					var oldpos = locations[c];
					if (c is Seeker s)
					{
						if (locations[s] == null)
							continue; // HACK - seeker is destroyed but still showing up in turn order
						if (locations[s.Target] == null)
						{
							s.Hitpoints = 0; // seekers self destruct when their target is destroyed
							Events.Last().Add(new CombatantDisappearsEvent(s));
							continue;
						}
						s.DistanceTraveled += Math.Min(s.CombatSpeed, locations[s].DistanceToEightWay(locations[s.Target]));
						locations[s] = IntVector2.InterpolateEightWay(locations[s], locations[s.Target], s.CombatSpeed);
						if (s.DistanceTraveled > s.WeaponInfo.MaxRange)
						{
							s.Hitpoints = 0;
							Events.Last().Add(new CombatantDisappearsEvent(s));
						}
					}
					else
					{
						// TODO - both pursue target and evade scary enemies at the same time using heatmap
						// find out how good each target is
						var targetiness = new SafeDictionary<ICombatant, double>();
						foreach (var target in Combatants.Where(x => c.IsHostileTo(x.Owner) && c.CanTarget(x)))
						{
							targetiness[target] = 1d / (locations[target] - locations[c]).LengthEightWay;
						}

						if (!targetiness.Any())
						{
							// evade enemies
							var heatmap = new HeatMap();
							foreach (var e in Combatants.Where(x => x.IsHostileTo(c.Owner) && x.CanTarget(c)))
							{
								int threat;
								if (e.Weapons.Any())
									threat = e.CombatSpeed + e.Weapons.Where(w => w.CanTarget(c)).Max(w => w.Template.WeaponMaxRange);
								else
									threat = 0;
								heatmap.AddLinearGradientEightWay(locations[e], threat, threat, -1);
							}
							locations[c] = heatmap.FindMin(locations[c], c.CombatSpeed);
						}
						else
						{
							// move as close as possible to the best target
							ICombatant bestTarget = targetiness.WithMax(x => x.Value).First().Key;
							if (bestTarget != null)
							{
								var targetPos = locations[bestTarget];
								locations[c] = IntVector2.InterpolateEightWay(locations[c], targetPos, c.CombatSpeed);
							}
						}
					}
					if (locations[c] != oldpos)
						Events.Last().Add(new CombatantMovesEvent(c, oldpos, locations[c]));
				}

				UpdateBounds(i, locations.Values);

				// phase 2: combatants launch units
				foreach (var c in turnorder)
				{
					// find launchable units
					var unitsToLaunch = new List<(ICombatant, SpaceVehicle)>();
					if (c is Planet)
					{
						// planets can launch infinite units per turn
						var p = (Planet)c;
						if (p.Cargo != null && p.Cargo.Units != null)
						{
							foreach (var u in p.Cargo.Units.OfType<SpaceVehicle>())
								unitsToLaunch.Add((p, u));
						}
					}
					else if (c is ICargoTransferrer)
					{
						// ships, etc. can launch units based on abilities
						var ct = (ICargoTransferrer)c;
						foreach (var vt in Enum.GetValues(typeof(VehicleTypes)).Cast<VehicleTypes>().Distinct())
						{
							var rate = ct.GetAbilityValue("Launch/Recover " + vt.ToSpacedString() + "s").ToInt();
							foreach (var u in ct.Cargo.Units.Where(u => u.Design.VehicleType == vt).OfType<SpaceVehicle>().Take(rate))
								unitsToLaunch.Add((c, u));
						}
					}

					// launch them temporarily for combat
					foreach (var info in unitsToLaunch)
					{
						Combatants.Add(info.Item2);
						locations[info.Item2] = new IntVector2(locations[info.Item1]);
						Events.Last().Add(new CombatantAppearsEvent(c, locations[c]));
					}
				}

				turnorder = Combatants.Where(x => x.IsAlive).OrderBy(x => x.CombatSpeed).ThenShuffle(Dice).ToArray();

				// phase 3: combatants fire point defense non-warhead weapons starting with the fastest (so the faster ships get to inflict damage first and possibly KO enemies preventing them from firing back)
				foreach (var c in turnorder.Reverse())
				{
					foreach (var w in c.Weapons.Where(w => w.Template.ComponentTemplate.WeaponInfo.IsPointDefense && !w.Template.ComponentTemplate.WeaponInfo.IsWarhead))
						TryFireWeapon(c, w, reloads, locations, multiplex);
				}

				turnorder = Combatants.Where(x => x.IsAlive).OrderBy(x => x.CombatSpeed).ThenShuffle(Dice).ToArray();

				// phase 4: point defense seekers detonate
				foreach (var s in turnorder.Reverse().OfType<Seeker>().Where(s => s.WeaponInfo.IsPointDefense))
				{
					CheckSeekerDetonation(s, locations);
				}

				turnorder = Combatants.Where(x => x.IsAlive).OrderBy(x => x.CombatSpeed).ThenShuffle(Dice).ToArray();

				// phase 5: ships fire non-PD non-warhead weapons starting with the fastest (so the faster ships get to inflict damage first and possibly KO enemies preventing them from firing back)
				foreach (var c in turnorder.Reverse())
				{
					foreach (var w in c.Weapons.Where(w => !w.Template.ComponentTemplate.WeaponInfo.IsPointDefense && !w.Template.ComponentTemplate.WeaponInfo.IsWarhead))
						TryFireWeapon(c, w, reloads, locations, multiplex);
				}

				turnorder = Combatants.Where(x => x.IsAlive).OrderBy(x => x.CombatSpeed).ThenShuffle(Dice).ToArray();

				// phase 6: non-PD seekers detonate
				foreach (var s in turnorder.Reverse().OfType<Seeker>().Where(s => !s.WeaponInfo.IsPointDefense))
				{
					CheckSeekerDetonation(s, locations);
				}

				turnorder = Combatants.Where(x => x.IsAlive).OrderBy(x => x.CombatSpeed).ThenShuffle(Dice).ToArray();

				// phase 7: ramming! only activates if ship has no other weapons
				foreach (var c in turnorder.Reverse())
				{
					foreach (var w in c.Weapons.Where(w => w.Template.ComponentTemplate.WeaponInfo.IsWarhead))
						TryFireWeapon(c, w, reloads, locations, multiplex);
				}

				UpdateBounds(i, locations.Values);

				var alives = Combatants.Where(q => q.IsAlive);
				bool hostile = false;
				foreach (var a in alives)
				{
					foreach (var b in alives)
					{
						if (a.IsHostileTo(b.Owner))
						{
							hostile = true;
							break;
						}
					}
					if (hostile)
						break;
				}
				if (!hostile)
					break;
			}

			// validate fleets since some ships might have died
			foreach (var fleet in Sector.SpaceObjects.OfType<Fleet>())
				fleet.Validate();

			// replenish combatants' shields
			foreach (var combatant in Sector.SpaceObjects.OfType<ICombatant>())
				combatant.ReplenishShields();

			// mark battle complete
			Current.Remove(this);
			Previous.Add(this);

			// update memories
			foreach (var sobj in Combatants.OfType<ISpaceObject>().Where(x => !x.IsMemory).ToArray())
			{
				foreach (var emp in Empires)
				{
					emp.UpdateMemory(sobj); ;
				}
			}
		}

		#endregion Public Methods

		#region Private Methods

		private void CheckSeekerDetonation(Seeker s, SafeDictionary<ICombatant, IntVector2> locations)
		{
			if (locations[s] == locations[s.Target])
			{
				var range = s.DistanceTraveled;
				var shot = new Shot(s.LaunchingCombatant, s.LaunchingComponent, s.Target, range);
				s.Target.TakeDamage(new Hit(shot, s.Target, s.Damage.Evaluate(shot)));
				s.Hitpoints = 0;
				Events.Last().Add(new CombatantDisappearsEvent(s));
				if (s.Target.IsDestroyed)
				{
					locations.Remove(s.Target);
					Events.Last().Add(new CombatantDisappearsEvent(s.Target));
				}
			}
		}

		private void TryFireWeapon(ICombatant c, Component w, SafeDictionary<Component, double> reloads, SafeDictionary<ICombatant, IntVector2> locations, SafeDictionary<ICombatant, HashSet<ICombatant>> multiplex)
		{
			// find suitable targets in range

			ICombatant target;
			if (w.Template.ComponentTemplate.WeaponInfo.IsWarhead)
			{
				// warheads only work at range zero and are unaffected by multiplex tracking limits
				target = Combatants.Where(x =>
				{
					if (!x.IsAlive)
						return false;
					if (!x.Owner.IsEnemyOf(w.Owner, StarSystem))
						return false;
					if (!w.CanTarget(x))
						return false;
					var range = locations[c].DistanceToEightWay(locations[x]);
					return range == 0;
				}).FirstOrDefault();
			}
			else
			{
				target = Combatants.Where(x =>
				{
					if (!x.IsAlive)
						return false;
					if (!x.Owner.IsEnemyOf(w.Owner, StarSystem))
						return false;
					if (!w.CanTarget(x))
						return false;
					var range = locations[c].DistanceToEightWay(locations[x]);
					return range >= w.Template.WeaponMinRange && range <= w.Template.WeaponMaxRange;
				}).FirstOrDefault(x => multiplex[c].Contains(x) || multiplex[c].Count < c.MaxTargets);
				if (target != null)
					multiplex[c].Add(target);
			}

			if (target == null)
				return;

			// fire!
			while (reloads[w] <= 0)
			{
				if (w.Template.ComponentTemplate.WeaponType == WeaponTypes.Seeking || w.Template.ComponentTemplate.WeaponType == WeaponTypes.SeekingPointDefense)
				{
					var seeker = new Seeker(this, w.Owner, c, w, target);
					Combatants.Add(seeker);
					locations[seeker] = new IntVector2(locations[c]);
					Events.Last().Add(new CombatantAppearsEvent(seeker, locations[seeker]));
				}
				else
				{
					// fire
					int dmg = 0;
					var winfo = w.Template.ComponentTemplate.WeaponInfo;
					var minrng = w.Template.WeaponMinRange;
					var maxrng = w.Template.WeaponMinRange;
					var range = locations[c].DistanceToEightWay(locations[target]);
					var shot = new Shot(c, w, target, range);
					if (w.Template.ComponentTemplate.WeaponInfo.IsWarhead || shot.RollAccuracy(Dice))
					{
						dmg += shot.FullDamage;
						if (w.Template.ComponentTemplate.WeaponInfo.IsWarhead)
						{
							// warheads have a damage modifer
							target.TakeDamage(new Hit(shot, target, w.Template.GetWeaponDamage(range) * Mod.Current.Settings.RammingSourceHitpointsDamagePercent / 100));
							// warheads damage the firing ship too
							c.TakeDamage(new Hit(shot, target, w.Template.GetWeaponDamage(range) * Mod.Current.Settings.RammingTargetHitpointsDamagePercent / 100));
							// warheads destroy themselves on activation
							w.Hitpoints = 0;
						}
						else
							target.TakeDamage(new Hit(shot, target, w.Template.GetWeaponDamage(range)));
						Events.Last().Add(new WeaponFiresEvent(c, locations[c], target, locations[target], true));
					}
					else
						Events.Last().Add(new WeaponFiresEvent(c, locations[c], target, locations[target], false));
				}
				// TODO - mounts that affect reload rate?
				reloads[w] += w.Template.ComponentTemplate.WeaponInfo.ReloadRate;
			}

			if (target.IsDestroyed)
			{
				locations.Remove(target);
				Events.Last().Add(new CombatantDisappearsEvent(target));
			}
		}

		private void UpdateBounds(int round, IEnumerable<IntVector2> positions)
		{
			while (UpperLeft.Count() <= round)
				UpperLeft.Add(new IntVector2());
			while (LowerRight.Count() <= round)
				LowerRight.Add(new IntVector2());
			UpperLeft[round].X = positions.MinOrDefault(q => q.X);
			LowerRight[round].X = positions.MaxOrDefault(q => q.X);
			UpperLeft[round].Y = positions.MinOrDefault(q => q.Y);
			LowerRight[round].Y = positions.MaxOrDefault(q => q.Y);
		}

		#endregion Private Methods
	}
}