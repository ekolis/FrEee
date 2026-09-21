using System.Drawing;

namespace FrEee.UI.Blazor.Components;

public class PagerViewModel : ViewModelBase
{
	public int CurrentPage { get; set; } = 0;
	public int TotalPages { get; set; } = 1;
	public bool ShowPager { get; set; } = true;
	public Action<int>? PageChanged { get; set; }

	public bool CanPrev => CurrentPage > 0;
	public bool CanNext => CurrentPage < TotalPages - 1;

	public void Prev()
	{
		if (CanPrev)
		{
			CurrentPage--;
			PageChanged?.Invoke(CurrentPage);
		}
	}

	public void Next()
	{
		if (CanNext)
		{
			CurrentPage++;
			PageChanged?.Invoke(CurrentPage);
		}
	}
}
