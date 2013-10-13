using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.History
{
	/// <summary>
	/// Keyframe for when an object becomes visible, or at the beginning of the turn.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class AppearKeyframe : IKeyframe<IFoggable>
	{
		public AppearKeyframe(double timestamp)
		{
			Timestamp = timestamp;
		}

		public void Apply(IFoggable target)
		{
			target.IsMemory = false;
			target.IsKnownToBeDestroyed = false;
		}

		public double Timestamp
		{
			get;
			set;
		}

		void IKeyframe.Apply(IHistorical target)
		{
			if (!(target is IFoggable))
				throw new Exception("Appear keyframes can only be applied to IFoggable objects.");
			Apply((IFoggable)target);
		}
	}
}
