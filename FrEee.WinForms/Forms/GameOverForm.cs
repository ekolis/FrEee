using FrEee.Utility;
using FrEee.WinForms.Objects;
using System;
using System.IO;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class GameOverForm : GameForm
	{
		public GameOverForm(bool victory)
		{
			InitializeComponent();

			if (victory)
			{
				Text = "Victory!";
				pic.Image = Pictures.GetModImage(Path.Combine("Pictures", "Game", "Finale", "victory"));
				Music.Play(MusicMode.GameOver, MusicMood.Upbeat);
			}
			else
			{
				Text = "Defeat!";
				pic.Image = Pictures.GetModImage(Path.Combine("Pictures", "Game", "Finale", "defeat"));
				Music.Play(MusicMode.GameOver, MusicMood.Sad);
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}