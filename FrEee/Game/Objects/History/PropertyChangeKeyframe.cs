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
	public class PropertyChangeKeyframe : IKeyframe<IHistorical>
	{
		public PropertyChangeKeyframe(double timestamp, string propertyName, object newValue)
		{
			Timestamp = timestamp;
			PropertyName = propertyName;
			NewValue = newValue;
		}

		public void Apply(IHistorical target)
		{
			target.SetPropertyValue(PropertyName, NewValue);
		}

		public double Timestamp
		{
			get;
			set;
		}

		public string PropertyName {get; set;}

		public object NewValue { get; set;}
	}
}
