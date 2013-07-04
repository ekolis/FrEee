using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A command for an empire to create a design.
	/// </summary>
	public interface ICreateDesignCommand : ICommand<Empire>
	{
		IDesign Design { get; }
	}
}
