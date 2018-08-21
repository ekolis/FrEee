using FrEee.Game.Interfaces;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat.Grid
{
	public class CombatantDisappearsEvent : IBattleEvent
	{
		public CombatantDisappearsEvent(ICombatant combatant)
		{
			Combatant = combatant;
		}

		public ICombatant Combatant { get; set; }

		public IntVector2 EndPosition { get; set; }
		public IntVector2 StartPosition { get; set; }
	}
}