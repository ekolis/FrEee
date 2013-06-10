using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.WinForms.Utility.Extensions;

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
			if (Image != null)
			{
				var form = new Form();
				form.Text = text;
				form.MaximizeBox = false;
				form.FormBorderStyle = FormBorderStyle.FixedDialog;
				form.ClientSize = Image.Size;
				if (form.Width > Screen.PrimaryScreen.WorkingArea.Width)
					form.Width = Screen.PrimaryScreen.WorkingArea.Width;
				if (form.Height > Screen.PrimaryScreen.WorkingArea.Height)
					form.Height = Screen.PrimaryScreen.WorkingArea.Height;
				form.StartPosition = FormStartPosition.CenterParent;
				var pic = new PictureBox();
				pic.Image = Image;
				pic.Size = form.ClientSize;
				pic.BackColor = Color.Black;
				pic.SizeMode = PictureBoxSizeMode.Zoom;
				form.Controls.Add(pic);
				this.FindForm().ShowChildForm(form);
			}
		}
	}
}
