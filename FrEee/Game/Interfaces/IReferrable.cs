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
	public interface IReferrable<out T>
	{
		/// <summary>
		/// A unique ID by which the player orders file can reference this orderable.
		/// </summary>
		int ID { get; set; }

		/// <summary>
		/// The owner of this object. Only the owner can issue orders or see this object in his referrables list.
		/// </summary>
		Empire Owner { get; }
	}
}
