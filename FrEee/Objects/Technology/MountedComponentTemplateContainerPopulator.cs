using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Interfaces;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;
using FrEee.Utility;

namespace FrEee.Objects.Technology
{
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
				return The.Game.Referrables.OfType<IDesign<IVehicle>>().SingleOrDefault(q => q.Components.Contains(mct));
			}
			else
			{
				return null;
			}
		}
	}
}
