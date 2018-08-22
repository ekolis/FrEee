﻿using FrEee.Game.Interfaces;
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
					reportPanel.Controls.Add(logListBox);
			}
		}

		private ListBox logListBox;

		private ListBox CreateLogListBox()
		{
			logListBox = new ListBox();
			logListBox.BackColor = Color.Black;
			logListBox.ForeColor = Color.White;
			logListBox.Dock = DockStyle.Fill;
			reportPanel.Controls.Add(logListBox);
			foreach (var roundEvents in Battle.Events)
			{
				logListBox.Items.Add($"Begin round {Battle.Events.IndexOf(roundEvents) + 1}!");
				foreach (var e in roundEvents)
				{
					if (e is CombatantAppearsEvent cae)
						logListBox.Items.Add($"{cae.Combatant} appears!");
					else if (e is CombatantDisappearsEvent cde)
						logListBox.Items.Add($"{cde.Combatant} disappears!");
					else if (e is WeaponFiresEvent wfe)
					{
						if (wfe.IsHit)
							logListBox.Items.Add($"{wfe.Combatant} fires at {wfe.Target} and hits!");
						else
							logListBox.Items.Add($"{wfe.Combatant} fires at {wfe.Target} and misses.");
					}
				}
			}
			return logListBox;
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

		private void timer_Tick(object sender, EventArgs e)
		{
			if (!battleView.IsPaused)
			{
				logListBox.TopIndex = logListBox.Items.IndexOf($"Begin round {battleView.Round + 1}!");
			}
		}
	}
}