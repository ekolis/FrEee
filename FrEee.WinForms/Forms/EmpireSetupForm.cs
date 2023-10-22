using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Setup;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using FrEee.Extensions;

namespace FrEee.WinForms.Forms
{
	public partial class EmpireSetupForm : GameForm
	{
		public EmpireSetupForm()
		{
			InitializeComponent();
			BindChoices();

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
		}

		public EmpireTemplate EmpireTemplate
		{
			get
			{
				return empireTemplate;
			}
			set
			{
				originalEmpireTemplate = value.Copy();
				originalRace = value.PrimaryRace.Copy();
				empireTemplate = value;
				Bind();
			}
		}

		public int PointsSpent
		{
			get
			{
				int result = 0;
				foreach (var t in raceTraitPicker.CheckedTraits)
					result += t.Cost.Value;
				result += aptitudePicker.Cost;
				return result;
			}
		}

		/// <summary>
		/// The number of empire setup points available for spending.
		/// </summary>
		public int PointsToSpend { get; set; }

		private EmpireTemplate empireTemplate;

		private string lastDdlPicText = null;

		/// <summary>
		/// A copy of the original empire template, in case the user cancels.
		/// </summary>
		private EmpireTemplate originalEmpireTemplate;

		/// <summary>
		/// A copy of the original race, in case the user cancels.
		/// </summary>
		private Race originalRace;

		private void aptitudePicker_AptitudeValueChanged(Controls.AptitudePicker ap, Aptitude aptitude, int newValue)
		{
			BindPointsSpent();
		}

		private void Bind()
		{
			if (EmpireTemplate == null)
			{
				EmpireTemplate = new EmpireTemplate();
				EmpireTemplate.IsPlayerEmpire = true;
			}
			if (EmpireTemplate.PrimaryRace == null)
				EmpireTemplate.PrimaryRace = new Race();

			txtName.Text = EmpireTemplate.Name;
			txtLeaderName.Text = EmpireTemplate.LeaderName;
			ddlLeaderPortrait.Text = EmpireTemplate.LeaderPortraitName;
			spnColorRed.Value = EmpireTemplate.Color.R;
			spnColorGreen.Value = EmpireTemplate.Color.G;
			spnColorBlue.Value = EmpireTemplate.Color.B;
			ddlInsignia.Text = EmpireTemplate.InsigniaName;
			ddlShipset.Text = EmpireTemplate.ShipsetName;
			ddlAI.Text = EmpireTemplate.AIName;
			ddlCulture.SelectedItem = EmpireTemplate.Culture;

			// race general stuff
			txtRaceName.Text = EmpireTemplate.PrimaryRace.Name;
			ddlRacePopulationIcon.Text = EmpireTemplate.PrimaryRace.PopulationIconName;
			ddlRaceNativeSurface.SelectedItem = EmpireTemplate.PrimaryRace.NativeSurface;
			ddlRaceNativeAtmosphere.SelectedItem = EmpireTemplate.PrimaryRace.NativeAtmosphere;
			ddlRaceHappiness.SelectedItem = EmpireTemplate.PrimaryRace.HappinessModel;

			// race traits
			foreach (var trait in raceTraitPicker.Traits)
				raceTraitPicker.SetTraitChecked(trait, EmpireTemplate.PrimaryRace.Traits.Contains(trait));

			// race aptitudes
			foreach (var apt in Aptitude.All)
			{
				int val = 100;
				if (EmpireTemplate.PrimaryRace.Aptitudes.ContainsKey(apt.Name))
					val = EmpireTemplate.PrimaryRace.Aptitudes[apt.Name];
				aptitudePicker.SetValue(apt, val);
			}

			// is AI empire?
			chkAIsCanUse.Checked = EmpireTemplate.AIsCanUse;

			BindPointsSpent();

			BindPictures();
		}

