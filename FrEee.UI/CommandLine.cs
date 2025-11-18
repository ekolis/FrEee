using System.Reflection;
using System.Text.RegularExpressions;
using FrEee.Extensions;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Plugins;
using FrEee.Utility;

namespace FrEee.UI;

/// <summary>
/// Command line utility for FrEee.
/// </summary>
public abstract class CommandLine
{
	/// <summary>
	/// Values that can be returned from the game.
	/// </summary>
	public enum ReturnValue
	{
		Success = 0,
		SyntaxError = 1,
		MissingRequestedFile = 2,
		Crash = 3,
		UnsupportedFeature = 4,
		MissingPlr1File = 101,
		// MissingPlr2File is 102, etc.
	}
	
	/// <summary>
	/// The main entry point for the application.
	/// Return values:
	/// 0 for success
	/// 1 for syntax error in command line
	/// 2 for missing GAM or PLR file specified to load
	/// 3 for crash
	/// 4 for unsupported feature (e.g. playing a turn in the command line interface)
	/// 1xx for missing PLR file for player xx when running in "safe processing" mode
	/// </summary>
	public ReturnValue Run(string[] args, bool redirectConsole)
	{
		// HACK: set up dependency injection for GUI before we load the main set of plugins
		PluginLibrary.Instance.LoadDefaultPlugins(LoadUIPlugins);

		// HACK - so many things are based on the working directory...
		Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

		if (redirectConsole)
		{
			try
			{
				var stdout = new FileStream("stdout.txt", FileMode.Create);
				var swOut = new StreamWriter(stdout);
				swOut.AutoFlush = true;
				Console.SetOut(swOut);
			}
			catch (IOException)
			{
				Console.Error.WriteLine("Cannot open stdout.txt.");
			}

			try
			{
				var stderr = new FileStream("stderr.txt", FileMode.Create);
				var swErr = new StreamWriter(stderr);
				swErr.AutoFlush = true;
				Console.SetError(swErr);
			}
			catch (IOException)
			{
				Console.Error.WriteLine("Cannot open stderr.txt.");
			}
		}

		if (args.Length == 0)
		{
			return PerformDefaultAction();
		}
		else if (args.Length == 1)
		{
			if (args[0].TrimStart('-').ToLower() == "help")
			{
				DisplaySyntax();
				return ReturnValue.Success;
			}
			else
			{
				var file = args[0];
				return PerformFileAction(file);
			}
		}
		else if (args.Length == 2)
		{
			var operation = args[0];
			var file = args[1];
			return PerformFileAction(file, operation.TrimStart('-').ToLower());
		}
		else if (args.Length == 3)
		{
			var operation = args[0];
			var file = args[1];
			var extra = args[2];
			return PerformFileAction(file, operation.TrimStart('-').ToLower(), extra);
		}
		else
		{
			return DisplaySyntax();
		}
	}

	/// <summary>
	/// Loads any plugins required by the current UI in addition to the default plugins.
	/// Override to load different plugins, e.g. for the GUI.
	/// </summary>
	public abstract void LoadUIPlugins();

	/// <summary>
	/// Displays a message to the user.
	/// Override to display messages in different formats, e.g. a dialog box in WinForms.
	/// </summary>
	/// <param name="message"></param>
	public abstract void DisplayMessage(string message);

	/// <summary>
	/// Displays valid syntax options for the current UI.
	/// </summary>
	/// <returns></returns>
	public abstract ReturnValue DisplaySyntax();

	/// <summary>
	/// Performs the default action for the current UI.
	/// Override to perform a UI specific action, e.g. load a GUI or display command line options.
	/// </summary>
	public abstract ReturnValue PerformDefaultAction();

	public HttpClient Http { get; } = new();

