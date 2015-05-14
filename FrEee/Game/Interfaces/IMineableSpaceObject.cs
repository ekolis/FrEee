using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A space object with a resource value.
	/// </summary>
	public interface IMineableSpaceObject : ISpaceObject
	{
		ResourceQuantity ResourceValue { get; }
	}
}
