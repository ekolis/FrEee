using FrEee.Objects.Civilization;

namespace FrEee.UI.Blazor.Components;

public class TraitPickerViewModel : ViewModelBase
{
	public IEnumerable<Trait> Traits { get; set; } = Enumerable.Empty<Trait>();
	public HashSet<Trait> CheckedTraits { get; set; } = new();

	public Action<Trait, bool>? TraitToggled { get; set; }

	public bool IsTraitChecked(Trait trait) => CheckedTraits.Contains(trait);

	public void SetTraitChecked(Trait trait, bool isChecked)
	{
		if (isChecked)
			CheckedTraits.Add(trait);
		else
			CheckedTraits.Remove(trait);
		TraitToggled?.Invoke(trait, isChecked);
	}
}
