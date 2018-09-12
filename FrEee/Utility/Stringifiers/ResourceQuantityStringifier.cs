using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
