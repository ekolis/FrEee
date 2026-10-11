namespace FrEee.UI.Blazor.Components.Pages;

public class GenericPickerViewModel : PageViewModelBase
{
	public string Title { get; set; } = "Select Item";
	public IEnumerable<object> Items { get; set; } = Enumerable.Empty<object>();
	public object? SelectedItem { get; set; }
	public string SearchFilter { get; set; } = string.Empty;

	public IEnumerable<object> FilteredItems =>
		string.IsNullOrWhiteSpace(SearchFilter)
			? Items
			: Items.Where(i => i.ToString()?.Contains(SearchFilter, StringComparison.OrdinalIgnoreCase) == true);

	public void Select()
	{
		Close();
	}
}
