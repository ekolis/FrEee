using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Interfaces;
using System;
using System.Drawing;
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

				txtAge.Text = Storm.Timestamp.GetMemoryAgeDescription();
				txtAge.BackColor = txtAge.Text == "Current" ? Color.Transparent : Color.FromArgb(64, 64, 0);

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