using FrEee.Game.Objects.AI;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Setup;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class EmpireSetupForm : Form
	{
		public EmpireSetupForm()
		{
			InitializeComponent();
			BindChoices();

			// hide trait pages if none to pick
			if (!Mod.Current.Traits.Where(t => t.IsRacial).Any())
				tabs.TabPages.Remove(tabRaceTraits);
			if (!Mod.Current.Traits.Where(t => !t.IsRacial).Any())
				tabs.TabPages.Remove(tabEmpireTraits);
		}

		/// <summary>
		/// A copy of the original empire template, in case the user cancels.
		/// </summary>
		private EmpireTemplate originalEmpireTemplate;

		/// <summary>
		/// A copy of the original race, in case the user cancels.
		/// </summary>
		private Race originalRace;


		private EmpireTemplate empireTemplate;
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

		/// <summary>
		/// The number of empire setup points available for spending.
		/// </summary>
		public int PointsToSpend { get; set; }

		private void BindChoices()
		{
			foreach (var portrait in Pictures.ListLeaderPortraits())
			{
				ddlRaceLeaderPortrait.Items.Add(portrait);
				ddlLeaderPortrait.Items.Add(portrait);
			}
			foreach (var icon in Pictures.ListPopulationIcons())
			{
				ddlRacePopulationIcon.Items.Add(icon);
			}
			foreach (var surface in Mod.Current.StellarObjectTemplates.OfType<Planet>().Select(p => p.Surface).Distinct())
			{
				ddlRaceNativeSurface.Items.Add(surface);
			}
			foreach (var atmosphere in Mod.Current.StellarObjectTemplates.OfType<Planet>().Select(p => p.Atmosphere).Distinct())
			{
				ddlRaceNativeAtmosphere.Items.Add(atmosphere);
			}
			foreach (var insignia in Pictures.ListInsignia())
			{
				ddlRaceInsignia.Items.Add(insignia);
				ddlInsignia.Items.Add(insignia);
			}
			foreach (var shipset in Pictures.ListShipsets())
			{
				ddlRaceShipset.Items.Add(shipset);
				ddlShipset.Items.Add(shipset);
			}
			foreach (var ai in ListAIs())
			{
				ddlRaceAI.Items.Add(ai);
				ddlAI.Items.Add(ai);
			}
			foreach (var h in Mod.Current.HappinessModels)
			{
				ddlRaceHappiness.Items.Add(h);
				ddlHappiness.Items.Add(h);
			}
			foreach (var c in Mod.Current.Cultures)
			{
				ddlRaceCulture.Items.Add(c);
				ddlCulture.Items.Add(c);
			}
			raceTraitPicker.Traits = Mod.Current.Traits.Where(t => t.IsRacial);
			empireTraitPicker.Traits = Mod.Current.Traits.Where(t => !t.IsRacial);
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

		private IEnumerable<string> ListAIs()
		{
			// TODO - list empire AIs
			yield break;
		}

		private void Bind()
		{
			if (EmpireTemplate == null)
				EmpireTemplate = new EmpireTemplate();
			if (EmpireTemplate.PrimaryRace == null)
				EmpireTemplate.PrimaryRace = new Race();

			// race general stuff
			txtRaceName.Text = EmpireTemplate.PrimaryRace.Name;
			chkRaceAIsCanUse.Checked = EmpireTemplate.PrimaryRace.AIsCanUse;
			txtRaceDefaultEmpireName.Text = EmpireTemplate.PrimaryRace.EmpireName;
			txtRaceLeaderName.Text = EmpireTemplate.PrimaryRace.LeaderName;
			ddlRaceLeaderPortrait.Text = EmpireTemplate.PrimaryRace.LeaderPortraitName;
			ddlRacePopulationIcon.Text = EmpireTemplate.PrimaryRace.PopulationIconName;
			ddlRaceNativeSurface.SelectedItem = EmpireTemplate.PrimaryRace.NativeSurface;
			ddlRaceNativeAtmosphere.SelectedItem = EmpireTemplate.PrimaryRace.NativeAtmosphere;
			spnRaceColorRed.Value = EmpireTemplate.PrimaryRace.Color.R;
			spnRaceColorGreen.Value = EmpireTemplate.PrimaryRace.Color.G;
			spnRaceColorBlue.Value = EmpireTemplate.PrimaryRace.Color.B;
			ddlRaceInsignia.Text = EmpireTemplate.PrimaryRace.InsigniaName;
			ddlRaceShipset.Text = EmpireTemplate.PrimaryRace.ShipsetPath;
			// TODO - race AI
			ddlRaceHappiness.SelectedItem = EmpireTemplate.PrimaryRace.HappinessModel;
			ddlRaceCulture.SelectedItem = EmpireTemplate.PrimaryRace.Culture;

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
			
			// empire overrides for race stuff
			if (EmpireTemplate.Name == null)
			{
				txtName.Text = EmpireTemplate.PrimaryRace.EmpireName;
				chkNameFromRace.Checked = true;
			}
			else
			{
				txtName.Text = EmpireTemplate.Name;
				chkNameFromRace.Checked = false;
			}
			if (EmpireTemplate.LeaderName == null)
			{
				txtLeaderName.Text = EmpireTemplate.PrimaryRace.LeaderName;
				chkLeaderNameFromRace.Checked = true;
			}
			else
			{
				txtLeaderName.Text = EmpireTemplate.LeaderName;
				chkLeaderNameFromRace.Checked = false;
			}
			if (EmpireTemplate.LeaderPortraitName == null)
			{
				ddlLeaderPortrait.Text = EmpireTemplate.PrimaryRace.LeaderName;
				chkLeaderPortraitFromRace.Checked = true;
			}
			else
			{
				ddlLeaderPortrait.Text = EmpireTemplate.LeaderPortraitName;
				chkLeaderPortraitFromRace.Checked = false;
			}
			if (EmpireTemplate.Color == null)
			{
				spnColorRed.Value = EmpireTemplate.PrimaryRace.Color.R;
				spnColorGreen.Value = EmpireTemplate.PrimaryRace.Color.G;
				spnColorBlue.Value = EmpireTemplate.PrimaryRace.Color.B;
				chkLeaderPortraitFromRace.Checked = true;
			}
			else
			{
				spnColorRed.Value = EmpireTemplate.Color.Value.R;
				spnColorGreen.Value = EmpireTemplate.Color.Value.G;
				spnColorBlue.Value = EmpireTemplate.Color.Value.B;
				chkLeaderPortraitFromRace.Checked = false;
			}
			if (EmpireTemplate.InsigniaName == null)
			{
				ddlInsignia.Text = EmpireTemplate.PrimaryRace.InsigniaName;
				chkInsigniaFromRace.Checked = true;
			}
			else
			{
				ddlInsignia.Text = EmpireTemplate.InsigniaName;
				chkInsigniaFromRace.Checked = false;
			}
			if (EmpireTemplate.ShipsetPath == null)
			{
				ddlShipset.Text = EmpireTemplate.PrimaryRace.ShipsetPath;
				chkShipsetFromRace.Checked = true;
			}
			else
			{
				ddlShipset.Text = EmpireTemplate.ShipsetPath;
				chkShipsetFromRace.Checked = false;
			}
			// TODO - empire AI
			if (EmpireTemplate.HappinessModel == null)
			{
				ddlHappiness.SelectedValue = EmpireTemplate.PrimaryRace.HappinessModel;
				chkHappinessFromRace.Checked = true;
			}
			else
			{
				ddlHappiness.SelectedValue = EmpireTemplate.HappinessModel;
				chkHappinessFromRace.Checked = false;
			}
			if (EmpireTemplate.Culture == null)
			{
				ddlCulture.SelectedValue = EmpireTemplate.PrimaryRace.Culture;
				chkCultureFromRace.Checked = true;
			}
			else
			{
				ddlCulture.SelectedValue = EmpireTemplate.Culture;
				chkCultureFromRace.Checked = false;
			}

			// empire traits
			foreach (var trait in empireTraitPicker.Traits)
				empireTraitPicker.SetTraitChecked(trait, EmpireTemplate.Traits.Contains(trait));

			BindPointsSpent();

			BindPictures();
		}

		private void BindPictures()
		{
			ddlRaceLeaderPortrait_TextChanged(null, null);
			ddlRacePopulationIcon_TextChanged(null, null);
			ddlRaceInsignia_TextChanged(null, null);
			ddlRaceShipset_TextChanged(null, null);
			spnRaceColor_ValueChanged(null, null);
			ddlLeaderPortrait_TextChanged(null, null);
			ddlInsignia_TextChanged(null, null);
			ddlShipset_TextChanged(null, null);
			spnColor_ValueChanged(null, null);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			// revert changes
			EmpireTemplate = originalEmpireTemplate;
			EmpireTemplate.PrimaryRace = originalRace;
			DialogResult = DialogResult.Cancel;
			Close();
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

		private void SaveChanges()
		{
			// save changes
			var et = EmpireTemplate;
			var r = et.PrimaryRace;
			r.Name = txtRaceName.Text;
			r.AIsCanUse = chkRaceAIsCanUse.Checked;
			r.EmpireName = txtRaceDefaultEmpireName.Text;
			r.LeaderName = txtRaceLeaderName.Text;
			r.LeaderPortraitName = ddlRaceLeaderPortrait.Text;
			r.PopulationIconName = ddlRacePopulationIcon.Text;
			r.NativeSurface = (string)ddlRaceNativeSurface.SelectedItem;
			r.NativeAtmosphere = (string)ddlRaceNativeAtmosphere.SelectedItem;
			r.Color = picRaceColor.BackColor;
			r.InsigniaName = ddlRaceInsignia.Text;
			r.ShipsetPath = ddlRaceShipset.Text;
			// TODO - set race AI
			r.HappinessModel = (HappinessModel)ddlRaceHappiness.SelectedItem;
			r.Culture = (Culture)ddlRaceCulture.SelectedItem;
			r.Traits.Clear();
			foreach (var t in raceTraitPicker.CheckedTraits)
				r.Traits.Add(t);
			foreach (var kvp in aptitudePicker.Values)
			{
				if (r.Aptitudes.ContainsKey(kvp.Key.Name))
					r.Aptitudes[kvp.Key.Name] = kvp.Value;
				else
					r.Aptitudes.Add(kvp.Key.Name, kvp.Value);
			}
			if (chkNameFromRace.Checked)
				et.Name = null;
			else
				et.Name = txtName.Text;
			et.AIsCanUse = chkAIsCanUse.Checked;
			if (chkLeaderNameFromRace.Checked)
				et.LeaderName = null;
			else
				et.LeaderName = txtLeaderName.Text;
			if (chkLeaderPortraitFromRace.Checked)
				et.LeaderPortraitName = null;
			else
				et.LeaderPortraitName = ddlLeaderPortrait.Text;
			if (chkColorFromRace.Checked)
				et.Color = null;
			else
				et.Color = Color.FromArgb((int)spnColorRed.Value, (int)spnColorGreen.Value, (int)spnColorBlue.Value);
			if (chkInsigniaFromRace.Checked)
				et.InsigniaName = null;
			else
				et.InsigniaName = ddlInsignia.Text;
			if (chkShipsetFromRace.Checked)
				et.ShipsetPath = null;
			else
				et.ShipsetPath = ddlShipset.Text;
			// TODO - empire AI
			if (chkHappinessFromRace.Checked)
				et.HappinessModel = null;
			else
				et.HappinessModel = (HappinessModel)ddlHappiness.SelectedItem;
			if (chkCultureFromRace.Checked)
				et.Culture = null;
			else
				et.Culture = (Culture)ddlCulture.SelectedItem;

			et.Traits.Clear();
			foreach (var t in empireTraitPicker.CheckedTraits)
				et.Traits.Add(t);
		}

		#region silly internal consistency stuff

		private void ddlRaceLeaderPortrait_TextChanged(object sender, EventArgs e)
		{
			var emp = new Empire();
			emp.LeaderPortraitName = ddlRaceLeaderPortrait.Text;
			picRaceLeaderPortrait.Image = emp.Portrait;
			if (chkLeaderPortraitFromRace.Checked)
				ddlLeaderPortrait.Text = ddlRaceLeaderPortrait.Text;
		}

		private void ddlRacePopulationIcon_TextChanged(object sender, EventArgs e)
		{
			var emp = new Empire();
			emp.PrimaryRace = new Race();
			emp.PrimaryRace.PopulationIconName = ddlRacePopulationIcon.Text;
			picRacePopulationIcon.Image = emp.PrimaryRace.Icon;
		}

		private void ddlRaceInsignia_TextChanged(object sender, EventArgs e)
		{
			var emp = new Empire();
			emp.InsigniaName = ddlRaceInsignia.Text;
			picRaceInsignia.Image = emp.Icon;
			if (chkInsigniaFromRace.Checked)
				ddlInsignia.Text = ddlRaceInsignia.Text;
		}


		private void ddlRaceShipset_TextChanged(object sender, EventArgs e)
		{
			picRaceShipset.Image = Mod.Current.Hulls.First().GetPortrait(ddlRaceShipset.Text);
			if (chkShipsetFromRace.Checked)
				ddlShipset.Text = ddlRaceShipset.Text;
		}
		
		private void spnRaceColor_ValueChanged(object sender, EventArgs e)
		{
			picRaceColor.BackColor = Color.FromArgb((int)spnRaceColorRed.Value, (int)spnRaceColorGreen.Value, (int)spnRaceColorBlue.Value);
			if (chkColorFromRace.Checked)
			{
				spnColorRed.Value = spnRaceColorRed.Value;
				spnColorGreen.Value = spnRaceColorGreen.Value;
				spnColorBlue.Value = spnRaceColorBlue.Value;
			}
		}

		private void ddlLeaderPortrait_TextChanged(object sender, EventArgs e)
		{
			var emp = new Empire();
			emp.LeaderPortraitName = ddlLeaderPortrait.Text;
			picLeaderPortrait.Image = emp.Portrait;
			if (chkLeaderPortraitFromRace.Checked)
				ddlLeaderPortrait.Text = ddlRaceLeaderPortrait.Text;
		}

		private void spnColor_ValueChanged(object sender, EventArgs e)
		{
			picColor.BackColor = Color.FromArgb((int)spnRaceColorRed.Value, (int)spnColorGreen.Value, (int)spnColorBlue.Value);
		}

		private void ddlInsignia_TextChanged(object sender, EventArgs e)
		{
			var emp = new Empire();
			emp.InsigniaName = ddlInsignia.Text;
			picInsignia.Image = emp.Icon;
		}

		private void ddlShipset_TextChanged(object sender, EventArgs e)
		{
			picShipset.Image = Mod.Current.Hulls.First().GetPortrait(ddlShipset.Text);
		}

		private void chkNameFromRace_CheckedChanged(object sender, EventArgs e)
		{
			txtName.Enabled = !chkNameFromRace.Checked;
			if (chkNameFromRace.Checked)
				txtName.Text = txtRaceDefaultEmpireName.Text;
		}

		private void chkLeaderNameFromRace_CheckedChanged(object sender, EventArgs e)
		{
			txtLeaderName.Enabled = !chkLeaderNameFromRace.Checked;
			if (chkLeaderNameFromRace.Checked)
				txtLeaderName.Text = txtRaceLeaderName.Text;
		}

		private void chkLeaderPortraitFromRace_CheckedChanged(object sender, EventArgs e)
		{
			ddlLeaderPortrait.Enabled = !chkLeaderPortraitFromRace.Checked;
			if (chkLeaderPortraitFromRace.Checked)
				ddlLeaderPortrait.Text = ddlRaceLeaderPortrait.Text;
		}

		private void chkColorFromRace_CheckedChanged(object sender, EventArgs e)
		{
			spnColorRed.Enabled = spnColorGreen.Enabled = spnColorBlue.Enabled = !chkColorFromRace.Checked;
			if (chkColorFromRace.Checked)
			{
				spnColorRed.Value = spnRaceColorRed.Value;
				spnColorGreen.Value = spnRaceColorGreen.Value;
				spnColorBlue.Value = spnRaceColorBlue.Value;
			}
		}

		private void chkInsigniaFromRace_CheckedChanged(object sender, EventArgs e)
		{
			ddlInsignia.Enabled = !chkInsigniaFromRace.Checked;
			if (chkInsigniaFromRace.Checked)
				ddlInsignia.Text = ddlRaceInsignia.Text;
		}

		private void chkShipsetFromRace_CheckedChanged(object sender, EventArgs e)
		{
			ddlShipset.Enabled = !chkShipsetFromRace.Checked;
			if (chkShipsetFromRace.Checked)
				ddlShipset.Text = ddlRaceShipset.Text;
		}

		private void chkAIFromRace_CheckedChanged(object sender, EventArgs e)
		{
			ddlAI.Enabled = !chkAIFromRace.Checked;
			if (chkAIFromRace.Checked)
				ddlAI.SelectedItem = ddlRaceAI.SelectedItem;
		}

		private void txtRaceDefaultEmpireName_TextChanged(object sender, EventArgs e)
		{
			if (chkNameFromRace.Checked)
				txtName.Text = txtRaceDefaultEmpireName.Text;
		}

		private void txtRaceLeaderName_TextChanged(object sender, EventArgs e)
		{
			if (chkLeaderNameFromRace.Checked)
				txtLeaderName.Text = txtRaceLeaderName.Text;
		}

		private void ddlRaceAI_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (chkAIFromRace.Checked)
				ddlAI.SelectedItem = ddlRaceAI.SelectedItem;
		}

		private void ddlRaceHappiness_SelectedIndexChanged(object sender, EventArgs e)
		{
			var h = (HappinessModel)ddlRaceHappiness.SelectedItem;
			if (h != null)
				txtRaceHappiness.Text = h.Description;
			else
				txtRaceHappiness.Text = "Please choose a happiness model.";
			if (chkHappinessFromRace.Checked)
				ddlHappiness.SelectedItem = ddlRaceHappiness.SelectedItem;
		}

		private void ddlHappiness_SelectedIndexChanged(object sender, EventArgs e)
		{
			var h = (HappinessModel)ddlRaceHappiness.SelectedItem;
			if (h != null)
				txtRaceHappiness.Text = h.Description;
			else
				txtRaceHappiness.Text = "Please choose a happiness model.";
		}

		private void chkHappinessFromRace_CheckedChanged(object sender, EventArgs e)
		{
			ddlHappiness.Enabled = !chkHappinessFromRace.Checked;
			if (chkHappinessFromRace.Checked)
				ddlHappiness.SelectedItem = ddlRaceHappiness.SelectedItem;
		}

		private void raceTraitPicker_TraitToggled(Controls.TraitPicker picker, Trait trait, bool state)
		{
			BindPointsSpent();
		}

		private void empireTraitPicker_TraitToggled(Controls.TraitPicker picker, Trait trait, bool state)
		{
			BindPointsSpent();
		}

		private void ddlRaceCulture_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlRaceCulture.SelectedItem != null)
				txtRaceCulture.Text = ((Culture)ddlRaceCulture.SelectedItem).Description;
			else
				txtRaceCulture.Text = "Please choose a culture.";
			if (chkCultureFromRace.Checked)
				ddlCulture.SelectedItem = ddlRaceCulture.SelectedItem;
		}

		private void ddlCulture_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlCulture.SelectedItem != null)
				txtCulture.Text = ((Culture)ddlCulture.SelectedItem).Description;
			else
				txtCulture.Text = "Please choose a culture.";
		}

		private void chkCultureFromRace_CheckedChanged(object sender, EventArgs e)
		{
			ddlCulture.Enabled = !chkCultureFromRace.Checked;
			if (chkCultureFromRace.Checked)
				ddlCulture.SelectedItem = ddlRaceCulture.SelectedItem;
		}

		private void aptitudePicker_AptitudeValueChanged(Controls.AptitudePicker ap, Aptitude aptitude, int newValue)
		{
			BindPointsSpent();	
		}

		#endregion


		public int PointsSpent
		{
			get
			{
				int result = 0;
				foreach (var t in raceTraitPicker.CheckedTraits)
					result += t.Cost;
				result += aptitudePicker.Cost;
				foreach (var t in empireTraitPicker.CheckedTraits)
					result += t.Cost;
				return result;
			}
		}

		private void btnRaceCompareCultures_Click(object sender, EventArgs e)
		{
			CompareCultures();
		}

		private void gameButton1_Click(object sender, EventArgs e)
		{
			CompareCultures();
		}

		private void CompareCultures()
		{
			// TODO - show culture comparison form
		}

		private void btnLoadRace_Click(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			dlg.InitialDirectory = "Races";
			dlg.Filter = "Races (*.rac)|*.rac";
			var result = dlg.ShowDialog();
			if (result == DialogResult.OK)
			{
				EmpireTemplate.PrimaryRace = Race.Load(dlg.FileName);
				Bind();
			}
		}

		private void btnSaveRace_Click(object sender, EventArgs e)
		{
			SaveChanges();
			var dlg = new SaveFileDialog();
			dlg.InitialDirectory = "Races";
			dlg.Filter = "Races (*.rac)|*.rac";
			var result = dlg.ShowDialog();
			if (result == DialogResult.OK)
				EmpireTemplate.PrimaryRace.Save(dlg.FileName);
		}
	}
}
