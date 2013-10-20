using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can be owned by an empire.
	/// </summary>
	public interface IOwnable
	{
		Empire Owner { get; }
	}
}
