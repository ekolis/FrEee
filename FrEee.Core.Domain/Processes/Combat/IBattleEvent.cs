using FrEee.Utility;

namespace FrEee.Processes.Combat;

public interface IBattleEvent
{
    IBattle Battle { get; }
    ICombatant Combatant { get; }
    Vector2<int> EndPosition { get; }
    Vector2<int> StartPosition { get; }
}