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
using FrEee.Game.Objects.Vehicles;
using FrEee.WinForms.Controls;
using FrEee.Game.Objects.Combat.Simple;

namespace FrEee.WinForms.Forms
{
	public partial class LogForm : Form
	{
		/// <summary>
		/// Creates a log form.
		/// </summary>
		/// <param name="gameForm"></param>
		/// <param name="battle">The battle whose log we should display, or null to display the turn log.</param>
		public LogForm(GameForm gameForm, IList<LogMessage> log)
		{
			InitializeComponent();
			this.gameForm = gameForm;
			messages = log.OrderByDescending(m => m.TurnNumber).ToArray();

			try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}

			ShowInTaskbar = !gameForm.HasLogBeenShown;
		}

		IEnumerable<LogMessage> messages;

		private GameForm gameForm;

		private void LogForm_Load(object sender, EventArgs e)
		{
			lstLog.Initialize(32, 32);
			foreach (var message in messages)
			{
				var item = lstLog.AddItemWithImage(null, message.TurnNumber.ToStardate(), message, message.Picture);
				item.SubItems.Add(message.Text);
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
					else if (context is IUnit)
					{
						// go to whatever contains the unit
						var unit = (IUnit)context;
						var container = unit.FindContainer();
						if (container != null)
						{
							if (container is Sector)
								gameForm.SelectSpaceObject((ISpaceObject)unit);
							else
								gameForm.SelectSpaceObject((ISpaceObject)container);
							Close();
						}
					}
					else if (context is Facility)
					{
						// go to the planet
						var facility = (Facility)context;
						var container = facility.Container;
						gameForm.SelectSpaceObject(container);
						Close();
					}
					else if (context is Technology)
					{
						// go to research screen
						gameForm.ShowResearchForm();
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
						// show battle results
						var form = new BattleResultsForm((Battle)context);
						this.ShowChildForm(form);
                    }
                    else if (context is IMessage)
                    {
                        // show diplomacy screen
                        var form = new DiplomacyForm((IMessage)context);
                        gameForm.ShowChildForm(form);
                        Close();
                    }

					// TODO - more types of goto-messages
				}
			}
		}

		private void LogForm_SizeChanged(object sender, EventArgs e)
		{
			// auto size it again
			lstLog.Columns[1].Width = -2;
		}
	}
}
