using FrEee.Utility; using FrEee.Serialization;

namespace FrEee.Interfaces
{
	/// <summary>
	/// A space object with a resource value.
	/// </summary>
	public interface IMineableSpaceObject : ISpaceObject
	{
		ResourceQuantity ResourceValue { get; }
	}
}