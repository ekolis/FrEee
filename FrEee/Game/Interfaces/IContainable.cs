using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can be contained by another object.
	/// </summary>
	public interface IContainable<out TContainer>
	{
		/// <summary>
		/// The container of this object.
		/// </summary>
		TContainer Container { get; }
	}
}
