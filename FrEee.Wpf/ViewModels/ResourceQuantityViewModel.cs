using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicViewModel;
using FrEee.Utility;

namespace FrEee.Wpf.ViewModels
{
	public class ResourceQuantityViewModel : DynamicViewModel<ResourceQuantity>
	{
		public ResourceQuantityViewModel(ResourceQuantity x = null)
			: base(typeof(ResourceQuantity), x)
		{
		}

		public IEnumerable<SingleResourceQuantityViewModel> Quantities
		{
			get
			{
				foreach (var kvp in Model)
					yield return new SingleResourceQuantityViewModel(Tuple.Create(kvp.Key, kvp.Value));
			}
		}
	}
}
