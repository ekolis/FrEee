using FrEee.Game.Interfaces;
using FrEee.Utility;
using System;
using System.Linq;

namespace FrEee.Game.Objects.Combat.Grid
{
	public class CombatantAppearsEvent : IBattleEvent
	{
		public CombatantAppearsEvent(ICombatant combatant, IntVector2 position)
		{
			Combatant = combatant;
			StartPosition = EndPosition = position;
			IsUnarmed = !(Combatant is Seeker) && !Combatant.Weapons.Any();
		}

		public ICombatant Combatant { get; set; }

		public IntVector2 EndPosition { get; set; }
		public IntVector2 StartPosition { get; set; }

		public bool IsUnarmed { get; set; }
	}
}