using System;

namespace FrEee.UI.WinForms;

// this needs to come after the namespace declaration to properly reference the system console inside the namespace
using Console = System.Console;

/// <summary>
/// Command line utility for FrEee for use by the Windows Forms UI.
/// </summary>
public class WinFormsCommandLine
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

FrEee.UI.WinForms: display main menu

FrEee.UI.WinForms --help: display this help

FrEee.UI.WinForms --host gamename_turnnumber.gam: load host console
	Shortcut: FrEee.UI.WinForms gamename_turnnumber.gam
FrEee.UI.WinForms --process gamename_turnnumber.gam: process turn
FrEee.UI.WinForms --process-safe gamename_turnnumber.gam: process turn, halting if any plr files are missing

FrEee.UI.WinForms --play gamename_turnnumber_playernumber.gam: play a turn, resuming from where you left off if a plr file is present
	Shortcut: FrEee.UI.WinForms gamename_turnnumber_playernumber.gam
FrEee.UI.WinForms --play gamename_turnnumber_playernumber.plr: play a turn, resuming from where you left off
	Shortcut: FrEee.UI.WinForms gamename_turnnumber_playernumber.plr
FrEee.UI.WinForms --load gamename_turnnumber_playernumber.gam: play a turn, prompting to resume if plr file is present
FrEee.UI.WinForms --restart gamename_turnnumber_playernumber.gam: play a turn, restarting from the beginning of the turn");
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
		throw new NotImplementedException();
	}

	public override ReturnValue PlayTurn(string? plrfile = null)
	{
		throw new NotImplementedException();
	}

	public override bool PromptYesNo(string prompt, string title)
	{
		DisplayMessage($"{title}: {prompt} [yn]");
		char? key = null;
		do
		{
			key = Console.ReadKey().KeyChar.ToString().ToLower()[0];
		} while (!"yn".Contains(key.Value));

		return key == 'y';
	}
}