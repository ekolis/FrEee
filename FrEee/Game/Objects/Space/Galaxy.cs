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
			Galaxy.Current = this;
			StarSystemLocations = new List<ObjectLocation<StarSystem>>();
			Empires = new List<Empire>();
			Name = "Unnamed";
			TurnNumber = 1;
			referrables = new Dictionary<long, IReferrable>();
			VictoryConditions = new List<IVictoryCondition>();
		}

		public Galaxy(Mod mod)
			: this()
		{
			Mod = mod;
		}

		#region Properties

		/// <summary>
		/// The current galaxy. Shouldn't change except at loading a game or turn processing.
		/// </summary>
		public static Galaxy Current { get; private set; }

		/// <summary>
		/// Should players have an omniscient view of all explored systems?
		/// Does not prevent cloaking from working; this is just basic sight.
		/// Also does not give battle reports for other empires' battles.
		/// </summary>
		public bool OmniscientView { get; set; }

		/// <summary>
		/// Model to use for standard planetary mining.
		/// </summary>
		public MiningModel StandardMiningModel { get; set; }

		/// <summary>
		/// Model to use for remote mining.
		/// </summary>
		public MiningModel RemoteMiningModel { get; set; }

		public int MinPlanetValue { get; set; }

		public int MinSpawnedPlanetValue { get; set; }

		public int MaxSpawnedPlanetValue { get; set; }

		public int MaxPlanetValue { get; set; }

		public int MinAsteroidValue { get; set; }

		public int MinSpawnedAsteroidValue { get; set; }

		public int MaxSpawnedAsteroidValue { get; set; }

		/// <summary>
		/// Who can view empire scores?
		/// </summary>
		public ScoreDisplay ScoreDisplay { get; set; }

		/// <summary>
		/// Is this a single player game? If so, autoprocess the turn after the player takes his turn.
		/// </summary>
		public bool IsSinglePlayer { get; set; }

		/// <summary>
		/// The mod being played.
		/// </summary>
		public Mod Mod { get; set; }

		/// <summary>
		/// The game name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Technology research cost formula.
		/// Low = Level * BaseCost
		/// Medium = BaseCost for level 1, Level ^ 2 * BaseCost / 2 otherwise
		/// Hight = Level ^ 2 * BaseCost
		/// </summary>
		public TechnologyCost TechnologyCost { get; set; }

		/// <summary>
		/// The locations of the star systems in the galaxy.
		/// </summary>
		public ICollection<ObjectLocation<StarSystem>> StarSystemLocations { get; private set; }

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
					return Name + "_" + TurnNumber + "_" + PlayerNumber + FrEeeConstants.SaveGameExtension;
				else
					return Name + "_" + TurnNumber + FrEeeConstants.SaveGameExtension;
			}
		}

		public string CommandFileName
		{
			get
			{
				if (PlayerNumber > 0)
					return Name + "_" + TurnNumber + "_" + PlayerNumber + FrEeeConstants.PlayerCommandsSaveGameExtension;
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

		public int Width
		{
			get;
			set;
		}

		public int Height
		{
			get;
			set;
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
		/// The current turn number.
		/// </summary>
		public int TurnNumber { get; set; }

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
				return TurnNumber.ToStardate();
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

		/// <summary>
		/// Game victory conditions.
		/// </summary>
		public IList<IVictoryCondition> VictoryConditions { get; private set; }

		/// <summary>
		/// Delay in turns before victory conditions take effect.
		/// </summary>
		public int VictoryDelay { get; set; }

		/// <summary>
		/// Is this a "humans vs. AI" game?
		/// </summary>
		public bool IsHumansVsAI { get; set; }

		/// <summary>
		/// Allowed trades in this game.
		/// </summary>
		public AllowedTrades AllowedTrades { get; set; }

		public bool IsSurrenderAllowed { get; set; }

		public bool IsIntelligenceAllowed { get; set; }

		public bool IsAnalysisAllowed { get; set; }

		public bool CanColonizeOnlyBreathable { get; set; }

		public bool CanColonizeOnlyHomeworldSurface { get; set; }

		#endregion

		#region Data Access

		/// <summary>
		/// Serializes the game state.
		/// </summary>
		/// <returns></returns>
		private void SerializeGameState(Stream stream)
		{
			Serializer.Serialize(this, stream);
		}

		/// <summary>
		/// Deserializes the game state.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		private static Galaxy DeserializeGameState(Stream stream)
		{
			var gal = Serializer.Deserialize<Galaxy>(stream);
			// HACK - why isn't the deserializer setting the ID's?
			/*for (int id = 0; id < gal.Referrables.Count; id++)
			{
				if (gal.Referrables[id] != null)
					gal.Referrables[id].ID = id;
			}*/
			return gal;
		}

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

		/// <summary>
		/// Saves the game to an appropriately named file in the Savegame folder.
		/// Files are named GameName_TurnNumber_PlayerNumber.gam for players (PlayerNumber is 1-indexed)
		/// and GameName_TurnNumber.gam for the host.
		/// </summary>
		/// <returns>The filename saved to without the folder name (which is Savegame).</returns>
		public string Save()
		{
			AssignIDs();
			string filename;
			if (CurrentEmpire == null)
				filename = Name + "_" + TurnNumber + ".gam";
			else
				filename = Name + "_" + TurnNumber + "_" + (Empires.IndexOf(CurrentEmpire) + 1).ToString("d4") + ".gam";
			if (!Directory.Exists(FrEeeConstants.SaveGameDirectory))
				Directory.CreateDirectory(FrEeeConstants.SaveGameDirectory);
			var fs = new FileStream(Path.Combine(FrEeeConstants.SaveGameDirectory, filename), FileMode.Create);
			SerializeGameState(fs);
			fs.Close();
			return filename;
		}

		/// <summary>
		/// Saves the master view and all players' views of the galaxy, unless single player, in which case only the first player's view is saved.
		/// </summary>
		/// <exception cref="InvalidOperationException">if CurrentEmpire is not null.</exception>
		public static void SaveAll(Status status = null, double desiredProgress = 1d)
		{
			if (Current.CurrentEmpire != null)
				throw new InvalidOperationException("Can only save player galaxy views from the master galaxy view.");

			// recycle ID's
			/*for (int i = 0; i < Current.Referrables.Count; i++)
			{
				Current.Referrables[i] = Current.Referrables[i].Where(r => r != null).ToList();
			}*/

			var progressPerSaveLoad = (desiredProgress - (status == null ? 0d : status.Progress)) / (Current.IsSinglePlayer ? 3 : (Current.Empires.Count + 2));

			// save master view
			if (status != null)
				status.Message = "Saving game (host)";
			var gamname = Current.Save();
			if (status != null)
				status.Progress += progressPerSaveLoad;

			// save player views
			for (int i = 0; i < Current.Empires.Count; i++)
			{
				if (i == 0 || !Current.IsSinglePlayer)
				{
					if (status != null)
						status.Message = "Saving game (player " + (i + 1) + ")";
					// TODO - save player views without requiring reloading of master view each time (do it in memory)
					Load(gamname);
					Current.CurrentEmpire = Current.Empires[i];
					Current.Redact();
					Current.Save();
					if (status != null)
						status.Progress += progressPerSaveLoad;
				}
			}

			// TODO - only reload master view if we really need to
			if (status != null)
				status.Message = "Saving game";
			Load(gamname);
			if (status != null)
				status.Progress += progressPerSaveLoad;
		}

		/// <summary>
		/// Loads a savegame from the Savegame folder.
		/// Note that if it was renamed, it might have different game name, turn number, player number, etc. than the filename indicates.
		/// </summary>
		/// <param name="filename"></param>
		public static void Load(string filename)
		{
			var fs = new FileStream(Path.Combine(FrEeeConstants.SaveGameDirectory, filename), FileMode.Open);
			Galaxy.Current = DeserializeGameState(fs);
			Mod.Current = Galaxy.Current.Mod;
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
			Load(gameName + "_" + turnNumber + "_" + playerNumber + FrEeeConstants.SaveGameExtension);
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
					cmds = cmds.Where(cmd => cmd != null).ToList(); // HACK - why would we have null commands in a plr file?!
					fs.Close();
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
					/*idmap = new Dictionary<long, long>();
					foreach (var cmd in cmds)
					{
						// promote promotable objects
						foreach (var p in cmd.NewReferrables.Values)
						{
							if (p is IPromotable)
							{
								var oldid = p.ID;
								var newid = AssignID(p);
								idmap.Add(oldid, newid);
							}
							else
							{
								// TODO - constrain objects to be both promotable and referrable using a derived interface
								// do nothing
							}
						}
					}
					foreach (var cmd in cmds)
						cmd.ReplaceClientIDs(idmap); // convert client IDs to server IDs*/
				}
				else
					Console.WriteLine(emp.Name + " did not submit a PLR file.");
			}
		}

		private string GetEmpireCommandsSavePath(Empire emp)
		{
			return Path.Combine("Savegame", String.Format("{0}_{1}_{2:d4}{3}", Name, TurnNumber, Empires.IndexOf(emp) + 1, FrEeeConstants.PlayerCommandsSaveGameExtension));
		}

		private string GetEmpireSavePath()
		{
			return Path.Combine("Savegame", CurrentEmpire == null ?
				String.Format("{0}_{1}{2}", Name, TurnNumber, FrEeeConstants.SaveGameExtension) :
				String.Format("{0}_{1}_{2}{3}", Name, TurnNumber, Empires.IndexOf(CurrentEmpire) + 1, FrEeeConstants.SaveGameExtension));
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Removes any space objects, etc. that the current empire cannot see.
		/// </summary>
		public void Redact()
		{
			AssignIDs();
			if (CurrentEmpire != null)
			{
				foreach (var ssl in StarSystemLocations)
					ssl.Item.Redact(CurrentEmpire);

				foreach (var kvp in referrables.Where(kvp => kvp.Value is IFoggable).ToArray())
				{
					var id = kvp.Key;
					var obj = (IFoggable)kvp.Value;
					var vis = obj.CheckVisibility(CurrentEmpire);
					// TODO - memory sight - show only visible objects here, but also show memory caches of fogged objects
					if (vis < Visibility.Fogged)
						referrables.Remove(id);
				}

				foreach (var emp in Empires.Where(emp => emp != CurrentEmpire))
				{
					emp.StoredResources.Clear();
					emp.KnownDesigns.Clear();
					emp.Log.Clear();
					emp.ResearchedTechnologies.Clear();
					emp.AccumulatedResearch.Clear();
					emp.ResearchQueue.Clear();
					emp.ResearchSpending.Clear();
				}

				foreach (var d in CurrentEmpire.KnownDesigns.Where(d => d.Owner != CurrentEmpire))
				{
					d.VehiclesBuilt = 0;
				}
			}
		}

		/// <summary>
		/// Processes the turn.
		/// </summary>
		/// <exception cref="InvalidOperationException">if the current empire is not null, or this galaxy is not the current galaxy..</exception>
		public void ProcessTurn()
		{
			if (CurrentEmpire != null)
				throw new InvalidOperationException("Can't process the turn if there is a current empire. Load the game host's view of the galaxy instead.");

			if (Galaxy.Current != this)
				throw new InvalidOperationException("Can't process the turn on a galaxy that is not the current galaxy. Set Galaxy.Current = this first.");

			// load commands
			LoadCommands();

			// advance turn number
			TurnNumber++;

			// reproduction
			if (TurnNumber % (Mod.Current.Settings.ReproductionDelay == 0 ? 1 : Mod.Current.Settings.ReproductionDelay) == 0)
			{
				foreach (var p in FindSpaceObjects<Planet>(p => p.Colony != null).Flatten().Flatten())
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
							pop[race] = (long)(pop[race] / ratio);
					}
				}
			}

			// mining
			foreach (var tuple in FindSpaceObjects<Planet>(p => p.Owner != null).Squash())
			{
				var p = tuple.Item3;
				var sys = tuple.Item1.Item;
				if (sys.FindSpaceObjects<Planet>().Flatten().Any(p2 => p2.Owner == p.Owner && p2.HasAbility("Spaceport")))
				{
					// give owner his income
					p.Owner.StoredResources += p.Income;

					// adjust resource value
					foreach (var kvp in p.Income)
						p.ResourceValue[kvp.Key] -= StandardMiningModel.GetDecay(kvp.Value, p.ResourceValue[kvp.Key]);
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
						p.ResourceValue[kvp.Key] -= StandardMiningModel.GetDecay(kvp.Value, p.ResourceValue[kvp.Key]);
				}
			}

			// TODO - remote mining and raw resource generation

			// empire stuff
			foreach (var emp in Empires)
			{
				// pay maintenance on on ships/bases
				// TODO - allow mod to specify maintenance on units/facilities too?
				foreach (var v in emp.OwnedSpaceObjects.OfType<AutonomousSpaceVehicle>())
					emp.StoredResources -= v.MaintenanceCost;

				// if not enough funds, lose ships/bases
				// TODO - if mods allow unit/facility maintenance, lose those too?
				// TODO - check if SE4 "saves up" deficits between turns to destroy ships slower than one per turn
				var deficit = -emp.StoredResources.Values.Where(r => r < 0).Sum();
				var lostShips = deficit / Mod.Current.Settings.MaintenanceDeficitToDestroyOneShip;
				for (int i = 0; i < lostShips; i++)
				{
					var ship = emp.OwnedSpaceObjects.OfType<AutonomousSpaceVehicle>().PickRandom();
					if (ship != null)
					{
						emp.Log.Add(ship.CreateLogMessage(ship + " fell into disrepair and was scuttled due to lack of funding for maintenance."));
						ship.Dispose();
					}
				}

				// don't let stored resources actually fall below zero
				foreach (var r in emp.StoredResources.Keys.Where(r => emp.StoredResources[r] < 0))
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
			}

			// replenish shields
			foreach (var sobj in FindSpaceObjects<ICombatSpaceObject>().Flatten().Flatten())
				sobj.ReplenishShields();

			// construction queues
			foreach (var q in Referrables.OfType<ConstructionQueue>().ToArray())
				q.ExecuteOrders();

			// ship movement
			CurrentTick = 0;
			foreach (var v in Referrables.OfType<IMobileSpaceObject>().Shuffle())
				v.RefillMovement();
			while (CurrentTick <= 1)
			{
				ComputeNextTickSize();
				foreach (var v in Referrables.OfType<IMobileSpaceObject>().Shuffle())
				{
					// mark system explored if not already
					var sys = v.FindStarSystem();
					if (sys == null)
						continue; // space object is dead, or not done being built

					v.ExecuteOrders();
					if (!sys.ExploredByEmpires.Contains(v.Owner))
						sys.ExploredByEmpires.Add(v.Owner);

					// check for battles
					// TODO - alliances
					var sector = v.FindSector();
					if (sector != null && sector.SpaceObjects.OfType<ICombatObject>().Any(sobj => sobj.Owner != v.Owner && sobj.Owner != null))
					{
						var battle = new Battle(sector);
						battle.Resolve();
						foreach (var emp in battle.Empires)
							emp.Log.Add(battle.CreateLogMessage(battle.Name));
					}
				}
				CurrentTick += NextTickSize;
			}

			// TODO - more turn stuff

			// resource spoilage
			foreach (var emp in Empires)
				emp.StoredResources = ResourceQuantity.Min(emp.StoredResources, emp.ResourceStorage);

			// clear empire commands
			foreach (var emp in Empires)
				emp.Commands.Clear();

			// clear completed orders
			foreach (var order in Referrables.OfType<IOrder>().Where(o => o.IsComplete).ToArray())
				order.Dispose();

			// check for victory/defeat
			foreach (var vc in VictoryConditions)
			{
				if (vc is TotalEliminationVictoryCondition || TurnNumber > VictoryDelay)
				{
					var winners = new List<Empire>();
					foreach (var emp in Empires)
					{
						if (vc.GetProgress(emp) >= 1d)
						{
							// empire won!
							emp.Log.Add(emp.CreateLogMessage(vc.GetVictoryMessage(emp)));
						}
					}
					if (winners.Any())
					{
						foreach (var emp in Empires.Where(e => !winners.Contains(e)))
						{
							// empire lost
							emp.Log.Add(emp.CreateLogMessage(vc.GetDefeatMessage(emp, winners)));
						}
					}
				}
			}
		}

		/// <summary>
		/// Anything in the game that can be referenced from the client side
		/// using a Reference object instead of passing whole objects around.
		/// Stuff needs to be registered to be found though!
		/// </summary>
		internal IDictionary<long, IReferrable> referrables { get; private set; }

		public IEnumerable<IReferrable> Referrables { get { return referrables.Values; } }

		internal long AssignID(IReferrable r)
		{
			if (referrables.ContainsKey(r.ID) && referrables[r.ID] == r)
				return r.ID; // no need to reassign ID

			if (CurrentEmpire != null)
			{
				// doesn't have an ID yet, and we're in a player GAM? then the player probably shouldn't see this object anyway...
				r.Dispose();
			}

			var oldid = r.ID;

			if (Referrables.LongCount() == long.MaxValue)
				throw new Exception("No more IDs are available to assign for objects.");

			long id;
			do
			{
				id = RandomHelper.Range(1L, long.MaxValue);
			} while (referrables.ContainsKey(id));
			r.ID = id;
			referrables.Add(id, r);

			// clean up old IDs
			if (oldid > 0 && referrables.ContainsKey(oldid))
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
					throw new Exception("Can't unassign ID for " + r + " because it says it has ID=" + r.ID + " but the galaxy says that ID belongs to " + referrables[r.ID] + "!");
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
			Current.IsSinglePlayer = gsu.IsSinglePlayer;

			if (status != null)
				status.Message = "Saving game";

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
			var objs = Referrables.OfType<AutonomousSpaceVehicle>().Where(obj => obj.Orders.Any());
			if (objs.Any() && CurrentTick < 1.0)
				NextTickSize = Math.Min(1.0 - CurrentTick, objs.Min(v => v.TimeToNextMove));
			else
				NextTickSize = double.PositiveInfinity;
		}

		#endregion
	}
}
