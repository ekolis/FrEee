using System.Windows.Forms;
using FrEee.Game.Objects.Space;

namespace FrEee.WinForms.Controls
{
	/// <summary>
	/// Displays a report on a star.
	/// </summary>
	public partial class StarReport : UserControl
	{
		public StarReport()
		{
			InitializeComponent();
		}

		public StarReport(Star star)
			: this()
		{
			Star = star;
		}

		private Star star;

		/// <summary>
		/// The star for which to display a report.
		/// </summary>
		public Star Star
		{
			get { return star; }
			set
			{
				star = value;
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (Star == null)
				Visible = false;
			else
			{
				Visible = true;
				txtAge.Text = Star.Age;
				txtBrightness.Text = Star.Brightness;
				txtName.Text = Star.Name;
				txtSizeColor.Text = Star.StellarSize + " " + Star.Color + " Star";
				txtDescription.Text = Star.Description;
				picPortrait.Image = Star.Portrait;
				lstAbilities.Items.Clear();
				foreach (var abil in Star.Abilities)
					lstAbilities.Items.Add(abil.Description);
			}

			base.OnPaint(e);
		}
	}
}
