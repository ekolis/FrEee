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
using static System.Math;

namespace FrEee.Game.Objects.Combat.Grid
{
	public abstract class Battle : IBattle
	{
		static Battle()
		{
			Current = new HashSet<Battle>();
			Previous = new HashSet<Battle>();
		}

		public Battle()
		{
			Log = new List<LogMessage>();

			double stardate = Galaxy.Current.Timestamp;
			Timestamp = stardate;
		}

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
		public ISet<ICombatant> Combatants { get; protected set; }

		/// <summary>
		/// Saved-up fractional combat speed from the previous round.
		/// </summary>
		/// TODO - enable DoNotSerialize for this property after the current game
		//[DoNotSerialize(false)]
		public SafeDictionary<ICombatant, double> CombatSpeedBuffer { get; private set; } = new SafeDictionary<ICombatant, double>();

		/// <summary>
		/// The percentage of normal damage that weapons inflict.
		/// </summary>
		public abstract int DamagePercentage { get; }

		public PRNG Dice { get; set; }

		/// <summary>
		/// The number of rounds this battle lasted.
		/// </summary>
		public int Duration => Events.Count;

		/// <summary>
		/// The empires engaged in battle.
		/// </summary>
		public IEnumerable<Empire> Empires { get; protected set; }

		public List<IList<IBattleEvent>> Events
		{
			get; private set;
		}

		public System.Drawing.Image Icon
		{
			get { return Combatants.OfType<ISpaceObject>().Largest()?.Icon; }
		}

		public IEnumerable<string> IconPaths
		{
			get
			{
				return Combatants.OfType<ISpaceObject>().Largest()?.IconPaths ?? Enumerable.Empty<string>();
			}
		}

		private SafeDictionary<ICombatant, int> DistancesToTargets { get; } = new SafeDictionary<ICombatant, int>();

		private SafeDictionary<ICombatant, HashSet<ICombatant>> IgnoredTargets { get; } = new SafeDictionary<ICombatant, HashSet<ICombatant>>(true);

		public IList<LogMessage> Log { get; private set; }

		public IList<IntVector2> LowerRight { get; private set; } = new List<IntVector2>();

		public abstract int MaxRounds { get; }

		/// <summary>
		/// The name of the battle. Battles are typicaly named after their location.
		/// </summary>
		public abstract string Name { get; }

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
			get { return Combatants.OfType<ISpaceObject>().Largest()?.Portrait; }
		}

