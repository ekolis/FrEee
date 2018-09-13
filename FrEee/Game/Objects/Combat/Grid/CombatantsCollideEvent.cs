using FrEee.Game.Interfaces;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat.Grid
{
	/// <summary>
	/// When combatants collide - seeker detonation or ship ramming
	/// </summary>
	/// <seealso cref="FrEee.Game.Objects.Combat.Grid.IBattleEvent" />
	public class CombatantsCollideEvent : IBattleEvent
	{
		public CombatantsCollideEvent(ICombatant combatant, ICombatant target, IntVector2 location, int combatantDamage, int targetDamage)
		{
			Combatant = combatant;
			Target = target;
			StartPosition = EndPosition = location;
			CombatantDamage = combatantDamage;
			TargetDamage = targetDamage;
		}

		public ICombatant Combatant { get; set; }
		public ICombatant Target { get; set; }

		public IntVector2 EndPosition { get; set; }
		public IntVector2 StartPosition { get; set; }

		public int CombatantDamage { get; set; }
		public int TargetDamage { get; set; }
	}
}