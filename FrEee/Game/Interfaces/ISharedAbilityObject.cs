using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An object that can contain different abilities for different empires.
	/// </summary>
	public interface ISharedAbilityObject
	{
		/// <summary>
		/// Finds any child ability objects owned by an empire.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		IEnumerable<IAbilityObject> GetContainedAbilityObjects(Empire emp);

		AbilityTargets AbilityTarget { get; }
	}
}
