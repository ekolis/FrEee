using FrEee.Game.Interfaces;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat.Grid
{
	public interface IBattleEvent
	{
		ICombatant Combatant { get; }
		IntVector2 EndPosition { get; }
		IntVector2 StartPosition { get; }
	}
}