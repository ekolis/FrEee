using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility.Extensions;
using System.Runtime.Serialization.Formatters.Binary;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A galaxy in which the game is played.
	/// </summary>
	 [Serializable] public class Galaxy
	{
		public Galaxy()
		{
			StarSystemLocations = new List<ObjectLocation<StarSystem>>();
			Empires = new List<Empire>();
			Name = "Unnamed";
			TurnNumber = 24000;
			OrderTargets = new List<IOrderable>();
		}

		#region Properties

		/// <summary>
		/// The current galaxy. Shouldn't change except at loading a game or turn processing.
		/// </summary>
		public static Galaxy Current { get; set; }

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
		public void SerializeGameState(Stream stream)
		{
			var bf = new BinaryFormatter();
			bf.Serialize(stream, this);
		}

		/// <summary>
		/// Deserializes the game state.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static Galaxy DeserializeGameState(TextReader reader)
		{
			return JsonSerializer.Deserialize<Galaxy>(new JsonTextReader(reader));
		}

		/// <summary>
		/// Serializes the player's commands.
		/// </summary>
		/// <exception cref="InvalidOperationException">if no current empire</exception>
		/// <returns></returns>
		public void SerializeCommands(Stream stream)
		{
			if (CurrentEmpire == null)
				throw new InvalidOperationException("Can't serialize commands if there is no current empire.");

			var bf = new BinaryFormatter();
			bf.Serialize(stream, CurrentEmpire.Commands);
		}

		/// <summary>
		/// Deserializes the game state.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static Galaxy DeserializeGameState(Stream stream)
		{
			var bf = new BinaryFormatter();
			return (Galaxy)bf.Deserialize(stream);
		}

		/// <summary>
		/// Deserializes the player's commands.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static IList<ICommand> DeserializeCommands(Stream stream)
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
		/// Loads a savegame from the Savegame folder.
		/// Note that if it was renamed, it might have different game name, turn number, player number, etc. than the filename indicates.
		/// </summary>
		/// <param name="filename"></param>
		public static Galaxy Load(string filename)
		{
			var sr = new StreamReader(Path.Combine(FrEeeConstants.SaveGameDirectory, filename));
			var gal = DeserializeGameState(sr);
			sr.Close();
			return gal;
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
			var sw = new StreamWriter(Path.Combine(FrEeeConstants.SaveGameDirectory, GetEmpireCommandsSavePath()));
			sw.Write(SerializeCommands());
			sw.Close();
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
				var fs = new FileStream(GetEmpireCommandsSavePath());
				var cmds = DeserializeCommands(fs);
				fs.Close();
				emp.Commands.Clear();
				foreach (var cmd in cmds)
					emp.Commands.Add(cmd);
			}
		}

		private string GetEmpireCommandsSavePath()
		{
			return String.Format("{0}_{1}_{2}{3}", Name, TurnNumber, Empires.IndexOf(CurrentEmpire) + 1, FrEeeConstants.PlayerCommandsSaveGameExtension);
		}

		private string GetEmpireSavePath()
		{
			return CurrentEmpire == null ?
				String.Format("{0}_{1}{2}", Name, TurnNumber, FrEeeConstants.SaveGameExtension) :
				String.Format("{0}_{1}_{2}{3}", Name, TurnNumber, Empires.IndexOf(CurrentEmpire) + 1, FrEeeConstants.SaveGameExtension);
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

			// empire stuff
			foreach (var emp in Empires)
			{
				// give empire its incom				emp.StoredResources += emp.Income;

				// execute commands
				foreach (var cmd in emp.Commands)
					cmd.Execute();
			}

			// TODO - other turn processing stuff
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

		#endregion
	}
}
