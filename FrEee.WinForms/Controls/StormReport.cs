using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Interfaces;
using System;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
	public partial class StormReport : UserControl, IBindable<Storm>
	{
		public StormReport()
		{
			InitializeComponent();
		}

		public StormReport(Storm storm)
			: this()
		{
			Storm = storm;
		}

		/// <summary>
		/// The storm for which to display a report.
		/// </summary>
		public Storm Storm
		{
			get { return storm; }
			set
			{
				storm = value;
				Bind();
			}
		}

		private Storm storm;

		public void Bind()
		{
			SuspendLayout();
			if (Storm == null)
				Visible = false;
			else
			{
				Visible = true;

				picPortrait.Image = Storm.Portrait;

				if (Storm.Timestamp == Galaxy.Current.Timestamp)
					txtAge.Text = "Current";
				else if (Galaxy.Current.Timestamp - Storm.Timestamp <= 1)
					txtAge.Text = "Last turn";
				else
					txtAge.Text = Math.Ceiling(Galaxy.Current.Timestamp - Storm.Timestamp) + " turns ago";

				txtName.Text = Storm.Name;
				txtSize.Text = Storm.StellarSize + " Storm";
				txtDescription.Text = Storm.Description;

				abilityTreeView.Abilities = Storm.AbilityTree();
				abilityTreeView.IntrinsicAbilities = Storm.IntrinsicAbilities;
			}
			ResumeLayout();
		}

		public void Bind(Storm data)
		{
			Storm = data;
			Bind();
		}

		private void picPortrait_Click(object sender, System.EventArgs e)
		{
			picPortrait.ShowFullSize(Storm.Name);
		}
	}
}