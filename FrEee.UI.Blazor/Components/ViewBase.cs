using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace FrEee.UI.Blazor.Components;

/// <summary>
/// Base class for Razor views in FrEee.
/// </summary>
/// <typeparam name="TVM"></typeparam>
public class ViewBase<TVM>
	: ComponentBase
	where TVM : INotifyPropertyChanged, new()
{
	/// <summary>
	/// The view model for this view.
	/// </summary>
	[Parameter]
	public TVM VM { get; set; } = new();

	/// <summary>
	/// When the view model's properties change, update the UI.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void ViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		StateHasChanged();
	}

	protected override void OnInitialized()
	{
		VM.PropertyChanged += ViewModelPropertyChanged;
	}

	public void Dispose()
	{
		VM.PropertyChanged -= ViewModelPropertyChanged;
	}
}
