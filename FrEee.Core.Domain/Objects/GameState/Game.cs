using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Diplomacy.Clauses;
using FrEee.Objects.Events;
using FrEee.Objects.LogMessages;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.Space;
using FrEee.Processes.Combat;
using FrEee.Modding.Abilities;
using FrEee.Processes.Setup;
using FrEee.Modding.Scripts;
using FrEee.Modding.Loaders;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Notes;
using FrEee.Vehicles.Types;
using FrEee.Processes.Construction;
using FrEee.Persistence;

namespace FrEee.Objects.GameState;

/// <summary>
/// A game of FrEee.
/// </summary>
[Serializable]
public class Game
{
	public Game()
	{
		Current = this;
		if (Mod.Current != null)
			ModPath = Mod.Current.RootPath;
		Empires = new List<Empire>();
		Name = "Unnamed";
		TurnNumber = 1;
		referrables = new Dictionary<long, IReferrable>();
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
	/// The current game. Shouldn't change except at loading a game or turn processing.
	/// </summary>
	public static Game? Current { get; set; }

	/// <summary>
	/// The galaxy in which this game is played.
	/// </summary>
	public Galaxy Galaxy { get; set; }

	/// <summary>
	/// The battles which have taken place this turn.
	/// </summary>
	public ICollection<IBattle> Battles { get; private set; }

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

	/// <summary>
	/// The empire whose turn it is, or null if it's not any empire's turn.
	/// </summary>
	public Empire? CurrentEmpire { get; set; }

	/// <summary>
	/// The current tick in turn processing. 0 = start of turn, 1 = end of turn.
	/// </summary>
	public double CurrentTick { get; set; }

	/// <summary>
	/// The empires participating in the game.
	/// </summary>
	public IList<Empire> Empires { get; private set; }

	/// <summary>
	/// Per mille chance of a random event occurring, per turn, per player.
	/// </summary>
	public double EventFrequency { get; set; }

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

	/// <summary>
	/// Is the ability cache enabled?
	/// Always enabled on the client side; only when a flag is set on the server side.
	/// </summary>
	public bool IsAbilityCacheEnabled
	{
		get
		{
			return Empire.Current != null || isAbilityCacheEnabled;
		}
	}

	/// <summary>
	/// Is this a single player game? If so, autoprocess the turn after the player takes his turn.
	/// </summary>
	public bool IsSinglePlayer { get; set; }

	private string modPath;

	/// <summary>
	/// The path to the mod being played, or null for the stock mod.
	/// </summary>
	/// <remarks>
	/// Setting this will load the specified mod.
	/// </remarks>
	[SerializationPriority(1)]
	[ForceSerializationWhenDefaultValue]
	public string ModPath
	{
		get => modPath;
		set
		{
			new ModLoader().Load(value, includeGuiPlugins: Empire.Current != null);
			modPath = value;
		}
	}

	/// <summary>
	/// The game name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// The next tick size, for ship movement.
	/// </summary>
	public double NextTickSize { get; set; }

	/// <summary>
	/// Events which have been warned of and are pending execution.
	/// </summary>
	public ICollection<Event> PendingEvents { get; private set; } = new List<Event>();

	/// <summary>
	/// The current player number (1 is the first player, 0 is the game host).
	/// </summary>
	public int PlayerNumber
	{
		get { return Empires.IndexOf(CurrentEmpire) + 1; }
	}

	public IEnumerable<IReferrable> Referrables { get { return referrables.Values; } }

	/// <summary>
	/// Notes that mod scripts can play with.
	/// </summary>
	public DynamicDictionary ScriptNotes { get; private set; }

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
	/// Current time equals turn number plus tick minus 1.
	/// </summary>
	public double Timestamp { get { return TurnNumber + CurrentTick - 1; } }

	/// <summary>
	/// The current turn number.
	/// </summary>
	public int TurnNumber { get; set; }

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
	/// Cache of abilities belonging to game objects.
	/// </summary>
	[DoNotSerialize]
	internal SafeDictionary<IAbilityObject, IEnumerable<Ability>> AbilityCache { get; private set; }

	/// <summary>
	/// Cache of abilities belonging to common game objects that can have different abilities for each empire.
	/// </summary>
	[DoNotSerialize]
	internal SafeDictionary<Tuple<ICommonAbilityObject, Empire>, IEnumerable<Ability>> CommonAbilityCache { get; private set; }

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

	/// <summary>
	/// Anything in the game that can be referenced from the client side
	/// using a Reference object instead of passing whole objects around.
	/// Stuff needs to be registered to be found though!
	/// </summary>
	[SerializationPriority(2)]
	internal IDictionary<long, IReferrable> referrables { get; set; }

	/// <summary>
	/// Cache of abilities that are shared to empires from other objects due to treaties.
	/// </summary>
	[DoNotSerialize]
	internal SafeDictionary<Tuple<IOwnableAbilityObject, Empire>, IEnumerable<Ability>> SharedAbilityCache { get; private set; }

	/// <summary>
	/// Serialized string value of the game at the beginning of the turn.
	/// </summary>
	[DoNotSerialize]
	public string? StringValue
	{
		get
		{
			if (stringValue == null)
				StringValue = SaveToString(false);
			return stringValue;
		}
		set
		{
			stringValue = value;
		}
	}

	private bool isAbilityCacheEnabled;

	private IDictionary<Sector, double> lastBattleTimestamps = new SafeDictionary<Sector, double>();

	private string? stringValue;

	public static string GetEmpireCommandsSavePath(string gameName, int turnNumber, int empireNumber)
	{
		return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Savegame", string.Format("{0}_{1}_{2:d4}{3}", gameName, turnNumber, empireNumber, FrEeeConstants.PlayerCommandsSaveGameExtension));
	}

	public static string GetGameSavePath(string gameName, int turnNumber, int empireNumber)
	{
		return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Savegame", empireNumber < 1 ?
			string.Format("{0}_{1}{2}", gameName, turnNumber, FrEeeConstants.SaveGameExtension) :
			string.Format("{0}_{1}_{2:d4}{3}", gameName, turnNumber, empireNumber, FrEeeConstants.SaveGameExtension));
	}

