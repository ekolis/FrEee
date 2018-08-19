using FrEee.Game.Interfaces;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat.Grid
{
    public class CombatantAppearsEvent : IBattleEvent
    {
        #region Public Constructors

        public CombatantAppearsEvent(ICombatant combatant, IntVector2 position)
        {
            Combatant = combatant;
            StartPosition = EndPosition = position;
        }

        #endregion Public Constructors

        #region Public Properties

        public ICombatant Combatant { get; set; }

        public IntVector2 EndPosition { get; set; }
        public IntVector2 StartPosition { get; set; }

        #endregion Public Properties
    }
}
