using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Civilization.Diplomacy.Clauses;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Combat.Grid;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Events;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.VictoryConditions;
using FrEee.Game.Setup;
using FrEee.Game.Setup.WarpPointPlacementStrategies;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using Microsoft.Scripting.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A galaxy in which the game is played.
	/// </summary>
	[Serializable]
	public class Galaxy : ICommonAbilityObject
	{
		public Galaxy()
		{
			Galaxy.Current = this;
			if (Mod.Current != null)
				ModPath = Mod.Current.RootPath;
			StarSystemLocations = new List<ObjectLocation<StarSystem>>();
			Empires = new List<Empire>();
			Name = "Unnamed";
			TurnNumber = 1;
			referrables = new Dictionary<long, IReferrable>();
			VictoryConditions = new List<IVictoryCondition>();
			AbilityCache = new SafeDictionary<IAbilityObject, IEnumerable<Ability>>();
			CommonAbilityCache = new SafeDictionary<Tuple<ICommonAbilityObject, Empire>, IEnumerable<Ability>>();
			SharedAbilityCache = new SafeDictionary<Tuple<IOwnableAbilityObject, Empire>, IEnumerable<Ability>>();
			GivenTreatyClauseCache = new SafeDictionary<Empire, ILookup<Empire, Clause>>();
			ReceivedTreatyClauseCache = new SafeDictionary<Empire, ILookup<Empire, Clause>>();
			Battles = new HashSet<IBattle>();
			ScriptNotes = new DynamicDictionary();
			/*if (Mod.Current != null)
			{
				foreach (var q in Mod.Current.Objects.OfType<IReferrable>())
					AssignID(q);
			}*/
		}

		/// <summary>
		/// The current galaxy. Shouldn't change except at loading a game or turn processing.
		/// </summary>
		// public static Galaxy Current { get; set; }

		public AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Galaxy; }
		}

		/// <summary>
		/// Allowed trades in this game.
		/// </summary>
		public AllowedTrades AllowedTrades { get; set; }

		/// <summary>
		/// Should players have sensor data for all systems from the get-go?
		/// </summary>
		public bool AllSystemsExploredFromStart { get; set; }

		/// <summary>
		/// The battles which have taken place this turn.
		/// </summary>
		public ICollection<IBattle> Battles { get; private set; }

		public bool CanColonizeOnlyBreathable { get; set; }

		public bool CanColonizeOnlyHomeworldSurface { get; set; }

		public IEnumerable<IAbilityObject> Children
		{
			get { return StarSystemLocations.Select(l => l.Item); }
		}

		/// <summary>
		/// Per mille chance of a random event occurring, per turn, per player.
		/// </summary>
		public double EventFrequency { get; set; }

		public int Height
		{
			get;
			set;
		}

		public IEnumerable<Ability> IntrinsicAbilities
		{
			// TODO - galaxy wide abilities?
			get { yield break; }
		}

		public bool IsAnalysisAllowed { get; set; }

		/// <summary>
		/// Is this a "humans vs. AI" game?
		/// </summary>
		public bool IsHumansVsAI { get; set; }

		public bool IsIntelligenceAllowed { get; set; }

		/// <summary>
		/// Is this a single player game? If so, autoprocess the turn after the player takes his turn.
		/// </summary>
		public bool IsSinglePlayer { get; set; }

		public bool IsSurrenderAllowed { get; set; }

		/// <summary>
		/// The maximum event severity in this game.
		/// </summary>
		public EventSeverity MaximumEventSeverity { get; set; }

		public int MaxPlanetValue { get; set; }

		public int MaxSpawnedAsteroidValue { get; set; }

		public int MaxSpawnedPlanetValue { get; set; }

		public int MaxX
		{
			get { return StarSystemLocations.MaxOrDefault(ssl => ssl.Location.X); }
		}

		public int MaxY
		{
			get { return StarSystemLocations.MaxOrDefault(ssl => ssl.Location.Y); }
		}

		public int MinAsteroidValue { get; set; }

		public int MinPlanetValue { get; set; }

		public int MinSpawnedAsteroidValue { get; set; }

		public int MinSpawnedPlanetValue { get; set; }

		public int MinX
		{
			get { return StarSystemLocations.MinOrDefault(ssl => ssl.Location.X); }
		}

		public int MinY
		{
			get { return StarSystemLocations.MinOrDefault(ssl => ssl.Location.Y); }
		}

		/// <summary>
		/// The next tick size, for ship movement.
		/// </summary>
		public double NextTickSize { get; internal set; }

		/// <summary>
		/// Should players have an omniscient view of all explored systems?
		/// Does not prevent cloaking from working; this is just basic sight.
		/// Also does not give battle reports for other empires' battles.
		/// </summary>
		public bool OmniscientView { get; set; }

		public IEnumerable<IAbilityObject> Parents
		{
			get
			{
				yield break;
			}
		}

		/// <summary>
		/// Events which have been warned of and are pending execution.
		/// </summary>
		public ICollection<Event> PendingEvents { get; private set; } = new List<Event>();

		/// <summary>
		/// Model to use for remote mining.
		/// </summary>
		public MiningModel RemoteMiningModel { get; set; }

		/// <summary>
		/// Who can view empire scores?
		/// </summary>
		public ScoreDisplay ScoreDisplay { get; set; }

		/// <summary>
		/// Model to use for standard planetary mining.
		/// </summary>
		public MiningModel StandardMiningModel { get; set; }		

		/// <summary>
		/// The locations of the star systems in the galaxy.
		/// </summary>
		public ICollection<ObjectLocation<StarSystem>> StarSystemLocations { get; private set; }

		/// <summary>
		/// Technology research cost formula.
		/// Low = Level * BaseCost
		/// Medium = BaseCost for level 1, Level ^ 2 * BaseCost / 2 otherwise
		/// Hight = Level ^ 2 * BaseCost
		/// </summary>
		public TechnologyCost TechnologyCost { get; set; }

		/// <summary>
		/// Zero means normal tech cost; positive values make researching techs that other players know already harder; negative makes it easier.
		/// </summary>
		public int TechnologyUniqueness { get; set; }

		/// <summary>
		/// Current time equals turn number plus tick minus 1.
		/// </summary>
		public double Timestamp { get { return TurnNumber + CurrentTick - 1; } }

		/// <summary>
		/// Number of turns of uninterrupted galactic peace (Non-Aggression or better between all surviving empires).
		/// </summary>
		public int TurnsOfPeace
		{
			get
			{
				// TODO - treaties
				return 0;
			}
		}

		/// <summary>
		/// Vertical space occupied by star systems.
		/// </summary>
		public int UsedHeight
		{
			get
			{
				if (!StarSystemLocations.Any())
					return 0;
				return StarSystemLocations.Max(ssl => ssl.Location.Y) - StarSystemLocations.Min(ssl => ssl.Location.Y) + 1;
			}
		}

		/// <summary>
		/// Horizontal space occuped by star systems.
		/// </summary>
		public int UsedWidth
		{
			get
			{
				if (!StarSystemLocations.Any())
					return 0;
				return StarSystemLocations.Max(ssl => ssl.Location.X) - StarSystemLocations.Min(ssl => ssl.Location.X) + 1;
			}
		}

		/// <summary>
		/// Game victory conditions.
		/// </summary>
		public IList<IVictoryCondition> VictoryConditions { get; private set; }

		/// <summary>
		/// Delay in turns before victory conditions take effect.
		/// </summary>
		public int VictoryDelay { get; set; }

		public WarpPointPlacementStrategy WarpPointPlacementStrategy { get; set; }

		public int Width
		{
			get;
			set;
		}

		

		private IDictionary<Sector, double> lastBattleTimestamps = new SafeDictionary<Sector, double>();


		/// <summary>
		/// Saves all empires' tech levels in the other empires for uniqueness calculations.
		/// </summary>
		internal void SaveTechLevelsForUniqueness()
		{
			if (Current.TechnologyUniqueness != 0)
			{
				foreach (var emp in Current.Empires)
				{
					emp.OtherPlayersTechLevels.Clear();
					foreach (var emp2 in Current.Empires.ExceptSingle(emp))
					{
						foreach (var tech in Mod.Current.Technologies)
						{
							if (emp.OtherPlayersTechLevels[tech] == null)
								emp.OtherPlayersTechLevels[tech] = new List<int>();
							emp.OtherPlayersTechLevels[tech].Add(emp2.ResearchedTechnologies[tech]);
						}
					}
				}
			}
		}


		public void ComputeNextTickSize()
		{
			var objs = FindSpaceObjects<IMobileSpaceObject>().Where(obj => obj.Orders.Any());
			objs = objs.Where(obj => !obj.IsMemory);
			if (objs.Where(v => v.TimeToNextMove > 0).Any() && CurrentTick < 1.0)
			{
				// HACK - why are objects getting zero time to next move?!
				var nextTickSize = objs.Where(v => v.TimeToNextMove > 0).Min(v => v.TimeToNextMove);
				NextTickSize = Math.Min(1.0 - CurrentTick, nextTickSize);
			}
			else if (objs.Any())
			{
				NextTickSize = objs.Min(v => v.TimePerMove);
			}
			else
				NextTickSize = double.PositiveInfinity;
		}

		/// <summary>
		/// Creates a new empire.
		/// </summary>
		/// <returns></returns>
		public Empire CreateNewEmpire()
		{
			var emp = new Empire();
			emp.Name = "Randomly Generated Empire"; // TODO - load from EmpireNames.txt / EmpireTypes.txt

			// TODO - assign AI and primary race to empire
			Empires.Add(emp);
			return emp;
		}

		/// <summary>
		/// Disables the server side ability cache.
		/// </summary>
		public void DisableAbilityCache()
		{
			isAbilityCacheEnabled = false;
			AbilityCache.Clear();
			CommonAbilityCache.Clear();
			SharedAbilityCache.Clear();
		}

		/// <summary>
		/// Enables the server side ability cache.
		/// </summary>
		public void EnableAbilityCache()
		{
			isAbilityCacheEnabled = true;
		}

		/// <summary>
		/// Finds referrable objects in the galaxy.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="condition"></param>
		/// <returns></returns>
		public IEnumerable<T> Find<T>(Func<T, bool> condition = null) where T : IReferrable
		{
			if (condition == null)
				condition = t => true;
			return Referrables.OfType<T>().Where(r => condition(r));
		}

		/// <summary>
		/// Searches for space objects matching criteria.
		/// </summary>
		/// <typeparam name="T">The type of space object.</typeparam>
		/// <param name="criteria">The criteria.</param>
		/// <returns>The matching space objects.</returns>
		public IEnumerable<T> FindSpaceObjects<T>(Func<T, bool> criteria = null)
		{
			return StarSystemLocations.SelectMany(l => l.Item.FindSpaceObjects<T>(criteria));
		}

		public IEnumerable<IAbilityObject> GetContainedAbilityObjects(Empire emp)
		{
			return StarSystemLocations.Select(ssl => ssl.Item).Concat(StarSystemLocations.SelectMany(ssl => ssl.Item.GetContainedAbilityObjects(emp)));
		}

		public IReferrable GetReferrable(long key)
		{
			if (!referrables.ContainsKey(key))
				return null;
			return referrables[key];
		}

		/// <summary>
		/// Finds the real version of a fake referrable.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fakeobj">The fake referrable.</param>
		/// <returns></returns>
		public T GetReferrable<T>(T fakeobj)
			where T : IReferrable
		{
			return (T)GetReferrable(fakeobj.ID);
		}

		/// <summary>
		/// Loads player commands into the current game state.
		/// If this is the host view, commands will be loaded for all players.
		/// If this is the player view, commands will be immediately executed so as to provide the player with a fresh game state.
		/// </summary>
		/// <returns>Player empires which did not submit commands and are not defeated.</returns>
		public IEnumerable<Empire> LoadCommands()
		{
			// whose commands are we loading?
			var emps = new List<Empire>();
			if (CurrentEmpire == null)
				emps.AddRange(Empires);
			else
				emps.Add(CurrentEmpire);

			var noCmds = new List<Empire>();

			foreach (var emp in emps)
			{
				var plrfile = GetEmpireCommandsSavePath(emp);
				if (File.Exists(plrfile))
				{
					var fs = new FileStream(plrfile, FileMode.Open);
					var cmds = DeserializeCommands(fs);
					LoadCommands(emp, cmds);
					fs.Close(); fs.Dispose();
				}
				else if (emp.IsPlayerEmpire)
					noCmds.Add(emp);
			}

			if (CurrentEmpire != null)
			{
				foreach (var cmd in CurrentEmpire.Commands)
				{
					if (cmd.Executor == null)
						CurrentEmpire.Log.Add(CurrentEmpire.CreateLogMessage($"{cmd} cannot be issued because its executor does not exist. Probably a bug...", LogMessageType.Error));
					else if (cmd.Issuer != cmd.Executor.Owner && cmd.Issuer != cmd.Executor)
						CurrentEmpire.Log.Add(CurrentEmpire.CreateLogMessage("We cannot issue commands to " + cmd.Executor + " because it does not belong to us!", LogMessageType.Error));
					else
						cmd.Execute();
				}
			}

			return noCmds;
		}

		/// <summary>
		/// Only public for unit tests. You should probably call ProcessTurn instead.
		/// </summary>
		public void MoveShips()
		{
			var vlist = FindSpaceObjects<IMobileSpaceObject>().Where(sobj => sobj.Container == null && !sobj.IsMemory).Shuffle();
			foreach (var v in vlist)
			{
				// mark system explored if not already
				var sys = v.StarSystem;
				if (sys == null)
					continue; // space object is dead, or not done being built

				if (CurrentTick == 0d && !v.Orders.OfType<IMovementOrder>().Any())
					v.DealWithMines();

				bool didStuff = v.ExecuteOrders();
				sys.MarkAsExploredBy(v.Owner);

				// update memory sight after movement
				if (didStuff)
				{
					v.UpdateEmpireMemories();
					if (v.StarSystem != null && v.Owner != null)
					{
						foreach (var sobj in v.StarSystem.FindSpaceObjects<ISpaceObject>().Where(sobj => sobj != v && !sobj.IsMemory && v.Owner.CanSee(sobj)).ToArray())
							v.Owner.UpdateMemory(sobj);
					}

					// replenish shields after moving (who knows, we might be out of supplies, or about to hit a minefield)
					v.ReplenishShields();
				}

				// check for battles
				var sector = v.Sector;
				if (v.Owner != null && // unowned objects can't pick fights
					sector != null && // can't fight nowhere
					sector.SpaceObjects.OfType<ICombatant>().Any(
						sobj =>
							(sobj.IsHostileTo(v.Owner) && sobj.Weapons.Any() || v.IsHostileTo(sobj.Owner) && v.Weapons.Any()) // any enemies?
							&& (sobj.Owner.CanSee(v) || v.Owner.CanSee(sobj)) // enemies are visible?
							&& (!lastBattleTimestamps.ContainsKey(sector) || lastBattleTimestamps[sector] < Timestamp - (v.StrategicSpeed == 0 ? 1d : 1d / v.StrategicSpeed)))) // have we fought here too recently?
				{
					// resolve the battle
					var battle = new SpaceBattle(sector);
					battle.Resolve();
					Battles.Add(battle);
					foreach (var emp in battle.Empires)
						emp.Log.Add(battle.CreateLogMessage(battle.NameFor(emp), LogMessageType.Battle));
					lastBattleTimestamps[sector] = Current.Timestamp;
				}
			}
		}

		public Sector PickRandomSector(PRNG prng = null)
		{
			return StarSystemLocations.PickRandom(prng).Item.PickRandomSector(prng);
		}

		

		public override string ToString()
		{
			if (CurrentEmpire == null)
				return Name;
			return Name + " - " + CurrentEmpire.Name + " - " + CurrentEmpire.LeaderName + " - " + Stardate;
		}


		internal void SpaceObjectIDCheck(string when)
		{
			foreach (var sobj in FindSpaceObjects<ISpaceObject>().ToArray())
			{
				if (!referrables.ContainsKey(sobj.ID))
					AssignID(sobj);
				if (sobj.ID > 0)
				{
					var r = referrables[sobj.ID];
					if (r != sobj)
					{
						// HACK - assume the space object that's actually in space is "real"
						referrables[sobj.ID] = sobj;
						Console.Error.WriteLine("Space object identity mismatch " + when + " for ID=" + sobj.ID + ". " + sobj + " is actually in space so it is replacing " + r + " in the referrables collection.");
					}
				}
			}
		}

		/// <summary>
		/// Deserializes the player's commands.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		private static IList<ICommand> DeserializeCommands(Stream stream)
		{
			var cmds = Serializer.Deserialize<IList<ICommand>>(stream);

			// check for client safety
			foreach (var cmd in cmds.Where(cmd => cmd != null))
			{
				cmd.CheckForClientSafety();
			}

			return cmds;
		}

		internal void LoadCommands(Empire emp, IList<ICommand> cmds)
		{
			cmds = cmds.Where(cmd => cmd != null).Distinct().ToList(); // HACK - why would we have null/duplicate commands in a plr file?!
			emp.Commands.Clear();
			var idmap = new Dictionary<long, long>();
			foreach (var cmd in cmds)
			{
				if (cmd.NewReferrables.Any(r => r.IsDisposed))
				{
					emp.Log.Add(new GenericLogMessage("Command \"" + cmd + "\" contained a reference to deleted object \"" + cmd.NewReferrables.First(r => r.IsDisposed) + "\" and will be ignored. This may be a game bug."));
					continue;
				}
				emp.Commands.Add(cmd);
				foreach (var r in cmd.NewReferrables)
				{
					var clientid = r.ID;
					var serverid = AssignID(r);
					if (idmap.ContainsKey(clientid))
					{
						if (idmap[clientid] != serverid)
							throw new InvalidOperationException($"Adding {r} with ID {serverid} to client ID {clientid} for {emp} when that client ID is already mapped to server ID {idmap[clientid]}.");
						// else do nothing
					}
					else
						idmap.Add(clientid, serverid);
				}
			}
			foreach (var cmd in cmds)
				cmd.ReplaceClientIDs(idmap); // convert client IDs to server IDs
		}

		/// <summary>
		/// Disposes of any space objects that aren't in space, under construction, or part of the mod definition.
		/// </summary>
		private void SpaceObjectCleanup()
		{
			foreach (var sobj in Referrables.OfType<ISpaceObject>().ToArray())
			{
				bool dispose = true;
				if (sobj.Sector != null)
					dispose = false; // save space objects that are in space
				else if (this is IUnit u && u.FindContainer() != null) // save units that are in cargo
					dispose = false;
				else if (Mod.Current.StellarObjectTemplates.Contains(sobj as StellarObject))
					dispose = false; // save stellar objects that are part of the mod templates
				else if (Referrables.OfType<ConstructionQueue>().Any(q => q.Orders.Any(o => o.Item == sobj as IConstructable)))
					dispose = false; // save constructable space objects under construction
				if (dispose)
					sobj.Dispose();
			}
		}

		// TODO - replace all those duplicate properties with a reference to the game setup
		/*
		/// <summary>
		/// The game setup used to create this galaxy.
		/// </summary>
		public GameSetup GameSetup { get; set; }
		*/
	}
}
