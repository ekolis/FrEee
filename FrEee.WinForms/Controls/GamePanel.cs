using System;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
    public partial class GamePanel : Panel
    {
        #region Private Fields

        private Color borderColor;

        #endregion Private Fields

        #region Public Constructors

        public GamePanel()
        {
            InitializeComponent();
            this.SizeChanged += GamePanel_SizeChanged;
            BackColor = Color.Black;
            ForeColor = Color.White;
            BorderColor = Color.CornflowerBlue;
            DoubleBuffered = true;
            Padding = new Padding(3);
            BorderStyle = BorderStyle.FixedSingle;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Color of the border for BorderStyle.FixedSingle mode.
        /// </summary>
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        #endregion Public Properties

        #region Protected Methods

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (BorderStyle == BorderStyle.FixedSingle)
                ControlPaint.DrawBorder(pe.Graphics, ClientRectangle, BorderColor, ButtonBorderStyle.Solid);
        }

        // http://support.microsoft.com/kb/953934
        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.Handle != null)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    base.OnSizeChanged(e);
                });
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void GamePanel_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #endregion Private Methods
    }
}
