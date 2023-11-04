using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Serialization; using FrEee.Serialization.Attributes;
using FrEee.Utility;

namespace FrEee.Objects.Combat.Grid
{
	/// <summary>
	/// When combatants collide - seeker detonation or ship ramming
	/// </summary>
	/// <seealso cref="Game.Game.Objects.Combat.Grid.IBattleEvent" />
	public class CombatantsCollideEvent : BattleEvent
	{
		public CombatantsCollideEvent(Battle battle, ICombatant combatant, ICombatant target, IntVector2 location, int combatantDamage, int targetDamage, bool wasCombatantDisarmed, bool wasTargetDisarmed)
			: base(battle, combatant, location, location)
		{
			Target = target;
			CombatantDamage = combatantDamage;
			TargetDamage = targetDamage;
			WasCombatantDisarmed = wasCombatantDisarmed;
			WasTargetDisarmed = wasTargetDisarmed;
		}

		private ICombatant OurTarget { get; set; }

		[DoNotSerialize]
		public ICombatant Target
		{
			get => OurTarget ?? Battle?.StartCombatants?[OurTarget.ID];
			set => OurTarget = value;
		}

		public int CombatantDamage { get; set; }
		public int TargetDamage { get; set; }

		public bool WasCombatantDisarmed { get; set; }
		public bool WasTargetDisarmed { get; set; }
	}
}
