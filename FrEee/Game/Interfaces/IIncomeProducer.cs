﻿using FrEee.Utility;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// Something which can produce income for an empire.
    /// </summary>
    public interface IIncomeProducer : IOwnableAbilityObject, ILocated
    {
        #region Public Properties

        /// <summary>
        /// Ratio of income from this object that occurs even without a spaceport.
        /// 1.0 = all of it
        /// 0.5 = half of it
        /// 0.0 = none of it
        /// </summary>
        double MerchantsRatio { get; }

        /// <summary>
        /// Modifiers to remote mining due to racial aptitudes and the like.
        /// </summary>
        ResourceQuantity RemoteMiningIncomePercentages { get; }

        /// <summary>
        /// Value for mined resources.
        /// </summary>
        ResourceQuantity ResourceValue { get; }

        /// <summary>
        /// Modifiers to standard income due to population, happiness, racial aptitudes, lack of spaceport, etc.
        /// 100% = normal income
        /// </summary>
        ResourceQuantity StandardIncomePercentages { get; }

        #endregion Public Properties
    }
}
