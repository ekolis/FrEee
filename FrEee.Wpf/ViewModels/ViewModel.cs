using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Wpf.ViewModels
{
	public class ViewModel<T>
	{
		public ViewModel()
			: this(default(T))
		{
		}

		public ViewModel(T model)
		{
			Model = model;
		}

		protected T Model { get; private set; }
	}
}
