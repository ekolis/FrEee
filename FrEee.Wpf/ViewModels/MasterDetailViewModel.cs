using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Wpf.ViewModels
{
	public class MasterDetailViewModel<TMaster, TDetailViewModel> : ViewModel<TMaster>
	{
		public MasterDetailViewModel(Func<TMaster, IEnumerable<TDetailViewModel>> getter)
			: base()
		{
		}

		public MasterDetailViewModel(TMaster model)
			: base(model)
		{
		}

		private TDetailViewModel[] itemViewModels;

		public IEnumerable<TDetailViewModel> ItemViewModels
		{
			get
			{
				if (itemViewModels == null)
					itemViewModels = GetItemViewModels();
			}
		}
	}
}
