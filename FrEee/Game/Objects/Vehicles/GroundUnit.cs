using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	/// <summary>
	/// A unit which operates on the ground.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class GroundUnit<T> : Unit<T> where T : GroundUnit<T>, new()
	{
	}
}
