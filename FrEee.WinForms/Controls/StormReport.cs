using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;

namespace FrEee.WinForms.Controls
{
	public partial class StormReport : UserControl
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
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (Storm == null)
				Visible = false;
			else
			{
				Visible = true;

				picOwnerFlag.Image = null; // TODO - load owner flag
				picPortrait.Image = Storm.Portrait;

				txtName.Text = Storm.Name;
				txtSize.Text = Storm.StellarSize + " Storm";
				txtDescription.Text = Storm.Description;

				abilityTreeView.Abilities = Storm.Abilities.StackToTree();
				abilityTreeView.IntrinsicAbilities = Storm.IntrinsicAbilities;
			}

			base.OnPaint(e);
		}

		private void picPortrait_Click(object sender, System.EventArgs e)
		{
			picPortrait.ShowFullSize(Storm.Name);
		}
	}
}
