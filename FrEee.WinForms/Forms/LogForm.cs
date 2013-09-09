using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.WinForms.Utility.Extensions;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Templates;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;

namespace FrEee.WinForms.Forms
{
	public partial class LogForm : Form
	{
		/// <summary>
		/// Creates a log form.
		/// </summary>
		/// <param name="gameForm"></param>
		/// <param name="battle">The battle whose log we should display, or null to display the turn log.</param>
		public LogForm(GameForm gameForm, Battle battle = null)
		{
			InitializeComponent();
			this.gameForm = gameForm;
			if (battle == null)
				messages = Empire.Current.Log.OrderByDescending(message => message.TurnNumber);
			else
				messages = battle.Log;

			try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
		}

		IEnumerable<LogMessage> messages;

		private GameForm gameForm;

		private void LogForm_Load(object sender, EventArgs e)
		{
			lstLog.Initialize(32, 32);
			foreach (var message in messages)
			{
				var item = lstLog.AddItemWithImage(message.TurnNumber.ToStardate(), message.TurnNumber.ToStardate() + ": " + message.Text, message, message.Picture);
				if (message.TurnNumber < Galaxy.Current.TurnNumber)
					item.ForeColor = Color.Gray;
			}
		}

		private void lstLog_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var item = lstLog.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				var message = (LogMessage)item.Tag;
				if (message is IPictorialLogMessage<IPictorial>)
				{
					var context = ((IPictorialLogMessage<IPictorial>)message).Context;
					if (context is ISpaceObject)
					{
						// go to space object
						gameForm.SelectSpaceObject((ISpaceObject)context);
						Close();
					}
					else if (context is Technology)
					{
						// go to research screen
						gameForm.ShowResearchForm(new ResearchForm());
						Close();
					}
					else if (context is IHull<IVehicle>)
					{
						// go to design screen and create a new design using this hull 
						var hull = (IHull<IVehicle>)context;
						gameForm.ShowVehicleDesignForm(new VehicleDesignForm(hull));
						Close();
					}
					else if (context is ComponentTemplate || context is Mount)
					{
						// go to design screen
						gameForm.ShowVehicleDesignForm(new VehicleDesignForm());
						Close();
					}
					else if (context is Battle)
					{
						// show battle log
						gameForm.ShowLogForm(new LogForm(gameForm, (Battle)context));
						Close();
					}

					// TODO - more types of goto-messages
				}
			}
		}
	}
}
