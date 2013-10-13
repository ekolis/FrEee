using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using FrEee.Utility;

namespace FrEee.Game.Objects.History
{
	/// <summary>
	/// Keyframe for when an object's properties change.
	/// Designed for IReferrable objects, stores only the object ID to save space and time.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ReferencePropertyChangeKeyframe : IKeyframe<IHistorical>
	{
		public ReferencePropertyChangeKeyframe(double timestamp, string propertyName, IReferrable newValue)
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

		private IReference<IReferrable> newValue { get; set; }

		[DoNotSerialize]
		public IReferrable NewValue
		{
			get { return newValue == null ? null : newValue.Value; }
			set { newValue = value == null ? null : value.Reference(); }
		}
	}
}
