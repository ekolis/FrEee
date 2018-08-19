using FrEee.WinForms.Utility.Extensions;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
    public partial class GamePictureBox : PictureBox
    {
        #region Public Constructors

        public GamePictureBox()
        {
            InitializeComponent();
            SizeMode = PictureBoxSizeMode.Zoom;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Shows a full-size version of the picture in its own window.
        /// </summary>
        /// <param name="text">The title for the form.</param>
        public void ShowFullSize(string text)
        {
            if (Image != null)
            {
                var pic = new PictureBox();
                pic.Image = Image;
                pic.Size = Image.Size;
                pic.BackColor = Color.Black;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                this.FindForm().ShowChildForm(pic.CreatePopupForm());
            }
        }

        #endregion Public Methods
    }
}
