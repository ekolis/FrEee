using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something that can be referred to from the client side using an ID.
	/// </summary>
	public interface IReferrable : IDisposable, IOwnable, IHistorical
	{
		long ID { get; set; }
	}
}
