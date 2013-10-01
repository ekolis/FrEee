using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Flag interface for collection/dictionary types which contain IReference objects.
	/// Used to prevent serialization of referenced objects (only the references should be serialized).
	/// </summary>
	public interface IReferenceEnumerable
	{
	}
}