		public IEnumerable<string> PortraitPaths
		{
			get
			{
				return Combatants.OfType<ISpaceObject>().Largest()?.PortraitPaths ?? Enumerable.Empty<string>();
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

		public int GetDiameter(int round)
		{
			return UpperLeft[round].DistanceToEightWay(LowerRight[round]) + 1;
		}

		public virtual void Initialize(IEnumerable<ICombatant> combatants)
		{
			Combatants = combatants.ToHashSet();

			foreach (var c in Combatants)
			{
				OriginalHitpoints[c] = c.Hitpoints;
				OriginalOwners[c] = c.Owner;
			}
		}

		public abstract void PlaceCombatants(SafeDictionary<ICombatant, IntVector2> locations);

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

			PlaceCombatants(locations);

			Events = new List<IList<IBattleEvent>>();

			UpdateBounds(0, locations.Values);

			// let all combatants scan each other
			foreach (var c in Combatants)
				c.UpdateEmpireMemories();

			// make a query so we can check who's alive
			var alives = Combatants.Where(q => q.IsAlive);

			for (int i = 0; i < MaxRounds; i++)
			{
				var combatSpeeds = new SafeDictionary<ICombatant, double>();
				var multiplex = new SafeDictionary<ICombatant, HashSet<ICombatant>>(true);
				foreach (var c in Combatants)
					combatSpeeds[c] = c.CombatSpeed;

				int GetCombatSpeedThisRound(ICombatant c)
				{
					return (int)(combatSpeeds[c] + CombatSpeedBuffer[c]);
				}

				Events.Add(new List<IBattleEvent>());

				if (i == 0)
				{
					// first round, all combatants appear
					foreach (var c in Combatants)
						Events.Last().Add(new CombatantAppearsEvent(c, locations[c]));
				}

				var turnorder = alives.OrderBy(x => x is Seeker ? 1 : 0).ThenBy(x => combatSpeeds[x]).ThenShuffle(Dice).ToArray();

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
							Events.Last().Add(new CombatantDestroyedEvent(s, locations[s]));
							continue;
						}
						s.DistanceTraveled += Math.Min(GetCombatSpeedThisRound(c), locations[s].DistanceToEightWay(locations[s.Target]));
						locations[s] = IntVector2.InterpolateEightWay(locations[s], locations[s.Target], GetCombatSpeedThisRound(c));
						if (s.DistanceTraveled > s.WeaponInfo.MaxRange)
						{
							s.Hitpoints = 0;
							Events.Last().Add(new CombatantDestroyedEvent(s, locations[s]));
						}
					}
					else
					{
						// TODO - both pursue target and evade scary enemies at the same time using heatmap
						// find out how good each target is
						var targetiness = new SafeDictionary<ICombatant, double>();
						foreach (var target in alives.Where(x =>
							c.IsHostileTo(x.Owner) &&
							(c.CanTarget(x) || (x is Planet && c is ICargoContainer cc && cc.Cargo.Units.OfType<Troop>().Any()))))
						{
							targetiness[target] = 1d / (locations[target] - locations[c]).LengthEightWay;
						}

						if (!targetiness.Any())
						{
							// evade enemies
							var heatmap = new HeatMap();
							foreach (var e in alives.Where(x => x.IsHostileTo(c.Owner) && x.CanTarget(c)))
							{
								int threat;
								if (e.Weapons.Any())
									threat = GetCombatSpeedThisRound(e) + e.Weapons.Where(w => w.CanTarget(c)).Max(w => w.Template.WeaponMaxRange);
								else
									threat = 0;
								heatmap.AddLinearGradientEightWay(locations[e], threat, threat, -1);
							}
							locations[c] = heatmap.FindMin(locations[c], GetCombatSpeedThisRound(c));
						}
						else
						{
							// move to max range that we can inflict max damage on best target
							var goodTargets = targetiness.Where(x => !IgnoredTargets[c].Contains(x.Key)).WithMax(x => x.Value);
							ICombatant bestTarget = null;
							if (goodTargets.Any())
								bestTarget = goodTargets.First().Key;
							if (bestTarget == null)
							{
								// try previously ignored targets
								IgnoredTargets[c].Clear();
								goodTargets = targetiness.Where(x => !IgnoredTargets[c].Contains(x.Key)).WithMax(x => x.Value);
								if (goodTargets.Any())
									bestTarget = goodTargets.First().Key;
							}
							if (bestTarget != null)
							{
							gotosAreEvil:
								var maxdmg = 0;
								var maxdmgrange = 0;
								if (c.Weapons.Any())
								{
									for (var range = 0; range < c.Weapons.Max(w => w.Template.WeaponMaxRange); range++)
									{
										var dmg = c.Weapons.Where(w => w.CanTarget(bestTarget)).Sum(w => w.Template.GetWeaponDamage(range));
										if (dmg >= maxdmg)
										{
											maxdmg = dmg;
											maxdmgrange = range;
										}
									}
								}
								if (c.Weapons.Any(w => w.Template.ComponentTemplate.WeaponInfo.IsSeeker))
								{
									// adjust desired range due to seeker speed and target speed
									var roundsToClose = c.Weapons.Where(w => w.Template.ComponentTemplate.WeaponInfo.IsSeeker).Max(w =>
										(int)Math.Ceiling((double)w.Template.WeaponMaxRange / (double)(w.Template.ComponentTemplate.WeaponInfo as SeekingWeaponInfo).SeekerSpeed));
									var distanceAdjustment = (int)Ceiling(combatSpeeds[bestTarget] * roundsToClose);
									maxdmgrange -= distanceAdjustment;
								}
								var targetPos = locations[bestTarget];
								var tiles = new HashSet<IntVector2>();
								for (var x = targetPos.X - maxdmgrange; x <= targetPos.X + maxdmgrange; x++)
								{
									tiles.Add(new IntVector2(x, targetPos.Y - maxdmgrange));
									tiles.Add(new IntVector2(x, targetPos.Y + maxdmgrange));
								}
								for (var y = targetPos.Y - maxdmgrange; y <= targetPos.Y + maxdmgrange; y++)
								{
									tiles.Add(new IntVector2(targetPos.X - maxdmgrange, y));
									tiles.Add(new IntVector2(targetPos.X + maxdmgrange, y));
								}
								var closest = tiles.WithMin(t => t.DistanceToEightWay(locations[c])).First();
								locations[c] = IntVector2.InterpolateEightWay(locations[c], closest, GetCombatSpeedThisRound(c));
								var newdist = locations[c].DistanceToEightWay(locations[bestTarget]);
								if (DistancesToTargets.ContainsKey(c) && newdist >= DistancesToTargets[c] && combatSpeeds[c] <= combatSpeeds[bestTarget] && !c.Weapons.Any(w => w.Template.WeaponMaxRange >= newdist))
								{
									DistancesToTargets.Remove(c);
									IgnoredTargets[c].Add(bestTarget); // can't catch it, might as well find a new target
									goodTargets = targetiness.Where(x => !IgnoredTargets[c].Contains(x.Key)).WithMax(x => x.Value);
									bestTarget = null;
									if (goodTargets.Any())
										bestTarget = goodTargets.First().Key;
									if (bestTarget == null)
										goto gotosAreVeryEvil;
									goto gotosAreEvil;
								}
								else
									DistancesToTargets[c] = newdist;
							}
							else
								DistancesToTargets.Remove(c);
						}
					}
				gotosAreVeryEvil:
					if (locations[c] != oldpos)
						Events.Last().Add(new CombatantMovesEvent(c, oldpos, locations[c]));
				}

