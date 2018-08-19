using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
    public partial class TraitPicker : UserControl
    {
        #region Private Fields

        private IEnumerable<Trait> traits;

        #endregion Private Fields

        #region Public Constructors

        public TraitPicker()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Delegates

        public delegate void TraitToggledDelegate(TraitPicker picker, Trait trait, bool state);

        #endregion Public Delegates

        #region Public Events

        public event TraitToggledDelegate TraitToggled;

        #endregion Public Events

        #region Public Properties

        public IEnumerable<Trait> CheckedTraits
        {
            get { return Traits.Where(t => IsTraitChecked(t)); }
        }

        public IEnumerable<Trait> Traits
        {
            get
            {
                return traits;
            }
            set
            {
                traits = value;
                Bind();
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void Bind()
        {
            pnlTraits.Controls.Clear();
            if (Traits != null)
            {
                foreach (var trait in Traits)
                {
                    var chk = new CheckBox();
                    chk.Text = "(" + trait.Cost + " pts) " + trait.Name;
                    chk.Tag = trait;
                    chk.CheckedChanged += chk_CheckedChanged;
                    pnlTraits.Controls.Add(chk);

                    var lbl = new Label();
                    lbl.Text = trait.Description;
                    lbl.Padding = new Padding(40, 0, 0, 0);
                    pnlTraits.Controls.Add(lbl);
                }
            }
        }

        public bool IsTraitChecked(Trait trait)
        {
            if (!Traits.Contains(trait))
                return false;
            var chk = pnlTraits.Controls.OfType<CheckBox>().Single(c => c.Tag == trait);
            return chk.Checked;
        }

        public void SetTraitChecked(Trait trait, bool state)
        {
            if (!Traits.Contains(trait))
                throw new Exception(trait + " is not in this trait picker.");
            var chk = pnlTraits.Controls.OfType<CheckBox>().Single(c => c.Tag == trait);
            chk.Checked = state;
        }

        #endregion Public Methods

        #region Private Methods

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            if (TraitToggled != null)
                TraitToggled(this, (Trait)chk.Tag, chk.Checked);
        }

        private void pnlTraits_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control c in pnlTraits.Controls)
                c.Width = pnlTraits.Width - 32;
        }

        #endregion Private Methods
    }
}
