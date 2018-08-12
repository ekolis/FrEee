using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.WinForms.Utility.Extensions;
using FrEee.Utility;
using System.IO;

namespace FrEee.WinForms.Forms
{
	public partial class GameOverForm : Form
	{
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

		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
