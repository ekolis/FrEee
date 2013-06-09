using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
	/// <summary>
	/// A model for how resources should be mined.
	/// </summary>
	public class MiningModel
	{
		/// <summary>
		/// Rate, expressed as a percentage.
		/// </summary>
		public double RatePercentage {get; set;}

		/// <summary>
		/// The basic rate at which resources are mined.
		/// Does not affect abilities which directly generate resources; only mining and remote mining.
		/// </summary>
		public double Rate
		{
			get { return RatePercentage / 100d; }
			set { RatePercentage = value * 100d; }
		}

		/// <summary>
		/// A percentage bonus to the mining rate for each point of planet/asteroid value.
		/// </summary>
		public double ValuePercentageBonus { get; set; }

		/// <summary>
		/// Value bonus expressed as a factor.
		/// </summary>
		public double ValueBonus
		{
			get { return ValuePercentageBonus / 100d; }
			set { ValuePercentageBonus = value * 100d; }
		}

		/// <summary>
		/// Depletion of value for each point of resources mined.
		/// </summary>
		public double ValueDepletionPerResource { get; set; }

		/// <summary>
		/// Depletion of value for each turn that resources are mined.
		/// </summary>
		public int ValueDepletionPerTurn { get; set; }

		/// <summary>
		/// Does the value percentage bonus apply to the value depletion?
		/// </summary>
		public bool BonusAffectsDepletion { get; set; }

		public int GetRate(int baseRate, int value)
		{
			return (int)(baseRate * Rate + baseRate * ValueBonus * value);
		}

		public int GetDecay(int baseRate, int value)
		{
			double rate;
			if (BonusAffectsDepletion)
				rate = baseRate * ValueBonus * value;
			else
				rate = baseRate;
			return (int)(rate * ValueDepletionPerResource) + ValueDepletionPerTurn;
		}
	}
}
