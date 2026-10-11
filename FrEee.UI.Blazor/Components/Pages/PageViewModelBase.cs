namespace FrEee.UI.Blazor.Components.Pages;

public abstract class PageViewModelBase : ViewModelBase
{
	public bool IsOpen { get; set; } = true;
	public Action? OnClose { get; set; }

	public virtual void Close()
	{
		IsOpen = false;
		OnClose?.Invoke();
	}
}
