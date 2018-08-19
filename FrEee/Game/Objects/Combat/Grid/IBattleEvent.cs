using FrEee.Game.Interfaces;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat.Grid
{
    public interface IBattleEvent
    {
        #region Public Properties

        ICombatant Combatant { get; }
        IntVector2 EndPosition { get; }
        IntVector2 StartPosition { get; }

        #endregion Public Properties
    }
}
