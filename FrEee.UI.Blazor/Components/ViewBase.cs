using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

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

	[Inject]
	private IJSRuntime JSRuntime { get; set; }

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
	
	/// <summary>
	/// Displays an alert message for unimplemented functionality.
	/// </summary>
	/// <param name="functionality"></param>
	protected async Task AlertStub(string functionality)
	{
		await Alert($"Stub: {functionality} functionality is not yet implemented.");
	}

	/// <summary>
	/// Displays an alert message.
	/// </summary>
	/// <param name="message"></param>
	protected async Task Alert(string message)
	{
		await JSRuntime.InvokeVoidAsync("alert", message);
	}
}
