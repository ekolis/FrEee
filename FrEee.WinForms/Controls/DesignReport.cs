using System.Windows.Forms;
using System.Linq;
using FrEee.Game.Objects.Space;
using FrEee.Game.Interfaces;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System.Drawing;

namespace FrEee.WinForms.Controls
{
	/// <summary>
	/// Displays a report on a vehicle design.
	/// </summary>
	public partial class DesignReport : UserControl
	{
		public DesignReport()
		{
			InitializeComponent();
		}

		public DesignReport(IDesign design)
			: this()
		{
			Design = design;
		}

		private IDesign design;

		/// <summary>
		/// The design for which to display a report.
		/// </summary>
		public IDesign Design
		{
			get { return design; }
			set
			{
				design = value;
				Bind();
			}
		}

		public void Bind()
		{
			if (Design == null)
				Visible = false;
			else
			{
				Visible = true;
				txtName.Text = Design.Name;
				txtVehicleType.Text = Design.VehicleTypeName;
				txtHull.Text = Design.Hull.Name + " (" + Design.Hull.Size.Kilotons() + ")";
				txtRole.Text = Design.Role;
				txtStrategy.Text = ""; // TODO - implement design combat strategies
				txtDate.Text = Design.TurnNumber.ToStardate();

				lstComponents.Items.Clear();
				lstComponents.Initialize(32, 32);
				foreach (var g in Design.Components.GroupBy(mct => mct))
					lstComponents.AddItemWithImage(g.First().ComponentTemplate.Group, g.Count() + "x " + g.First().ToString(), g, g.First().Icon);
			}
		}

		private void picPortrait_Click(object sender, System.EventArgs e)
		{
			picPortrait.ShowFullSize(Design.Name);
		}
	}
}
