using FrEee.Game.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
	/// <summary>
	/// Requires an empire to have visibility of an object to see this property's value.
	/// Only works on IHistorical objects.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class RequiresVisibilityAttribute : Attribute
	{
		public RequiresVisibilityAttribute(Visibility visibility, object defaultValue = null)
		{
			Visibility = visibility;
			DefaultValue = defaultValue;
		}

		public Visibility Visibility { get; set; }

		public object DefaultValue { get; set; }
	}
}
