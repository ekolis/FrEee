using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An object that can fire weapons and/or targeted by weapons.
	/// </summary>
	public interface ICombatant : IPictorial, ITargetable, IDisposable, IFoggable, ILocated, INamed
	{
		/// <summary>
		/// Can this object fire on another object?
		/// </summary>
		/// <param name="target"></param>
		/// <returns>true if the target is an enemy and this combatant has weapons capable of targeting it</returns>
		bool CanTarget(ITargetable target);

		/// <summary>
		/// Any undamaged weapons this combatant is armed with.
		/// </summary>
		IEnumerable<Component> Weapons { get; }

		bool IsHostileTo(Empire emp);

		/// <summary>
		/// Accuracy rating of this combatant.
		/// </summary>
		int Accuracy { get; }

		/// <summary>
		/// Is this combatant still alive or is it destroyed/glassed?
		/// </summary>
		bool IsAlive { get; }
    }
}