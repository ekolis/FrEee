using System.ComponentModel;
using FrEee.Utility;

namespace FrEee.UI.Blazor.Components
{
	public class ResourceQuantityDisplayViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ResourceQuantity Amounts { get; set; } = new();

		public ResourceQuantity Changes { get; set; } = new();

		/// <summary>
		/// Resources to show. Resources not in this list will be hidden.
		/// </summary>
		public IEnumerable<Resource> ResourcesToShow { get; set; } = Resource.All.Where(it => it.IsGlobal).ToArray();

		/// <summary>
		/// Should resources with amounts and changes of zero be shown?
		/// </summary>
		public bool ShowZeroes { get; set; } = true;

		public IEnumerable<ResourceDisplayViewModel> ResourceViewModels
		{
			get
			{
				foreach (var resource in ResourcesToShow)
				{
					if (ShowZeroes || Amounts[resource] != 0 || Changes[resource] != 0)
					{
						yield return new ResourceDisplayViewModel
						{
							Resource = resource,
							Amount = Amounts[resource],
							Change = Changes[resource]
						};
					}
				}
			}
		}
	}
}
