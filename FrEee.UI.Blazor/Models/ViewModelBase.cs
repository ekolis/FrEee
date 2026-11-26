using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace FrEee.UI.Blazor.Models;

public class ViewModelBase
	: INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;

	public NavigationManager Navigation { get; internal set; }
	
	public ProtectedSessionStorage Session { get; internal set; }
}
