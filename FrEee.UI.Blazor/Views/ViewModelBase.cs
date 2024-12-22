using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.UI.Blazor.Views;
public class ViewModelBase
	: INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;
}
