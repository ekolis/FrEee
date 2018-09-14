using FrEee.Game.Interfaces;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat.Grid
{
	public class CombatantLaunchedEvent : IBattleEvent
	{
		public CombatantLaunchedEvent(ICombatant launcher, ICombatant combatant, IntVector2 position)
		{
			Launcher = launcher;
			Combatant = combatant;
			StartPosition = EndPosition = position;
		}

		public ICombatant Launcher { get; set; }
		public ICombatant Combatant { get; set; }

		public IntVector2 EndPosition { get; set; }
		public IntVector2 StartPosition { get; set; }
	}
}