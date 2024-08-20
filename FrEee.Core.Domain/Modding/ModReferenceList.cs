using FrEee.Utility;

namespace FrEee.Modding
{
	public class ModReferenceList<T>
		: ReferenceList<ModReference<T>, T>
		where T : IModObject
	{
	}
}
