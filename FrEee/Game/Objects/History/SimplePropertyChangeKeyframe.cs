using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.History
{
	/// <summary>
	/// Keyframe for when an object's properties change.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class SimplePropertyChangeKeyframe : IKeyframe<IReferrable>, IPropertyChangeKeyframe
	{
		public SimplePropertyChangeKeyframe(string propertyName, object newValue)
		{
			PropertyName = propertyName;
			NewValue = newValue;
		}

		public void Apply(IReferrable target)
		{
			target.SetPropertyValue(PropertyName, NewValue);
		}

		public string PropertyName {get; set;}

		public object NewValue { get; set;}
	}
}
