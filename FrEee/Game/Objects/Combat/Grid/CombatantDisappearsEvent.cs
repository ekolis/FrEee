using FrEee.Game.Interfaces;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat.Grid
{
    public class CombatantDisappearsEvent : IBattleEvent
    {
        #region Public Constructors

        public CombatantDisappearsEvent(ICombatant combatant)
        {
            Combatant = combatant;
        }

        #endregion Public Constructors

        #region Public Properties

        public ICombatant Combatant { get; set; }

        public IntVector2 EndPosition { get; set; }
        public IntVector2 StartPosition { get; set; }

        #endregion Public Properties
    }
}
