using FrEee.Objects.Civilization.Diplomacy.Messages;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Processes.Combat;
using FrEee.Vehicles;

namespace FrEee.Utility;

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

	/// <summary>
	/// Focuses a space object on the map.
	/// </summary>
	/// <param name="sobj"></param>
	void Focus(ISpaceObject context);
	
	/// <summary>
	/// Focuses a star system on the map.
	/// </summary>
	/// <param name="sobj"></param>
	void Focus(StarSystem context);

	/// <summary>
	/// Focuses a technology on the research screen.
	/// </summary>
	/// <param name="sobj"></param>
	void Focus(Technology context);

	/// <summary>
	/// Focuses a hull on the design screen.
	/// </summary>
	/// <param name="sobj"></param>
	void Focus(IHull context);

	/// <summary>
	/// Focuses a battle on the battle summary screen.
	/// </summary>
	/// <param name="sobj"></param>
	void Focus(IBattle context);

	/// <summary>
	/// Focuses a message on the diplomacy screen.
	/// </summary>
	/// <param name="sobj"></param>
	void Focus(IMessage context);
}