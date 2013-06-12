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
	public interface IReferrable : IDisposable
	{
		/// <summary>
		/// The owner of this object. If this is null, everyone can see it, otherwise only the owner can.
		/// </summary>
		Empire Owner { get; }
	}
}
