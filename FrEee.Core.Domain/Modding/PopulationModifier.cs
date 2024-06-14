namespace FrEee.Modding;

/// <summary>
/// Modifiers to production/construction rate from population.
/// TODO - let population modifiers take a formula parameter so we don't need to list a gazillion of them!
/// </summary>
public class PopulationModifier
{
	/// <summary>
	/// Rate of construction as percentage of normal rate.
	/// </summary>
	public int ConstructionRate { get; set; }

	/// <summary>
	/// Maximum amount of population for this modifier; if more population is present, the next better modifier (if present) will be used instead.
	/// </summary>
	public long PopulationAmount { get; set; }

	/// <summary>
	/// Rate of resource/research/intel production as percentage of normal rate.
	/// </summary>
	public int ProductionRate { get; set; }
}