	/// <summary>
	/// Initializes a new game. Sets Game.Current.
	/// </summary>
	/// <exception cref="InvalidOperationException">if there is no mod loaded.</exception>
	/// <param name="status">A status object to report status back to the GUI.</param>
	/// <param name="desiredProgress">How much progress should we report back to the GUI when we're done initializing the galaxy? 1.0 means all done with everything that needs to be done.</param>
	public static void Initialize(GameSetup gsu, PRNG dice, Status status = null, double desiredProgress = 1.0)
	{
		if (Mod.Current == null)
			throw new InvalidOperationException("Cannot initialize a galaxy without a mod. Load a mod into Mod.Current first.");

		if (dice == null)
			dice = new PRNG(DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute);

		var startProgress = status == null ? 0d : status.Progress;
		var progressPerStep = (desiredProgress - startProgress) / 4d;

		// create the game
		if (Current == null)
		{
			var galtemp = gsu.GalaxyTemplate;
			galtemp.GameSetup = gsu;
			Current = galtemp.Instantiate(status, startProgress + progressPerStep, dice);
		}
		else
		{
			Current.Setup = gsu;
		}
		if (status != null)
			status.Message = "Populating galaxy";
		gsu.PopulateGame(Current, dice);
		Current.SaveTechLevelsForUniqueness();
		if (status != null)
			status.Progress += progressPerStep;

		// set single player flag
		Current.IsSinglePlayer = gsu.IsSinglePlayer;

		// run init script
		if (status != null)
			status.Message = "Executing script";
		PythonScriptEngine.RunScript<object>(Mod.Current.GameInitScript);
		if (status != null)
			status.Progress += progressPerStep;

		// save the game
		if (status != null)
			status.Message = "Saving game";
		SaveAll(status, desiredProgress);
	}

	/// <summary>
	/// Loads a savegame from the Savegame folder.
	/// Note that if it was renamed, it might have different game name, turn number, player number, etc. than the filename indicates.
	/// </summary>
	/// <param name="filename"></param>
	public static void Load(string filename)
	{
		Current = Services.Persistence.Game.LoadFromFile(filename);

		// TODO: put all this code in GamePersister
		new ModLoader().Load(Current.ModPath);
		if (Empire.Current != null)
		{
			// load library of designs
			Services.DesignLibrary.Load();

			// TODO: load libraries of strategies, etc once we have those
		}
		Current.PopulatePropertyValues();
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
	/// <param name="playerNumber"></param>
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
		Current = Services.Persistence.Game.LoadFromString(serializedData);

		// TODO: put all this code in GamePersister
		new ModLoader().Load(Current.ModPath);

		if (Empire.Current != null)
		{
			// initialize IronPython galaxy on load
			Current.StringValue = serializedData;
			var formula = new ComputedFormula<int>("Galaxy.Current.TurnNumber", null, true);
			var turn = formula.Value;

			// load library of designs
			Services.DesignLibrary.Load();

			// TODO: load libraries of strategies, etc once we have those
		}

		Current.PopulatePropertyValues();
	}

