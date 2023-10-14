using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FrEee.Enumerations;
using FrEee.Interfaces;
using FrEee.Objects.Abilities;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Diplomacy.Clauses;
using FrEee.Objects.Commands;
using FrEee.Objects.Events;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Setup;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee;

/// <summary>
/// An instance of the game.
/// </summary>
public class Game
{
	private Game(Mod mod, GameSetup setup, PRNG? dice = null)
	{
		Mod = mod;
		Setup = setup;
		Dice = dice ?? new PRNG(DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute);
	}

	/// <summary>
	/// Starts a new game. Sets the current game to this new game.
	/// </summary>
	/// <param name="mod">The mod to use for the game.</param>
	/// <param name="gameSetup">The game setup to use for the game.</param>
	/// <param name="dice">The random number generator to use for this game.</param>
	/// <param name="status">A status object to report status back to the GUI.</param>
	/// <param name="desiredProgress">How much progress should we report back to the GUI when we're done initializing the galaxy? 1.0 means all done with everything that needs to be done.</param>
	/// <returns>The new game.</returns>
	public static Game Start(Mod mod, GameSetup gameSetup, PRNG? dice = null, Status? status = null, double desiredProgress = 1.0)
	{
		Game game = new(mod, gameSetup, dice);
		game.Initialize(status ?? new Status(), desiredProgress);
		The.Game = game;
		return game;
	}

	/// <summary>
	/// The game name.
	/// </summary>
	public string Name { get; set; }

	private string modPath;

	/// <summary>
	/// The path to the mod being played.
	/// </summary>
	[SerializationPriority(1)]
	[ForceSerializationWhenDefaultValue]
	public string ModPath
	{
		get => modPath;
		set
		{
			if (Mod is null || Mod.RootPath != value)
				Mod = Mod.Load(value);
			modPath = value;
		}
	}

	/// <summary>
	/// The mod being played in this game.
	/// </summary>
	public Mod Mod { get; private set; }

	/// <summary>
	/// The setup of this game.
	/// </summary>
	public GameSetup Setup { get; private set; }

	/// <summary>
	/// The galaxy in which this game is being played.
	/// </summary>
	public Galaxy Galaxy { get; private set; }

	/// <summary>
	/// The random numer generator used by this game.
	/// </summary>
	public PRNG Dice { get; private set; }

	/// <summary>
	/// Is this a single-player game?
	/// </summary>
	public bool IsSinglePlayer { get; set; }

	/// <summary>
	/// The empires participating in the game.
	/// </summary>
	public IList<Empire> Empires { get; private set; } = new List<Empire>();

	/// <summary>
	/// The empire whose turn it is, or null if it's not an empire's turn.
	/// </summary>
	public Empire? CurrentEmpire { get; set; }

	/// <summary>
	/// The current player number (1 is the first player, 0 is the game host).
	/// </summary>
	public int PlayerNumber
	{
		get { return CurrentEmpire is null ? 0 : Empires.IndexOf(CurrentEmpire) + 1; }
	}

	/// <summary>
	/// The current turn number.
	/// </summary>
	public int TurnNumber { get; set; }

	/// <summary>
	/// The current stardate. Advances 0.1 years per turn.
	/// </summary>
	public string Stardate => TurnNumber.ToStardate();

	/// <summary>
	/// The current tick in turn processing. 0 = start of turn, 1 = end of turn.
	/// </summary>
	public double CurrentTick { get; set; }

	/// <summary>
	/// Current time equals turn number plus tick minus 1.
	/// </summary>
	public double Timestamp => TurnNumber + CurrentTick - 1;

	/// <summary>
	/// The next tick size, for ship movement.
	/// </summary>
	public double NextTickSize { get; internal set; }

