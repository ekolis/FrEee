using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Interfaces;
using System;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
    public partial class FacilityReport : UserControl, IBindable<Facility>, IBindable<FacilityTemplate>, IBindable<FacilityUpgrade>
    {
        #region Private Fields

        private Facility facility;

        private bool isUpgrading = false;

        #endregion Private Fields

        #region Public Constructors

        public FacilityReport()
        {
            InitializeComponent();
        }

        public FacilityReport(Facility f)
        {
            InitializeComponent();
            Bind(f);
        }

        public FacilityReport(FacilityTemplate ft)
        {
            InitializeComponent();
            Bind(ft);
        }

        public FacilityReport(FacilityUpgrade fu)
        {
            InitializeComponent();
            Bind(fu);
        }

        #endregion Public Constructors

        #region Public Properties

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

        #endregion Public Properties

        #region Public Methods

        public void Bind(Facility data)
        {
            isUpgrading = false;
            Facility = data;
        }

        public void Bind()
        {
            SuspendLayout();
            if (Facility == null)
                Visible = false;
            else
            {
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

        public void Bind(FacilityTemplate data)
        {
            isUpgrading = false;
            Bind(new Facility(data));
        }

        public void Bind(FacilityUpgrade data)
        {
            isUpgrading = true;
            Bind(data.New);
            txtName.Text = "Upgrade to " + txtName.Text;
        }

        #endregion Public Methods

        #region Private Methods

        private void picPortrait_Click(object sender, EventArgs e)
        {
            if (Facility != null)
                picPortrait.ShowFullSize(Facility.Name);
        }

        #endregion Private Methods
    }
}
