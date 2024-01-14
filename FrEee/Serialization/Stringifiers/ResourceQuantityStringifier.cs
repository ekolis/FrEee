using FrEee.Utility;
using System.ComponentModel.Composition;

namespace FrEee.Serialization.Stringifiers;

[Export(typeof(IStringifier))]
public class ResourceQuantityStringifier : Stringifier<ResourceQuantity>
{
	public override ResourceQuantity Destringify(string s)
	{
		return ResourceQuantity.Parse(s);
	}

	public override string Stringify(ResourceQuantity t)
	{
		return t.ToString();
	}
}
