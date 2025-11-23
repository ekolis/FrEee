using System;
using System.IO;
using System.Windows.Forms;
using FrEee.Objects.GameState;
using FrEee.Plugins;
using FrEee.UI.WinForms.Forms;
using FrEee.UI.WinForms.Persistence;
using FrEee.UI.WinForms.Utility;
using FrEee.UI.WinForms.Utility.Extensions;
using FrEee.Utility;

namespace FrEee.UI.WinForms;

/// <summary>
/// Command line utility for FrEee for use by the Windows Forms UI.
/// </summary>
public class WinFormsCommandLine
	: CommandLine
{
	public override void LoadUIPlugins()
	{
		DI.RegisterSingleton<IGuiController, GuiController>();
		DI.RegisterSingleton<IClientSettingsPersister, ClientSettingsPersister>();
	}

	public override void DisplayMessage(string message)
	{
		MessageBox.Show(message);
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
	
	public override ReturnValue PerformDefaultAction()
	{
		Application.Run(MainMenuForm.GetInstance());
		return ReturnValue.Success;
	}

	public override ReturnValue DisplayHostConsole()
	{
		HostConsoleForm form = new();
		form.StartPosition = FormStartPosition.CenterScreen;
		Application.Run(form);
		return ReturnValue.Success;
	}

	public override ReturnValue PlayTurn(string? plrfile = null)
	{
		if (plrfile is not null)
		{
			if (File.Exists(plrfile))
			{
				try
				{
					Game.Current.LoadCommands();
				}
				catch
				{
					DisplayMessage("An error occurred while loading your commands. You will need to restart your turn from the beginning.");
					Game.Load(Game.Current.GameFileName); // in case some commands got loaded
				}
			}
			else
			{
				DisplayMessage(plrfile + " does not exist. You will need to start your turn from the beginning.");
			}
		}

		Services.Designs.ImportDesignsFromLibrary();

		MainGameForm form = new(false, true);
		form.KeyPreview = true;
		form.StartPosition = FormStartPosition.CenterScreen;
		form.KeyDown += GuiExtensions.childForm_KeyDown_forDebugConsole;
		Application.Run(form);
		return ReturnValue.Success;
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
	
	protected override bool IncludeGuiPlugins => true;
}