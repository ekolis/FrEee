using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An order that can be issued to a planet.
	/// </summary>
	public interface IPlanetOrder : IOrder<Planet>
	{
	}
}
