using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility.Extensions;
using System.Threading.Tasks;
using FrEee.Modding;
using System.Drawing;
using FrEee.Utility;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Vehicles;
using System.Reflection;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Setup;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.VictoryConditions;
using FrEee.Game.Objects.History;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A galaxy in which the game is played.
	/// </summary>
	[Serializable]
	public class Galaxy
	{
		public Galaxy()
		{
			Current = this;
			Master = this;
			Empires = new List<Empire>();
			Settings = new GalaxySettings();
			Settings.TurnNumber = 1;
			referrables = new Dictionary<long, IReferrable>();
		}

		public Galaxy(Mod mod)
			: this()
		{
			Settings.Mod = mod;
		}

		public Galaxy(GalaxyHistory history)
		{
			Current = this;
			Empires = new List<Empire>();
			referrables = new Dictionary<long, IReferrable>();
			Settings = history.Settings;
			CurrentEmpire = history.Empire;
			foreach (var kvp in history.Empire.History)
			{
				var h = kvp.Key;
				if (h is IReferrable)
					Current.AssignID((IReferrable)h); // TODO - make sure empires can't get out of order?
				if (h is Empire)
					Empires.Add((Empire)h);
				var keyframes = kvp.Value;
				var last = keyframes.OrderBy(k => k.Key).LastOrDefault().Value;
				if (last != null)
					last.Apply(h);
			}
			Mod.Current = history.Settings.Mod;
			foreach (var r in Mod.Current.Referrables)
				AssignID(r);
		}

		#region Properties

		public GalaxySettings Settings {get; set;}

		/// <summary>
		/// The current galaxy. Shouldn't change except at loading a game or turn processing.
		/// </summary>
		public static Galaxy Current { get; private set; }

		/// <summary>
		/// The master galaxy view (of the host), if known.
		/// </summary>
		public static Galaxy Master { get; private set; }

		/// <summary>
		/// The locations of the star systems in the galaxy.
		/// </summary>
		public IEnumerable<ObjectLocation<StarSystem>> StarSystemLocations
		{
			get
			{
				return Referrables.OfType<StarSystem>().Select(sys => new ObjectLocation<StarSystem>(sys, sys.Coordinates));
			}
		}

		/// <summary>
		/// The empires participating in the game.
		/// </summary>
		public IList<Empire> Empires { get; private set; }

		/// <summary>
		/// The empire whose turn it is.
		/// </summary>
		public Empire CurrentEmpire { get; set; }

		public string GameFileName
		{
			get
			{
				if (PlayerNumber > 0)
					return Settings.Name + "_" + Settings.TurnNumber + "_" + PlayerNumber + FrEeeConstants.SaveGameExtension;
				else
					return Settings.Name + "_" + Settings.TurnNumber + FrEeeConstants.SaveGameExtension;
			}
		}

		public string CommandFileName
		{
			get
			{
				if (PlayerNumber > 0)
					return Settings.Name + "_" + Settings.TurnNumber + "_" + PlayerNumber + FrEeeConstants.PlayerCommandsSaveGameExtension;
				else
					throw new InvalidOperationException("The game host does not have a command file.");
			}
		}

		public int MinX
		{
			get { return StarSystemLocations.MinOrDefault(ssl => ssl.Location.X); }
		}

		public int MinY
		{
			get { return StarSystemLocations.MinOrDefault(ssl => ssl.Location.Y); }
		}

		public int MaxX
		{
			get { return StarSystemLocations.MaxOrDefault(ssl => ssl.Location.X); }
		}

		public int MaxY
		{
			get { return StarSystemLocations.MaxOrDefault(ssl => ssl.Location.Y); }
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
		/// The current player number (1 is the first player, 0 is the game host).
		/// </summary>
		public int PlayerNumber
		{
			get { return Empires.IndexOf(CurrentEmpire) + 1; }
		}

		/// <summary>
		/// The current stardate. Advances 0.1 years per turn.
		/// </summary>
		public string Stardate
		{
			get
			{
				return Settings.TurnNumber.ToStardate();
			}
		}

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

		#endregion

		#region Data Access

		/// <summary>
		/// Serializes the player's commands.
		/// </summary>
		/// <exception cref="InvalidOperationException">if no current empire</exception>
		/// <returns></returns>
		private void SerializeCommands(Stream stream)
		{
			if (CurrentEmpire == null)
				throw new InvalidOperationException("Can't serialize commands if there is no current empire.");

			Serializer.Serialize(CurrentEmpire.Commands, stream);
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

		/// <summary>
		/// Assigns IDs to referrable objects in the galaxy.
		/// </summary>
		internal void AssignIDs()
		{
			var parser = new ObjectGraphParser();
			parser.EndObject += o =>
			{
				if (o is IReferrable)
				{
					var r = (IReferrable)o;
					AssignID(r);
				}
			};
			parser.Parse(this);
		}

		public void Save(Stream stream)
		{
			AssignIDs();
			var filename = GetGameSavePath(CurrentEmpire);
			if (!Directory.Exists(FrEeeConstants.SaveGameDirectory))
				Directory.CreateDirectory(FrEeeConstants.SaveGameDirectory);
			Serializer.Serialize(this, stream);
		}

		public string SaveToString()
		{
			AssignIDs();
			return Serializer.SerializeToString(this);
		}

		/// <summary>
		/// Saves the game to an appropriately named file in the Savegame folder.
		/// Files are named GameName_TurnNumber_PlayerNumber.gam for players (PlayerNumber is 1-indexed)
		/// and GameName_TurnNumber.gam for the host.
		/// </summary>
		/// <returns>The filename saved to without the folder name (which is Savegame).</returns>
		public string Save()
		{
			AssignIDs();
			var filename = GetGameSavePath(CurrentEmpire);
			if (!Directory.Exists(FrEeeConstants.SaveGameDirectory))
				Directory.CreateDirectory(FrEeeConstants.SaveGameDirectory);
			var fs = new FileStream(filename, FileMode.Create);
			Serializer.Serialize(this, fs);
			fs.Close();
			return filename;
		}

		/// <summary>
		/// Saves the master view and all players' views of the galaxy, unless single player, in which case only the first player's view is saved.
		/// </summary>
		/// <exception cref="InvalidOperationException">if CurrentEmpire is not null.</exception>
		public static void SaveAll(Status status = null, double desiredProgress = 1d)
		{
			if (Master.CurrentEmpire != null)
				throw new InvalidOperationException("Can only save player galaxy views from the master galaxy view.");

			var progressPerSaveLoad = (desiredProgress - (status == null ? 0d : status.Progress)) / (Master.Settings.IsSinglePlayer ? 3 : (Master.Empires.Count + 2));

			// save player history views
			for (int i = 0; i < Master.Empires.Count; i++)
			{
				if (i == 0 || !Master.Settings.IsSinglePlayer)
				{
					if (status != null)
						status.Message = "Saving game (player " + (i + 1) + ")";
					Master.SaveHistory(Master.Empires[i]);
					if (status != null)
						status.Progress += progressPerSaveLoad;
				}
			}

			// save master view to memory without the history (only players need that)
			if (status != null)
				status.Message = "Saving game (host)";
			foreach (var emp in Master.Empires)
				emp.History.Clear();
			Master.Save();
			if (status != null)
				status.Progress += progressPerSaveLoad;
		}

		public void SaveHistory(Empire emp)
		{
			var filename = GetGameSavePath(emp);
			var fs = File.OpenWrite(filename);
			Serializer.Serialize(new GalaxyHistory(this, emp), fs);
			fs.Close();
		}

		/// <summary>
		/// Loads a savegame from a stream.
		/// </summary>
		/// <param name="stream"></param>
		public static void Load(Stream stream)
		{
			var obj = Serializer.Deserialize(stream);
			if (obj is Galaxy)
				Current = (Galaxy)obj;
			else if (obj is GalaxyHistory)
				Current = new Galaxy((GalaxyHistory)obj);
			else
				throw new Exception("Expected Galaxy or GalaxyHistory, found " + obj.GetType());
			if (Current.CurrentEmpire == null)
				Master = Current;
			Mod.Current = Current.Settings.Mod;
		}

		/// <summary>
		/// Loads a savegame from the Savegame folder.
		/// Note that if it was renamed, it might have different game name, turn number, player number, etc. than the filename indicates.
		/// </summary>
		/// <param name="filename"></param>
		public static void Load(string filename)
		{
			var fs = new FileStream(Path.Combine(FrEeeConstants.SaveGameDirectory, filename), FileMode.Open);
			Load(fs);
			fs.Close();
		}

		/// <summary>
		/// Loads a host savegame from the Savegame folder.
		/// </summary>
		/// <param name="gameName"></param>
		/// <param name="turnNumber"></param>
		public static void Load(string gameName, int turnNumber)
		{
			Load(gameName + "_" + turnNumber + FrEeeConstants.SaveGameExtension);
		}

		/// <summary>
		/// Loads a player savegame from the Savegame folder.
		/// </summary>
		/// <param name="gameName"></param>
		/// <param name="turnNumber"></param>
		/// <param param name="playerNumber"></param>
		public static void Load(string gameName, int turnNumber, int playerNumber)
		{
			Load(gameName + "_" + turnNumber + "_" + playerNumber.ToString("d4") + FrEeeConstants.SaveGameExtension);
		}

		/// <summary>
		/// Loads from a string in memory.
		/// </summary>
		/// <param name="serializedData"></param>
		public static void LoadFromString(string serializedData)
		{
			Galaxy.Current = Serializer.DeserializeFromString<Galaxy>(serializedData);
			Mod.Current = Current.Settings.Mod;
		}

		/// <summary>
		/// Saves the player's commands to an appropriately named file in the Savegame folder.
		/// Files are named GameName_TurnNumber_PlayerNumber.plr. (PlayerNumber is 1-indexed and padded to 4 digits with zeroes)
		/// This doesn't make sense for the host view, so an exception will be thrown if there is no current empire.
		/// </summary>
		/// <returns>The filename saved to without the folder name (which is Savegame).</returns>
		/// <exception cref="InvalidOperationException">if there is no current empire.</exception>
		public string SaveCommands()
		{
			AssignIDs();
			if (CurrentEmpire == null)
				throw new InvalidOperationException("Can't save commands without a current empire.");
			if (!Directory.Exists(FrEeeConstants.SaveGameDirectory))
				Directory.CreateDirectory(FrEeeConstants.SaveGameDirectory);
			var filename = GetEmpireCommandsSavePath(CurrentEmpire);
			var fs = new FileStream(filename, FileMode.Create);
			SerializeCommands(fs);
			fs.Close();
			return filename;
		}

		/// <summary>
		/// Loads player commands into the current game state.
		/// If this is the host view, commands will be loaded for all players.
		/// </summary>
		public void LoadCommands()
		{
			// whose commands are we loading?
			var emps = new List<Empire>();
			if (CurrentEmpire == null)
				emps.AddRange(Empires);
			else
				emps.Add(CurrentEmpire);

			foreach (var emp in emps)
			{
				var plrfile = GetEmpireCommandsSavePath(emp);
				if (File.Exists(plrfile))
				{
					var fs = new FileStream(plrfile, FileMode.Open);
					var cmds = DeserializeCommands(fs);
					LoadCommands(emp, cmds);
					fs.Close();
				}
				else if (emp.IsPlayerEmpire)
					Console.WriteLine(emp.Name + " did not submit a PLR file.");
			}
		}

		private void LoadCommands(Empire emp, IList<ICommand> cmds)
		{
			cmds = cmds.Where(cmd => cmd != null).ToList(); // HACK - why would we have null commands in a plr file?!
			emp.Commands.Clear();
			var idmap = new Dictionary<long, long>();
			foreach (var cmd in cmds)
			{
				emp.Commands.Add(cmd);
				foreach (var r in cmd.NewReferrables)
				{
					var clientid = r.ID;
					var serverid = AssignID(r);
					idmap.Add(clientid, serverid);
				}

			}
			foreach (var cmd in cmds)
				cmd.ReplaceClientIDs(idmap); // convert client IDs to server IDs
		}

		public string GetEmpireCommandsSavePath(Empire emp)
		{
			return GetEmpireCommandsSavePath(Settings.Name, Settings.TurnNumber, Empires.IndexOf(emp) + 1);
		}

		public string GetGameSavePath(Empire emp = null)
		{
			if (emp == null)
				emp = CurrentEmpire;
			return GetGameSavePath(Settings.Name, Settings.TurnNumber, emp == null ? 0 : (Empires.IndexOf(emp) + 1));
		}

		public static string GetEmpireCommandsSavePath(string gameName, int turnNumber, int empireNumber)
		{
			return Path.Combine("Savegame", String.Format("{0}_{1}_{2:d4}{3}", gameName, turnNumber, empireNumber, FrEeeConstants.PlayerCommandsSaveGameExtension));
		}

		public static string GetGameSavePath(string gameName, int turnNumber, int empireNumber)
		{
			return Path.Combine("Savegame", empireNumber < 1 ?
				String.Format("{0}_{1}{2}", gameName, turnNumber, FrEeeConstants.SaveGameExtension) :
				String.Format("{0}_{1}_{2:d4}{3}", gameName, turnNumber, empireNumber, FrEeeConstants.SaveGameExtension));
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Creates a version of the master galaxy that contains only information that the specified empire can see.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public static Galaxy Redact(Empire emp)
		{
			if (Master == null)
				throw new Exception("Can't redact without a master galaxy.");
			// copy the history so we don't modify existing objects
			var history = new GalaxyHistory(Master, emp);
			var serializedHistory = Serializer.SerializeToString(history);
			var deserializedHistory = Serializer.DeserializeFromString<GalaxyHistory>(serializedHistory);
			var galaxy = new Galaxy(deserializedHistory);
			Current = galaxy;
			return galaxy;
		}

		/// <summary>
		/// Processes the turn.
		/// </summary>
		/// <exception cref="InvalidOperationException">if the current empire is not null, or this galaxy is not the current galaxy..</exception>
		public static void ProcessTurn(Status status = null, double desiredProgress = 1d)
		{
			if (Empire.Current != null)
				throw new InvalidOperationException("Can't process the turn if there is a current empire. Load the game host's view of the galaxy instead.");

			double progressPerOperation;
			if (status == null)
				progressPerOperation = 0d;
			else
				progressPerOperation = (desiredProgress - status.Progress) / (11 + Current.Empires.Count);

			// clear history
			if (status != null)
				status.Message = "Clearing history";
			foreach (var emp in Current.Empires)
				emp.History.Clear();
			if (status != null)
				status.Progress += progressPerOperation;

			// AI commands
			if (status != null)
				status.Message = "Playing AI turns";
			var cmds = new Dictionary<int, IList<ICommand>>();
			foreach (var i in Current.Empires.Where(e => !e.IsPlayerEmpire && e.AI != null).Select(e => Current.Empires.IndexOf(e)).ToArray())
			{
				Redact(Master.Empires[i]);
				Current.CurrentEmpire.AI.Act(Current.CurrentEmpire, Current, Current.CurrentEmpire.AI.MinisterNames);
				cmds.Add(i, Current.CurrentEmpire.Commands);
			}
			Current = Master;
			foreach (var i in Current.Empires.Where(e => !e.IsPlayerEmpire && e.AI != null).Select(e => Current.Empires.IndexOf(e)).ToArray())
				Current.LoadCommands(Current.Empires[i], cmds[i]);
			if (status != null)
				status.Progress += progressPerOperation;

			// load commands
			if (status != null)
				status.Message = "Loading player commands";
			Current.LoadCommands();
			if (status != null)
				status.Progress += progressPerOperation;

			// advance turn number
			Current.Settings.TurnNumber++;

			// reproduction and population replacement from cargo
			if (status != null)
				status.Message = "Growing population";
			if (Current.Settings.TurnNumber % (Mod.Current.Settings.ReproductionDelay == 0 ? 1 : Mod.Current.Settings.ReproductionDelay) == 0)
			{
				foreach (var p in Current.FindSpaceObjects<Planet>(p => p.Colony != null).Flatten().Flatten())
				{
					var pop = p.Colony.Population;
					foreach (var race in pop.Keys.ToArray())
					{
						pop[race] += p.PopulationChangePerTurnPerRace[race];
					}

					// deal with overpopulation
					var ratio = (double)pop.Sum(kvp => kvp.Value) / (double)p.MaxPopulation;
					if (ratio > 1)
					{
						foreach (var race in pop.Keys.ToArray())
						{
							// TODO - should planetary population spill over into cargo?
							// this might be annoying for homeworlds, as their cargo space would fill up quickly...
							// especially in Proportions Mod with its 1000kT/1M population!
							pop[race] = (long)(pop[race] / ratio);
						}
					}

					// deal with population in cargo
					ratio = (double)pop.Sum(kvp => kvp.Value) / (double)p.MaxPopulation;
					if (ratio < 1)
					{
						var cargo = p.Cargo;
						if (cargo != null)
						{
							// bring population out of cold storage
							// do this by removing and adding the population
							// this will work since population is removed from cargo storage first but added to population storage first
							foreach (var kvp in cargo.Population.ToArray())
							{
								var amount = kvp.Value;
								amount -= p.RemovePopulation(kvp.Key, kvp.Value);
								p.AddPopulation(kvp.Key, kvp.Value);
							}
						}
					}
				}
			}
			if (status != null)
				status.Progress += progressPerOperation;

			// resource generation
			if (status != null)
				status.Message = "Generating resources";
			foreach (var tuple in Current.FindSpaceObjects<Planet>(p => p.Owner != null).Squash())
			{
				var p = tuple.Item3;
				var sys = tuple.Item1.Item;
				if (sys.FindSpaceObjects<Planet>().Flatten().Any(p2 => p2.Owner == p.Owner && p2.HasAbility("Spaceport")))
				{
					// give owner his income
					p.Owner.StoredResources += p.Income;

					// adjust resource value
					foreach (var kvp in p.Income)
						p.ResourceValue[kvp.Key] -= Current.Settings.StandardMiningModel.GetDecay(kvp.Value, p.ResourceValue[kvp.Key]);
				}
				else
				{
					// prorated income based on how much population doesn't need spaceport
					var merchants = p.Colony.Population.Where(kvp => kvp.Key.HasAbility("No Spaceports")).Sum(kvp => kvp.Value);
					var totalPop = p.Colony.Population.Sum(kvp => kvp.Value);
					var ratio = (double)merchants * (double)totalPop;
					if (ratio == 0)
						p.Owner.Log.Add(p.CreateLogMessage(p + " earned no income due to lack of a spaceport."));
					else if (ratio < 1)
						p.Owner.Log.Add(p.CreateLogMessage(p + " earned only " + Math.Floor(ratio / 100) + "% of normal income due to lack of a spaceport."));
					p.Owner.StoredResources += p.Income * ratio;
					p.Owner.StoredResources = ResourceQuantity.Min(p.Owner.StoredResources, p.Owner.ResourceStorage);

					// adjust resource value
					// pay full value decay for non-prorated income though, since the resources were extracted and wasted!
					foreach (var kvp in p.Income)
						p.ResourceValue[kvp.Key] -= Current.Settings.StandardMiningModel.GetDecay(kvp.Value, p.ResourceValue[kvp.Key]);
				}
			}
			// TODO - remote mining and raw resource generation
			if (status != null)
				status.Progress += progressPerOperation;

			// empire stuff
			foreach (var emp in Current.Empires)
			{
				if (status != null)
					status.Message = "Maintaining empires (" + (Current.Empires.IndexOf(emp) + 1) + " of " + Current.Empires.Count + ")";

				// pay maintenance on on ships/bases
				// TODO - allow mod to specify maintenance on units/facilities too?
				foreach (var v in emp.OwnedSpaceObjects.OfType<SpaceVehicle>())
					emp.StoredResources -= v.MaintenanceCost;

				// if not enough funds, lose ships/bases
				// TODO - if mods allow unit/facility maintenance, lose those too?
				// TODO - check if SE4 "saves up" deficits between turns to destroy ships slower than one per turn
				var deficit = -emp.StoredResources.Values.Where(r => r < 0).Sum();
				var lostShips = deficit / Mod.Current.Settings.MaintenanceDeficitToDestroyOneShip;
				for (int i = 0; i < lostShips; i++)
				{
					var ship = emp.OwnedSpaceObjects.OfType<SpaceVehicle>().PickRandom();
					if (ship != null)
					{
						emp.Log.Add(ship.CreateLogMessage(ship + " fell into disrepair and was scuttled due to lack of funding for maintenance."));
						ship.Dispose();
					}
				}

				// don't let stored resources actually fall below zero
				foreach (var r in emp.StoredResources.Keys.Where(r => emp.StoredResources[r] < 0).ToArray())
					emp.StoredResources[r] = 0;

				// execute commands
				foreach (var cmd in emp.Commands.Where(cmd => cmd != null))
				{
					if (cmd.Issuer == emp)
						cmd.Execute();
					else
					{
						// no hacking!
						cmd.Issuer.Log.Add(new GenericLogMessage(cmd.Issuer.Name + " cannot issue a command to an object belonging to " + emp + "!"));
					}
				}

				// do research
				var Spending = emp.ResearchSpending;
				var Queue = emp.ResearchQueue;
				// spend research from spending % priorities
				var spendable = emp.NetIncome[Resource.Research] + emp.BonusResearch;
				foreach (var tech in Spending.Keys.ToArray())
					emp.Research(tech, Spending[tech] * spendable / 100);

				// spend research from queues
				var leftovers = (100 - Spending.Sum(kvp => kvp.Value)) * spendable / 100;
				while (Queue.Any() && leftovers > 0)
				{
					// first tech in queue
					var tech = Queue.First();
					var toSpend = Math.Min(leftovers, tech.GetNextLevelCost(emp));
					emp.Research(tech, toSpend);
					leftovers -= toSpend;
				}

				// no items queued?
				if (!Queue.Any() && leftovers > 0)
				{
					if (Spending.Any(kvp => kvp.Value > 0))
					{
						// pick tech with highest % focus
						emp.Research(Spending.Where(kvp => kvp.Value == Spending.Max(kvp2 => kvp2.Value)).First().Key, leftovers);
					}
					else
					{
						// no techs queued or prioritized, pick a random tech
						var tech = emp.AvailableTechnologies.PickRandom();
						if (tech != null)
							emp.Research(emp.AvailableTechnologies.PickRandom(), leftovers);
					}
				}

				// clear bonus research for this turn
				emp.BonusResearch = 0;

				if (status != null)
					status.Progress += progressPerOperation;
			}

			// validate fleets
			foreach (var f in Current.Referrables.OfType<Fleet>())
				f.Validate();

			// replenish shields
			if (status != null)
				status.Message = "Replenishing shields";
			foreach (var sobj in Current.FindSpaceObjects<ICombatSpaceObject>().Flatten().Flatten())
				sobj.ReplenishShields();
			if (status != null)
				status.Progress += progressPerOperation;

			// take history snapshot - beginning of turn
			if (status != null)
				status.Message = "Taking pre-turn history snapshot";
			Current.TakeSnapshot(false);
			if (status != null)
				status.Progress += progressPerOperation;

			// ship movement
			if (status != null)
				status.Message = "Moving ships";
			Current.CurrentTick = 0;
			foreach (var v in Current.Referrables.OfType<IMobileSpaceObject>().Shuffle())
				v.RefillMovement();
			while (Current.CurrentTick <= 1)
			{
				Current.ComputeNextTickSize();
				// Don't let ships in fleets move separate from their fleets!
				// Also don't let ships under construction take orders
				foreach (var v in Current.Referrables.OfType<IMobileSpaceObject>().Where(sobj => sobj.Container == null).Shuffle())
				{
					// mark system explored if not already
					var sys = v.StarSystem;
					if (sys == null)
						continue; // space object is dead, or not done being built

					v.ExecuteOrders();
					if (!sys.ExploredByEmpires.Contains(v.Owner) && v.Owner != null)
						v.Owner.ExploredStarSystems.Add(sys);

					// take history snapshot
					v.TakeSnapshot(false);

					// check for battles
					// TODO - alliances
					var sector = v.FindSector();
					if (v.Owner != null && sector != null && sector.SpaceObjects.OfType<ICombatObject>().Any(sobj => sobj.Owner != v.Owner && sobj.Owner != null))
					{
						var battle = new Battle(sector);
						battle.Resolve();
						foreach (var emp in battle.Empires)
							emp.Log.Add(battle.CreateLogMessage(battle.Name));
					}
				}
				Current.CurrentTick += Current.NextTickSize;
				if (status != null && Current.NextTickSize != double.PositiveInfinity)
					status.Progress += progressPerOperation * Current.NextTickSize;
			}

			// construction queues
			if (status != null)
				status.Message = "Constructing objects";
			foreach (var q in Current.Referrables.OfType<ConstructionQueue>().ToArray())
				q.ExecuteOrders();
			if (status != null)
				status.Progress += progressPerOperation;

			// TODO - more turn stuff? or do we have everything?

			if (status != null)
				status.Message = "Cleaning up";

			// deal with population in cargo again, in case colonies took damage and lost some population
			foreach (var p in Galaxy.Current.FindSpaceObjects<Planet>().Flatten().Flatten().Where(p => p.Colony != null))
			{
				var pop = p.Colony.Population;
				var ratio = (double)pop.Sum(kvp => kvp.Value) / (double)p.MaxPopulation;
				if (ratio < 1)
				{
					var cargo = p.Cargo;
					if (cargo != null)
					{
						// bring population out of cold storage
						// do this by removing and adding the population
						// this will work since population is removed from cargo storage first but added to population storage first
						foreach (var kvp in cargo.Population.ToArray())
						{
							var amount = kvp.Value;
							amount -= p.RemovePopulation(kvp.Key, kvp.Value);
							p.AddPopulation(kvp.Key, kvp.Value);
						}
					}
				}
			}

			// replenish shields again, so the players see the full shield amounts in the GUI
			foreach (var sobj in Current.FindSpaceObjects<ICombatSpaceObject>().Flatten().Flatten())
				sobj.ReplenishShields();

			// repair facilities
			foreach (var facility in Current.FindSpaceObjects<Planet>().Flatten().Flatten().Select(p => p.Colony).Where(c => c != null).SelectMany(c => c.Facilities))
				facility.Hitpoints = facility.MaxHitpoints;

			// resource spoilage
			foreach (var emp in Current.Empires)
				emp.StoredResources = ResourceQuantity.Min(emp.StoredResources, emp.ResourceStorage);

			// clear empire commands
			foreach (var emp in Current.Empires)
				emp.Commands.Clear();

			// clear completed orders
			foreach (var order in Current.Referrables.OfType<IOrder>().Where(o => o.IsComplete).ToArray())
				order.Dispose();

			// check for victory/defeat
			foreach (var vc in Current.Settings.VictoryConditions)
			{
				if (vc is TotalEliminationVictoryCondition || Current.Settings.TurnNumber > Current.Settings.VictoryDelay)
				{
					var winners = new List<Empire>();
					foreach (var emp in Current.Empires)
					{
						if (vc.GetProgress(emp) >= 1d)
						{
							// empire won!
							emp.Log.Add(emp.CreateLogMessage(vc.GetVictoryMessage(emp)));
						}
					}
					if (winners.Any())
					{
						foreach (var emp in Current.Empires.Where(e => !winners.Contains(e)))
						{
							// empire lost
							emp.Log.Add(emp.CreateLogMessage(vc.GetDefeatMessage(emp, winners)));
						}
					}
				}
			}

			// take history snapshot - end of turn
			if (status != null)
				status.Message = "Taking post-turn history snapshot";
			Current.TakeSnapshot(true);
			if (status != null)
				status.Progress += progressPerOperation;

			// update memory sight
			if (status != null)
				status.Message = "Updating memory sight caches";
			foreach (var emp in Current.Empires)
			{
				emp.Memory.Clear();
				foreach (var f in Current.Referrables.OfType<IFoggable>().Where(r => r.CheckVisibility(emp) >= Visibility.Visible))
					emp.UpdateMemory(f);
			}
			if (status != null)
				status.Progress += progressPerOperation;
		}

		/// <summary>
		/// Anything in the game that can be referenced from the client side
		/// using a Reference object instead of passing whole objects around.
		/// Stuff needs to be registered to be found though!
		/// </summary>
		internal IDictionary<long, IReferrable> referrables { get; private set; }

		public IEnumerable<IReferrable> Referrables { get { return referrables.Values; } }

		/// <summary>
		/// Assigns an ID to an object.
		/// </summary>
		/// <param name="r">The object.</param>
		/// <param name="id">The ID, or 0 to generate a new ID.</param>
		/// <returns>The new ID.</returns>
		internal long AssignID(IReferrable r, long id = 0)
		{
			if (referrables.ContainsKey(r.ID) && referrables[r.ID] == r)
				return r.ID; // no need to reassign ID

			if (CurrentEmpire != null)
			{
				// doesn't have an ID yet, and we're in a player GAM? then the player probably shouldn't see this object anyway...
				r.Dispose();
			}

			var oldid = r.ID;
			id = oldid;

			if (Referrables.LongCount() == long.MaxValue)
				throw new Exception("No more IDs are available to assign for objects.");

			while (id <= 0 || referrables.ContainsKey(id))
			{
				id = RandomHelper.Range(1L, long.MaxValue);
			}
			r.ID = id;
			referrables.Add(id, r);

			// clean up old IDs
			if (oldid > 0 && referrables.ContainsKey(oldid) && oldid != id)
				referrables.Remove(oldid);

			return id;
		}

		public void UnassignID(long id)
		{
			if (referrables.ContainsKey(id))
			{
				var r = referrables[id];
				r.ID = 0;
				referrables.Remove(id);
			}
		}

		public void UnassignID(IReferrable r)
		{
			if (r != null && referrables.ContainsKey(r.ID))
			{
				if (referrables[r.ID] == r)
				{
					r.ID = 0;
					referrables.Remove(r.ID);
				}
				else
				{
					r.ID = 0;
					var galaxyThinksTheIDIs = referrables.SingleOrDefault(kvp => kvp.Value == r);
					referrables.Remove(galaxyThinksTheIDIs);
				}
			}
		}

		/// <summary>
		/// Initializes a new game. Sets Galaxy.Current.
		/// </summary>
		/// <exception cref="InvalidOperationException">if there is no mod loaded.</exception>
		/// <param name="status">A status object to report status back to the GUI.</param>
		/// <param name="desiredProgress">How much progress should we report back to the GUI when we're done initializing the galaxy? 1.0 means all done with everything that needs to be done.</param>
		public static void Initialize(GameSetup gsu, Status status = null, double desiredProgress = 1.0)
		{
			if (Mod.Current == null)
				throw new InvalidOperationException("Cannot initialize a galaxy without a mod. Load a mod into Mod.Current first.");

			// create the game
			var galtemp = gsu.GalaxyTemplate;

			galtemp.GameSetup = gsu;
			var curProgress = status == null ? 0d : status.Progress;
			Current = galtemp.Instantiate(status, curProgress + (desiredProgress - curProgress) / 2d);
			gsu.PopulateGalaxy(Current);

			// set single player flag
			Current.Settings.IsSinglePlayer = gsu.IsSinglePlayer;

			if (status != null)
				status.Message = "Saving game";

			// create keyframes
			Galaxy.Current.TakeSnapshot(true);

			// save the game
			Galaxy.SaveAll(status, desiredProgress);
		}

		/// <summary>
		/// Searches for space objects matching criteria.
		/// </summary>
		/// <typeparam name="T">The type of space object.</typeparam>
		/// <param name="criteria">The criteria.</param>
		/// <returns>The matching space objects, grouped by location.</returns>
		public ILookup<ObjectLocation<StarSystem>, ILookup<Point, T>> FindSpaceObjects<T>(Func<T, bool> criteria = null) where T : ISpaceObject
		{
			var list = new List<Tuple<ObjectLocation<StarSystem>, ILookup<Point, T>>>();
			foreach (var ssl in StarSystemLocations)
			{
				var lookup = ssl.Item.FindSpaceObjects(criteria);
				if (lookup.Any())
					list.Add(Tuple.Create(ssl, lookup));
			}
			return list.ToLookup(t => t.Item1, t => t.Item2);
		}

		/// <summary>
		/// The next tick size, for ship movement.
		/// </summary>
		public double NextTickSize { get; private set; }

		/// <summary>
		/// The current tick in turn processing. 0 = start of turn, 1 = end of turn.
		/// </summary>
		public double CurrentTick { get; set; }

		public void ComputeNextTickSize()
		{
			var objs = Referrables.OfType<IMobileSpaceObject>().Where(obj => obj.Orders.Any());
			if (objs.Any() && CurrentTick < 1.0)
				NextTickSize = Math.Min(1.0 - CurrentTick, objs.Min(v => v.TimeToNextMove));
			else
				NextTickSize = double.PositiveInfinity;
		}

		#endregion
	}
}
