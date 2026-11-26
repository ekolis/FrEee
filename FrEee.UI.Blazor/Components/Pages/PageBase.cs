using FrEee.UI.Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace FrEee.UI.Blazor.Components.Pages;

public abstract class PageBase<TViewModel> : ViewBase<TViewModel>
	where TViewModel : ViewModelBase, new()
{
}