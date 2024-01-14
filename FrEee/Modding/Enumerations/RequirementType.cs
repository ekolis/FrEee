namespace FrEee.Modding.Enumerations;

/// <summary>
/// Type of requirement.
/// </summary>
public enum RequirementType
{
	/// <summary>
	/// Requirement to unlock something.
	/// </summary>
	Unlock,

	/// <summary>
	/// Requirement to build something that has been unlocked.
	/// </summary>
	Build,

	/// <summary>
	/// Requirement to use something that has been built.
	/// </summary>
	Usage
}