		private void BindChoices()
		{
			foreach (var portrait in Pictures.ListLeaderPortraits())
			{
				ddlLeaderPortrait.Items.Add(portrait);
			}
			foreach (var icon in Pictures.ListPopulationIcons())
			{
				ddlRacePopulationIcon.Items.Add(icon);
			}
			foreach (var surface in The.Mod.StellarObjectTemplates.OfType<Planet>().Select(p => p.Surface).Distinct())
			{
				ddlRaceNativeSurface.Items.Add(surface);
			}
			foreach (var atmosphere in The.Mod.StellarObjectTemplates.OfType<Planet>().Select(p => p.Atmosphere).Distinct())
			{
				ddlRaceNativeAtmosphere.Items.Add(atmosphere);
			}
			foreach (var insignia in Pictures.ListInsignia())
			{
				ddlInsignia.Items.Add(insignia);
			}
			foreach (var shipset in Pictures.ListShipsets())
			{
				ddlShipset.Items.Add(shipset);
			}
			foreach (var ai in The.Mod.EmpireAIs)
			{
				ddlAI.Items.Add(ai);
			}
			foreach (var h in The.Mod.HappinessModels)
			{
				ddlRaceHappiness.Items.Add(h);
			}
			foreach (var c in The.Mod.Cultures)
			{
				ddlCulture.Items.Add(c);
			}
			foreach (var n in The.Mod.DesignNamesFiles)
			{
				ddlDesignNames.Items.Add(n);
			}
			raceTraitPicker.Traits = The.Mod.Traits;
		}

		private void BindPictures()
		{
			ddlRacePopulationIcon_TextChanged(null, null);
			ddlLeaderPortrait_TextChanged(null, null);
			ddlInsignia_TextChanged(null, null);
			ddlShipset_TextChanged(null, null);
			spnColor_ValueChanged(null, null);
		}