	/// <summary>
	/// Populates property values specified by <see cref="PopulateAttribute{T}"/>.
	/// </summary>
	private void PopulatePropertyValues()
	{
		// TODO: cache list of properties to populate when deserializing?
		// enumerate all referrables
		foreach (var referrable in Referrables)
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
	/// Saves all empires' tech levels in the other empires for uniqueness calculations.
	/// </summary>
	public void SaveTechLevelsForUniqueness()
	{
		if (Current.Setup.TechnologyUniqueness != 0)
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

	/// <summary>
	/// Saves the master view and all players' views of the game, unless single player, in which case only the first player's view is saved.
	/// </summary>
	/// <exception cref="InvalidOperationException">if CurrentEmpire is not null.</exception>
	public static void SaveAll(Status status = null, double desiredProgress = 1d)
	{
		if (Current.CurrentEmpire != null)
			throw new InvalidOperationException("Can only save player galaxy views from the master galaxy view.");

		var progressPerSaveLoad = (desiredProgress - (status == null ? 0d : status.Progress)) / (Current.IsSinglePlayer ? 3 : Current.Empires.Count + 2);
		//Current.SpaceObjectIDCheck("before saving");

		// save master view
		if (status != null)
			status.Message = "Saving game (host)";
		var gamname = Current.Save();
		if (status != null)
			status.Progress += progressPerSaveLoad;
		//Current.SpaceObjectIDCheck("after saving master view to disk");

		// save player views
		for (int i = 0; i < Current.Empires.Count; i++)
		{
			Load(gamname);
			if (Current.Empires[i].IsPlayerEmpire)
			{
				if (status != null)
					status.Message = "Saving game (player " + (i + 1) + ")";
				Current.CurrentEmpire = Current.Empires[i];
				Current.Redact();
				//Current.SpaceObjectIDCheck("after creating player view for " + Current.Empires[i]);
				Current.Save(false); // already asssigned IDs in the redact phase
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

		//Current.SpaceObjectIDCheck("after reloading master view");
	}

	/// <summary>
	/// Assigns an ID to an object.
	/// Will dispose of an object that has a negative ID if it hasn't already been disposed of.
	/// </summary>
	/// <param name="r">The object.</param>
	/// <param name="id">The ID, or 0 to generate a new ID (unless the ID is already valid).</param>
	/// <returns>The new ID.</returns>
	public long AssignID(IReferrable r, long id = 0)
	{
		if (r.ID < 0 || r.IsDisposed)
		{
			if (!r.IsDisposed)
				r.Dispose();
			return r.ID;
		}

		if (r.HasValidID())
			return r.ID; // no need to reassign ID
		else if (referrables.ContainsKey(r.ID))
		{
			// HACK - already exists, just log an error but don't overwrite anything
			// we need to fix start combatants having the same IDs as the real objects...
			Console.Error.WriteLine("The galaxy thinks that " + referrables[r.ID] + " has the ID " + r.ID + " but " + r + " claims to have that ID as well.");
			return r.ID;
		}

		var oldid = r.ID;
		long newid = oldid <= 0 ? id : oldid;

		while (newid <= 0 || referrables.ContainsKey(newid))
		{
			newid = RandomHelper.Range(1L, long.MaxValue);
		}
		r.ID = newid;
		referrables.Add(newid, r);

		// clean up old IDs
		if (oldid > 0 && referrables.ContainsKey(oldid) && oldid != newid)
			referrables.Remove(oldid);

		return newid;
	}

	/// <summary>
	/// Assigns IDs to referrable objects in the galaxy and purges disposed objects.
	/// Doesn't assign IDs to objects via DoNotAssignID properties, or to memories (or sub-objects of them).
	/// </summary>
	public void CleanGameState()
	{
		var parser = new ObjectGraphParser();
		bool canAssign = true;
		foreach (var kvp in referrables.ToArray())
		{
			if (kvp.Value.IsDisposed)
				referrables.Remove(kvp.Key);
		}
		parser.Property += (pname, o, val) =>
			{
				var prop = o.GetType().FindProperty(pname);
				var isMemory = val is IFoggable && (val as IFoggable).IsMemory;
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
							AssignID(r);
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
	/// Finds referrable objects in the game.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="condition"></param>
	/// <returns></returns>
	public IEnumerable<T> Find<T>(Func<T, bool>? condition = null) where T : IReferrable
	{
		if (condition is null)
			condition = t => true;
		return Referrables.OfType<T>().Where(r => condition(r));
	}

	public string GetEmpireCommandsSavePath(Empire emp)
	{
		return GetEmpireCommandsSavePath(Name, TurnNumber, Empires.IndexOf(emp) + 1);
	}

	public string GetGameSavePath(Empire emp = null)
	{
		if (emp == null)
			emp = CurrentEmpire;
		return GetGameSavePath(Name, TurnNumber, emp == null ? 0 : Empires.IndexOf(emp) + 1);
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
		var vlist = Galaxy.FindSpaceObjects<IMobileSpaceObject>().Where(sobj => sobj.Container == null && !sobj.IsMemory).Shuffle();
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
				var battle = Services.Battles.CreateSpaceBattle(sector);
				battle.Resolve();
				Battles.Add(battle);
				foreach (var emp in battle.Empires)
					emp.Log.Add(battle.CreateLogMessage(battle.NameFor(emp), LogMessageType.Battle));
				lastBattleTimestamps[sector] = Current.Timestamp;
			}
		}
	}

	/// <summary>
	/// Removes any space objects, etc. that the current empire cannot see.
	/// </summary>
	public void Redact()
	{
		// save off empire scores first, before data is removed
		foreach (var emp in Empires)
		{
			emp.Scores[TurnNumber] = emp.ComputeScore(Empire.Current);
		}

		// the galaxy data itself
		if (Empire.Current != null)
			ScriptNotes.Clear();

		// redact sub objects
		var parser = new ObjectGraphParser();
		parser.StartObject += redactParser_StartObject;
		parser.Parse(this);

		// clean up redacted objects that are not IFoggable
		foreach (var x in Galaxy.StarSystemLocations.Where(x => x.Item.IsDisposed).ToArray())
			Galaxy.StarSystemLocations.Remove(x);

		// delete memories since they've been copied to "physical" objects already
		foreach (var kvp in Empire.Current.Memory.ToArray())
		{
			kvp.Value.Dispose();
			Empire.Current.Memory.Remove(kvp);
		}
	}

	public void Save(Stream stream, bool assignIDs = true)
	{
		// TODO: put all this code in GamePersister
		if (assignIDs)
			CleanGameState();
		foreach (var kvp in referrables.Where(kvp => kvp.Value.IsDisposed).ToArray())
			referrables.Remove(kvp);

		Services.Persistence.Game.SaveToStream(this, stream);
	}

	/// <summary>
	/// Saves the game to an appropriately named file in the Savegame folder.
	/// Files are named GameName_TurnNumber_PlayerNumber.gam for players (PlayerNumber is 1-indexed)
	/// and GameName_TurnNumber.gam for the host.
	/// </summary>
	/// <returns>The filename saved to without the folder name (which is Savegame).</returns>
	public string Save(bool assignIDs = true)
	{
		// TODO: put all this code in GamePersister
		if (assignIDs)
			CleanGameState();
		foreach (var kvp in referrables.Where(kvp => kvp.Value.ID < 0).ToArray())
			referrables.Remove(kvp);
		string filename;
		if (CurrentEmpire == null)
			filename = Name + "_" + TurnNumber + ".gam";
		else
			filename = Name + "_" + TurnNumber + "_" + (Empires.IndexOf(CurrentEmpire) + 1).ToString("d4") + ".gam";
		if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FrEeeConstants.SaveGameDirectory)))
			Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FrEeeConstants.SaveGameDirectory));
		using FileStream fs = new(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FrEeeConstants.SaveGameDirectory, filename), FileMode.Create);

		Services.Persistence.Game.SaveToStream(this, fs);
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
		// TODO: move this code to CommandPersister
		CleanGameState();
		if (CurrentEmpire == null)
			throw new InvalidOperationException("Can't save commands without a current empire.");
		foreach (var c in Empire.Current.Commands.OfType<ISetPlayerInfoCommand>().ToArray())
			Empire.Current.Commands.Remove(c);
		var cmd = Services.Commands.Notes.SetPlayerInfo(Empire.Current, Empire.Current.PlayerInfo);
		Empire.Current.Commands.Add(cmd);
		if (!Directory.Exists(FrEeeConstants.SaveGameDirectory))
			Directory.CreateDirectory(FrEeeConstants.SaveGameDirectory);
		var filename = GetEmpireCommandsSavePath(CurrentEmpire);
		var fs = new FileStream(filename, FileMode.Create);
		SerializeCommands(fs);
		fs.Close(); fs.Dispose();

		// save library of designs
		Services.DesignLibrary.Save();

		return filename;
	}

	public string SaveToString(bool assignIDs = true)
	{
		if (assignIDs)
			CleanGameState();
		return Services.Persistence.Game.SaveToString(this);
	}

	public override string ToString()
	{
		if (CurrentEmpire == null)
			return Name;
		return Name + " - " + CurrentEmpire.Name + " - " + CurrentEmpire.LeaderName + " - " + Stardate;
	}

	public void UnassignID(long id)
	{
		if (referrables.ContainsKey(id))
		{
			var r = referrables[id];
			r.ID = -1;
			referrables.Remove(id);
		}
	}

	public void UnassignID(IReferrable r)
	{
		if (r == null || r.ID < 0)
			return; // nothing to do
		if (referrables.ContainsKey(r.ID))
		{
			if (referrables[r.ID] == r)
				referrables.Remove(r.ID);
			else
			{
				var galaxyThinksTheIDIs = referrables.Where(kvp => kvp.Value == r);
				foreach (var wrongID in galaxyThinksTheIDIs)
				{
					referrables.Remove(wrongID);
				}
			}
		}
		else if (referrables.Values.Contains(r))
		{
			try
			{
				referrables.Remove(referrables.Single(kvp => kvp.Value == r));
			}
			catch (InvalidOperationException ex)
			{
				// HACK - why is the item not being found? sequence contains no matching element? it's right there!
				Console.Error.WriteLine(ex);
			}
		}
		//r.ID = -1;
	}

	internal void SpaceObjectIDCheck(string when)
	{
		foreach (var sobj in Galaxy.FindSpaceObjects<ISpaceObject>().ToArray())
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
		var cmds = Services.Persistence.Commands.LoadFromStream(stream);

		// TODO: put code below in CommandPersister
		// check for client safety
		foreach (var cmd in cmds.Where(cmd => cmd != null))
		{
			cmd.CheckForClientSafety();
		}

		return cmds;
	}

	public void LoadCommands(Empire emp, IList<ICommand> cmds)
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
				else if (clientid != serverid)
				{
					idmap.Add(clientid, serverid);
				}
				else
				{
					// no need to change ID if it's not changing
					idmap.Remove(clientid);
				}
			}
		}
		if (idmap.Any())
		{
			foreach (var cmd in cmds)
			{
				cmd.ReplaceClientIDs(idmap); // convert client IDs to server IDs
			}
		}
	}

	private void redactParser_StartObject(object o)
	{
		if (o is IReferrable)
			AssignID(o as IReferrable);
		if (o is IFoggable obj && o is IReferrable r)
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
				obj.Redact(Empire.Current);
			}
			else
			{
				// memories are only visible to the empire which is seeing them!
				// well unless we add some sort of intel project to see them or something...
				if (!CurrentEmpire.Memory.Values.Contains(obj))
					obj.Dispose();
			}
		}
		//SpaceObjectIDCheck("when redacting " + o);
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

		Services.Persistence.Commands.SaveToStream(CurrentEmpire.Commands, stream);
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
			else if (Referrables.OfType<IConstructionQueue>().Any(q => q.Orders.Any(o => o.Item == sobj as IConstructable)))
				dispose = false; // save constructable space objects under construction
			if (dispose)
				sobj.Dispose();
		}
	}

	/// <summary>
	/// The game setup used to create this galaxy.
	/// </summary>
	public GameSetup Setup { get; set; }

	/// <summary>
	/// Resets the treaty clause caches in this game.
	/// </summary>
	public void ResetTreatyClauseCaches()
	{
		GivenTreatyClauseCache = new SafeDictionary<Empire, ILookup<Empire, Clause>>();
		ReceivedTreatyClauseCache = new SafeDictionary<Empire, ILookup<Empire, Clause>>();
	}
}
