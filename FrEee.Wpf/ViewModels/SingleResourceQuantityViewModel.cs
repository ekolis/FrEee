using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicViewModel;
using FrEee.Utility;

namespace FrEee.Wpf.ViewModels
{
	public class SingleResourceQuantityViewModel : DynamicViewModel<Tuple<Resource, int>>
	{
		public SingleResourceQuantityViewModel(Tuple<Resource, int> x = null)
			: base(typeof(Tuple<Resource, int>), x)
		{
		}

		public ResourceViewModel Resource { get { return new ResourceViewModel(Model.Item1); } }

		public int Quantity { get { return Model.Item2; } }
	}
}
