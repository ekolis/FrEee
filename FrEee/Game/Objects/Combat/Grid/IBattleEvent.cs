using FrEee.Game.Interfaces;
using FrEee.Utility;

#nullable enable

namespace FrEee.Game.Objects.Combat.Grid
{
	public interface IBattleEvent
	{
		IBattle Battle { get; }
		ICombatant? Combatant { get; }
		IntVector2 EndPosition { get; }
		IntVector2 StartPosition { get; }
	}
}
