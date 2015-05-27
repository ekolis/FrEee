using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
	public static class GalaxyViewModes
	{
		private static IGalaxyViewMode[] all = new IGalaxyViewMode[]
		{
			new PresenceMode(),

		};

		public static IEnumerable<IGalaxyViewMode> All
		{
			get
			{
				return all;
			}
		}
	}
}
