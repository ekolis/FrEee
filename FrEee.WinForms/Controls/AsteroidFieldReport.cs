using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using FrEee.Utility;
using FrEee.WinForms.Interfaces;
using System;

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
				Bind();
			}
		}

		private void picPortrait_Click(object sender, System.EventArgs e)
		{
			picPortrait.ShowFullSize(AsteroidField.Name);
		}

		public void Bind(AsteroidField data)
		{
			AsteroidField = data;
		}

		public void Bind()
		{
			if (AsteroidField == null)
				Visible = false;
			else
			{
				Visible = true;

				picPortrait.Image = AsteroidField.Portrait;
				if (AsteroidField.Timestamp == Galaxy.Current.Timestamp)
					txtAge.Text = "Current";
				else if (Galaxy.Current.Timestamp - AsteroidField.Timestamp <= 1)
					txtAge.Text = "Last turn";
				else
					txtAge.Text = Math.Ceiling(Galaxy.Current.Timestamp - AsteroidField.Timestamp) + " turns ago";

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
		}
	}
}
