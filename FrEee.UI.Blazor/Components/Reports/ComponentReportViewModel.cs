using FrEee.Modding.Templates;
using FrEee.Objects.Technology;

namespace FrEee.UI.Blazor.Components.Reports;

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
