using FrEee.Game.Interfaces;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat.Grid
{
    public class CombatantMovesEvent : IBattleEvent
    {
        #region Public Constructors

        public CombatantMovesEvent(ICombatant combatant, IntVector2 here, IntVector2 there)
        {
            Combatant = combatant;
            StartPosition = here;
            EndPosition = there;
        }

        #endregion Public Constructors

        #region Public Properties

        public ICombatant Combatant { get; set; }

        public IntVector2 EndPosition { get; set; }
        public IntVector2 StartPosition { get; set; }

        #endregion Public Properties
    }
}
