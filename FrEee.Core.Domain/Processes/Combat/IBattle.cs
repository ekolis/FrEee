using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Utility;
using System.Collections.Generic;

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
    int GetDiameter(int round);
	int Duration { get; }
	IList<Vector2<int>> UpperLeft { get; } // HACK: this list data structure is specific to turn based battles
	IList<Vector2<int>> LowerRight { get; } // HACK: this list data structure is specific to turn based battles
	List<IList<IBattleEvent>> Events { get; } // HACK: this list data structure is specific to turn based battles

	void Resolve();
}