using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility.Extensions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using FrEee.Modding;
using System.Drawing;
using FrEee.Utility;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A galaxy in which the game is played.
	/// </summary>
	[Serializable]
	public class Galaxy
	{
		public Galaxy(Mod mod)
		{
			StarSystemLocations = new List<ObjectLocation<StarSystem>>();
			Empires = new List<Empire>();
			Name = "Unnamed";
			TurnNumber = 24000;
			OrderTargets = new List<IOrderable>();
			Mod = mod;
			Galaxy.Current = this;
		}

		#region Properties

		/// <summary>
		/// The current galaxy. Shouldn't change except at loading a game or turn processing.
		/// </summary>
		public static Galaxy Current { get; private set; }

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
			get { return MaxX - MinX + 1; }
		}

		public int Height
		{
			get { return MaxY - MinY + 1; }
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
				return (TurnNumber / 10.0).ToString("0.0");
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
			var bf = new BinaryFormatter();
			bf.Serialize(stream, this);
		}

		/// <summary>
		/// Deserializes the game state.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		private static Galaxy DeserializeGameState(Stream stream)
		{
			var bf = new BinaryFormatter();
			return (Galaxy)bf.Deserialize(stream);
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

			var bf = new BinaryFormatter();
			bf.Serialize(stream, CurrentEmpire.Commands);
		}

		/// <summary>
		/// Deserializes the player's commands.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		private static IList<ICommand> DeserializeCommands(Stream stream)
		{
			var bf = new BinaryFormatter();
			return (IList<ICommand>)bf.Deserialize(stream);
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
		/// Saves the master view and all players' views of the galaxy.
		/// </summary>
		/// <exception cref="InvalidOperationException">if CurrentEmpire is not null.</exception>
		public static void SaveAll()
		{
			if (Current.CurrentEmpire != null)
				throw new InvalidOperationException("Can only save player galaxy views from the master galaxy view.");
			var gamname = Current.Save();
			for (int i = 0; i < Current.Empires.Count; i++)
			{
				Load(gamname);
				Current.CurrentEmpire = Current.Empires[i];
				Current.Redact();
				Current.Save();
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
				{
					ssl.Item.Redact(this);
				}

				for (int i = 0; i < OrderTargets.Count; i++)
				{
					if (OrderTargets[i].Owner != CurrentEmpire)
						OrderTargets[i] = null;
				}

				foreach (var emp in Empires.Where(emp => emp != CurrentEmpire))
				{
					emp.StoredResources.Clear();
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

			// empire stuff
			foreach (var emp in Empires)
			{
				// give empire its income
				emp.StoredResources += emp.Income;

				// execute commands
				foreach (var cmd in emp.Commands)
				{
					if (cmd.Issuer == emp)
						cmd.Execute();
					else
					{
						// no hacking!
						Console.WriteLine(cmd.Issuer.Name + " cannot issue a command to an object belonging to " + emp + "!");
					}
				}
			}

			// construction queues
			foreach (var q in OrderTargets.OfType<ConstructionQueue>())
				q.ExecuteOrders();

			// TODO - more turn stuff
		}

		/// <summary>
		/// Anything in the game that can receive orders... stuff needs to be registered to be found though!
		/// </summary>
		public IList<IOrderable> OrderTargets { get; private set; }

		/// <summary>
		/// Registers something so it can receive orders.
		/// </summary>
		/// <param name="orderable"></param>
		public void Register(IOrderable orderable)
		{
			orderable.ID = OrderTargets.Count;
			OrderTargets.Add(orderable);
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

			gsu.Empires.Add(new Empire { Name = "Jraenar Empire", Color = Color.Red, EmperorTitle = "Master General", EmperorName = "Jar-Nolath" });
			gsu.Empires.Add(new Empire { Name = "Eee Consortium", Color = Color.Cyan });
			gsu.Empires.Add(new Empire { Name = "Drushocka Empire", Color = Color.Green });
			gsu.Empires.Add(new Empire { Name = "Norak Ascendancy", Color = Color.Blue });
			gsu.Empires.Add(new Empire { Name = "Abbidon Enclave", Color = Color.Orange });
			galtemp.GameSetup = gsu;
			Current = galtemp.Instantiate(status, desiredProgress);
			gsu.PopulateGalaxy(Current);

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

		#endregion
	}
}
