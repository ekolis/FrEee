using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Utility;
using System.Collections.Generic;

#nullable enable

namespace FrEee.Game.Objects.Combat
{
	public interface IBattle : INamed, IPictorial, ILocated
	{
		ISet<ICombatant>? Combatants { get; }
		PRNG? Dice { get; set; }
		IEnumerable<Empire>? Empires { get; }
		IList<LogMessage> Log { get; }
		double Timestamp { get; }
		IDictionary<long, ICombatant> StartCombatants { get; }
		IDictionary<long, ICombatant> EndCombatants { get; }

		void Resolve();
	}
}
