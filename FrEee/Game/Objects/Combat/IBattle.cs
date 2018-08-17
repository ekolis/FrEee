using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Utility;
using System.Collections.Generic;
using System.Drawing;

namespace FrEee.Game.Objects.Combat
{
	public interface IBattle : INamed, IPictorial, ILocated
	{
		ISet<ICombatant> Combatants { get; }
		PRNG Dice { get; set; }
		IEnumerable<Empire> Empires { get; }
		IList<LogMessage> Log { get; }
		SafeDictionary<ICombatant, int> OriginalHitpoints { get; }
		SafeDictionary<ICombatant, Empire> OriginalOwners { get; }
		double Timestamp { get; }

		void Resolve();
	}
}