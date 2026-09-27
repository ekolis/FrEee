using FrEee.Objects.Technology;
using FrEee.Modding.Templates;

namespace FrEee.UI.Blazor.Components;

public class ComponentReportViewModel : ViewModelBase
{
	public ComponentTemplate? ComponentTemplate { get; set; }
	public MountedComponentTemplate? MountedComponentTemplate { get; set; }
	public Component? Component { get; set; }

	public ComponentTemplate? EffectiveTemplate
	{
		get
		{
			if (MountedComponentTemplate != null)
				return MountedComponentTemplate.ComponentTemplate;
			if (ComponentTemplate != null)
				return ComponentTemplate;
			return Component?.Template.ComponentTemplate;
		}
	}

	public Mount? EffectiveMount => MountedComponentTemplate?.Mount;
}
