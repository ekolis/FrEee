using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Interfaces;
using System;

namespace FrEee.WinForms.Controls
{
	public partial class WarpPointReport : UserControl, IBindable<WarpPoint>
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
				Bind();
			}
		}

		public void Bind()
		{
			if (WarpPoint == null)
				Visible = false;
			else
			{
				Visible = true;

				picPortrait.Image = WarpPoint.Portrait;
				if (WarpPoint.Timestamp == Galaxy.Current.Timestamp)
					txtAge.Text = "Current";
				else if (Galaxy.Current.Timestamp - WarpPoint.Timestamp <= 1)
					txtAge.Text = "Last turn";
				else
					txtAge.Text = Math.Ceiling(Galaxy.Current.Timestamp - WarpPoint.Timestamp) + " turns ago";

				txtName.Text = WarpPoint.Name;
				txtSize.Text = WarpPoint.StellarSize + " Warp Point";
				txtDescription.Text = WarpPoint.Description;

				abilityTreeView.Abilities = WarpPoint.UnstackedAbilities.StackToTree(WarpPoint);
				abilityTreeView.IntrinsicAbilities = WarpPoint.IntrinsicAbilities;
			}
		}

		public void Bind(WarpPoint data)
		{
			WarpPoint = data;
			Bind();
		}

		private void picPortrait_Click(object sender, System.EventArgs e)
		{
			picPortrait.ShowFullSize(WarpPoint.Name);
		}
	}
}
