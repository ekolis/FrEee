using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A keyframe in object history.
	/// </summary>
	public interface IKeyframe
	{
		/// <summary>
		/// The timestamp of the keyframe.
		/// 0 = beginning of turn, 1 = end of turn
		/// </summary>
		double Timestamp { get; set; }

		/// <summary>
		/// Applies the data in this keyframe to an object.
		/// </summary>
		/// <param name="target"></param>
		void Apply(IHistorical target);
	}

	/// <summary>
	/// A keyframe in object history.
	/// </summary>
	public interface IKeyframe<in T> : IKeyframe where T : IHistorical
	{
		/// <summary>
		/// Applies the data in this keyframe to an object.
		/// </summary>
		/// <param name="target"></param>
		void Apply(T target);
	}
}
