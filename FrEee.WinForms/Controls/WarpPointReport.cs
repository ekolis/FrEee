using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;

namespace FrEee.WinForms.Controls
{
	public partial class WarpPointReport : UserControl
	{
		public WarpPointReport()
		{
			InitializeComponent();
		}

		public WarpPointReport(WarpPoint warpPoint)
			: this()
		{
			WarpPoint = warpPoint;
		}

		private WarpPoint warpPoint;

		/// <summary>
		/// The warp point for which to display a report.
		/// </summary>
		public WarpPoint WarpPoint
		{
			get { return warpPoint; }
			set
			{
				warpPoint = value;
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (WarpPoint == null)
				Visible = false;
			else
			{
				Visible = true;

				picPortrait.Image = WarpPoint.Portrait;

				txtName.Text = WarpPoint.Name;
				txtSize.Text = WarpPoint.StellarSize + " Warp Point";
				txtDescription.Text = WarpPoint.Description;

				abilityTreeView.Abilities = WarpPoint.UnstackedAbilities.StackToTree();
				abilityTreeView.IntrinsicAbilities = WarpPoint.IntrinsicAbilities;
			}

			base.OnPaint(e);
		}

		private void picPortrait_Click(object sender, System.EventArgs e)
		{
			picPortrait.ShowFullSize(WarpPoint.Name);
		}
	}
}
