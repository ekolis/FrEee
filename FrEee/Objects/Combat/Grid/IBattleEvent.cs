using FrEee.Interfaces;
using FrEee.Utility;
namespace FrEee.Objects.Combat.Grid;

public interface IBattleEvent
{
	IBattle Battle { get; }
	ICombatant Combatant { get; }
	IntVector2 EndPosition { get; }
	IntVector2 StartPosition { get; }
}