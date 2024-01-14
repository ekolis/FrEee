using FrEee.Objects.Civilization;
using System.Collections.Generic;

namespace FrEee.Interfaces;

/// <summary>
/// A victory condition for the game.
/// </summary>
public interface IVictoryCondition
{
	/// <summary>
	/// Gets a defeat message for an empire.
	/// </summary>
	/// <param name="emp"></param>
	/// <param name="winners"></param>
	/// <returns></returns>
	string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners);

	/// <summary>
	/// Gets the progress of an empire toward this victory condition.
	/// 0 represents no progress; 1 represents completion.
	/// Progress can go over 1 if the empire exceeds the requirements.
	/// </summary>
	/// <param name="emp"></param>
	/// <returns></returns>
	double GetProgress(Empire emp);

	/// <summary>
	/// Gets a victory message for an empire.
	/// </summary>
	/// <param name="emp"></param>
	/// <returns></returns>
	string GetVictoryMessage(Empire emp);
}