using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace FrEee.UI.Blazor.Components;
public class ViewModelBase
	: INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;

	public NavigationManager Navigation { get; internal set; }
}
