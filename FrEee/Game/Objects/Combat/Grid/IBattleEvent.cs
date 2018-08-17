using FrEee.Game.Interfaces;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Game.Objects.Combat.Grid
{
	public interface IBattleEvent
	{
		ICombatant Combatant { get; }
		IntVector2 StartPosition { get; }
		IntVector2 EndPosition { get; }
	}
}
