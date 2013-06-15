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
			StarSystemLocations = new List<ObjectLocation<StarSystem>>();
			Empires = new List<Empire>();
			Name = "Unnamed";
			TurnNumber = 24000;
			Referrables = new List<IList<IReferrable>>();
		}

		public Galaxy(Mod mod)
			: this()
		{
			Mod = mod;
			Galaxy.Current = this;

			// register objects that need shared client/server ID's
			foreach (var componentTemplate in Mod.ComponentTemplates)
				Register(componentTemplate);
			foreach (var mount in Mod.Mounts)
				Register(mount);
			foreach (var facilityTemplate in Mod.FacilityTemplates)
				Register(facilityTemplate);
			foreach (var hull in Mod.Hulls)
				Register(hull);
			foreach (var tech in Mod.Technologies)
				Register(tech);
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

		public int MinPlanetValue {get; set;}

		public int MinSpawnedPlanetValue {get; set;}

		public int MaxSpawnedPlanetValue {get; set;}

		public int MaxPlanetValue {get; set;}

		public int MinAsteroidValue {get; set;}

		public int MinSpawnedAsteroidValue {get; set;}

		public int MaxSpawnedAsteroidValue {get; set;}

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
		/// Saves the game to an appropriately named file in the Savegame folder.
		/// Files are named GameName_TurnNumber_PlayerNumber.gam for players (PlayerNumber is 1-indexed)
		/// and GameName_TurnNumber.gam for the host.
		/// </summary>
		/// <returns>The filename saved to without the folder name (which is Savegame).</returns>
		public string Save()
		{
			string filename;
			if (CurrentEmpire == null)
				filename = Name + "_" + TurnNumber + ".gam";
			else
				filename = Name + "_" + TurnNumber + "_" + (Empires.IndexOf(CurrentEmpire) + 1) + ".gam";
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
		public static void SaveAll()
		{
			if (Current.CurrentEmpire != null)
				throw new InvalidOperationException("Can only save player galaxy views from the master galaxy view.");

			// recycle ID's
			for (int i = 0; i < Current.Referrables.Count; i++)
			{
				Current.Referrables[i] = Current.Referrables[i].Where(r => r != null).ToList();
			}

			// save master view
			var gamname = Current.Save();
	
			// save player views
			for (int i = 0; i < Current.Empires.Count; i++)
			{
				if (i == 0 || !Current.IsSinglePlayer)
				{
					Load(gamname);
					Current.CurrentEmpire = Current.Empires[i];
					Current.Redact();
					Current.Save();
				}
			}
			Load(gamname);
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
		/// Files are named GameName_TurnNumber_PlayerNumber.plr. (PlayerNumber is 1-indexed)
		/// This doesn't make sense for the host view, so an exception will be thrown if there is no current empire.
		/// </summary>
		/// <returns>The filename saved to without the folder name (which is Savegame).</returns>
		/// <exception cref="InvalidOperationException">if there is no current empire.</exception>
		public string SaveCommands()
		{
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
					fs.Close();
					emp.Commands.Clear();
					foreach (var cmd in cmds)
						emp.Commands.Add(cmd);
				}
				else
					Console.WriteLine(emp.Name + " did not submit a PLR file.");
			}
		}

		private string GetEmpireCommandsSavePath(Empire emp)
		{
			return Path.Combine("Savegame", String.Format("{0}_{1}_{2}{3}", Name, TurnNumber, Empires.IndexOf(emp) + 1, FrEeeConstants.PlayerCommandsSaveGameExtension));
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
			if (CurrentEmpire != null)
			{
				foreach (var ssl in StarSystemLocations)
					ssl.Item.Redact(this);

				for (int i = 0; i < Referrables[0].Count; i++)
				{
					if (Referrables[0][i].Owner != CurrentEmpire && Referrables[0][i].Owner != null)
						Referrables[0][i] = null; // keep stuff with the same indices so PLR files can find it
				}
				for (int i = 1; i < Referrables.Count; i++)
				{
					if (i != Empires.IndexOf(CurrentEmpire) + 1)
						Referrables[i] = null;
				}
				if (Referrables.Count > Empires.IndexOf(CurrentEmpire) + 1)
				{
					for (int i = 0; i < Referrables[Empires.IndexOf(CurrentEmpire) + 1].Count; i++)
					{
						if (Referrables[Empires.IndexOf(CurrentEmpire) + 1][i].Owner != CurrentEmpire && Referrables[Empires.IndexOf(CurrentEmpire) + 1][i].Owner != null)
							Referrables[Empires.IndexOf(CurrentEmpire) + 1][i] = null; // keep stuff with the same indices so PLR files can find it
					}
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

			// mining
			foreach (var p in FindSpaceObjects<Planet>(p => p.Owner != null).Flatten().Flatten())
			{
				// give owner his income
				p.Owner.StoredResources += p.Income;
				p.Owner.StoredResources = Resources.Min(p.Owner.StoredResources, p.Owner.ResourceStorage);

				// adjust resource value
				foreach (var kvp in p.Income)
					p.ResourceValue[kvp.Key] -= StandardMiningModel.GetDecay(kvp.Value, p.ResourceValue[kvp.Key]);
			}

			// TODO - remote mining and raw resource generation

			// empire stuff
			foreach (var emp in Empires)
			{
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
				// TODO - first turn, allow player to spend first turn bonus research from game setup
				var Spending = emp.ResearchSpending;
				var Queue = emp.ResearchQueue;
				// spend research from spending % priorities
				foreach (var tech in Spending.Keys)
					emp.Research(tech, Spending[tech] * emp.Income[Resource.Research] / 100);

				// spend research from queues
				var leftovers = (100 - Spending.Sum(kvp => kvp.Value)) * emp.Income[Resource.Research] / 100;
				if (Queue.Any())
					// first tech in queue
					emp.Research(Queue.First(), leftovers);
				else if (Spending.Any(kvp => kvp.Value > 0))
					// no queued techs, pick tech with highest % focus
					emp.Research(Spending.Where(kvp => kvp.Value == Spending.Max(kvp2 => kvp2.Value)).First().Key, leftovers);
				else
					// no techs queued or prioritized, pick a random tech
					emp.Research(emp.AvailableTechnologies.PickRandom(), leftovers);

			}

			// replenish shields
			foreach (var sobj in FindSpaceObjects<ICombatSpaceObject>().Flatten().Flatten())
				sobj.ReplenishShields();

			// construction queues
			foreach (var q in Referrables.SelectMany(g => g).OfType<ConstructionQueue>().ToArray())
				q.ExecuteOrders();

			// ship movement
			CurrentTick = 0;
			foreach (var v in Referrables.SelectMany(g => g).OfType<IMobileSpaceObject>().Shuffle())
				v.RefillMovement();
			while (CurrentTick < 1)
			{
				ComputeNextTickSize();
				foreach (var v in Referrables.SelectMany(g => g).OfType<IMobileSpaceObject>().Shuffle())
				{
					v.ExecuteOrders();
					
					// check for battles
					// TODO - alliances
					var sector = v.FindSector();
					if (sector != null && sector.SpaceObjects.OfType<ICombatObject>().Any(sobj => sobj.Owner != v.Owner && sobj.Owner != null))
					{
						var battle = new Battle(sector);
						battle.Resolve();
						foreach (var emp in battle.Empires)
							emp.Log.Add(battle.CreateLogMessage(battle.Name));
						
						// check for defeated empires
						foreach (var emp in Empires)
						{
							if (emp.IsDefeated)
							{
								foreach (var emp2 in Empires)
								{
									if (emp2 == emp)
										emp2.Log.Add(emp.CreateLogMessage("You have been defeated! Your colonies and fleets are no more..."));
									else
										emp2.Log.Add(emp.CreateLogMessage(emp + " has been defeated!"));
								}
							}
						}
						
						// check for victory
						var survivors = Empires.Where(emp => !emp.IsDefeated);
						if (survivors.Count() == 1)
						{
							var winner = survivors.First();
							winner.Log.Add(winner.CreateLogMessage("You are victorious! You have eliminated all opposition and conquered the galaxy!"));
						}
					}
				}
				CurrentTick += NextTickSize;
			}

			// TODO - more turn stuff

			// clear empire commands
			foreach (var emp in Empires)
				emp.Commands.Clear();
		}

		/// <summary>
		/// Anything in the game that can be referenced from the client side
		/// using a Reference object instead of passing whole objects around.
		/// Stuff needs to be registered to be found though!
		/// </summary>
		public IList<IList<IReferrable>> Referrables { get; private set; }

		/// <summary>
		/// Registers something so it can be referenced from the client side by a specific player.
		/// </summary>
		/// <param name="orderable"></param>
		public int Register(IReferrable r, Empire emp)
		{
			var empnum = Empires.IndexOf(emp) + 1;
			while (Referrables.Count <= empnum)
				Referrables.Add(new List<IReferrable>());
			if (!Referrables[empnum].Contains(r))
			{
				Referrables[empnum].Add(r); // add to player list
				int id = Referrables[empnum].Count - 1;
				return id;
			}
			else
				return Referrables[empnum].IndexOf(r);
		}

		/// <summary>
		/// Registers something so it can be referenced from the client side by all players.
		/// </summary>
		/// <param name="r"></param>
		public int Register(IReferrable r)
		{
			if (!Referrables.Any())
				Referrables.Add(new List<IReferrable>());
			if (!Referrables[0].Contains(r))
			{
				Referrables[0].Add(r); // add to global list
				int id = Referrables[0].Count - 1;
				return id;
			}
			else
			{
				int id = Referrables[0].IndexOf(r);
				return id;
			}
		}

		/// <summary>
		/// Unregisters something so it can no longer be referenced from the client side by all players.
		/// </summary>
		/// <param name="r"></param>
		public void Unregister(IReferrable r)
		{
			var id = Referrables[0].IndexOf(r);
			if (id >= 0)
				Referrables[0][id] = null;
		}

		/// <summary>
		/// Unregisters something so it can no longer be referenced from the client side by a specific player.
		/// </summary>
		/// <param name="r"></param>
		public void Unregister(IReferrable r, Empire emp)
		{
			var empnum = Empires.IndexOf(emp) + 1;
			var id = Referrables[empnum].IndexOf(r);
			if (id >= 0)
				Referrables[empnum][id] = null;
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
			var galtemp = Mod.Current.GalaxyTemplates.PickRandom();

			galtemp.GameSetup = gsu;
			Current = galtemp.Instantiate(status, desiredProgress);
			gsu.PopulateGalaxy(Current);

			// set single player flag
			Current.IsSinglePlayer = gsu.IsSinglePlayer;

			if (status != null)
				status.Message = "Saving game";

			// save the game
			Galaxy.SaveAll();
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
			if (objs.Any())
				NextTickSize = Math.Min(1.0 - CurrentTick, objs.Min(v => v.TimeToNextMove));
			else
				NextTickSize = 1.0 - CurrentTick;
		}

		#endregion
	}
}
