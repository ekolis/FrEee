using FrEee.UI.WinForms.Objects;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FrEee.UI.WinForms.Forms;

public partial class GameForm : Form
{
	public GameForm()
	{
		InitializeComponent();
	}

	/// <summary>
	/// The wiki page associated with this form.
	/// </summary>
	protected virtual string WikiPage => GetType().Name.Replace("Form", "Screen");

	/// <summary>
	/// Opens the appropriate wiki page for this form in a browser.
	/// </summary>
	protected void OpenWikiPage()
	{
		Process.Start($"https://github.com/ekolis/FrEee/wiki/{WikiPage}");
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
		if (!DesignMode)
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