				UpdateBounds(i, locations.Values);

				// phase 2: combatants launch units
				foreach (var c in turnorder)
				{
					// find launchable units
					var unitsToLaunch = new List<(ICombatant Launcher, SpaceVehicle Launchee)>();
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
						locations[info.Launchee] = new IntVector2(locations[info.Launcher]);
						Events.Last().Add(new CombatantLaunchedEvent(info.Launcher, info.Launchee, locations[info.Launchee]));
					}
				}

				turnorder = alives.OrderBy(x => x.CombatSpeed).ThenShuffle(Dice).ToArray();

				// phase 3: combatants fire point defense non-warhead weapons starting with the fastest (so the faster ships get to inflict damage first and possibly KO enemies preventing them from firing back)
				foreach (var c in turnorder.Reverse())
				{
					foreach (var w in c.Weapons.Where(w => w.Template.ComponentTemplate.WeaponInfo.IsPointDefense && !w.Template.ComponentTemplate.WeaponInfo.IsWarhead))
						TryFireWeapon(c, w, reloads, locations, multiplex);
				}

				turnorder = alives.OrderBy(x => x.CombatSpeed).ThenShuffle(Dice).ToArray();

				// phase 4: point defense seekers detonate
				foreach (var s in turnorder.Reverse().OfType<Seeker>().Where(s => s.WeaponInfo.IsPointDefense))
				{
					CheckSeekerDetonation(s, locations);
				}

				turnorder = alives.OrderBy(x => x.CombatSpeed).ThenShuffle(Dice).ToArray();

				// phase 5: ships fire non-PD non-warhead weapons starting with the fastest (so the faster ships get to inflict damage first and possibly KO enemies preventing them from firing back)
				foreach (var c in turnorder.Reverse())
				{
					foreach (var w in c.Weapons.Where(w => !w.Template.ComponentTemplate.WeaponInfo.IsPointDefense && !w.Template.ComponentTemplate.WeaponInfo.IsWarhead))
						TryFireWeapon(c, w, reloads, locations, multiplex);
				}

				turnorder = alives.OrderBy(x => x.CombatSpeed).ThenShuffle(Dice).ToArray();

				// phase 6: non-PD seekers detonate
				foreach (var s in turnorder.Reverse().OfType<Seeker>().Where(s => !s.WeaponInfo.IsPointDefense))
				{
					CheckSeekerDetonation(s, locations);
				}

				turnorder = alives.OrderBy(x => x.CombatSpeed).ThenShuffle(Dice).ToArray();

				// phase 7: ramming! only activates if ship has no other weapons
				foreach (var c in turnorder.Reverse())
				{
					if (!c.Weapons.Any(w => !w.Template.ComponentTemplate.WeaponInfo.IsWarhead))
					{
						// TODO - add damage from ship HP on both sides
						foreach (var w in c.Weapons.Where(w => w.Template.ComponentTemplate.WeaponInfo.IsWarhead))
							TryFireWeapon(c, w, reloads, locations, multiplex);
					}
				}

				turnorder = alives.OrderBy(x => x.CombatSpeed).ThenShuffle(Dice).ToArray();

				// TODO - boarding

				// phase 8: drop troops
				foreach (var c in turnorder.Reverse())
				{
					if (c is ICargoTransferrer cc && cc.AllUnits.OfType<Troop>().Any())
					{
						// find enemy planets in the same square
						var dropTargets = locations.Where(q => q.Key != c && q.Value == locations[c] && q.Key is Planet p && c.IsHostileTo(p.Owner)).Select(q => q.Key).Cast<Planet>();
						var dropTarget = dropTargets.PickRandom(Dice);
						if (dropTarget != null)
						{
							var cd = new CargoDelta();
							cd.UnitTypeTonnage.Add(VehicleTypes.Troop, null);
							cc.TransferCargo(cd, dropTarget, cc.Owner, true);
							var groundBattle = new GroundBattle(dropTarget);
							groundBattle.Resolve();
						}
					}
				}

				// clear used combat speed buffer speed
				foreach (var x in Combatants)
				{
					CombatSpeedBuffer[x] += x.CombatSpeed - Floor(x.CombatSpeed);
					CombatSpeedBuffer[x] -= Floor(CombatSpeedBuffer[x]);
				}

				UpdateBounds(i, locations.Values);

				bool hostile = false;
				foreach (var a in alives)
				{
					foreach (var b in alives)
					{
						// TODO - check if ships want to ram even if they have no weapons
						if (a.IsHostileTo(b.Owner) && a.Weapons.Any())
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

			// modify happiness
			ModifyHappiness();
		}

		public abstract void ModifyHappiness();

		public override string ToString()
		{
			return Name;
		}

		private void CheckSeekerDetonation(Seeker s, SafeDictionary<ICombatant, IntVector2> locations)
		{
			if (locations[s] == locations[s.Target])
			{
				var range = s.DistanceTraveled;
				var shot = new Shot(s.LaunchingCombatant, s.LaunchingComponent, s.Target, range);
				var hit = new Hit(shot, s.Target, s.Damage.Evaluate(shot));
				bool wasArmed = s.Target is Seeker || s.Target.Weapons.Any();
				s.Target.TakeDamage(hit, Dice);
				bool isArmed = s.Target is Seeker || s.Target.Weapons.Any();
				Events.Last().Add(new CombatantsCollideEvent(s, s.Target, locations[s.Target], s.Hitpoints, hit.NominalDamage, false, wasArmed && !isArmed));
				s.Hitpoints = 0;
				Events.Last().Add(new CombatantDestroyedEvent(s, locations[s]));
				locations.Remove(s);
				if (s.Target.IsDestroyed)
				{
					var loc = locations[s.Target];
					Events.Last().Add(new CombatantDestroyedEvent(s.Target, locations[s.Target]));
					locations.Remove(s.Target);
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
					Events.Last().Add(new CombatantLaunchedEvent(c, seeker, locations[seeker]));
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
					bool wasArmed = target is Seeker || target.Weapons.Any();
					if (w.Template.ComponentTemplate.WeaponInfo.IsWarhead || shot.RollAccuracy(Dice))
					{
						dmg += shot.FullDamage;
						Hit hit;
						if (w.Template.ComponentTemplate.WeaponInfo.IsWarhead)
						{
							hit = new Hit(shot, target, w.Template.GetWeaponDamage(range) * Mod.Current.Settings.RammingSourceHitpointsDamagePercent / 100);
							// warheads have a damage modifer
							target.TakeDamage(hit);
							// warheads damage the firing ship too
							c.TakeDamage(new Hit(shot, target, w.Template.GetWeaponDamage(range) * Mod.Current.Settings.RammingTargetHitpointsDamagePercent / 100));
							// warheads destroy themselves on activation
							w.Hitpoints = 0;
						}
						else
						{
							hit = new Hit(shot, target, w.Template.GetWeaponDamage(range));
							target.TakeDamage(hit);
						}
						bool isArmed = target is Seeker || target.Weapons.Any();
						Events.Last().Add(new WeaponFiresEvent(c, locations[c], target, locations[target], w, hit, wasArmed && !isArmed));
					}
					else
					{
						bool isArmed = target is Seeker || target.Weapons.Any();
						Events.Last().Add(new WeaponFiresEvent(c, locations[c], target, locations[target], w, null, wasArmed && !isArmed));
					}
				}
				// TODO - mounts that affect reload rate?
				reloads[w] += w.Template.ComponentTemplate.WeaponInfo.ReloadRate.Evaluate(w.Template);
			}

			if (target.IsDestroyed)
			{
				Events.Last().Add(new CombatantDestroyedEvent(target, locations[target]));
				locations.Remove(target);
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
	}
}