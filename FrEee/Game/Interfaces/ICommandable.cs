using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can accept commands from an empire.
	/// </summary>
	public interface ICommandable : IReferrable<ICommandable>
	{
	}
}
