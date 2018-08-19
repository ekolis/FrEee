using FrEee.Utility;
using System;
using System.IO;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
    public partial class GameOverForm : Form
    {
        #region Public Constructors

        public GameOverForm(bool victory)
        {
            InitializeComponent();

            if (victory)
            {
                Text = "Victory!";
                pic.Image = Pictures.GetModImage(Path.Combine("Pictures", "Game", "Finale", "victory"));
            }
            else
            {
                Text = "Defeat!";
                pic.Image = Pictures.GetModImage(Path.Combine("Pictures", "Game", "Finale", "defeat"));
            }
        }

        #endregion Public Constructors

        #region Private Methods

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion Private Methods
    }
}
