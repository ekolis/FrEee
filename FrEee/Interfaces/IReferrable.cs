using System;

namespace FrEee.Interfaces
{
	/// <summary>
	/// Something that can be referred to from the client side using an ID.
	/// </summary>
	public interface IReferrable : IDisposable, IOwnable
	{
		long ID { get; set; }

		bool IsDisposed { get; set; }
	}
}