	public void ComputeNextTickSize()
	{
		var objs = Galaxy.FindSpaceObjects<IMobileSpaceObject>().Where(obj => obj.Orders.Any());
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
	/// Notes that mod scripts can play with.
	/// </summary>
	public DynamicDictionary ScriptNotes { get; private set; } = new DynamicDictionary();

	/// <summary>
	/// Any referrable objects in the game, keyed by ID.
	/// </summary>
	public ReferrableRepository ReferrableRepository { get; private set; }

	/// <summary>
	/// Any referrable objects in the game.
	/// </summary>
	public IEnumerable<IReferrable> Referrables => ReferrableRepository.Values;

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

	private string stringValue;

	/// <summary>
	/// Serialized string value of the galaxy at the beginning of the turn.
	/// </summary>
	[DoNotSerialize]
	internal string StringValue
	{
		get
		{
			if (stringValue is null)
				StringValue = SaveToString(false);
			return stringValue!;
		}
		set
		{
			stringValue = value;
		}
	}

	/// <summary>
	/// Is the ability cache enabled?
	/// Always enabled on the client side; only when a flag is set on the server side.
	/// </summary>
	public bool IsAbilityCacheEnabled
	{
		get
		{
			return CurrentEmpire != null || isAbilityCacheEnabled;
		}
	}

	/// <summary>
	/// Events which have been warned of and are pending execution.
	/// </summary>
	public ICollection<Event> PendingEvents { get; private set; } = new List<Event>();

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
	/// Initializes a new game.
	/// </summary>
	/// <param name="status">A status object to report status back to the GUI.</param>
	/// <param name="desiredProgress">How much progress should we report back to the GUI when we're done initializing the galaxy? 1.0 means all done with everything that needs to be done.</param>
	private void Initialize(Status status, double desiredProgress)
	{
		// make a fake status so if we don't have one to avoid null checks later
		status ??= new Status();

		// create galaxy
		var startProgress = status.Progress;
		var progressPerStep = (desiredProgress - startProgress) / 4d;
		var galaxyTemplate = Setup.GalaxyTemplate;
		galaxyTemplate.GameSetup = Setup;
		var galaxy = galaxyTemplate.Instantiate(status, startProgress + progressPerStep, Dice);
		status.Message = "Populating galaxy";
		Setup.Initialize(this, Dice);
		SaveTechLevelsForUniqueness();
		status.Progress += progressPerStep;

		// run init script
		status.Message = "Executing script";
		PythonScriptEngine.RunScript<object>(Mod.GameInitScript);
		status.Progress += progressPerStep;

		// save the game
		status.Message = "Saving game";
		SaveAll(status, desiredProgress);
	}

	/// <summary>
	/// Saves the master view and all players' views of the game, unless single player, in which case only the first player's view is saved.
	/// </summary>
	/// <exception cref="InvalidOperationException">if CurrentEmpire is not null.</exception>
	public void SaveAll(Status status, double desiredProgress = 1d)
	{
		if (CurrentEmpire != null)
		{
			throw new InvalidOperationException("Can only save all player games from the master game.");
		}

		var progressPerSaveLoad = (desiredProgress - status.Progress) / (IsSinglePlayer ? 3 : Empires.Count + 2);

		// save master view
		status.Message = "Saving game (host)";
		var gamname = Save();
		status.Progress += progressPerSaveLoad;

		// save player views
		for (var i = 0; i < Empires.Count; i++)
		{
			// load, redact, and save master view
			Load(gamname);
			if (Empires[i].IsPlayerEmpire)
			{
				status.Message = "Saving game (player " + (i + 1) + ")";
				CurrentEmpire = Empires[i];
				Redact();
				Save(false); // already asssigned IDs in the redact phase
				status.Progress += progressPerSaveLoad;
			}
		}

		// TODO - only reload master view if we really need to
		status.Message = "Saving game";
		Load(gamname);
		status.Progress += progressPerSaveLoad;
	}

	public void Save(Stream stream, bool assignIDs = true)
	{
		if (assignIDs)
			CleanGameState();
		foreach (var kvp in ReferrableRepository.Where(kvp => kvp.Value.IsDisposed).ToArray())
			ReferrableRepository.UnassignID(kvp.Key);
		Serializer.Serialize(this, stream);
	}

	/// <summary>
	/// Saves the game to an appropriately named file in the Savegame folder.
	/// Files are named GameName_TurnNumber_PlayerNumber.gam for players (PlayerNumber is 1-indexed)
	/// and GameName_TurnNumber.gam for the host.
	/// </summary>
	/// <returns>The filename saved to without the folder name (which is Savegame).</returns>
	public string Save(bool assignIDs = true)
	{
		if (assignIDs)
			CleanGameState();
		foreach (var kvp in ReferrableRepository.Where(kvp => kvp.Value.ID < 0).ToArray())
			ReferrableRepository.UnassignID(kvp.Key);
		string filename;
		if (CurrentEmpire == null)
			filename = Name + "_" + TurnNumber + ".gam";
		else
			filename = Name + "_" + TurnNumber + "_" + (Empires.IndexOf(CurrentEmpire) + 1).ToString("d4") + ".gam";
		if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FrEeeConstants.SaveGameDirectory)))
			Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FrEeeConstants.SaveGameDirectory));
		var fs = new FileStream(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FrEeeConstants.SaveGameDirectory, filename), FileMode.Create);
		Serializer.Serialize(this, fs);
		fs.Close(); fs.Dispose();
		return filename;
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
		CleanGameState();
		if (CurrentEmpire == null)
			throw new InvalidOperationException("Can't save commands without a current empire.");
		foreach (var c in CurrentEmpire.Commands.OfType<SetPlayerInfoCommand>().ToArray())
			CurrentEmpire.Commands.Remove(c);
		CurrentEmpire.Commands.Add(new SetPlayerInfoCommand(CurrentEmpire) { PlayerInfo = CurrentEmpire.PlayerInfo });
		if (!Directory.Exists(FrEeeConstants.SaveGameDirectory))
			Directory.CreateDirectory(FrEeeConstants.SaveGameDirectory);
		var filename = GetEmpireCommandsSavePath(CurrentEmpire);
		var fs = new FileStream(filename, FileMode.Create);
		SerializeCommands(fs);
		fs.Close(); fs.Dispose();

		// save library of designs, commands, etc.
		Library.Save();

		return filename;
	}

	public string SaveToString(bool assignIDs = true)
	{
		if (assignIDs)
			CleanGameState();
		return Serializer.SerializeToString(this);
	}

	/// <summary>
	/// Removes any space objects, etc. that the current empire cannot see.
	/// </summary>
	public void Redact()
	{
		// nothing to do if there's no current empire
		if (CurrentEmpire is null)
		{
			return;
		}

		// save off empire scores first, before data is removed
		foreach (var emp in Empires)
		{
			emp.Scores[TurnNumber] = emp.ComputeScore(CurrentEmpire);
		}

		// the game data itself
		ScriptNotes.Clear();

		// redact sub objects
		var parser = new ObjectGraphParser();
		parser.StartObject += redactParser_StartObject;
		parser.Parse(this);

		// clean up redacted objects that are not IFoggable
		foreach (var x in Galaxy.StarSystemLocations.Where(x => x.Item.IsDisposed).ToArray())
			Galaxy.StarSystemLocations.Remove(x);

		// delete memories since they've been copied to "physical" objects already
		foreach (var kvp in CurrentEmpire.Memory.ToArray())
		{
			kvp.Value.Dispose();
			CurrentEmpire.Memory.Remove(kvp);
		}
	}

	private void redactParser_StartObject(object o)
	{
		if (o is IReferrable r)
		{
			ReferrableRepository.AssignID(r);

			if (o is IFoggable obj)
			{
				if (!obj.IsMemory)
				{
					var id = r.ID;
					var vis = obj.CheckVisibility(CurrentEmpire);
					if (vis < Visibility.Fogged)
						obj.Dispose();
					if (vis == Visibility.Fogged && CurrentEmpire.Memory.ContainsKey(id))
					{
						var mem = (IReferrable)CurrentEmpire.Memory[id];
						mem.CopyToExceptID(r, IDCopyBehavior.PreserveDestination); // memory sight!
						if (mem is ILocated l1 && r is ILocated l2)
							l2.Sector = l1.Sector; // hack to copy location just now without always copying it when an object is copied
						obj.IsMemory = true;
					}
					obj.Redact(CurrentEmpire);
				}
				else
				{
					// memories are only visible to the empire which is seeing them!
					// well unless we add some sort of intel project to see them or something...
					if (CurrentEmpire != null && !CurrentEmpire.Memory.Values.Contains(obj))
						obj.Dispose();
				}
			}
		}
	}

	/// <summary>
	/// Loads a savegame from the Savegame folder.
	/// Note that if it was renamed, it might have different game name, turn number, player number, etc. than the filename indicates.
	/// </summary>
	/// <param name="filename"></param>
	public static Game Load(string filename)
	{
		var fs = new FileStream(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FrEeeConstants.SaveGameDirectory, filename), FileMode.Open);
		var game = Serializer.Deserialize<Game>(fs);
		if (game.ModPath == null)
			game.Mod = Mod.Load(null); // skipped in deserialization because it is null but the mod needs to be loaded!
		if (The.Empire != null)
		{
			// load library of designs, strategies, etc.
			Library.Load();
		}
		fs.Close();
		fs.Dispose();
		game.PopulatePropertyValues();
		return game;
	}

	/// <summary>
	/// Loads a host savegame from the Savegame folder.
	/// </summary>
	/// <param name="gameName"></param>
	/// <param name="turnNumber"></param>
	public static Game Load(string gameName, int turnNumber)
		=> Load(gameName + "_" + turnNumber + FrEeeConstants.SaveGameExtension);

	/// <summary>
	/// Loads a player savegame from the Savegame folder.
	/// </summary>
	/// <param name="gameName"></param>
	/// <param name="turnNumber"></param>
	/// <param name="playerNumber"></param>
	public static Game Load(string gameName, int turnNumber, int playerNumber)
		=> Load(gameName + "_" + turnNumber + "_" + playerNumber.ToString("d4") + FrEeeConstants.SaveGameExtension);

	/// <summary>
	/// Loads from a string in memory.
	/// </summary>
	/// <param name="serializedData"></param>
	public static Game LoadFromString(string serializedData)
	{
		var game = Serializer.DeserializeFromString<Game>(serializedData);
		//Current.SpaceObjectIDCheck("after loading from memory");


		if (game.ModPath is null)
			game.Mod = Mod.Load(null); // skipped in deserialization because it is null but the mod needs to be loaded!

		if (The.Empire is not null)
		{
			// initialize IronPython galaxy on load
			game.StringValue = serializedData;
			var formula = new ComputedFormula<int>("The.Game.TurnNumber", null, true);
			var turn = formula.Value;

			// load library of designs, strategies, etc.
			Library.Load();
		}

		game.PopulatePropertyValues();

		return game;
	}

	/// <summary>
	/// Assigns IDs to referrable objects in the galaxy and purges disposed objects.
	/// Doesn't assign IDs to objects via DoNotAssignID properties, or to memories (or sub-objects of them).
	/// </summary>
	public void CleanGameState()
	{
		var parser = new ObjectGraphParser();
		var canAssign = true;
		foreach (var kvp in ReferrableRepository.ToArray())
		{
			if (kvp.Value.IsDisposed)
				ReferrableRepository.UnassignID(kvp.Key);
		}
		parser.Property += (pname, o, val) =>
		{
			var prop = o.GetType().FindProperty(pname);
			var isMemory = val is IFoggable f && f.IsMemory;
			canAssign = !prop.HasAttribute<DoNotAssignIDAttribute>() && !isMemory;
			if (isMemory)
				return false; // no recursion!
			if (prop.GetAttributes<DoNotAssignIDAttribute>().Any(a => a.Recurse))
				return false; // no recursion!
			else
				return true;
		};
		var colls = new List<IEnumerable>();
		parser.StartObject += o =>
		{
			if (o is IReferrable && canAssign)
			{
				var r = (IReferrable)o;
				ReferrableRepository.AssignID(r);
			}
			if (o is IEnumerable)
			{
				colls.Add((IEnumerable)o);
			}
		};
		parser.EndObject += o =>
		{
			if (o is IEnumerable)
			{
				colls.Remove((IEnumerable)o);
			}
		};
		parser.Parse(this);
		foreach (var l in Galaxy.StarSystemLocations.ToArray())
		{
			if (l.Item == null)
				Galaxy.StarSystemLocations.Remove(l);
			else
			{
				foreach (var l2 in l.Item.SpaceObjectLocations.ToArray())
				{
					if (l2.Item == null || l2.Item.IsDisposed)
						l.Item.SpaceObjectLocations.Remove(l2);
				}
			}
		}
	}

	/// <summary>
	/// Populates property values specified by <see cref="PopulateAttribute{T}"/>.
	/// </summary>
	private void PopulatePropertyValues()
	{
		// TODO: cache list of properties to populate when deserializing?
		// enumerate all referrables
		foreach (var referrable in ReferrableRepository)
		{
			// find referrable's properties
			var props = referrable.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (var prop in props)
			{
				// search property's attributes for PopulateAttribute<T>
				foreach (var att in prop.GetCustomAttributes())
				{
					if (att.GetType().IsGenericType && att.GetType().GetGenericTypeDefinition() == typeof(PopulateAttribute<>))
					{
						// found PopulateAttribute<T>
						// create populator
						var populatorType = att.GetType().GetGenericArguments()[0];
						var populator = (IPopulator)populatorType.Instantiate();

						// get value from populator and save it into the referrable's property
						prop.SetValue(referrable, populator.Populate(referrable));
					}
				}
			}
		}
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

	public string GetGameSavePath(Empire? emp = null)
	{
		if (emp is null)
			emp = CurrentEmpire;
		return GetGameSavePath(Name, TurnNumber, emp == null ? 0 : Empires.IndexOf(emp) + 1);
	}

	public static string GetGameSavePath(string gameName, int turnNumber, int empireNumber)
	{
		return Path.Combine(RootDirectory, "Savegame", empireNumber < 1 ?
			string.Format("{0}_{1}{2}", gameName, turnNumber, FrEeeConstants.SaveGameExtension) :
			string.Format("{0}_{1}_{2:d4}{3}", gameName, turnNumber, empireNumber, FrEeeConstants.SaveGameExtension));
	}

	public string GetEmpireCommandsSavePath(Empire emp)
		=> GetEmpireCommandsSavePath(Name, TurnNumber, Empires.IndexOf(emp) + 1);

	public static string GetEmpireCommandsSavePath(string gameName, int turnNumber, int empireNumber)
		=> Path.Combine(RootDirectory, "Savegame", string.Format("{0}_{1}_{2:d4}{3}", gameName, turnNumber, empireNumber, FrEeeConstants.PlayerCommandsSaveGameExtension));

	public string CommandFileName
	{
		get
		{
			if (PlayerNumber > 0)
			{
				return Name + "_" + TurnNumber + "_" + PlayerNumber + FrEeeConstants.PlayerCommandsSaveGameExtension;
			}
			else
			{
				// TODO: allow game host commands like in THANCS?
				throw new InvalidOperationException("The game host does not have a command file.");
			}
		}
	}
	/// <summary>
	/// The root directory where the FrEee executable is located.
	/// </summary>
	public static string RootDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

	/// <summary>
	/// Saves all empires' tech levels in the other empires for uniqueness calculations.
	/// </summary>
	internal void SaveTechLevelsForUniqueness()
	{
		if (Setup.TechnologyUniqueness != 0)
		{
			foreach (var emp in Empires)
			{
				emp.OtherPlayersTechLevels.Clear();
				foreach (var emp2 in Empires.ExceptSingle(emp))
				{
					foreach (var tech in Mod.Technologies)
					{
						if (emp.OtherPlayersTechLevels[tech] == null)
							emp.OtherPlayersTechLevels[tech] = new List<int>();
						emp.OtherPlayersTechLevels[tech].Add(emp2.ResearchedTechnologies[tech]);
					}
				}
			}
		}
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
				var serverid = ReferrableRepository.AssignID(r);
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

	public override string ToString()
	{
		if (CurrentEmpire is null)
			return Name;
		return Name + " - " + CurrentEmpire.Name + " - " + CurrentEmpire.LeaderName + " - " + Stardate;
	}

	#region move this to an AbilityManager or something
	private bool isAbilityCacheEnabled;

	/// <summary>
	/// Cache of abilities that are shared to empires from other objects due to treaties.
	/// </summary>
	[DoNotSerialize]
	internal SafeDictionary<Tuple<IOwnableAbilityObject, Empire>, IEnumerable<Ability>> SharedAbilityCache { get; private set; }

	/// <summary>
	/// Cache of abilities belonging to game objects.
	/// </summary>
	[DoNotSerialize]
	internal SafeDictionary<IAbilityObject, IEnumerable<Ability>> AbilityCache { get; private set; }

	/// <summary>
	/// Cache of abilities belonging to common game objects that can have different abilities for each empire.
	/// </summary>
	[DoNotSerialize]
	internal SafeDictionary<Tuple<ICommonAbilityObject, Empire>, IEnumerable<Ability>> CommonAbilityCache { get; private set; }
	#endregion

	#region move this to a TreatyManager or something
	/// <summary>
	/// Cache of treaty clauses given by empires.
	/// </summary>
	[DoNotSerialize]
	internal SafeDictionary<Empire, ILookup<Empire, Clause>> GivenTreatyClauseCache { get; set; }

	/// <summary>
	/// Cache of treaty clauses received by empires.
	/// </summary>
	[DoNotSerialize]
	internal SafeDictionary<Empire, ILookup<Empire, Clause>> ReceivedTreatyClauseCache { get; set; }
	#endregion
}
