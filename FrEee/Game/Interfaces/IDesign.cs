using FrEee.Game.Objects.Technology;
using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A vehicle design.
	/// </summary>
	public interface IDesign
	{
		/// <summary>
		/// The vehicle's components.
		/// </summary>
		IList<MountedComponentTemplate> Components { get; }
	}
}
