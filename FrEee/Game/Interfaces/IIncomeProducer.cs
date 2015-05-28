using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can produce income for an empire.
	/// </summary>
	public interface IIncomeProducer : IOwnableAbilityObject
	{
		/// <summary>
		/// Modifiers to standard income due to population, happiness, racial aptitudes, lack of spaceport, etc.
		/// 100% = normal income
		/// </summary>
		ResourceQuantity StandardIncomePercentages { get; }

		/// <summary>
		/// Modifiers to remote mining due to racial aptitudes and the like.
		/// </summary>
		ResourceQuantity RemoteMiningIncomePercentages { get; }

		/// <summary>
		/// Value for mined resources.
		/// </summary>
		ResourceQuantity ResourceValue { get; }
	}
}
