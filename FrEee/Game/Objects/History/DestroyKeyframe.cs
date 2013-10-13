using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.History
{
	/// <summary>
	/// Keyframe for when an object is seen to be destroyed.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DestroyKeyframe : IKeyframe<IFoggable>
	{
		public DestroyKeyframe(double timestamp)
		{
			Timestamp = timestamp;
		}

		public void Apply(IFoggable target)
		{
			target.IsKnownToBeDestroyed = true;
		}

		public double Timestamp
		{
			get;
			set;
		}

		void IKeyframe.Apply(IHistorical target)
		{
			if (!(target is IFoggable))
				throw new Exception("Destroy keyframes can only be applied to IFoggable objects.");
			Apply((IFoggable)target);
		}
	}
}
