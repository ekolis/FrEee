using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.WinForms.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Utility.Extensions;
using FrEee.Utility;
using FrEee.Modding;
using FrEee.WinForms.Forms;

namespace FrEee.WinForms.Controls {
    public partial class FacilityReport : UserControl, IBindable<Facility>, IBindable<FacilityTemplate>, IBindable<FacilityUpgrade> {
        public FacilityReport() {
            InitializeComponent();

            GlobalStuff.Default.DefaultFacilityReportViewModel.Facility.Template.Name = "Bybis raugintas";
            wpfHost.Child = new FacilityReportView();

        }

        private bool isUpgrading = false;

        public FacilityReport(Facility f) : this() {
            Bind(f);
        }

        public FacilityReport(FacilityTemplate ft) : this() {
            Bind(ft);
        }

        public FacilityReport(FacilityUpgrade fu) : this() {
            Bind(fu);
        }

        private Facility facility;

        public Facility Facility
        {
            get
            {
                return facility;
            }
            set
            {
                facility = value;
                Bind();
            }
        }

        public void Bind(Facility data) {
            isUpgrading = false;
            Facility = data;
        }

        public void Bind() {
            SuspendLayout();
            if (Facility == null)
                Visible = false;
            else {
                Visible = true;
                picPortrait.Image = Facility.Portrait;
                txtName.Text = Facility.Name;
                txtDescription.Text = Facility.Template.Description;
                double ratio = 1d;
                if (isUpgrading)
                    ratio = (double)Mod.Current.Settings.UpgradeFacilityPercentCost / 100d;
                resMin.Amount = (int)(Facility.Template.Cost[Resource.Minerals] * ratio);
                resOrg.Amount = (int)(Facility.Template.Cost[Resource.Organics] * ratio);
                resRad.Amount = (int)(Facility.Template.Cost[Resource.Radioactives] * ratio);
                abilityTree.IntrinsicAbilities = Facility.Abilities;
                abilityTree.Abilities = Facility.Abilities.StackToTree(Facility);
            }
            ResumeLayout();
        }

        public void Bind(FacilityTemplate data) {
            isUpgrading = false;
            Bind(new Facility(data));
        }

        public void Bind(FacilityUpgrade data) {
            isUpgrading = true;
            Bind(data.New);
            txtName.Text = "Upgrade to " + txtName.Text;
        }

        private void picPortrait_Click(object sender, EventArgs e) {
            if (Facility != null)
                picPortrait.ShowFullSize(Facility.Name);
        }
    }
}
