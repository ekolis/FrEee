using FrEee.Objects.Civilization;
using FrEee.Processes.Construction;

namespace FrEee.UI.Blazor.Components.Screens;

public class ConstructionQueueListViewModel : ScreenViewModelBase
{
	public IEnumerable<IConstructionQueue> Queues => Empire.Current?.ConstructionQueues ?? Enumerable.Empty<IConstructionQueue>();
	public IConstructionQueue? SelectedQueue { get; set; }
}
