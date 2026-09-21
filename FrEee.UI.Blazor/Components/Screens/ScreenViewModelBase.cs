namespace FrEee.UI.Blazor.Components.Screens;

public abstract class ScreenViewModelBase : ViewModelBase
{
	public bool IsOpen { get; set; } = true;
	public Action? OnClose { get; set; }

	public virtual void Close()
	{
		IsOpen = false;
		OnClose?.Invoke();
	}
}
