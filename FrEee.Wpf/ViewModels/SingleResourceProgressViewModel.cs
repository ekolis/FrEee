using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicViewModel;
using FrEee.Utility;

namespace FrEee.Wpf.ViewModels
{
	public class SingleResourceProgressViewModel : DynamicViewModel<Tuple<Resource, Progress>>
	{
		public SingleResourceProgressViewModel(Tuple<Resource, Progress> x = null)
			: base(typeof(Tuple<Resource, Progress>), x)
		{
		}

		public ResourceViewModel Resource { get { return new ResourceViewModel(Model.Item1); } }

		public Progress Progress { get { return Model.Item2; } }
	}
}
