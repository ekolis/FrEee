using FrEee.Game.Interfaces;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Game.Objects.Combat.Grid
{
	public class WeaponFiresEvent : IBattleEvent
	{
		public WeaponFiresEvent(ICombatant combatant, IntVector2 here, ICombatant target, IntVector2 there)
		{
			Combatant = combatant;
			Target = target;
			StartPosition = here;
			EndPosition = there;
		}

		public ICombatant Combatant { get; set; }

		public ICombatant Target { get; set; }

		public IntVector2 StartPosition { get; set; }

		public IntVector2 EndPosition { get; set; }
	}
}
