using FrEee.Enumerations;

namespace FrEee.Interfaces;

/// <summary>
/// Something which can be specifically target by weapons.
/// </summary>
public interface ITargetable : IDamageableReferrable, ITransferrable
{
	/// <summary>
	/// Evasion rating of this combatant.
	/// </summary>
	int Evasion { get; }

	/// <summary>
	/// What type of object is this for weapon targeting purposes?
	/// </summary>
	WeaponTargets WeaponTargetType { get; }
}