using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
	public partial class GamePictureBox : PictureBox
	{
		public GamePictureBox()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Shows a full-size version of the picture in its own window.
		/// </summary>
		/// <param name="text">The title for the form.</param>
		public void ShowFullSize(string text)
		{
			var form = new Form();
			form.Text = text;
			form.MaximizeBox = false;
			form.FormBorderStyle = FormBorderStyle.FixedDialog;
			form.ClientSize = Image.Size;
			if (form.ClientSize.Width > Screen.PrimaryScreen.WorkingArea.Width)
				form.ClientSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, form.ClientSize.Height);
			if (form.ClientSize.Height > Screen.PrimaryScreen.WorkingArea.Height)
				form.ClientSize = new Size(form.ClientSize.Height, Screen.PrimaryScreen.WorkingArea.Height);
			form.StartPosition = FormStartPosition.CenterParent;
			var pic = new PictureBox();
			pic.Image = Image;
			pic.Size = Image.Size;
			pic.BackColor = Color.Black;
			pic.SizeMode = PictureBoxSizeMode.Zoom;
			form.Controls.Add(pic);
			form.ShowDialog();
		}
	}
}
