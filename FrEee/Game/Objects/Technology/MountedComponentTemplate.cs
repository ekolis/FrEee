using FrEee.Game.Interfaces;
using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A combination of component template and mount.
	/// </summary>
	public class MountedComponentTemplate : ITemplate<Component>
	{
		/// <summary>
		/// The component template used.
		/// </summary>
		public ComponentTemplate ComponentTemplate { get; set; }

		/// <summary>
		/// The mount used.
		/// </summary>
		public Mount Mount { get; set; }

		public Component Instantiate()
		{
			var comp = ComponentTemplate.Instantiate();
			comp.Mount = Mount;
			return comp;
		}
	}
}
