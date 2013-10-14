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
		/// Applies the data in this keyframe to an object.
		/// </summary>
		/// <param name="target"></param>
		void Apply(IReferrable target);
	}

	/// <summary>
	/// A keyframe in object history.
	/// </summary>
	public interface IKeyframe<in T> : IKeyframe where T : IReferrable
	{
		/// <summary>
		/// Applies the data in this keyframe to an object.
		/// </summary>
		/// <param name="target"></param>
		void Apply(T target);
	}
}
