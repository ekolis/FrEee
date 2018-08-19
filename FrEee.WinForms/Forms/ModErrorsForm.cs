using FrEee.Modding;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Interfaces;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
    /// <summary>
    /// Displays errors when loading a mod.
    /// </summary>
    public partial class ModErrorsForm : Form, IBindable<IEnumerable<DataParsingException>>
    {
        #region Public Constructors

        public ModErrorsForm()
        {
            InitializeComponent();
            Bind(Mod.Errors);
            try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
            catch { }
        }

        #endregion Public Constructors

        #region Public Properties

        public IEnumerable<DataParsingException> Errors { get; private set; }

        #endregion Public Properties

        #region Private Properties

        private DataParsingException SelectedError { get; set; }
        private IGrouping<string, DataParsingException> SelectedFile { get; set; }

        #endregion Private Properties

        #region Public Methods

        public void Bind(IEnumerable<DataParsingException> data)
        {
            Errors = data;
            Bind();
        }

        public void Bind()
        {
            lstFiles.Initialize(16, 16);
            if (Errors == null || !Errors.Any())
            {
                lblNoErrors.Visible = true;
                pnlDetails.Visible = false;
            }
            else
            {
                lblNoErrors.Visible = false;
                pnlDetails.Visible = true;
                var errorsByFile = Errors.GroupBy(e => e.Filename);
                foreach (var fgroup in errorsByFile)
                    lstFiles.AddItemWithImage(null, fgroup.Key, fgroup, null);
            }
            SelectedFile = null;
            BindErrors();
        }

        #endregion Public Methods

        #region Private Methods

        private void BindDetails()
        {
            if (SelectedError != null)
            {
                pnlDetails.Visible = true;
                lblMessage.Text = SelectedError.Message;
                lblRecord.Text = SelectedError.Record.ToSafeString();
                lblField.Text = SelectedError.Field.ToSafeString();
            }
            else
                pnlDetails.Visible = false;
        }

        private void BindErrors()
        {
            lstErrors.Initialize(16, 16);
            if (SelectedFile != null)
            {
                foreach (var e in SelectedFile)
                    lstErrors.AddItemWithImage(null, e.Message, e, null);
            }
            SelectedError = null;
            BindDetails();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lstErrors_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item != null && e.IsSelected)
                SelectedError = (DataParsingException)e.Item.Tag;
            else
                SelectedError = null;
            BindDetails();
        }

        private void lstFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item != null && e.IsSelected)
                SelectedFile = (IGrouping<string, DataParsingException>)e.Item.Tag;
            else
                SelectedFile = null;
            BindErrors();
        }

        #endregion Private Methods
    }
}
