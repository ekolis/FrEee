using FrEee.Game.Enumerations;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// Something which can be specifically target by weapons.
    /// </summary>
    public interface ITargetable : IDamageable, ITransferrable, IReferrable
    {
        #region Public Properties

        /// <summary>
        /// Evasion rating of this combatant.
        /// </summary>
        int Evasion { get; }

        /// <summary>
        /// What type of object is this for weapon targeting purposes?
        /// </summary>
        WeaponTargets WeaponTargetType { get; }

        #endregion Public Properties
    }
}
