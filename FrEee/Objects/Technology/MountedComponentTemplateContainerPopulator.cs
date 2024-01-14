using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Interfaces;
using FrEee.Objects.Space;
using FrEee.Utility; using FrEee.Serialization;

namespace FrEee.Objects.Technology;

/// <summary>
/// Populates the <see cref="MountedComponentTemplate.Container"/> property.
/// </summary>
public class MountedComponentTemplateContainerPopulator
	: IPopulator
{
	public object? Populate(object? context)
	{
		if (context is MountedComponentTemplate mct)
		{
			return Galaxy.Current.Referrables.OfType<IDesign>().SingleOrDefault(q => q.Components.Contains(mct));
		}
		else
		{
			return null;
		}
	}
}
