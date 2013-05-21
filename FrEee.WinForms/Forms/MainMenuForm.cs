using System.Windows.Threading;
using FrEee.Game;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrEee.Utility.Extensions;

namespace FrEee.WinForms.Forms
{
	public partial class MainMenuForm : Form
	{
		private static MainMenuForm _instance;
		public static MainMenuForm GetInstance()
		{
			return _instance ?? (_instance = new MainMenuForm());
		}

		private MainMenuForm()
		{
			InitializeComponent();
			pictureBox1.Image = Image.FromFile(Properties.Resources.FrEeeSplash);
			Icon = new Icon(Properties.Resources.FrEeeIcon);
		}

		private bool _isBusy;
		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				_isBusy = value;
				tblButtonPanel.Visible = !IsBusy;
				progressBar1.Visible = IsBusy;
				if (!IsBusy)
					lblStatus.Text = string.Empty;
			}
		}

		#region Button click handlers

		private void btnQuickStart_Click(object sender, EventArgs e)
		{
			IsBusy = true;

			var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
			Task.Factory.StartNewWithExceptionHandling(() =>
			{
				Mod.Load(null);
				var gsu = new GameSetup
				{
					GalaxyTemplate = Mod.Current.GalaxyTemplates.PickRandom(),
					StarSystemCount = 50,
					GalaxySize = new System.Drawing.Size(40, 30)
				};
				Galaxy.Initialize(gsu);
			})
				.ContinueWithWithExceptionHandling(t =>
				{
					var game = new GameForm(Galaxy.Current);
					game.Show();
					game.FormClosed += (s, args) =>
					{
						game.Dispose();
						Show();
						IsBusy = false;
					};
					Hide();
				}, scheduler);
		}

		private void btnQuit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		#endregion
	}
}
