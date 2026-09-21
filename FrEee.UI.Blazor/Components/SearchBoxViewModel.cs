namespace FrEee.UI.Blazor.Components;

public class SearchBoxViewModel : ViewModelBase
{
	public string Query { get; set; } = string.Empty;
	public string Placeholder { get; set; } = "Search...";
	public Action<string>? SearchRequested { get; set; }

	public void Search()
	{
		SearchRequested?.Invoke(Query);
	}
}
