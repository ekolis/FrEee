using Microsoft.AspNetCore.Components;

namespace FrEee.UI.Blazor.Components.Screens;

public abstract class ScreenBase<TViewModel> : ViewBase<TViewModel>
	where TViewModel : ViewModelBase, new()
{
}
