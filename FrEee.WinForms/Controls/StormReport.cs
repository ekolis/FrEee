using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Interfaces;

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

		private Storm storm;

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

		public void Bind()
		{
			if (Storm == null)
				Visible = false;
			else
			{
				Visible = true;

				picPortrait.Image = Storm.Portrait;

				txtName.Text = Storm.Name;
				txtSize.Text = Storm.StellarSize + " Storm";
				txtDescription.Text = Storm.Description;

				abilityTreeView.Abilities = Storm.UnstackedAbilities.StackToTree();
				abilityTreeView.IntrinsicAbilities = Storm.IntrinsicAbilities;
			}
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
