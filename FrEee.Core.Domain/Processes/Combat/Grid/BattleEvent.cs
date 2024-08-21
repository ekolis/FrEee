using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.Objects.GameState;

namespace FrEee.Processes.Combat.Grid;

public abstract class BattleEvent : IBattleEvent
{
    protected BattleEvent(IBattle battle, ICombatant combatant, Vector2<int> startPosition, Vector2<int> endPosition)
    {
        Battle = battle;
        Combatant = combatant;
        StartPosition = startPosition;
        EndPosition = endPosition;
    }

    [DoNotCopy]
    public IBattle Battle { get; set; }

    private GameReference<ICombatant> combatant { get; set; }

    [DoNotSerialize]
    public ICombatant Combatant
    {
        get => combatant?.Value ?? Battle?.StartCombatants?[combatant.ID];
        set => combatant = value.ReferViaGalaxy();
    }

    public Vector2<int> EndPosition { get; set; }

    public Vector2<int> StartPosition { get; set; }
}
