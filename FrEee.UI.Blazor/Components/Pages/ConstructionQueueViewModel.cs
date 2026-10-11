using FrEee.Processes.Construction;
using FrEee.Objects.GameState;
using FrEee.Objects.Civilization;

namespace FrEee.UI.Blazor.Components.Pages;

public class ConstructionQueueViewModel : PageViewModelBase
{
	public IConstructionQueue? Queue { get; set; }

	public void MoveUp(IConstructionOrder item)
	{
		// Move item up in queue
	}

	public void MoveDown(IConstructionOrder item)
	{
		// Move item down in queue
	}

	public void Delete(IConstructionOrder item)
	{
		// Remove item from queue
	}
}
