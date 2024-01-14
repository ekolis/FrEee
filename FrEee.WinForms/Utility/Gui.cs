using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrEee.WinForms.Forms;
using FrEee.WinForms.Objects;
using Microsoft.Scripting.Utils;

namespace FrEee.WinForms.Utility;

/// <summary>
/// Non-extension utility methods for manipulating the GUI.
/// </summary>
public static class Gui
{
	/// <summary>
	/// Exits the GUI.
	/// </summary>
	public static void Exit()
		=> Application.Exit();

	/// <summary>
	/// Closes all windows except the main menu. Shows the main menu.
	/// </summary>
	public static void ReturnToMainMenu()
	{
		foreach (Form form in Application.OpenForms)
		{
			if (form is MainMenuForm)
			{
				form.Show();
			}
			else
			{
				form.Close();
			}
		}
	}

	/// <summary>
	/// Exits or returns to the main menu, per the player's preference.
	/// </summary>
	public static void CloseGame()
	{
		if (ClientSettings.Instance.QuitToMainMenu)
		{
			ReturnToMainMenu();
		}
		else
		{
			Exit();
		}
	}
}
