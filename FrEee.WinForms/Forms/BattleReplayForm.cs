using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Combat;
using FrEee.Objects.Combat.Grid;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Controls;
using FrEee.WinForms.Interfaces;
using FrEee.WinForms.Objects;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class BattleReplayForm : GameForm, IBindable<Battle>
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

		private void battleView_MouseDown(object sender, MouseEventArgs e)
		{
			SelectedCombatant = battleView.SelectedCombatant;
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			battleView.Round--;
			minimap.Round--;
			ScrollLogListBox();
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
			ScrollLogListBox();
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

		private ListBox CreateLogListBox()
		{
			logListBox = new ListBox();
			logListBox.BackColor = Color.Black;
			logListBox.ForeColor = Color.White;
			logListBox.Dock = DockStyle.Fill;
			logListBox.HorizontalScrollbar = true;
			reportPanel.Controls.Add(logListBox);
			foreach (var roundEvents in Battle.Events)
			{
				logListBox.Items.Add($"Begin round {Battle.Events.IndexOf(roundEvents) + 1}!");
				foreach (var e in roundEvents)
				{
					if (e is CombatantAppearsEvent cae)
						logListBox.Items.Add($"{cae.Combatant} appears!");
					if (e is CombatantLaunchedEvent cle)
						logListBox.Items.Add($"{cle.Launcher} launches {cle.Combatant}.");
					else if (e is CombatantDisappearsEvent cde)
						logListBox.Items.Add($"{cde.Combatant} disappears!");
					else if (e is CombatantDestroyedEvent cde2)
						logListBox.Items.Add($"{cde2.Combatant} is destroyed!");
					else if (e is WeaponFiresEvent wfe)
					{
						if (wfe.IsHit)
							logListBox.Items.Add($"{wfe.Combatant} fires {wfe.Weapon} at {wfe.Target} and hits for {wfe.Damage} damage!");
						else
							logListBox.Items.Add($"{wfe.Combatant} fires {wfe.Weapon} at {wfe.Target} and misses.");
					}
					else if (e is CombatantsCollideEvent cce)
						logListBox.Items.Add($"{cce.Combatant} collides with {cce.Target}, causing {cce.TargetDamage} damage to {cce.Target} and {cce.CombatantDamage} to itself!");
				}
			}
			return logListBox;
		}

		private void minimap_MouseDown(object sender, MouseEventArgs e)
		{
			battleView.FocusedLocation = minimap.ClickLocation;
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

		private void ScrollLogListBox()
		{
			var text = $"Begin round {battleView.Round + 1}!";
			logListBox.TopIndex = logListBox.Items.IndexOf(text);
			logListBox.SelectedIndex= logListBox.Items.IndexOf(text);
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (!battleView.IsPaused)
				ScrollLogListBox();
		}

		private void BattleReplayForm_Load(object sender, EventArgs e)
		{
			MusicMood mood;
			switch (Battle.ResultFor(Empire.Current))
			{
				case "victory":
					mood = MusicMood.Upbeat;
					break;
				case "defeat":
					mood = MusicMood.Sad;
					break;
				case "battle":
					mood = MusicMood.Peaceful;
					break;
				default:
					mood = MusicMood.Tense;
					break;
			}
			Music.Play(MusicMode.Combat,mood);
		}

		private MusicMode prevMusicMode = Music.CurrentMode;
		private MusicMood prevMusicMood = Music.CurrentMood;

		private void BattleReplayForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			// revert to previously playing style of music
			Music.Play(prevMusicMode, prevMusicMood);
		}
	}
}