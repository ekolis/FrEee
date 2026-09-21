using FrEee.Modding;

namespace FrEee.UI.Blazor.Components.Screens;

public class ModErrorsViewModel : ScreenViewModelBase
{
	public IEnumerable<DataParsingException> Errors => Mod.Errors ?? Enumerable.Empty<DataParsingException>();
}
