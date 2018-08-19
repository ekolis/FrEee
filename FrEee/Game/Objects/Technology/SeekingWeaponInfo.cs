using FrEee.Modding;
using System;

namespace FrEee.Game.Objects.Technology
{
    [Serializable]
    public class SeekingWeaponInfo : WeaponInfo
    {
        #region Public Properties

        /// <summary>
        /// The durability of the seeker.
        /// </summary>
        public Formula<int> SeekerDurability { get; set; }

        /// <summary>
        /// The speed at which the seeker travels.
        /// </summary>
        public Formula<int> SeekerSpeed { get; set; }

        public override Enumerations.WeaponTypes WeaponType
        {
            get
            {
                if (IsPointDefense)
                    return Enumerations.WeaponTypes.SeekingPointDefense;
                else
                    return Enumerations.WeaponTypes.Seeking;
            }
        }

        #endregion Public Properties
    }
}
