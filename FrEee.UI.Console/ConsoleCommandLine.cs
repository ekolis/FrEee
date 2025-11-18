using System.Net.Mime;
using System.Reflection;
using System.Text.RegularExpressions;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Plugins;

namespace FrEee.UI.Console;

// this needs to come after the namespace declaration to properly reference the system console inside the namespace
using Console = System.Console;

/// <summary>
/// Command line utility for FrEee for use by the console UI.
/// </summary>
public class ConsoleCommandLine
	: CommandLine
{
	public override void LoadUIPlugins()
	{
		// do nothing here
	}
	
	public override void DisplayMessage(string message)
	{
		Console.WriteLine(message);
	}
	
	public override ReturnValue DisplaySyntax()
	{
		DisplayMessage(
			@"Syntax:

FrEee.UI.Console: display this help

FrEee.UI.Console --help: display this help

FrEee.UI.Console --process gamename_turnnumber.gam: process turn
FrEee.UI.Console --process-safe gamename_turnnumber.gam: process turn, halting if any plr files are missing");
		return ReturnValue.SyntaxError;
	}

	/// <summary>
	/// Performs the default action for the current UI.
	/// The default implementation displays the syntax help text.
	/// Can be overridden to perform a different action, e.g. load a GUI.
	/// </summary>
	public override ReturnValue PerformDefaultAction()
	{
		return DisplaySyntax();
	}

	public override ReturnValue DisplayHostConsole()
	{
		// TODO: add a text based host console
		DisplayMessage("Host console is not supported by this command line. Use FrEee.UI.WinForms instead.");
		return ReturnValue.UnsupportedFeature;
	}

	public override ReturnValue PlayTurn(string? plrfile = null)
	{
		DisplayMessage("Playing a turn is not suported by this command line. Use FrEee.UI.WinForms instead.");
		return ReturnValue.UnsupportedFeature;
	}

	public override bool PromptYesNo(string prompt, string title)
	{
		DisplayMessage($"{title}: {prompt} [yn]");
		char? key;
		do
		{
			key = Console.ReadKey().KeyChar.ToString().ToLower()[0];
		} while (!"yn".Contains(key.Value));
		return key == 'y';
	}

	protected override bool IncludeGuiPlugins { get; } = false;
}