using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An order for a mobile space object.
	/// </summary>
	/// <typeparam name="T">The type of mobile space object.</typeparam>
	public interface IMobileSpaceObjectOrder<T> : IOrder<T, IMobileSpaceObjectOrder<T>>
		where T : IMobileSpaceObject<T>
	{
	}
}
