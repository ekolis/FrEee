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
				lstLog.AddItemWithImage(message.TurnNumber.ToStardate(), message.Text, message, message.Picture);
		}

		private void lstLog_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var item = lstLog.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				var message = (LogMessage)item.Tag;
				if (message is IPictorialLogMessage<ISpaceObject>)
				{
					// go to space object
					var m = (IPictorialLogMessage<ISpaceObject>)message;
					gameForm.SelectSpaceObject(m.Context);
					Close();
				}

				// TODO - more types of goto-messages
			}
		}
	}
}
