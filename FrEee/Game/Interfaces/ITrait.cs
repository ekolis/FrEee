using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A trait that grants abilities to an empire or race.
	/// </summary>
	public interface ITrait : IAbilityObject
	{
	}

	public interface ITrait<out T> : ITrait where T : IAbilityObject
	{
	}
}
