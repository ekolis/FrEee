using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// For classes that need extra processing after being copied or whatnot.
	/// </summary>
	public interface ICleanable
	{
		void Clean();
	}
}
