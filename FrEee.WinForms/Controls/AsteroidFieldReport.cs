using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
	public partial class AsteroidFieldReport : UserControl, IBindable<AsteroidField>
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

		/// <summary>
		/// The asteroid field for which to display a report.
		/// </summary>
		public AsteroidField AsteroidField
		{
			get { return asteroidField; }
			set
			{
				asteroidField = value;
				Bind();
			}
		}

		private AsteroidField asteroidField;

		public void Bind(AsteroidField data)
		{
			AsteroidField = data;
		}

		public void Bind()
		{
			SuspendLayout();
			if (AsteroidField == null)
				Visible = false;
			else
			{
				Visible = true;

				picPortrait.Image = AsteroidField.Portrait;
				txtAge.Text = AsteroidField.Timestamp.GetMemoryAgeDescription();
				txtAge.BackColor = txtAge.Text == "Current" ? Color.Transparent : Color.FromArgb(64, 64, 0);

				txtName.Text = AsteroidField.Name;
				txtSizeSurface.Text = AsteroidField.Size + " " + AsteroidField.Surface + " Asteroid Field";
				txtAtmosphere.Text = AsteroidField.Atmosphere;
				txtConditions.Text = ""; // TODO - load conditions

				txtValueMinerals.Text = AsteroidField.ResourceValue[Resource.Minerals].ToUnitString();
				txtValueOrganics.Text = AsteroidField.ResourceValue[Resource.Organics].ToUnitString();
				txtValueRadioactives.Text = AsteroidField.ResourceValue[Resource.Radioactives].ToUnitString();

				txtDescription.Text = AsteroidField.Description;

				abilityTreeView.Abilities = AsteroidField.AbilityTree();
				abilityTreeView.IntrinsicAbilities = AsteroidField.IntrinsicAbilities;
			}
			ResumeLayout();
		}

		private void picPortrait_Click(object sender, System.EventArgs e)
		{
			picPortrait.ShowFullSize(AsteroidField.Name);
		}
	}
}