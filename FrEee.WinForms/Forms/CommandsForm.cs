using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Templates;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
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
}