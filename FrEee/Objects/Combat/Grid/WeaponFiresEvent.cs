using FrEee.Interfaces;
using FrEee.Objects.Technology;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;

namespace FrEee.Objects.Combat.Grid;

public class WeaponFiresEvent : BattleEvent
{
	public WeaponFiresEvent(Battle battle, ICombatant attacker, Vector2<int> here, ICombatant target, Vector2<int> there, Component weapon, Hit hit, bool wasTargetDisarmed)
		: base(battle, attacker, here, there)
	{
		Attacker = attacker;
		Target = target;
		Weapon = weapon;
		Hit = hit;
		IsHit = hit != null;
		Damage = hit?.NominalDamage ?? 0;
		WasTargetDisarmed = wasTargetDisarmed;
	}
	public bool IsHit { get; set; }

	private GalaxyReference<ICombatant> attacker { get; set; }

	private GalaxyReference<ICombatant> target { get; set; }

	[DoNotSerialize]
	public ICombatant Attacker
	{
		get => attacker?.Value ?? Battle?.StartCombatants?[attacker.ID];
		set => attacker = value.ReferViaGalaxy();
	}

	[DoNotSerialize]
	public ICombatant Target
	{
		get => target?.Value ?? Battle?.StartCombatants?[target.ID];
		set => target = value.ReferViaGalaxy();
	}

	// TODO - make this some sort of reference?
	public Component Weapon { get; set; }

	public Hit Hit { get; set; }
	public int Damage { get; set; }

	public bool WasTargetDisarmed { get; set; }
}