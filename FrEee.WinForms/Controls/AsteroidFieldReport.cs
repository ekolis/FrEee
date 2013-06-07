using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;

namespace FrEee.WinForms.Controls
{
	public partial class AsteroidFieldReport : UserControl
	{
		public AsteroidFieldReport()
		{
			InitializeComponent();
		}

		public AsteroidFieldReport(AsteroidField asteroidField)
			: this()
		{
			AsteroidField = asteroidField;
		}

		private AsteroidField asteroidField;

		/// <summary>
		/// The asteroid field for which to display a report.
		/// </summary>
		public AsteroidField AsteroidField
		{
			get { return asteroidField; }
			set
			{
				asteroidField = value;
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (AsteroidField == null)
				Visible = false;
			else
			{
				Visible = true;

				picOwnerFlag.Image = null; // TODO - load owner flag
				picPortrait.Image = AsteroidField.Portrait;

				txtName.Text = AsteroidField.Name;
				txtSizeSurface.Text = AsteroidField.Size + " " + AsteroidField.Surface + " Asteroid Field";
				txtAtmosphere.Text = AsteroidField.Atmosphere;
				txtConditions.Text = ""; // TODO - load conditions

				// TODO - load resource value
				txtValueMinerals.Text = "";
				txtValueOrganics.Text = "";
				txtValueRadioactives.Text = "";

				txtDescription.Text = AsteroidField.Description;

				treeAbilities.Nodes.Clear();
				foreach (var group in AsteroidField.Abilities.GroupBy(abil => abil.Name))
				{
					var branch = new TreeNode(group.Stack().First().ToString());
					if (group.Any(abil => !AsteroidField.IntrinsicAbilities.Contains(abil)))
						branch.NodeFont = new Font(Font, FontStyle.Italic);
					treeAbilities.Nodes.Add(branch);
					foreach (var abil in group)
					{
						var twig = new TreeNode(abil.Description);
						if (AsteroidField.IntrinsicAbilities.Contains(abil))
							twig.NodeFont = new Font(Font, FontStyle.Italic);
						branch.Nodes.Add(twig);
					}
				}
			}

			base.OnPaint(e);
		}

		private void picPortrait_Click(object sender, System.EventArgs e)
		{
			picPortrait.ShowFullSize(AsteroidField.Name);
		}
	}
}
