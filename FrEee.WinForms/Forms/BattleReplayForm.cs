using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Combat.Grid;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Controls;
using FrEee.WinForms.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class BattleReplayForm : Form, IBindable<Battle>
	{
		#region Public Constructors

		public BattleReplayForm(Battle b)
		{
			InitializeComponent();
			Bind(b);
		}

		#endregion Public Constructors

		#region Public Properties

		public Battle Battle { get; private set; }

		#endregion Public Properties

		#region Public Methods

		public void Bind(Battle data)
		{
			Battle = data;
			Bind();
		}

		public void Bind()
		{
			battleView.Battle = Battle;
			minimap.Battle = Battle;
			Text = Battle.NameFor(Empire.Current);
		}

		#endregion Public Methods

		#region Private Methods

		private void btnClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		#endregion Private Methods

		private void minimap_MouseDown(object sender, MouseEventArgs e)
		{
			battleView.FocusedLocation = minimap.ClickLocation;
			SelectedCombatant = minimap.SelectedCombatant;
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			battleView.Round--;
			minimap.Round--;
		}

		private void btnPause_Click(object sender, EventArgs e)
		{
			battleView.IsPaused = !battleView.IsPaused;
			minimap.IsPaused = !minimap.IsPaused;
			if (battleView.IsPaused)
				btnPause.Text = ">";
			else
				btnPause.Text = "||";
		}

		private void btnForward_Click(object sender, EventArgs e)
		{
			battleView.Round++;
			minimap.Round++;
		}

		private ICombatant selectedCombatant;

		public ICombatant SelectedCombatant
		{
			get => selectedCombatant;
			set
			{
				selectedCombatant = value;
				reportPanel.Controls.Clear();
				if (value is SpaceVehicle v)
					reportPanel.Controls.Add(new SpaceVehicleReport(v) { Dock = DockStyle.Fill });
				else if (value is Planet p)
					reportPanel.Controls.Add(new PlanetReport(p) { Dock = DockStyle.Fill });
				else if (value is Seeker s)
					reportPanel.Controls.Add(new Label { Text = $"{s.Name} targeting {s.Target} ({s.Hitpoints} HP)" });
			}
		}

		private void RefreshSelectedCombatant()
		{
			var ctl = reportPanel.Controls.Cast<Control>().FirstOrDefault();
			if (ctl == null)
				return;
			else if (ctl is IBindable b)
				b.Bind();
			else if (ctl is Label l)
			{
				if (SelectedCombatant is Seeker s)
					l.Text = $"{s.Name} targeting {s.Target} ({s.Hitpoints} HP)";
			}
		}
	}
}