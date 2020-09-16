using System.ComponentModel.Composition;

#nullable enable

namespace FrEee.Utility.Stringifiers
{
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
}
