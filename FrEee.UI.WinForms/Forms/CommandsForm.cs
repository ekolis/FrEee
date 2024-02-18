using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Drawing;

namespace FrEee.WinForms.Forms;

public partial class CommandsForm : GameForm
{ 
	public CommandsForm()
	{
		InitializeComponent();

		try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); } catch { }
	}

	private void CommandsForm_Load(object sender, EventArgs e)
	{
		lstCommands.Initialize(32, 32);
		foreach (var cmd in Empire.Current.Commands)
			lstCommands.AddItemWithImage(null, cmd.ToString(), cmd, (cmd.Executor is IPictorial p ? p.Icon : null));
	}
}