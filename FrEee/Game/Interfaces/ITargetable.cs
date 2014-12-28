using FrEee.Game.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Civilization;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can be specifically target by weapons.
	/// </summary>
	public interface ITargetable : IDamageable, ITransferrable, IReferrable
	{
		/// <summary>
		/// What type of object is this for weapon targeting purposes?
		/// </summary>
		WeaponTargets WeaponTargetType { get; }

		/// <summary>
		/// Evasion rating of this combatant.
		/// </summary>
		int Evasion { get; }
	}
}
