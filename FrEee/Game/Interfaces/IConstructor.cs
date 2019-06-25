using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which has a construction queue.
	/// </summary>
	public interface IConstructor : ISpaceObject, IOwnable
	{
		/// <summary>
		/// This object's construction queue, if any.
		/// </summary>
		ConstructionQueue ConstructionQueue { get; }
	}
}
