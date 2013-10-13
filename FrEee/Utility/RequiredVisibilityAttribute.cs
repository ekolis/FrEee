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
	public sealed class RequiredVisibilityAttribute : Attribute
	{
		public RequiredVisibilityAttribute(Visibility visibility)
		{
			Visibility = visibility;
		}

		public Visibility Visibility { get; set; }
	}
}
