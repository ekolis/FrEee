using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.WinForms.Utility.Extensions;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Combat.Simple;
using FrEee.Wpf;

namespace FrEee.WinForms.Forms
{
    public partial class LogForm : Form
    {
        private GameForm gameForm;

        /// <summary>
        /// Creates a log form.
        /// </summary>
        /// <param name="gameForm"></param>
        /// <param name="battle">The battle whose log we should display, or null to display the turn log.</param>
        public LogForm(GameForm gameForm, IList<LogMessage> log)
        {
            InitializeComponent();
            this.gameForm = gameForm;
            var messages = log.OrderByDescending(m => m.TurnNumber).ToArray();

            try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); } catch { }

            ShowInTaskbar = !gameForm.HasLogBeenShown;

            // Creating the data objects that are used by both WinForms and WPF log representations.
            GlobalStuff.Default.GameLogViewModel = new GameLogViewModel();
            GlobalStuff.Default.LogForm = this;
            foreach (var message in messages)
            {
                GlobalStuff.Default.GameLogViewModel.Messages.Add(message);
            }

            // Creating the WPF control that also list the messages, in a very similar manner as WinForms log. Eventually, UI representation, be it Winforms or WPF, should be only constructed once per application lifetime.
            var zeView = new GameLogView();
            zeView.DataContext = GlobalStuff.Default.GameLogViewModel;
            wpfHost.Child = zeView;
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            lstLog.Initialize(32, 32);
            foreach (var message in GlobalStuff.Default.GameLogViewModel.Messages)
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
                GlobalStuff.Default.GameLogViewModel.HandleNavigationFromLogForm(message);

                // This is some sort of a special case: closing Log form almost always, except...
                var context = ((IPictorialLogMessage<IPictorial>)message).Context;
                if ((context is Battle) == false) Close();
            }
        }

        private void LogForm_SizeChanged(object sender, EventArgs e)
        {
            // auto size it again
            lstLog.Columns[1].Width = -2;
        }
    }
}
