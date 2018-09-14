using FrEee.Game.Interfaces;
using FrEee.Utility;
using System;

namespace FrEee.Game.Objects.Combat.Grid
{
	[Obsolete("This class is deprecated; use CombatantDestroyedEvent if a combatant is destroyed.")]
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