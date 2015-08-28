using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicViewModel;
using FrEee.Utility;

namespace FrEee.Wpf.ViewModels
{
	public class ResourceProgressViewModel : DynamicViewModel<ResourceProgress>
	{
		public ResourceProgressViewModel(ResourceProgress x = null)
			: base(typeof(ResourceProgress), x)
		{
		}

		public IEnumerable<SingleResourceProgressViewModel> Quantities
		{
			get
			{
				foreach (var kvp in Model)
					yield return new SingleResourceProgressViewModel(Tuple.Create(kvp.Key, kvp.Value));
			}
		}
	}
}
