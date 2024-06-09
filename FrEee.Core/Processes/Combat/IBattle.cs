using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Utility;
using System.Collections.Generic;
using FrEee.Utility;

namespace FrEee.Processes.Combat;

public interface IBattle : INamed, IPictorial, ILocated
{
    ISet<ICombatant> Combatants { get; }
    PRNG Dice { get; set; }
    IEnumerable<Empire> Empires { get; }
    IList<LogMessage> Log { get; }
    double Timestamp { get; }
    IDictionary<long, ICombatant> StartCombatants { get; }
    IDictionary<long, ICombatant> EndCombatants { get; }

    void Resolve();
}