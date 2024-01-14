using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Technology;
using FrEee.Utility; using FrEee.Serialization;
using System.Collections.Generic;

namespace FrEee.Objects.Combat;

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