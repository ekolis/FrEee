using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.Objects.GameState;

namespace FrEee.Processes.Combat.Grid;

/// <summary>
/// When combatants collide - seeker detonation or ship ramming
/// </summary>
/// <seealso cref="IBattleEvent" />
public class CombatantsCollideEvent : BattleEvent
{
    public CombatantsCollideEvent(Battle battle, ICombatant combatant, ICombatant target, Vector2<int> location, int combatantDamage, int targetDamage, bool wasCombatantDisarmed, bool wasTargetDisarmed)
        : base(battle, combatant, location, location)
    {
        Target = target;
        CombatantDamage = combatantDamage;
        TargetDamage = targetDamage;
        WasCombatantDisarmed = wasCombatantDisarmed;
        WasTargetDisarmed = wasTargetDisarmed;
    }


    private GameReference<ICombatant> target { get; set; }

    [DoNotSerialize]
    public ICombatant Target
    {
        get => target?.Value ?? Battle?.StartCombatants?[target.ID];
        set => target = value.ReferViaGalaxy();
    }

    public int CombatantDamage { get; set; }
    public int TargetDamage { get; set; }

    public bool WasCombatantDisarmed { get; set; }
    public bool WasTargetDisarmed { get; set; }
}