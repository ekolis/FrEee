using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Technology;
using System;
using System.Collections.Generic;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An object that can fire weapons and/or targeted by weapons.
	/// </summary>
	public interface ICombatant : IPictorial, ITargetable, IDisposable, IFoggable, ILocated, INamed
	{
		#region Public Properties

		/// <summary>
		/// Accuracy rating of this combatant.
		/// </summary>
		int Accuracy { get; }

		/// <summary>
		/// Is this combatant still alive or is it destroyed/glassed?
		/// </summary>
		bool IsAlive { get; }

		/// <summary>
		/// How many targets can this combatant fire on per round (excluding point defense weapons or warheads)?
		/// </summary>
		int MaxTargets { get; }

		/// <summary>
		/// How fast can this combatant move in combat?
		/// </summary>
		int CombatSpeed { get; }

		/// <summary>
		/// Any undamaged weapons this combatant is armed with.
		/// </summary>
		IEnumerable<Component> Weapons { get; }

		#endregion Public Properties

		#region Public Methods

		/// <summary>
		/// Can this object fire on another object?
		/// </summary>
		/// <param name="target"></param>
		/// <returns>true if the target is an enemy and this combatant has weapons capable of targeting it</returns>
		bool CanTarget(ITargetable target);

		bool IsHostileTo(Empire emp);

		#endregion Public Methods
	}
}