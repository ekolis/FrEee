using FrEee.Modding;

namespace FrEee.UI.Blazor.Components.Pages;

public class ModErrorsViewModel : PageViewModelBase
{
	public IEnumerable<DataParsingException> Errors => Mod.Errors ?? Enumerable.Empty<DataParsingException>();
}
