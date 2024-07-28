using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Ecs.Interactions;

/// <summary>
/// Fires a weapon.
/// </summary>
/// <param name="Source">The combatant entity which is firing the weapon.</param>
/// <param name="Weapon">The weapon being fired.</param>
/// <param name="Target">The combatant entity which is being targeted.</param>
public record FireWeaponInteraction
(
	IEntity Source,
	IEntity Weapon,
	IEntity Target
): IInteraction
{
	/// <summary>
	/// Accuracy modfier for the source and the weapon.
	/// </summary>
	public int Accuracy { get; set; } = 0;

	/// <summary>
	/// Evasion modifier for the target.
	/// </summary>
	public int Evasion { get; set; } = 0;

	public void Execute()
	{
		// TODO: fire weapon
	}
}
