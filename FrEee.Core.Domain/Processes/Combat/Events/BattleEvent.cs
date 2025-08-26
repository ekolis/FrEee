using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.Objects.GameState;

namespace FrEee.Processes.Combat.Events;

// TODO: make this and its subclassses records
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

    private GameReference<ICombatant> combatant
    {
        get => Combatant.ReferViaGalaxy();
        set => Combatant = value?.Value ?? Battle?.StartCombatants?[value.ID];
	}

    [DoNotSerialize]
    public ICombatant Combatant { get; set; }

    // HACK: assumes 2D combat
    public Vector2<int> EndPosition { get; set; }

	// HACK: assumes 2D combat
	public Vector2<int> StartPosition { get; set; }
}
