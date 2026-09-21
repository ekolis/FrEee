using FrEee.Objects.Civilization;

namespace FrEee.UI.Blazor.Components;

public class AptitudePickerViewModel : ViewModelBase
{
	public AptitudePickerViewModel()
	{
		Values = Aptitude.All.ToDictionary(a => a, a => 100);
	}

	public IDictionary<Aptitude, int> Values { get; set; }

	public int Cost => Values?.Sum(kvp => kvp.Key.GetCost(kvp.Value)) ?? 0;

	public Action<Aptitude, int>? AptitudeValueChanged { get; set; }

	public void SetValue(Aptitude apt, int val)
	{
		if (Values.ContainsKey(apt))
		{
			Values[apt] = Math.Clamp(val, apt.MinPercent, apt.MaxPercent);
			AptitudeValueChanged?.Invoke(apt, Values[apt]);
		}
	}
}
