using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.History
{
	/// <summary>
	/// Keyframe for when an object becomes invisible.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DisappearKeyframe : IKeyframe<IFoggable>
	{
		public DisappearKeyframe(double timestamp)
		{
			Timestamp = timestamp;
		}

		public void Apply(IFoggable target)
		{
			target.IsMemory = true;
		}

		public double Timestamp
		{
			get;
			set;
		}

		void IKeyframe.Apply(IHistorical target)
		{
			if (!(target is IFoggable))
				throw new Exception("Disappear keyframes can only be applied to IFoggable objects.");
			Apply((IFoggable)target);
		}
	}
}
