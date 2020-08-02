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
using System.Drawing;
using System.Linq;
using static System.Math;

#nullable enable

namespace FrEee.Game.Objects.Combat.Grid
{
	public abstract class Battle : IBattle, IDisposable
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
		public ISet<ICombatant>? Combatants { get; protected set; }

		/// <summary>
		/// Copies of the combatants from the start of the battle.
		/// </summary>
		public IDictionary<long, ICombatant> StartCombatants { get; private set; } = new SafeDictionary<long, ICombatant>();

		/// <summary>
		/// Copies of the combatants from the end of the battle.
		/// </summary>
		public IDictionary<long, ICombatant> EndCombatants { get; private set; } = new SafeDictionary<long, ICombatant>();

		/// <summary>
		/// Starting HP of all combatants.
		/// </summary>
		[Obsolete("OriginalHitpoints is obsolete, use StartCombatants.")]
		// TODO - remove after games are over
		public SafeDictionary<ICombatant, int> OriginalHitpoints { get; private set; } = new SafeDictionary<ICombatant, int>();

		/// <summary>
		/// Who originally owned each combatant?
		/// </summary>
		[Obsolete("OriginalOwners is obsolete, use StartCombatants.")]
		// TODO - remove after games are over
		public SafeDictionary<ICombatant, Empire> OriginalOwners { get; private set; } = new SafeDictionary<ICombatant, Empire>();

		/// <summary>
		/// Who launched each unit? So we can recover them after combat.
		/// </summary>
		[DoNotSerialize(false)]
		private SafeDictionary<IVehicle, ICombatant> Launchers { get; set; } = new SafeDictionary<IVehicle, ICombatant>();

		/// <summary>
		/// Saved-up fractional combat speed from the previous round.
		/// </summary>
		[DoNotSerialize(false)]
		public SafeDictionary<ICombatant, double> CombatSpeedBuffer { get; private set; } = new SafeDictionary<ICombatant, double>();

		/// <summary>
		/// The percentage of normal damage that weapons inflict.
		/// </summary>
		public abstract int DamagePercentage { get; }

		public PRNG? Dice { get; set; }

		/// <summary>
		/// The number of rounds this battle lasted.
		/// </summary>
		public int Duration => Events?.Count ?? 0;

		/// <summary>
		/// The empires engaged in battle.
		/// </summary>
		public IEnumerable<Empire>? Empires { get; protected set; }

		public List<IList<IBattleEvent>>? Events { get; private set; }

		public Image? Icon => Combatants.OfType<ISpaceObject>().Largest()?.Icon;

		public Image Icon32 => Icon.Resize(32);

		public IEnumerable<string> IconPaths => Combatants.OfType<ISpaceObject>().Largest()?.IconPaths ?? Enumerable.Empty<string>();

		private SafeDictionary<ICombatant, int> DistancesToTargets { get; } = new SafeDictionary<ICombatant, int>();

		private SafeDictionary<ICombatant, HashSet<ICombatant>> IgnoredTargets { get; } = new SafeDictionary<ICombatant, HashSet<ICombatant>>(true);

		public IList<LogMessage> Log { get; private set; }

		public IList<IntVector2> LowerRight { get; private set; } = new List<IntVector2>();

		public abstract int MaxRounds { get; }

		/// <summary>
		/// The name of the battle. Battles are typicaly named after their location.
		/// </summary>
		public abstract string Name { get; }

		public Image? Portrait => Combatants.OfType<ISpaceObject>().Largest()?.Portrait;

		public IEnumerable<string> PortraitPaths => Combatants.OfType<ISpaceObject>().Largest()?.PortraitPaths ?? Enumerable.Empty<string>();

		/// <summary>
		/// The sector in which this battle took place.
		/// </summary>
		public Sector? Sector { get; set; }

		/// <summary>
		/// The star system in which this battle took place.
		/// </summary>
		public StarSystem? StarSystem => Sector?.StarSystem;

		public double Timestamp { get; private set; }

		public IList<IntVector2> UpperLeft { get; private set; } = new List<IntVector2>();

		public int GetDiameter(int round) => UpperLeft[round].DistanceToEightWay(LowerRight[round]) + 1;

		public virtual void Initialize(IEnumerable<ICombatant> combatants)
		{
			Combatants = combatants.ToHashSet();
			StartCombatants = combatants.Select(c => new { c.ID, Copy = c.CopyAndAssignNewID() }).ToDictionary(q => q.ID, q => q.Copy);
		}

		public abstract void PlaceCombatants(SafeDictionary<ICombatant, IntVector2> locations);

