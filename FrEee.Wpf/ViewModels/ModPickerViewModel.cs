using System.Collections.ObjectModel;
using DynamicViewModel;
using FrEee.Modding;

namespace FrEee.Wpf.ViewModels
{
	/// <summary>
	/// View model for the mod picker.
	/// Allows the user to choose a mod from a list of mods.
	/// </summary>
	public class ModPickerViewModel : DynamicSelectorViewModel<ObservableCollection<ModInfo>, ModInfo>
	{
		public ModPickerViewModel()
			: base(typeof(ObservableCollection<ModInfo>))
		{

		}
	}
}
