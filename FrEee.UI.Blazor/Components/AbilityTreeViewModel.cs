using FrEee.Modding.Abilities;

namespace FrEee.UI.Blazor.Components;

public class AbilityTreeViewModel : ViewModelBase
{
	public ILookup<Ability, Ability>? Abilities { get; set; }
	public IEnumerable<Ability>? IntrinsicAbilities { get; set; }
}