		/// <summary>
		/// Resolves the battle.
		/// </summary>
		public void Resolve()
		{
			// update memories
			foreach (var sobj in StarSystem?.SpaceObjects.Where(x => !x.IsMemory).ToArray() ?? Array.Empty<ISpaceObject>())
				sobj.UpdateEmpireMemories();

			Current.Add(this);

			var reloads = new SafeDictionary<Component, double>();
			var locations = new SafeDictionary<ICombatant, IntVector2>();

			PlaceCombatants(locations);

			Events = new List<IList<IBattleEvent>>();

			UpdateBounds(0, locations.Values);

			Combatants ??= new HashSet<ICombatant>();

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
						Events.Last().Add(new CombatantAppearsEvent(this, c, locations[c]));
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
							Events.Last().Add(new CombatantDestroyedEvent(this, s, locations[s]));
							continue;
						}
						s.DistanceTraveled += Math.Min(GetCombatSpeedThisRound(c), locations[s].DistanceToEightWay(locations[s.Target]));
						locations[s] = IntVector2.InterpolateEightWay(locations[s], locations[s.Target], GetCombatSpeedThisRound(c));
						if (s.DistanceTraveled > s.WeaponInfo.MaxRange)
						{
							s.Hitpoints = 0;
							Events.Last().Add(new CombatantDestroyedEvent(this, s, locations[s]));
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
							if (c.FillsCombatTile)
							{
								// only one ship/base/planet per tile
								foreach (var tile in heatmap.ToArray())
								{
									if (locations.Any(q => q.Key.FillsCombatTile && q.Value == tile.Key))
										heatmap.Remove(tile.Key);
								}
							}
							if (heatmap.Any())
								locations[c] = heatmap.FindMin(locations[c], GetCombatSpeedThisRound(c));
						}
						else
						{
							// move to max range that we can inflict max damage on best target
							var goodTargets = targetiness.Where(x => !IgnoredTargets[c].Contains(x.Key)).WithMax(x => x.Value);
							ICombatant? bestTarget = null;
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
								if (c.Weapons.Any(w => w.Template.ComponentTemplate.WeaponInfo.IsSeeker)
									&& locations[c].DistanceToEightWay(locations[bestTarget]) > DistancesToTargets[c])
								{
									// adjust desired range due to seeker speed and target speed if retreating
									var roundsToClose = c.Weapons.Where(w => w.Template.ComponentTemplate.WeaponInfo.IsSeeker).Max(w =>
										(int)Math.Ceiling(w.Template.WeaponMaxRange / (double)(w.Template.ComponentTemplate.WeaponInfo as SeekingWeaponInfo)?.SeekerSpeed));
									var distanceAdjustment = (int)Ceiling(combatSpeeds[bestTarget] * roundsToClose);
									maxdmgrange -= distanceAdjustment;
									if (maxdmgrange < 0)
										maxdmgrange = 0;
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
								if (c.FillsCombatTile)
								{
									foreach (var tile in tiles.ToArray())
									{
										if (locations.Any(q => q.Key.FillsCombatTile && q.Value == tile))
											tiles.Remove(tile);
									}
								}
								if (tiles.Any())
								{
									var closest = tiles.WithMin(t => t.DistanceToEightWay(locations[c])).First();
									locations[c] = IntVector2.InterpolateEightWay(locations[c], closest, GetCombatSpeedThisRound(c), vec => locations.Values.Contains(vec));
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
							}
							else
								DistancesToTargets.Remove(c);
						}
					}
				gotosAreVeryEvil:
					if (locations[c] != oldpos)
						Events.Last().Add(new CombatantMovesEvent(this, c, oldpos, locations[c]));
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
						Launchers[info.Launchee] = info.Launcher;
						if (info.Launcher is ICargoTransferrer ct && info.Launchee is IUnit u)
							ct.RemoveUnit(u);

						Combatants.Add(info.Item2);
						StartCombatants[info.Item2.ID] = info.Item2.Copy();
						for (var ix = 0; ix < info.Item2.Weapons.Count(); ix++)
						{
							var w = info.Item2.Weapons.ElementAt(ix);
							var wc = StartCombatants[info.Item2.ID].Weapons.ElementAt(ix);
						}
						locations[info.Launchee] = new IntVector2(locations[info.Launcher]);
						Events.Last().Add(new CombatantLaunchedEvent(this, info.Launcher, info.Launchee, locations[info.Launchee]));
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

