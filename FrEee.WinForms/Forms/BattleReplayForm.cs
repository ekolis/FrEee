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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class BattleReplayForm : Form, IBindable<Battle>
	{
		public BattleReplayForm(Battle b)
		{
			InitializeComponent();
			Bind(b);
			reportPanel.Controls.Add(CreateLogListBox());
		}

		public Battle Battle { get; private set; }

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
				else if (value is null)
					reportPanel.Controls.Add(CreateLogListBox());
			}
		}

		private ListBox CreateLogListBox()
		{
			var lb = new ListBox();
			lb.BackColor = Color.Black;
			lb.ForeColor = Color.White;
			lb.Dock = DockStyle.Fill;
			reportPanel.Controls.Add(lb);
			foreach (var roundEvents in Battle.Events)
			{
				lb.Items.Add($"Begin round {Battle.Events.IndexOf(roundEvents) + 1}!");
				foreach (var e in roundEvents)
				{
					if (e is CombatantAppearsEvent cae)
						lb.Items.Add($"{cae.Combatant} appears!");
					else if (e is CombatantDisappearsEvent cde)
						lb.Items.Add($"{cde.Combatant} disappears!");
					else if (e is WeaponFiresEvent wfe)
					{
						if (wfe.IsHit)
							lb.Items.Add($"{wfe.Combatant} fires at {wfe.Target} and hits!");
						else
							lb.Items.Add($"{wfe.Combatant} fires at {wfe.Target} and misses.");
					}
				}
			}
			return lb;
		}

		private ICombatant selectedCombatant;

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

		private void btnBack_Click(object sender, EventArgs e)
		{
			battleView.Round--;
			minimap.Round--;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnForward_Click(object sender, EventArgs e)
		{
			battleView.Round++;
			minimap.Round++;
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

		private void minimap_MouseDown(object sender, MouseEventArgs e)
		{
			battleView.FocusedLocation = minimap.ClickLocation;
			SelectedCombatant = minimap.SelectedCombatant;
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