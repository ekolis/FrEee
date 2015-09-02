using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicViewModel;
using FrEee.Utility;

namespace FrEee.Wpf.ViewModels
{
	public class ResourceProgressViewModel
	{
		public ResourceProgressViewModel()
			: this(null)
		{

		}

		public ResourceProgressViewModel(ResourceProgress x)
		{
			Model = x;
		}

		public IEnumerable<SingleResourceProgressViewModel> Quantities
		{
			get
			{
				if (Model == null)
					yield break;
				foreach (var kvp in Model)
					yield return new SingleResourceProgressViewModel(Tuple.Create(kvp.Key, kvp.Value));
			}
		}

		private ResourceProgress Model { get; set; }
	}
}
