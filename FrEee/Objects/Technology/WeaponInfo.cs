using FrEee.Objects.Combat;
using FrEee.Modding;
using System;

namespace FrEee.Objects.Technology;

/// <summary>
/// Information about a weapon component.
/// </summary>
[Serializable]
public abstract class WeaponInfo
{
	/// <summary>
	/// Damage of the weapon at range.
	/// </summary>
	public Formula<int> Damage { get; set; }

	/// <summary>
	/// The type of damage inflicted by this weapon.
	/// </summary>
	public DamageType DamageType { get; set; }

	/// <summary>
	/// The display effect used for this weapons.
	/// </summary>
	public WeaponDisplayEffect DisplayEffect { get; set; }

	/// <summary>
	/// Not sure what this is for, maybe the AI?
	/// </summary>
	public Formula<string> Family { get; set; }

	/// <summary>
	/// Does this weapon automatically fire at targets that come into range?
	/// </summary>
	public Formula<bool> IsPointDefense { get; set; }

	public bool IsSeeker { get { return WeaponType == WeaponTypes.Seeking || WeaponType == WeaponTypes.SeekingPointDefense; } }

	public bool IsWarhead { get { return WeaponType == WeaponTypes.Warhead || WeaponType == WeaponTypes.WarheadPointDefense; } }

	/// <summary>
	/// Maximum range of the weapon.
	/// </summary>
	public Formula<int> MaxRange { get; set; }

	/// <summary>
	/// Minimum range of the weapon.
	/// </summary>
	public Formula<int> MinRange { get; set; }

	/// <summary>
	/// The number of turns after firing that the weapon must wait to fire again. (1 means the weapon can fire every turn.)
	/// </summary>
	public Formula<double> ReloadRate { get; set; }

	/// <summary>
	/// The sound file to play when this weapon fires.
	/// </summary>
	public Formula<string> Sound { get; set; }

	/// <summary>
	/// If this is not null, it will be displayed in place of a simple list of things the weapon can target.
	/// </summary>
	public Formula<string> TargetDescription { get; set; }

	/// <summary>
	/// What can this weapon target?
	/// </summary>
	public WeaponTargets Targets { get; set; }

	/// <summary>
	/// The weapon type of this weapon.
	/// </summary>
	public abstract WeaponTypes WeaponType { get; }

	public int GetDamage(Shot shot)
	{
		if (shot.Range < MinRange || shot.Range > MaxRange)
			return 0;
		return Damage.Evaluate(shot);
	}
}