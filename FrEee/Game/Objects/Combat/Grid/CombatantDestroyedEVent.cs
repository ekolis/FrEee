using FrEee.Game.Interfaces;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat.Grid
{
	public class CombatantDestroyedEvent : IBattleEvent
	{
		public CombatantDestroyedEvent(ICombatant combatant, IntVector2 position)
		{
			Combatant = combatant;
			StartPosition = EndPosition = position;
		}

		public ICombatant Combatant { get; set; }

		public IntVector2 EndPosition { get; set; }
		public IntVector2 StartPosition { get; set; }
	}
}