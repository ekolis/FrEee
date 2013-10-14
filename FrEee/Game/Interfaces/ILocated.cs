using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which is located in a sector, or is a sector itself.
	/// </summary>
	public interface ILocated
	{
		Sector Sector { get; set; }
	}
}
