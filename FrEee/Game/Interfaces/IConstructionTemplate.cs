using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Template for constructable items.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IConstructionTemplate : IReferrable<object>
	{
		/// <summary>
		/// Does this template require a colony to build it?
		/// </summary>
		bool RequiresColonyQueue { get; }

		/// <summary>
		/// Does this template require a space yard to build it?
		/// </summary>
		bool RequiresSpaceYardQueue { get; }
	}
}
