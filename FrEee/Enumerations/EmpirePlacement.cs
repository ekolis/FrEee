using FrEee.Utility; using FrEee.Serialization;

namespace FrEee.Enumerations
{
	public enum EmpirePlacement
	{
		[CanonicalName("Can Start In Same System")]
		CanStartInSameSystem,

		[CanonicalName("Different Systems")]
		DifferentSystems,

		Equidistant
	}
}