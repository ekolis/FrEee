using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.History
{
	/// <summary>
	/// Keyframe for when an object moves.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class MoveKeyframe : IKeyframe<ISpaceObject>
	{
		public MoveKeyframe(double timestamp, Sector newLocation)
		{
			Timestamp = timestamp;
			NewSector = newLocation;
		}

		public void Apply(ISpaceObject target)
		{
			NewSector.Place(target);
		}

		public double Timestamp
		{
			get;
			set;
		}

		public Sector NewSector { get; set; }

		void IKeyframe.Apply(IHistorical target)
		{
			if (!(target is ISpaceObject))
				throw new Exception("Move keyframes can only be applied to space objects.");
			Apply((ISpaceObject)target);
		}
	}
}
