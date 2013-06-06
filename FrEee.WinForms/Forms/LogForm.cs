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

namespace FrEee.WinForms.Forms
{
	public partial class LogForm : Form
	{
		public LogForm(GameForm gameForm)
		{
			InitializeComponent();
			this.gameForm = gameForm;
		}

		private GameForm gameForm;

		private void LogForm_Load(object sender, EventArgs e)
		{
			lstLog.Initialize(32, 32);
			foreach (var message in Empire.Current.Log.OrderByDescending(message => message.TurnNumber))
				lstLog.AddItemWithImage(message.TurnNumber.ToStardate(), message.TurnNumber.ToStardate() + ": " + message.Text, message, message.Picture);
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
					}
					else if (context is IHull<IVehicle>)
					{
						// go to design screen and create a new design using this hull 
						var hull = (IHull<IVehicle>)context;
						gameForm.ShowVehicleDesignForm(new VehicleDesignForm(hull));
					}
					else if (context is ComponentTemplate || context is Mount)
					{
						// go to design screen
						gameForm.ShowVehicleDesignForm(new VehicleDesignForm());
					}

					// TODO - more types of goto-messages
				}
			}
		}
	}
}
