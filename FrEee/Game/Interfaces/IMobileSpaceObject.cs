using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	public interface IMobileSpaceObject : ISpaceObject
	{
		double TimeToNextMove { get; set; }
		double TimePerMove { get; }
		void ExecuteOrders();
		void RefillMovement();
	}

	/// <summary>
	/// A space object which can be ordered to move.
	/// </summary>
	/// <typeparam name="T">The type of mobile space object.</typeparam>
	public interface IMobileSpaceObject<T> : IMobileSpaceObject, IOrderable<T, IMobileSpaceObjectOrder<T>>
		where T : IMobileSpaceObject<T>
	{
		
	}
}