		private void BindPointsSpent()
		{
			int pointsAvailable = PointsToSpend - PointsSpent;
			txtPointsAvailable.Text = "Points Available: " + pointsAvailable + " / " + PointsToSpend;
			if (pointsAvailable < 0)
				txtPointsAvailable.ForeColor = Color.FromArgb(255, 128, 128);
			else
				txtPointsAvailable.ForeColor = Color.White;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			// revert changes
			EmpireTemplate = originalEmpireTemplate;
			EmpireTemplate.PrimaryRace = originalRace;
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnCompareCultures_Click(object sender, EventArgs e)
		{
			this.ShowChildForm(new CultureComparisonForm());
		}

		private void btnLoadRace_Click(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			dlg.InitialDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Races");
			dlg.Filter = "Races (*.rac)|*.rac";
			var result = dlg.ShowDialog();
			if (result == DialogResult.OK)
			{
				EmpireTemplate.PrimaryRace = Race.Load(dlg.FileName);
				Bind();
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			SaveChanges();

			// validate
			var warnings = EmpireTemplate.GetWarnings(PointsToSpend);
			if (warnings.Any())
				MessageBox.Show(warnings.First(), "FrEee");
			else
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void btnSaveRace_Click(object sender, EventArgs e)
		{
			SaveChanges();
			var dlg = new SaveFileDialog();
			dlg.InitialDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Races");
			dlg.Filter = "Races (*.rac)|*.rac";
			var result = dlg.ShowDialog();
			if (result == DialogResult.OK)
				EmpireTemplate.PrimaryRace.Save(dlg.FileName);
		}

		private void ddlCulture_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlCulture.SelectedItem != null)
				txtCulture.Text = ((Culture)ddlCulture.SelectedItem).Description;
			else
				txtCulture.Text = "Please choose a culture.";
		}

		private void ddlInsignia_TextChanged(object sender, EventArgs e)
		{
			var emp = new Empire();
			emp.InsigniaName = ddlInsignia.Text;
			picInsignia.Image = emp.Icon;
		}

		private void ddlLeaderPortrait_TextChanged(object sender, EventArgs e)
		{
			var emp = new Empire();
			emp.LeaderPortraitName = ddlLeaderPortrait.Text;
			picLeaderPortrait.Image = emp.Portrait;
		}

		private void ddlPic_Leave(object sender, EventArgs e)
		{
			// auto update ddl's that have no value to match this ddl
			var ddl = (ComboBox)sender;
			var text = ddl.Text;
			foreach (var ddl2 in new ComboBox[]
				{
					ddlLeaderPortrait,
					ddlRacePopulationIcon,
					ddlShipset,
					ddlInsignia
				})
			{
				if (string.IsNullOrWhiteSpace(ddl2.Text))
					ddl2.Text = ddl.Text;
			}
			lastDdlPicText = ddl.Text;
		}

		private void ddlPic_TextChanged(object sender, EventArgs e)
		{
		}

		private void ddlRaceHappiness_SelectedIndexChanged(object sender, EventArgs e)
		{
			var h = (HappinessModel)ddlRaceHappiness.SelectedItem;
			if (h != null)
				txtRaceHappiness.Text = h.Description;
			else
				txtRaceHappiness.Text = "Please choose a happiness model.";
		}

		private void ddlRacePopulationIcon_TextChanged(object sender, EventArgs e)
		{
			var emp = new Empire();
			emp.PrimaryRace = new Race();
			emp.PrimaryRace.PopulationIconName = ddlRacePopulationIcon.Text;
			picRacePopulationIcon.Image = emp.PrimaryRace.Icon;
		}

		private void ddlShipset_TextChanged(object sender, EventArgs e)
		{
			picShipset.Image = The.Mod.Hulls.First().GetPortrait(ddlShipset.Text);
		}

		private void empireTraitPicker_TraitToggled(Controls.TraitPicker picker, Trait trait, bool state)
		{
			BindPointsSpent();
		}

		private void raceTraitPicker_TraitToggled(Controls.TraitPicker picker, Trait trait, bool state)
		{
			BindPointsSpent();
		}

		private void SaveChanges()
		{
			// save changes
			var et = EmpireTemplate;
			var r = et.PrimaryRace;
			r.Name = txtRaceName.Text;
			r.PopulationIconName = ddlRacePopulationIcon.Text;
			r.NativeSurface = (string)ddlRaceNativeSurface.SelectedItem;
			r.NativeAtmosphere = (string)ddlRaceNativeAtmosphere.SelectedItem;
			r.HappinessModel = (HappinessModel)ddlRaceHappiness.SelectedItem;
			r.TraitNames.Clear();
			foreach (var t in raceTraitPicker.CheckedTraits)
				r.TraitNames.Add(t.Name);
			foreach (var kvp in aptitudePicker.Values)
			{
				if (r.Aptitudes.ContainsKey(kvp.Key.Name))
					r.Aptitudes[kvp.Key.Name] = kvp.Value;
				else
					r.Aptitudes.Add(kvp.Key.Name, kvp.Value);
			}
			et.Name = txtName.Text;
			et.AIsCanUse = chkAIsCanUse.Checked;
			et.LeaderName = txtLeaderName.Text;
			et.LeaderPortraitName = ddlLeaderPortrait.Text;
			et.Color = Color.FromArgb((int)spnColorRed.Value, (int)spnColorGreen.Value, (int)spnColorBlue.Value);
			et.InsigniaName = ddlInsignia.Text;
			et.ShipsetName = ddlShipset.Text;
			et.AIName = ddlAI.Text;
			et.Culture = (Culture)ddlCulture.SelectedItem;
			et.DesignNamesFile = (string)ddlDesignNames.SelectedItem;
		}

		private void spnColor_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		private void spnColor_Leave(object sender, EventArgs e)
		{
			// round to next 85
			var spn = (NumericUpDown)sender;
			spn.Value = Math.Ceiling(spn.Value / 85m) * 85m;
		}

		private void spnColor_ValueChanged(object sender, EventArgs e)
		{
			picColor.BackColor = Color.FromArgb((int)spnColorRed.Value, (int)spnColorGreen.Value, (int)spnColorBlue.Value);
		}

		private void spnColorRed_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
		}
	}
}