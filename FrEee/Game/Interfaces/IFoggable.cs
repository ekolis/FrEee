using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something that can be obscured by fog of war.
	/// </summary>
	public interface IFoggable : IReferrable, IHistorical
	{
		/// <summary>
		/// Is this object just a memory, or a real object?
		/// </summary>
		bool IsMemory { get; set; }

		/// <summary>
		/// Is this object known to be destroyed?
		/// </summary>
		bool IsKnownToBeDestroyed { get; set; }
	}
}
