using FrEee.Modding.Abilities;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;

namespace FrEee.UI.Blazor.Components.Screens;

public class ActivateAbilityViewModel : ScreenViewModelBase
{
	public IAbilityObject? AbilityObject { get; set; }
	public Ability? SelectedAbility { get; set; }
	public IReferrable? Target { get; set; }

	public void Activate()
	{
		// Queue ability activation order
		Close();
	}
}
