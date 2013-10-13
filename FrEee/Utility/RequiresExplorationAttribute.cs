using FrEee.Game.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
	/// <summary>
	/// Requires an object to have been explored for the property to be visible.
	/// For warp point targets, this means the warp point's target system has been explored.
	/// For vehicle designs, this means the design is known by the empire.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class RequiresExplorationAttribute : Attribute
	{
		public RequiresExplorationAttribute(object defaultValue = null)
		{
			DefaultValue = defaultValue;
		}

		public object DefaultValue { get; set; }
	}
}
