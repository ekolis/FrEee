using FrEee.Utility;

namespace FrEee.Objects.GameState
{
	public class GalaxyReferenceList<T>
		: ReferenceList<GalaxyReference<T>, T>
		where T : IReferrable
	{
	}
}