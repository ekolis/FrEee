using FrEee.Interfaces;
using FrEee.Utility;
namespace FrEee.Objects.Combat.Grid;

public interface IBattleEvent
{
	IBattle Battle { get; }
	ICombatant Combatant { get; }
	Vector2<int> EndPosition { get; }
	Vector2<int> StartPosition { get; }
}