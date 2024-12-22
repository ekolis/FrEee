namespace FrEee.UI;

/// <summary>
/// Navigates between the various screens in the UI.
/// </summary>
public interface IGuiController
{
	/// <summary>
	/// Shows a particular screen.
	/// </summary>
	/// <param name="screen"></param>
	void Show(Screen screen);

	/// <summary>
	/// Hides a particular screen.
	/// </summary>
	/// <param name="screen"></param>
	void Hide(Screen screen);

	/// <summary>
	/// Closes a particular screen.
	/// </summary>
	/// <param name="screen"></param>
	void Close(Screen screen);

	/// <summary>
	/// Exits the game.
	/// </summary>
	void Exit();

	/// <summary>
	/// Closes all windows except the main menu. Shows the main menu.
	/// </summary>
	void ReturnToMainMenu();

	/// <summary>
	/// Exits or returns to the main menu, per the player's preference.
	/// </summary>
	void CloseGame();
}
