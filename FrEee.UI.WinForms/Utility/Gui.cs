using System.Windows.Forms;
using FrEee.UI.WinForms.Forms;
using FrEee.UI.WinForms.Objects;

namespace FrEee.UI.WinForms.Utility;

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
