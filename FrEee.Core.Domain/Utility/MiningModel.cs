namespace FrEee.Utility;

/// <summary>
/// A model for how resources should be mined.
/// </summary>
public class MiningModel
{
	/// <summary>
	/// Does the value percentage bonus apply to the value depletion?
	/// </summary>
	public bool BonusAffectsDepletion { get; set; }

	/// <summary>
	/// Limit mining rate after all bonuses are applied to value?
	/// </summary>
	public bool LimitRateToValue { get; set; }

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
	/// Rate, expressed as a percentage.
	/// </summary>
	public double RatePercentage { get; set; }

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
	/// A percentage bonus to the mining rate for each point of planet/asteroid value.
	/// </summary>
	public double ValuePercentageBonus { get; set; }

	/// <summary>
	/// Gets the decay rate.
	/// </summary>
	/// <param name="baseRate">The base mining rate.</param>
	/// <param name="value">The planet/asteroid value.</param>
	/// <returns></returns>
	public int GetDecay(int baseRate, int value)
	{
		double rate;
		if (BonusAffectsDepletion)
			rate = baseRate + baseRate * ValueBonus * value;
		else
			rate = baseRate;
		return (int)(rate * ValueDepletionPerResource) + ValueDepletionPerTurn;
	}

	/// <summary>
	/// Gets the mining rate.
	/// </summary>
	/// <param name="baseRate">The base mining rate.</param>
	/// <param name="value">The planet/asteroid value.</param>
	/// <param name="factor">Any mining rate multipliers from things such as population, e.g. 1.4 for a 40% bonus.</param>
	/// <returns></returns>
	public int GetRate(int baseRate, int value, double factor)
	{
		var result = (int)((baseRate * Rate + baseRate * ValueBonus * value) * factor);
		if (LimitRateToValue && value < result)
			return value;
		return result;
	}
}