			// recover units
			var orphans = new List<IUnit>();
			foreach (var u in Combatants.OfType<IUnit>())
			{
				if (Launchers[u] is ICargoTransferrer cc && cc.CargoStorageFree() >= u.Design.Hull.Size && u.Owner == cc.Owner)
					cc.Cargo.Units.Add(u);
				else
					orphans.Add(u);
			}
			foreach (var u in orphans)
			{
				var recoverer = Combatants.OfType<ICargoTransferrer>().Where(q => q.CargoStorageFree() >= u.Design.Hull.Size && q.Owner == u.Owner).FirstOrDefault();
				if (recoverer != null)
					recoverer.Cargo.Units.Add(u);
				else
					u.Dispose(); // no one can recover this unit, it is destroyed
			}

			// save state of combatants at end of battle - set to undisposed so they don't get purged!
			EndCombatants = Combatants.Select(c => new { ID = c.ID, Copy = c.CopyAndAssignNewID() }).ToDictionary(q => q.ID, q => q.Copy);
			foreach (var c in EndCombatants.Values)
				c.IsDisposed = false;

			// validate fleets since some ships might have died
			foreach (var fleet in Sector?.SpaceObjects.OfType<Fleet>() ?? Enumerable.Empty<Fleet>())
				fleet.Validate();

			// replenish combatants' shields
			foreach (var combatant in Sector?.SpaceObjects.OfType<ICombatant>() ?? Enumerable.Empty<ICombatant>())
				combatant.ReplenishShields();

			// mark battle complete
			Current.Remove(this);
			Previous.Add(this);

			// update memories
			foreach (var sobj in Combatants.OfType<ISpaceObject>().Where(x => !x.IsMemory).ToArray())
			{
				foreach (var emp in Empires ?? Enumerable.Empty<Empire>())
				{
					emp.UpdateMemory(sobj); ;
				}
			}

			// modify happiness
			ModifyHappiness();
		}

		public abstract void ModifyHappiness();

		public override string ToString() => Name;

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
				Events.Last().Add(new CombatantsCollideEvent(this, s, s.Target, locations[s.Target], s.Hitpoints, hit.NominalDamage, false, wasArmed && !isArmed));
				s.Hitpoints = 0;
				Events.Last().Add(new CombatantDestroyedEvent(this, s, locations[s]));
				locations.Remove(s);
				if (s.Target.IsDestroyed)
				{
					var loc = locations[s.Target];
					Events.Last().Add(new CombatantDestroyedEvent(this, s.Target, locations[s.Target]));
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
			else if (w.Template.ComponentTemplate.WeaponInfo.IsPointDefense)
			{
				// point defense weapons are unaffected by multiplex tracking limits
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
				}).FirstOrDefault();
			}
			else
			{
				target = Combatants.Where(x =>
				{
					if (!x.IsAlive)
						return false;
					if (!x.Owner.IsEnemyOf(c.Owner, StarSystem))
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
				if (w.BurnSupplies())
				{
					if (w.Template.ComponentTemplate.WeaponType == WeaponTypes.Seeking || w.Template.ComponentTemplate.WeaponType == WeaponTypes.SeekingPointDefense)
					{
						var seeker = new Seeker(Sector, c.Owner, c, w, target);
						Galaxy.Current.AssignID(seeker);
						Combatants?.Add(seeker);
						StartCombatants[seeker.ID] = seeker.Copy();
						locations[seeker] = new IntVector2(locations[c]);
						Events.Last().Add(new CombatantLaunchedEvent(this, c, seeker, locations[seeker]));
					}
					else
					{
						// fire
						int dmg = 0;
						var winfo = w.Template.ComponentTemplate.WeaponInfo;
						var minrng = w.Template.WeaponMinRange;
						var maxrng = w.Template.WeaponMaxRange;
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
							Events.Last().Add(new WeaponFiresEvent(this, c, locations[c], target, locations[target], w, hit, wasArmed && !isArmed));
						}
						else
						{
							bool isArmed = target is Seeker || target.Weapons.Any();
							Events.Last().Add(new WeaponFiresEvent(this, c, locations[c], target, locations[target], w, null, wasArmed && !isArmed));
						}
					}
					// TODO - mounts that affect reload rate?
					reloads[w] += w.Template.ComponentTemplate.WeaponInfo.ReloadRate.Evaluate(w.Template);
				}
				else
					break;
			}

			if (target.IsDestroyed)
			{
				Events.Last().Add(new CombatantDestroyedEvent(this, target, locations[target]));
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

		public void Dispose()
		{
			foreach (var seeker in Combatants.OfType<Seeker>().ToArray())
				seeker.Dispose();
			Combatants?.Clear();
			if (CombatSpeedBuffer != null)
				CombatSpeedBuffer.Clear();
			Events?.Clear();
			LowerRight.Clear();
			if (StartCombatants != null)
				StartCombatants.Clear();
			if (EndCombatants != null)
				EndCombatants.Clear();
			UpperLeft.Clear();
		}
	}
}
