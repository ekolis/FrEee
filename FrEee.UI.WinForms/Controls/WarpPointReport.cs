using FrEee.Objects.Space;
using FrEee.Extensions;
using FrEee.UI.WinForms.Interfaces;
using System.Drawing;
using System.Windows.Forms;
using FrEee.Modding.Abilities;

namespace FrEee.UI.WinForms.Controls;

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

	private WarpPoint warpPoint;

	public void Bind()
	{
		SuspendLayout();
		if (WarpPoint == null)
			Visible = false;
		else
		{
			Visible = true;

			picPortrait.Image = WarpPoint.Portrait;
			txtAge.Text = WarpPoint.Timestamp.GetMemoryAgeDescription();
			txtAge.BackColor = txtAge.Text == "Current" ? Color.Transparent : Color.FromArgb(64, 64, 0);

			txtName.Text = WarpPoint.Name;
			txtSize.Text = WarpPoint.StellarSize + " Warp Point";
			txtDescription.Text = WarpPoint.Description;

			abilityTreeView.Abilities = WarpPoint.AbilityTree();
			abilityTreeView.IntrinsicAbilities = WarpPoint.IntrinsicAbilities;
		}
		ResumeLayout();
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