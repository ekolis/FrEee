using System.ComponentModel;

namespace FrEee.UI.Blazor.Components;
public class ViewModelBase
	: INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;
}
