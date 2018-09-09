﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Orders;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Objects;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class GameForm : Form
	{
		public GameForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// The wiki page associated with this form.
		/// </summary>
		protected virtual string WikiPage => GetType().Name.Replace("Form", "");

		/// <summary>
		/// Opens the appropriate wiki page for this form in a browser.
		/// </summary>
		protected void OpenWikiPage()
		{
			Process.Start($"https://bitbucket.org/ekolis/freee/wiki/Screens/{WikiPage}");
		}

		private void GameForm_KeyDown(object sender, KeyEventArgs e)
		{
			// open wiki in browser when F1 is pressed
			if (e.KeyCode == Keys.F1)
				OpenWikiPage();
		}

		private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			ClientSettings.Instance.WindowStates[GetType().Name] = WindowState;
			if (WindowState == FormWindowState.Normal)
			{
				ClientSettings.Instance.WindowSizes[GetType().Name] = Size;
				ClientSettings.Instance.WindowLocations[GetType().Name] = Location;
			}
		}

		private void GameForm_Load(object sender, EventArgs e)
		{
			if (ClientSettings.Instance.WindowStates.ContainsKey(GetType().Name))
				WindowState = ClientSettings.Instance.WindowStates[GetType().Name];
			if (WindowState == FormWindowState.Normal)
			{
				if (ClientSettings.Instance.WindowSizes.ContainsKey(GetType().Name))
					Size = ClientSettings.Instance.WindowSizes[GetType().Name];
				if (ClientSettings.Instance.WindowLocations.ContainsKey(GetType().Name))
					Location = ClientSettings.Instance.WindowLocations[GetType().Name];
			}
		}
	}
}