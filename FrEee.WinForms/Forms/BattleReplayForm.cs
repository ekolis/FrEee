using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat.Grid;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Interfaces;
using System;
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
		}

		private void btnForward_Click(object sender, EventArgs e)
		{
			battleView.Round++;
			minimap.Round++;
		}
	}
}