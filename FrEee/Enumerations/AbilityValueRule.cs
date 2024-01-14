using FrEee.Utility; using FrEee.Serialization;

namespace FrEee.Enumerations;

/// <summary>
/// Rules for grouping and stacking ability values within a group of similar abilities.
/// </summary>
public enum AbilityValueRule
{
	/// <summary>
	/// Do not group or stack abilities by this value.
	/// Note that this does not necessarily mean that only one instance of the ability will apply!
	/// To guarantee this, use TakeHighest, TakeAverage, or TakeLowest.
	/// </summary>
	None,

	/// <summary>
	/// Group the abilities by this value.
	/// </summary>
	Group,

	/// <summary>
	/// Add the values within the group. Only works properly for numeric values.
	/// </summary>
	Add,

	/// <summary>
	/// Take the highest value within the group. Only works properly for numeric values.
	/// </summary>
	[CanonicalName("Take Highest")]
	[Name("Highest")]
	TakeHighest,

	/// <summary>
	/// Take the average of the group values. Only works properly for numeric values.
	/// </summary>
	[CanonicalName("Take Average")]
	[Name("Average")]
	TakeAverage,

	/// <summary>
	/// Take the lowest value within the group. Only works properly for numeric values.
	/// </summary>
	[CanonicalName("Take Lowest")]
	[Name("Lowest")]
	TakeLowest
}