using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Modding.Templates;
using FrEee.Extensions;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Objects.Vehicles;
using FrEee.Objects.Civilization.Diplomacy.Messages;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;

namespace FrEee.UI.WinForms.Forms;

public partial class LogForm : GameForm
{
	/// <summary>
	/// Creates a log form.
	/// </summary>
	/// <param name="mainGameForm"></param>
	/// <param name="battle">The battle whose log we should display, or null to display the turn log.</param>
	public LogForm(MainGameForm mainGameForm, IList<LogMessage> log)
	{
		InitializeComponent();
		this.mainGameForm = mainGameForm;
		messages = log.OrderByDescending(m => m.TurnNumber).ToArray();

		try { this.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); } catch { }

		ShowInTaskbar = !mainGameForm.HasLogBeenShown;
	}

	private MainGameForm mainGameForm;

	private IEnumerable<LogMessage> messages;

	private void LogForm_Load(object sender, EventArgs e)
	{
		lstLog.Initialize(32, 32);
		foreach (var message in messages)
		{
			var item = lstLog.AddItemWithImage(null, message.TurnNumber.ToStardate(), message, message.Picture);
			item.SubItems.Add(message.Text);
			if (message.TurnNumber < Game.Current.TurnNumber)
				item.ForeColor = Color.Gray;
		}
	}

	private void LogForm_SizeChanged(object sender, EventArgs e)
	{
		// auto size it again
		lstLog.Columns[1].Width = -2;
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
					mainGameForm.SelectSpaceObject((ISpaceObject)context);
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
							mainGameForm.SelectSpaceObject((ISpaceObject)unit);
						else
							mainGameForm.SelectSpaceObject((ISpaceObject)container);
						Close();
					}
				}
				else if (context is Facility)
				{
					// go to the planet
					var facility = (Facility)context;
					var container = facility.Container;
					mainGameForm.SelectSpaceObject(container);
					Close();
				}
				else if (context is Technology)
				{
					// go to research screen
					mainGameForm.ShowResearchForm();
					Close();
				}
				else if (context is IHull<IVehicle>)
				{
					// go to design screen and create a new design using this hull
					var hull = (IHull<IVehicle>)context;
					mainGameForm.ShowVehicleDesignForm(new VehicleDesignForm(hull));
					Close();
				}
				else if (context is ComponentTemplate || context is Mount)
				{
					// go to design screen
					mainGameForm.ShowVehicleDesignForm(new VehicleDesignForm());
					Close();
				}
				else if (context is IBattle)
				{
					// show battle results
					var form = new BattleResultsForm((IBattle)context);
					this.ShowChildForm(form);
				}
				else if (context is IMessage)
				{
					// show diplomacy screen
					var form = new DiplomacyForm((IMessage)context);
					mainGameForm.ShowChildForm(form);
					Close();
				}
				else if (context is StarSystem sys)
				{
					// navigate game form to that system
					mainGameForm.SelectStarSystem(sys);
					Close();
				}

				// TODO - more types of goto-messages
			}
		}
	}
}