	/// <summary>
	/// Performs an action based on a file to be loaded.
	/// </summary>
	/// <param name="file">The name of the file to load.</param>
	/// <param name="operation">The operation to perform on the file.</param>
	/// <param name="extraArg">An extra general purpose parameter.</param>
	/// <returns></returns>
	public ReturnValue PerformFileAction(string file, string? operation = null, string? extraArg = null)
	{
		string? gamfile = null, plrfile = null;

		// guess operation from filename
		// TODO - move these regexes to the Game class?
		var playerRegex = @"(?i).*_.*_.*\.gam";
		var hostRegex = @"(?i).*_.*\.gam";
		var cmdRegex = @"(?i).*_.*_.*\.plr";

		if (operation is null)
		{
			// determine operation based on filename if possible
			if (Regex.IsMatch(file, playerRegex))
				operation = "play";
			else if (Regex.IsMatch(file, hostRegex))
				operation = "host";
			else if (Regex.IsMatch(file, cmdRegex))
				operation = "play";
			else
				return DisplaySyntax();
		}
		gamfile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Savegame", Path.GetFileNameWithoutExtension(file) + ".gam");
		plrfile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Savegame", Path.GetFileNameWithoutExtension(file) + ".plr");

		if (!File.Exists(gamfile))
		{
			DisplayMessage(gamfile + " does not exist.");
			return ReturnValue.MissingRequestedFile;
		}

		// load GAM file, see if it's a host or player view
		Game.Load(gamfile, IncludeGuiPlugins);
		if (Empire.Current == null)
		{
			// host view
			if (operation == "host")
			{
				return DisplayHostConsole();
			}
			else if (operation == "process")
				return ProcessTurn(false);
			else if (operation == "process-safe")
				return ProcessTurn(true);
			else
				return DisplaySyntax();
		}
		else
		{
			// player view
			if (operation == "play")
			{
				if (File.Exists(plrfile))
					return PlayTurn(plrfile);
				else
					return PlayTurn();
			}
			else if (operation == "load")
			{
				// load turn and prompt player to resume if PLR file exists
				if (File.Exists(plrfile))
				{
					var result = PromptYesNo("Player commands file exists for this turn. Resume turn from where you left off?", "Resume Turn");
					if (result)
						return PlayTurn(plrfile);
					else
						return PlayTurn();
				}
				else
					return PlayTurn();
			}
			else if (operation == "restart")
				return PlayTurn();
			else
				return DisplaySyntax();
		}
	}

	public abstract ReturnValue DisplayHostConsole();
	
	public abstract ReturnValue PlayTurn(string? plrfile = null);

	public abstract bool PromptYesNo(string prompt, string title);
	
	protected abstract bool IncludeGuiPlugins { get; }

	public ReturnValue ProcessTurn(bool safe)
	{
		try
		{
			var status = new Status();
			Action status_Changed = () =>
			{
				Console.WriteLine(status.Progress.ToString("p0") + ": " + status.Message);
				if (status.Exception != null)
					Console.Error.WriteLine(status.Exception);
			};
			status.Changed += new Status.ChangedDelegate(status_Changed);

			Console.WriteLine("Processing turn...");
			var processor = Services.TurnProcessor;
			var emps = processor.ProcessTurn(Game.Current, false, status);
			foreach (var emp in emps)
				Console.WriteLine(emp + " did not submit a PLR file.");
			if (safe && emps.Any())
			{
				Console.Error.WriteLine("Halting turn processing due to missing PLR file(s).");
				return ReturnValue.MissingPlr1File + Game.Current.Empires.IndexOf(emps.First());
			}
			Game.SaveAll(includeGuiPlugins: IncludeGuiPlugins);
			Console.WriteLine("Turn processed successfully. It is now turn " + Game.Current.TurnNumber + " (stardate " + Game.Current.Stardate + ").");
			Services.Gui.Exit();
			return ReturnValue.Success;
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine("Exception occurred (" + ex.Message + "): check errorlog.txt for details.");
			ex.LogFatal();
			return ReturnValue.Crash;
		}
	}

	public void Log(Exception exception)
	{
		try
		{
			exception.LogFatal();
			LogOnline(exception);
			var inner = exception;
			while (inner is TargetInvocationException)
				inner = inner.InnerException;
			DisplayMessage(inner.GetType().Name + ": " + inner.Message + "\n\nSee errorlog.txt for more details.");
		}
		catch (Exception ex)
		{
			DisplayMessage("Error logging exception: " + ex.Message);
		}
	}
	
	private void LogOnline(Exception ex)
	{
		if (ex.InnerException != null)
			Log(ex.InnerException);
		var values = new Dictionary<string, string>
		{
			{ "app", "FrEee (WinForms)" },
			{ "version", Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "Unknown" },
			{ "type", ex.GetType().Name },
			{ "message", ex.Message },
			{ "stackTrace", ex.StackTrace ?? "Unknown" },
		};
		var content = new FormUrlEncodedContent(values);
		Http.PostAsync("http://edkolis.com/errorlog", content);
	}
}