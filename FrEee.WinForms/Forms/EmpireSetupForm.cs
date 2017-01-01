using FrEee.Game.Objects.AI;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Setup;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
    public partial class EmpireSetupForm : Form
    {
        // Just making a shortcut for internal use.
        private EmpireSetupViewModel _vm = GlobalStuff.Default.EmpireSetupViewModel;


        public EmpireSetupForm()

        {
            InitializeComponent();
            BindChoices();

            try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
            catch { }

            _vm.Initialize();
            var view = new EmpireSetupView();
            view.DataContext = _vm;
            wpfHost.Child = view;

            SaveOriginalTemplate(_vm.CurrentEmpireTemplate);

            BindChoices();

            _vm.CurrentEmpireTemplate.PropertyChanged += UpdateWinFormsFieldsWithViewModelData;
            _vm.CurrentEmpireTemplate.PrimaryRace.PropertyChanged += UpdateWinFormsFieldsWithViewModelData;
        }

        private void UpdateWinFormsFieldsWithViewModelData(object sender, PropertyChangedEventArgs e)
        {
            ddlInsignia.Text = _vm.CurrentEmpireTemplate.InsigniaName;
            ddlShipset.Text = _vm.CurrentEmpireTemplate.ShipsetName;

            txtName.Text = _vm.CurrentEmpireTemplate.Name;

            txtLeaderName.Text = _vm.CurrentEmpireTemplate.LeaderName;
            ddlLeaderPortrait.Text = _vm.CurrentEmpireTemplate.LeaderPortraitName;

            spnColorRed.Value = _vm.CurrentEmpireTemplate.Color.R;
            spnColorGreen.Value = _vm.CurrentEmpireTemplate.Color.G;
            spnColorBlue.Value = _vm.CurrentEmpireTemplate.Color.B;

            txtRaceName.Text = _vm.CurrentEmpireTemplate.PrimaryRace.Name;
            ddlRacePopulationIcon.Text = _vm.CurrentEmpireTemplate.PrimaryRace.PopulationIconName;
            ddlRaceNativeSurface.SelectedItem = _vm.CurrentEmpireTemplate.PrimaryRace.NativeSurface;
            ddlRaceNativeAtmosphere.SelectedItem = _vm.CurrentEmpireTemplate.PrimaryRace.NativeAtmosphere;
            ddlRaceHappiness.SelectedItem = _vm.CurrentEmpireTemplate.PrimaryRace.HappinessModel;
        }

        private void SaveOriginalTemplate(EmpireTemplate empireTemplateToSave)
        {
            _vm.OriginalEmpireTemplate = empireTemplateToSave.Copy();
            _vm.OriginalRace = empireTemplateToSave.PrimaryRace.Copy();
            _vm.CurrentEmpireTemplate = empireTemplateToSave;
            Bind();
        }

        private void RestoreOriginalTemplate()
        {
            _vm.CurrentEmpireTemplate = _vm.OriginalEmpireTemplate;
            _vm.CurrentEmpireTemplate.PrimaryRace = _vm.OriginalRace;
            Bind();
        }

        private void BindChoices()
        {

            // foreach (var portrait in Pictures.ListLeaderPortraits())
            foreach (var portrait in _vm.AvailableLeaderPortraits) ddlLeaderPortrait.Items.Add(portrait);

            // foreach (var icon in Pictures.ListPopulationIcons())
            foreach (var icon in _vm.AvailablePopulationIcons) ddlRacePopulationIcon.Items.Add(icon);

            // foreach (var surface in Mod.Current.StellarObjectTemplates.OfType<Planet>().Select(p => p.Surface).Distinct())
            foreach (var surface in _vm.AvailableSurfaces) ddlRaceNativeSurface.Items.Add(surface);

            // foreach (var atmosphere in Mod.Current.StellarObjectTemplates.OfType<Planet>().Select(p => p.Atmosphere).Distinct())
            foreach (var atmosphere in _vm.AvailableAtmospheres) ddlRaceNativeAtmosphere.Items.Add(atmosphere);

            // foreach (var insignia in Pictures.ListInsignia()) ddlInsignia.Items.Add(insignia);
            foreach (var insignia in _vm.AvailableInsignia) ddlInsignia.Items.Add(insignia);

            // foreach (var shipset in Pictures.ListShipsets())
            foreach (var shipset in _vm.AvailableShipsets) ddlShipset.Items.Add(shipset);


            foreach (var ai in Mod.Current.EmpireAIs) ddlAI.Items.Add(ai);

            foreach (var h in Mod.Current.HappinessModels) ddlRaceHappiness.Items.Add(h);

            foreach (var c in Mod.Current.Cultures) ddlCulture.Items.Add(c);

            raceTraitPicker.Traits = Mod.Current.Traits;
        }

        private void BindPointsSpent()
        {
            int pointsAvailable = _vm.PointsToSpend - PointsSpent;
            txtPointsAvailable.Text = "Points Available: " + pointsAvailable + " / " + _vm.PointsToSpend;
            if (pointsAvailable < 0)
                txtPointsAvailable.ForeColor = Color.FromArgb(255, 128, 128);
            else
                txtPointsAvailable.ForeColor = Color.White;
        }

        private void Bind()
        {
            if (_vm.CurrentEmpireTemplate == null) SaveOriginalTemplate(new EmpireTemplate());
            //EmpireTemplate = new EmpireTemplate();
            if (_vm.CurrentEmpireTemplate.PrimaryRace == null)
                _vm.CurrentEmpireTemplate.PrimaryRace = new Race();


            // txtName.Text = _vm.CurrentEmpireTemplate.Name;
            // txtLeaderName.Text = _vm.CurrentEmpireTemplate.LeaderName;
            // ddlLeaderPortrait.Text = _vm.CurrentEmpireTemplate.LeaderPortraitName;
            //spnColorRed.Value = _vm.CurrentEmpireTemplate.Color.R;
            //spnColorGreen.Value = _vm.CurrentEmpireTemplate.Color.G;
            //spnColorBlue.Value = _vm.CurrentEmpireTemplate.Color.B;
            // ddlInsignia.Text = _vm.CurrentEmpireTemplate.InsigniaName;
            // ddlShipset.Text = _vm.CurrentEmpireTemplate.ShipsetName;
            ddlAI.Text = _vm.CurrentEmpireTemplate.AIName;
            ddlCulture.SelectedItem = _vm.CurrentEmpireTemplate.Culture;

            // race general stuff
            //txtRaceName.Text = _vm.CurrentEmpireTemplate.PrimaryRace.Name;
            //ddlRacePopulationIcon.Text = _vm.CurrentEmpireTemplate.PrimaryRace.PopulationIconName;
            //ddlRaceNativeSurface.SelectedItem = _vm.CurrentEmpireTemplate.PrimaryRace.NativeSurface;
            //ddlRaceNativeAtmosphere.SelectedItem = _vm.CurrentEmpireTemplate.PrimaryRace.NativeAtmosphere;
            //ddlRaceHappiness.SelectedItem = _vm.CurrentEmpireTemplate.PrimaryRace.HappinessModel;

            // race traits
            foreach (var trait in raceTraitPicker.Traits)
            {
                raceTraitPicker.SetTraitChecked(trait, _vm.CurrentEmpireTemplate.PrimaryRace.Traits.Contains(trait));

                if (_vm.CurrentEmpireTemplate.PrimaryRace.Traits.Contains(trait))
                {
                    var traitToCheck = _vm.EmpireTraits.FirstOrDefault(t => t.Trait.Name == trait.Name);
                    if (traitToCheck != null) traitToCheck.IsSelected = true;
                }

                
                // )?.IsSelected = true;
            }

            // race aptitudes
            foreach (var apt in Aptitude.All)
            {
                int val = 100;
                if (_vm.CurrentEmpireTemplate.PrimaryRace.Aptitudes.ContainsKey(apt.Name))
                {
                    val = _vm.CurrentEmpireTemplate.PrimaryRace.Aptitudes[apt.Name];
                }
                aptitudePicker.SetValue(apt, val);
            }

            // is AI empire?
            chkAIsCanUse.Checked = _vm.CurrentEmpireTemplate.AIsCanUse;

            BindPointsSpent();

            ddlRacePopulationIcon_TextChanged(null, null);
            ddlLeaderPortrait_TextChanged(null, null);
            ddlInsignia_TextChanged(null, null);
            ddlShipset_TextChanged(null, null);
            spnColor_ValueChanged(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // revert changes
            RestoreOriginalTemplate();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveChanges();

            // validate
            var warnings = _vm.CurrentEmpireTemplate.GetWarnings(_vm.PointsToSpend);
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
            var et = _vm.CurrentEmpireTemplate;
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
        }

        #region silly internal consistency stuff

        private void ddlRacePopulationIcon_TextChanged(object sender, EventArgs e)
        {
            var emp = new Empire();
            emp.PrimaryRace = new Race();
            emp.PrimaryRace.PopulationIconName = ddlRacePopulationIcon.Text;
            picRacePopulationIcon.Image = emp.PrimaryRace.Icon;
        }

        private void ddlLeaderPortrait_TextChanged(object sender, EventArgs e)
        {
            var emp = new Empire();
            emp.LeaderPortraitName = ddlLeaderPortrait.Text;
            picLeaderPortrait.Image = emp.Portrait;
        }

        private void spnColor_ValueChanged(object sender, EventArgs e)
        {
            picColor.BackColor = Color.FromArgb((int)spnColorRed.Value, (int)spnColorGreen.Value, (int)spnColorBlue.Value);
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

        private void ddlRaceHappiness_SelectedIndexChanged(object sender, EventArgs e)
        {
            var h = (HappinessModel)ddlRaceHappiness.SelectedItem;
            if (h != null)
                txtRaceHappiness.Text = h.Description;
            else
                txtRaceHappiness.Text = "Please choose a happiness model.";
        }

        private void raceTraitPicker_TraitToggled(Controls.TraitPicker picker, Trait trait, bool state)
        {
            BindPointsSpent();
        }

        private void empireTraitPicker_TraitToggled(Controls.TraitPicker picker, Trait trait, bool state)
        {
            BindPointsSpent();
        }

        private void ddlCulture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCulture.SelectedItem != null)
                txtCulture.Text = ((Culture)ddlCulture.SelectedItem).Description;
            else
                txtCulture.Text = "Please choose a culture.";
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
                    result += t.Cost.Value;
                result += aptitudePicker.Cost;
                return result;
            }
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
                _vm.CurrentEmpireTemplate.PrimaryRace = Race.Load(dlg.FileName);
                Bind();
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
            {
                _vm.CurrentEmpireTemplate.PrimaryRace.Save(dlg.FileName);
            }
        }

        private string lastDdlPicText = null;

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

        private void spnColor_Leave(object sender, EventArgs e)
        {
            // round to next 85
            var spn = (NumericUpDown)sender;
            spn.Value = Math.Ceiling(spn.Value / 85m) * 85m;
        }
    }
}
