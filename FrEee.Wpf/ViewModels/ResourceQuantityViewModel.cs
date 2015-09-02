using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicViewModel;
using FrEee.Utility;

namespace FrEee.Wpf.ViewModels
{
	public class ResourceQuantityViewModel
	{
		public ResourceQuantityViewModel()
			: this(null)
		{

		}

		public ResourceQuantityViewModel(ResourceQuantity x)
		{
			Model = x;
		}

		public IEnumerable<SingleResourceQuantityViewModel> Quantities
		{
			get
			{
				if (Model == null)
					yield break;
				foreach (var kvp in Model)
					yield return new SingleResourceQuantityViewModel(Tuple.Create(kvp.Key, kvp.Value));
			}
		}

		private ResourceQuantity Model { get; set; }
	}
}
