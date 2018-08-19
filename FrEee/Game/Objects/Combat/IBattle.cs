using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Utility;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Combat
{
    public interface IBattle : INamed, IPictorial, ILocated
    {
        #region Public Properties

        ISet<ICombatant> Combatants { get; }
        PRNG Dice { get; set; }
        IEnumerable<Empire> Empires { get; }
        IList<LogMessage> Log { get; }
        SafeDictionary<ICombatant, int> OriginalHitpoints { get; }
        SafeDictionary<ICombatant, Empire> OriginalOwners { get; }
        double Timestamp { get; }

        #endregion Public Properties

        #region Public Methods

        void Resolve();

        #endregion Public Methods
    }
}
